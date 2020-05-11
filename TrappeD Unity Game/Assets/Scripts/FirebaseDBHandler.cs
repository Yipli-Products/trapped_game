﻿using UnityEngine;
using Firebase;
using Firebase.Unity.Editor;
using Firebase.Database;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

public static class FirebaseDBHandler
{
    static Firebase.Auth.FirebaseAuth auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
    private const string projectId = "yipli-project"; //Taken from Firebase project settings
    //private static readonly string databaseURL = "https://yipli-project.firebaseio.com/"; // Taken from Firebase project settings

    public delegate void PostUserCallback();

    // Adds a PlayerSession to the Firebase Database
    public static void PostPlayerSession(PlayerSession session, PostUserCallback callback)
    {
        auth.SignInAnonymouslyAsync().ContinueWith(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("SignInAnonymouslyAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SignInAnonymouslyAsync encountered an error: " + task.Exception);
                return;
            }

            Firebase.Auth.FirebaseUser newUser = task.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);

            FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://yipli-project.firebaseio.com/");
            DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;

            string key = reference.Child("stage-bucket/player-sessions").Push().Key;
            reference.Child("stage-bucket/player-sessions").Child(key).SetRawJsonValueAsync(JsonConvert.SerializeObject(session.GetJsonDic(), Formatting.None, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            }));
        });
    }

    public static void ChangeCurrentPlayerInBackend(string strUserId, string strPlayerId, PostUserCallback callback)
    {
        auth.SignInAnonymouslyAsync().ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("SignInAnonymouslyAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SignInAnonymouslyAsync encountered an error: " + task.Exception);
                return;
            }

            Firebase.Auth.FirebaseUser newUser = task.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);

            FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://yipli-project.firebaseio.com/");
            DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;

            reference.Child("profiles/users").Child(strUserId).Child("current-player-id").SetValueAsync(strPlayerId);
        });
    }

    /* The function call to be allowed only if network is available */
    public static async Task<DataSnapshot> GetGameData(string userId, string playerId, string gameId, PostUserCallback callback)
    {
        DataSnapshot snapshot = null;
        if (userId.Equals(null) || playerId.Equals(null) || gameId.Equals(null))
        {
            Debug.Log("User ID not found");
        }
        else
        {
            try
            {
                Firebase.Auth.FirebaseUser newUser = await auth.SignInAnonymouslyAsync();
                Debug.LogFormat("User signed in successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);

                FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://yipli-project.firebaseio.com/");
                DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
                snapshot = await reference.Child("profiles/users/" + userId).Child("/players").Child(playerId).Child("/activity-statistics/game-statistics").Child(gameId).Child("/game-data").GetValueAsync();
            }
            catch(Exception exp)
            {
                Debug.Log("Failed to GetAllPlayerdetails : " + exp.Message);
            }
        }
        return snapshot;
    }

    /* The function call to be allowed only if network is available */
    public static async Task<List<YipliPlayerInfo>> GetAllPlayerdetails(string userId, PostUserCallback callback)
    {
        List<YipliPlayerInfo> players = new List<YipliPlayerInfo>();
        DataSnapshot snapshot = null;
        if (userId.Equals(null))
        {
            Debug.Log("User ID not found");
        }
        else
        {
            try
            {
                Firebase.Auth.FirebaseUser newUser = await auth.SignInAnonymouslyAsync();
                Debug.LogFormat("User signed in successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);

                FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://yipli-project.firebaseio.com/");
                DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
                snapshot = await reference.Child("profiles/users/" + userId).Child("/players").GetValueAsync();

                foreach (var childSnapshot in snapshot.Children)
                {
                    YipliPlayerInfo playerInstance = new YipliPlayerInfo(childSnapshot, childSnapshot.Key);
                    if(playerInstance.playerId != null)
                    {
                        players.Add(playerInstance);
                    }
                    else
                    {
                        Debug.Log("Skipping this instance of player, backend seems corrupted.");
                    }
                }
            }
            catch(Exception exp)
            {
                Debug.Log("Failed to GetAllPlayerdetails : " + exp.Message);
                return null;
            }
        }
        return players;
    }

    /* The function call to be allowed only if network is available */
    public static async Task<YipliPlayerInfo> GetCurrentPlayerdetails(string userId, PostUserCallback callback)
    {
        Debug.Log("Getting the Default player from backend");

        DataSnapshot snapshot = null;
        YipliPlayerInfo defaultPlayer = new YipliPlayerInfo();//Cant return null defaultPlayer. Initialze the default player.

        if (userId.Equals(null) || userId.Equals(""))
        {
            Debug.Log("User ID not found");
        }
        else
        {
            try
            {
                Firebase.Auth.FirebaseUser newUser = await auth.SignInAnonymouslyAsync();
                Debug.LogFormat("User signed in successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);

                FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://yipli-project.firebaseio.com/");
                DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;

                //First get the current player id from user Id
                snapshot = await reference.Child("profiles/users").Child(userId).GetValueAsync();
                string playerId = snapshot.Child("current-player-id").Value?.ToString() ?? "";

                //Now get the complete player details from Player Id
                DataSnapshot defaultPlayerSnapshot = await reference.Child("profiles/users/" + userId + "/players/" + playerId).GetValueAsync();

                defaultPlayer = new YipliPlayerInfo(defaultPlayerSnapshot, defaultPlayerSnapshot.Key);

                if (defaultPlayer.playerId != null)
                {
                    //Do Nothing
                    Debug.Log("Found Default player : " + defaultPlayer.playerId);
                }
                else
                {
                    //Case to handle if the default player object doesnt exist in backend/or is corrupted
                    Debug.Log("Default Player Not found. Returning null.");
                    return null;
                }
            }
            catch(Exception exp)
            {
                //If couldnt get defualt player details from the backend, return null.
                Debug.Log("Failed to GetAllPlayerdetails: " + exp.Message);
                return null;
            }
        }

        return defaultPlayer;
    }


    // Mat related queries
    
    /* The function call to be allowed only if network is available */
    public static async Task<YipliMatInfo> GetCurrentMatDetails(string userId, PostUserCallback callback)
    {
        Debug.Log("Getting the Default player from backend");
        DataSnapshot snapshot = null;
        YipliMatInfo defaultMat = new YipliMatInfo();

        if (userId.Equals(null) || userId.Equals(""))
        {
            Debug.Log("User ID not found");
        }
        else
        {
            try
            {
                Firebase.Auth.FirebaseUser newUser = await auth.SignInAnonymouslyAsync();
                Debug.LogFormat("User signed in successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);

                FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://yipli-project.firebaseio.com/");
                DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;

                //First get the current player id from user Id
                snapshot = await reference.Child("profiles/users").Child(userId).GetValueAsync();

                string matId = snapshot.Child("current-mat-id").Value?.ToString() ?? "";
                //Now get the complete player details from Player Id
                DataSnapshot defaultMatSnapshot = await reference.Child("profiles/users/" + userId + "/mats/" + matId).GetValueAsync();
                defaultMat = new YipliMatInfo(defaultMatSnapshot, defaultMatSnapshot.Key);

                if (defaultMat.matId != null)
                {
                    //Do Nothing
                    Debug.Log("Found Default mat : " + defaultMat.matId);
                }
                else
                {
                    //Case to handle if the default mat object doesnt exist in backend/or is corrupted
                    return null;
                }
            }
            catch(Exception exp)
            {
                Debug.Log("Failed to GetAllMatdetails : " + exp.Message);
                return null;
            }
        }
        return defaultMat;
    }

    /* The function call to be allowed only if network is available */
    public static async Task<List<YipliMatInfo>> GetAllMatDetails(string userId, PostUserCallback callback)
    {
        List<YipliMatInfo> mats = new List<YipliMatInfo>();
        DataSnapshot snapshot = null;
        if (userId.Equals(null) || userId.Equals(""))
        {
            Debug.Log("User ID not found");
        }
        else
        {
            try
            {
                Firebase.Auth.FirebaseUser newUser = await auth.SignInAnonymouslyAsync();
                Debug.LogFormat("User signed in successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);

                FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://yipli-project.firebaseio.com/");
                DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
                snapshot = await reference.Child("/profiles/users/" + userId + "/mats").GetValueAsync();

                foreach (var childSnapshot in snapshot.Children)
                {
                    YipliMatInfo matInstance = new YipliMatInfo(childSnapshot, childSnapshot.Key);
                    if(matInstance.matId != null)
                    {
                        mats.Add(matInstance);
                    }
                    else
                    {
                        Debug.Log("Skipping this instance of mat, backend seems corrupted.");
                    }
                }
            }
            catch(Exception exp)
            {
                Debug.Log("Failed to GetAllPlayerdetails : " + exp.Message);
            }
        }
        return mats;
    }
}