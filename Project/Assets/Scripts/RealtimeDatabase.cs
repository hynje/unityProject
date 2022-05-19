using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;
using UnityEngine.SceneManagement;
using System;
using System.Globalization;

public class RealtimeDatabase : MonoBehaviour
{
    private DatabaseReference databaseReference;

    private static RealtimeDatabase instance;
    public static RealtimeDatabase Instance
    {
        get { return instance; }
    }
    void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        instance = this;

        databaseReference = FirebaseDatabase.DefaultInstance.RootReference;

    }

    public class User
    {
        public string username;
        public string UID;
        public bool tutorial_state; 
        public int highscore;
        public int scores;
        public User()
        {
        }

        public User(string username, string UID, bool tutorial_state, int highscore, int scores)
        {
            this.username = username;
            this.UID = UID;
            this.tutorial_state = false;
            this.highscore = 0;
            this.scores = scores;
        }
    }

    public class Score
    {
        public int score;
        public string timestamp;

        public Score()
        { 
        }

        public Score(int score, string timestamp)
        {
            this.score = score;
            this.timestamp = timestamp;
        }
    }

    public void checkNewUser(string userId, string username)
    {        
        FirebaseDatabase.DefaultInstance.GetReference("User").OrderByChild("UID").EqualTo(userId)
            .GetValueAsync().ContinueWith(task => 
           {
               DataSnapshot snapshot = task.Result;

               if (snapshot.ChildrenCount == 0)
               {
                   Debug.LogError("New User");
                   writeNewUser(userId, username);
                   SceneManager.LoadScene("MainScene");
               }
               else
               {
                   Debug.LogError("not New User");
                   SceneManager.LoadScene("TutorialScene");
               }
            });
    }        

    public void writeNewUser(string userId, string username)
    {
        User user = new User();
        string json = JsonUtility.ToJson(user);
        databaseReference.Child("User").Child(userId).SetRawJsonValueAsync(json);
        databaseReference.Child("User").Child(userId).Child("username").SetValueAsync(username);
        databaseReference.Child("User").Child(userId).Child("UID").SetValueAsync(userId);
        databaseReference.Child("User").Child(userId).Child("highscore").SetValueAsync(0);
        scoreDefault(userId);
    }

    private void scoreDefault(string userId)
    {
        for (int i = 1; i < 11; i++)
        {
            int score = 0;
            string timestamp = "";
            Score sc = new Score(score, timestamp);
            
            string json = JsonUtility.ToJson(sc);

            string key = databaseReference.Child("User").Child(userId).Child("scores").Push().Key;
            databaseReference.Child("User").Child(userId).Child("scores").Child(key).SetRawJsonValueAsync(json);
        }
    }


    /*public void checkTutorial(string userId)
    {
        FirebaseDatabase.DefaultInstance.GetReference("User").Child(userId).GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError("Failed load tutorial data");
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;

                foreach (DataSnapshot data in snapshot.Children)
                {
                    IDictionary userInfo = (IDictionary)data.Value;
                    
                    string nowtutorial = userInfo["tutorial_state"].ToString();

                    if(nowtutorial == "true")
                    {
                        SceneManager.LoadScene("MainScene");
                    }
                    else if (nowtutorial == "false")
                    {
                        SceneManager.LoadScene("MainScene");
                    }

                }
            }
        });

    } */

    public void chagneTutorialstate(string userId)
    {
        databaseReference.Child("User").Child(userId).Child("tutorial_state").SetValueAsync(true);
    }

    public void saveScore(string userId) 
    {
        string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        int score = (int)GameDirector.Instance.time;

        Debug.Log("UID :" + userId + " / Score : " + score);

        Score sc = new Score(score, timestamp);
        string json = JsonUtility.ToJson(sc);

        string key = databaseReference.Child("User").Child(userId).Child("scores").Push().Key;
        databaseReference.Child("User").Child(userId).Child("scores").Child(key).SetRawJsonValueAsync(json);

        saveHighScore(userId);
    }

    public void saveHighScore(string userId)
    {
        FirebaseDatabase.DefaultInstance.GetReference("User").Child(userId).Child("scores")
            .OrderByChild("score").LimitToLast(1).GetValueAsync().ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    Debug.LogError("Failed load tutorial data");
                }
                else if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;

                    foreach (DataSnapshot data in snapshot.Children)
                    {
                        int highscore = int.Parse(data.Child("score").Value.ToString());

                        databaseReference.Child("User").Child(userId).Child("highscore").SetValueAsync(highscore);


                    }
                }

            });
    }



}
