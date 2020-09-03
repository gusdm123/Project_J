using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using Firebase;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class FirebaseLogin : MonoBehaviour
{

    FirebaseAuth auth;
    string firebaseAuth = "N/S";

    // Start is called before the first frame update
    void Start()
    {
        auth = FirebaseAuth.DefaultInstance;
    }

    public void LoginEmail()
    {
        auth.CreateUserWithEmailAndPasswordAsync("kos4597@gmail.com", "123456asd").ContinueWith(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return;
            }

            // Firebase user has been created.
            Firebase.Auth.FirebaseUser newUser = task.Result;
        });
    }


    public void LoginGoogle()
    {
        string idToken = ((PlayGamesLocalUser)Social.localUser).GetIdToken();

        Firebase.Auth.Credential credential =
   Firebase.Auth.GoogleAuthProvider.GetCredential(idToken, null);
        auth.SignInWithCredentialAsync(credential).ContinueWith(task => {
            if (task.IsCanceled)
            {
                firebaseAuth = "FireBase Fail(1)";
                Debug.LogError("SignInWithCredentialAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                firebaseAuth = "FireBase Fail(2)";
                Debug.LogError("SignInWithCredentialAsync encountered an error: " + task.Exception);
                return;
            }

            firebaseAuth = "FireBase Success";
            Firebase.Auth.FirebaseUser newUser = task.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);
        });
    }
}
