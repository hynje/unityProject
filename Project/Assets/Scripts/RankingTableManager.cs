using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Database;

public class RankingTableManager : MonoBehaviour
{
    public Text rank1, rank2, rank3, rank4, rank5, rank6, rank7, rank8, rank9, rank10;
    public Text rank11, rank12, rank13, rank14, rank15, rank16, rank17, rank18, rank19, rank20;

    public Text score1, score2, score3, score4, score5, score6, score7, score8, score9, score10;
    public Text score11, score12, score13, score14, score15, score16, score17, score18, score19, score20;

    public Text name1, name2, name3, name4, name5, name6, name7, name8, name9, name10;
    public Text name11, name12, name13, name14, name15, name16, name17, name18, name19, name20;

    public Text p_rank1, p_rank2, p_rank3, p_rank4, p_rank5;
    public Text p_rank6, p_rank7, p_rank8, p_rank9, p_rank10;

    public Text p_score1, p_score2, p_score3, p_score4, p_score5;
    public Text p_score6, p_score7, p_score8, p_score9, p_score10;

    private DatabaseReference databaseReference;
    void Start()
    {
        databaseReference = FirebaseDatabase.DefaultInstance.RootReference;

        readAllData();
        readPersonalData();
        //setValue();

    }
    class allRank
    {
        public string name;
        public int score;

        public allRank(string name, int score)
        {
            this.name = name;
            this.score = score;
        }
    }

    class personalRank
    {
        public string timestamp;
        public int score;

        public personalRank(string timestamp, int score)
        {
            this.timestamp = timestamp;
            this.score = score;
        }
    }


    List<allRank> ALLleaderBoard = new List<allRank>();
    List<personalRank> PersonalleaderBoard = new List<personalRank>();

    void readAllData()
    {
        FirebaseDatabase.DefaultInstance
           .GetReference("User")
           .OrderByChild("highscore")
           .LimitToLast(20)
           .GetValueAsync()
           .ContinueWith(task =>
           {
               if (task.IsFaulted)
               {
                   Debug.Log("Fail To Load");
               }

               else if (task.IsCompleted)
               {
                   DataSnapshot snapshot = task.Result;

                   foreach (DataSnapshot data in snapshot.Children)
                   {
                       IDictionary ranking = (IDictionary)data.Value;
                       //Debug.Log("이름: " + ranking["username"] + ", 점수: " + ranking["highscore"]);

                       allRank allrk = new allRank(
                           data.Child("username").Value.ToString(),
                           int.Parse(data.Child("highscore").Value.ToString())
                          );
                       ALLleaderBoard.Add(allrk);
                   }

                   for (int i = 0; i < ALLleaderBoard.Count; i++)
                   {
                       Debug.Log(ALLleaderBoard[i].name.ToString() + "     " + ALLleaderBoard[i].score.ToString());
                   }
               }
           });
        Debug.Log(ALLleaderBoard.Count);
    }

    void readPersonalData()
    {
        string uid = "CosZ14uxkGOtO0UDuaQEP7zCdJz2"; //test UID
        //string uid = LoginManager.user.UserId;

        FirebaseDatabase.DefaultInstance
           .GetReference("User")
           .Child(uid)
           .Child("scores")
           .OrderByChild("score")
           .LimitToLast(10)
           .GetValueAsync()
           .ContinueWith(task =>
           {
               if (task.IsCompleted)
               {
                   DataSnapshot snapshot = task.Result;

                   foreach (DataSnapshot data in snapshot.Children)
                   {
                       IDictionary ranking = (IDictionary)data.Value;
                       //Debug.Log("타임스탬프: " + ranking["timestamp"] + ", 점수: " + ranking["score"]);

                       personalRank rk = new personalRank(
                           data.Child("timestamp").Value.ToString(),
                           int.Parse(data.Child("score").Value.ToString())
                          );
                       PersonalleaderBoard.Add(rk);
                   }

                   for (int i = 0; i < PersonalleaderBoard.Count; i++)
                   {
                       Debug.Log(PersonalleaderBoard[i].timestamp.ToString() + "     " + PersonalleaderBoard[i].score.ToString());
                   }
               }
           });
        Debug.Log(PersonalleaderBoard.Count);
    }

    public void setValue()
    {
        rank1.text = "1st";
        rank2.text = "2nd";
        rank3.text = "3rd";
        rank4.text = "4th";
        rank4.text = "4th";
        rank5.text = "5th";
        rank6.text = "6th";
        rank7.text = "7th";
        rank8.text = "8th";
        rank9.text = "9th";
        rank10.text = "10th";
        rank11.text = "11th";
        rank12.text = "12th";
        rank13.text = "13th";
        rank14.text = "14th";
        rank15.text = "15th";
        rank16.text = "16th";
        rank17.text = "17th";
        rank18.text = "18th";
        rank19.text = "19th";
        rank20.text = "20th";

        score1.text = ALLleaderBoard[19].score.ToString();
        score2.text = ALLleaderBoard[18].score.ToString();
        score3.text = ALLleaderBoard[17].score.ToString();
        score4.text = ALLleaderBoard[16].score.ToString();
        score5.text = ALLleaderBoard[15].score.ToString();
        score6.text = ALLleaderBoard[14].score.ToString();
        score7.text = ALLleaderBoard[13].score.ToString();
        score8.text = ALLleaderBoard[12].score.ToString();
        score9.text = ALLleaderBoard[11].score.ToString();
        score10.text = ALLleaderBoard[10].score.ToString();
        score11.text = ALLleaderBoard[9].score.ToString();
        score12.text = ALLleaderBoard[8].score.ToString();
        score13.text = ALLleaderBoard[7].score.ToString();
        score14.text = ALLleaderBoard[6].score.ToString();
        score15.text = ALLleaderBoard[5].score.ToString();
        score16.text = ALLleaderBoard[4].score.ToString();
        score17.text = ALLleaderBoard[3].score.ToString();
        score18.text = ALLleaderBoard[2].score.ToString();
        score19.text = ALLleaderBoard[1].score.ToString();
        score20.text = ALLleaderBoard[0].score.ToString();

        name1.text = ALLleaderBoard[19].name;
        name2.text = ALLleaderBoard[18].name;
        name3.text = ALLleaderBoard[17].name;
        name4.text = ALLleaderBoard[16].name;
        name5.text = ALLleaderBoard[15].name;
        name6.text = ALLleaderBoard[14].name;
        name7.text = ALLleaderBoard[13].name;
        name8.text = ALLleaderBoard[12].name;
        name9.text = ALLleaderBoard[11].name;
        name10.text = ALLleaderBoard[10].name;
        name11.text = ALLleaderBoard[9].name;
        name12.text = ALLleaderBoard[8].name;
        name13.text = ALLleaderBoard[7].name;
        name14.text = ALLleaderBoard[6].name;
        name15.text = ALLleaderBoard[5].name;
        name16.text = ALLleaderBoard[4].name;
        name17.text = ALLleaderBoard[3].name;
        name18.text = ALLleaderBoard[2].name;
        name19.text = ALLleaderBoard[1].name;
        name20.text = ALLleaderBoard[0].name;

        p_rank1.text = "1st";
        p_rank2.text = "2nd";
        p_rank3.text = "3rd";
        p_rank4.text = "4th";
        p_rank4.text = "4th";
        p_rank5.text = "5th";
        p_rank6.text = "6th";
        p_rank7.text = "7th";
        p_rank8.text = "8th";
        p_rank9.text = "9th";
        p_rank10.text = "10th";

        p_score1.text = PersonalleaderBoard[9].score.ToString();
        p_score2.text = PersonalleaderBoard[8].score.ToString();
        p_score3.text = PersonalleaderBoard[7].score.ToString();
        p_score4.text = PersonalleaderBoard[6].score.ToString();
        p_score5.text = PersonalleaderBoard[5].score.ToString();
        p_score6.text = PersonalleaderBoard[4].score.ToString();
        p_score7.text = PersonalleaderBoard[3].score.ToString();
        p_score8.text = PersonalleaderBoard[2].score.ToString();
        p_score9.text = PersonalleaderBoard[1].score.ToString();
        p_score10.text = PersonalleaderBoard[0].score.ToString();
    }

    void Update()
    {
        if (ALLleaderBoard.Count > 1)
            setValue();

    }
}
