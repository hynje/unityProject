using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Database;

public class RankingTableManager : MonoBehaviour
{
    public Text rank1, rank2, rank3, rank4, rank5, rank6, rank7, rank8, rank9, rank10;
    public Text rank11, rank12, rank13, rank14, rank15, rank16, rank17, rank18, rank19, rank20;

    public Text p_rank1, p_rank2, p_rank3, p_rank4, p_rank5;
    public Text p_rank6, p_rank7, p_rank8, p_rank9, p_rank10;

    private DatabaseReference databaseReference;

    void Start()
    {
        databaseReference = FirebaseDatabase.DefaultInstance.RootReference;

        readAllData();
        readPersonalData();
        //setValue();

    }
    class Rank
    {
        public string name;
        public int score;

        public Rank(string name, int score)
        {
            this.name = name;
            this.score = score;
        }
    }
    class personal_Rank
    {
        public string timestamp;
        public int score;

        public personal_Rank(string timestamp, int score)
        {
            this.timestamp = timestamp;
            this.score = score;
        }
    }

    List<Rank> leaderBoard = new List<Rank>();
    List<personal_Rank> personal_leaderBoard = new List<personal_Rank>();

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
                       //Debug.Log("�̸�: " + ranking["username"] + ", ����: " + ranking["highscore"]);

                       Rank allrk = new Rank(
                           data.Child("username").Value.ToString(),
                           int.Parse(data.Child("highscore").Value.ToString())
                          );
                       leaderBoard.Add(allrk);
                   }

                   for (int i = 0; i < leaderBoard.Count; i++)
                   {
                       Debug.Log(leaderBoard[i].name.ToString() + "     " + leaderBoard[i].score.ToString());
                   }
               }
           });
        Debug.Log(leaderBoard.Count);
    }

    void readPersonalData()
    {
        //string uid = "1ldULXo86Jc7DvjF1O7GLe4DF8z1"; //test UID
        string uid = LoginManager.user.UserId;

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
                       //Debug.Log("Ÿ�ӽ����� : " + ranking["timestamp"] + ", ���� : " + ranking["score"]);

                       personal_Rank prank = new personal_Rank(
                           data.Child("timestamp").Value.ToString(),
                           int.Parse(data.Child("score").Value.ToString())
                          );
                       personal_leaderBoard.Add(prank);
                   }

                   for (int i = 0; i < personal_leaderBoard.Count; i++)
                   {
                       Debug.Log(personal_leaderBoard[i].timestamp.ToString() + "     " + personal_leaderBoard[i].score.ToString());
                   }
               }
           });
        Debug.Log(personal_leaderBoard.Count);
    }

    public void setValue()
    {
        rank1.text = "1��      " + leaderBoard[19].name + "      " + leaderBoard[19].score + "��";
        rank2.text = "2��      " + leaderBoard[18].name + "      " + leaderBoard[18].score + "��";
        rank3.text = "3��      " + leaderBoard[17].name + "      " + leaderBoard[17].score + "��";
        rank4.text = "4��      " + leaderBoard[18].name + "      " + leaderBoard[16].score + "��";
        rank5.text = "5��      " + leaderBoard[15].name + "      " + leaderBoard[15].score + "��";
        rank6.text = "6��      " + leaderBoard[14].name + "      " + leaderBoard[14].score + "��";
        rank7.text = "7��      " + leaderBoard[13].name + "      " + leaderBoard[13].score + "��";
        rank8.text = "8��      " + leaderBoard[12].name + "      " + leaderBoard[12].score + "��";
        rank9.text = "9��      " + leaderBoard[11].name + "      " + leaderBoard[11].score + "��";
        rank10.text = "10��      " + leaderBoard[10].name + "      " + leaderBoard[10].score + "��";
        rank11.text = "11��      " + leaderBoard[9].name + "      " + leaderBoard[9].score + "��";
        rank12.text = "12��      " + leaderBoard[8].name + "      " + leaderBoard[8].score + "��";
        rank13.text = "13��      " + leaderBoard[7].name + "      " + leaderBoard[7].score + "��";
        rank14.text = "14��      " + leaderBoard[6].name + "      " + leaderBoard[6].score + "��"; 
        rank15.text = "15��      " + leaderBoard[5].name + "      " + leaderBoard[5].score + "��";
        rank16.text = "16��      " + leaderBoard[4].name + "      " + leaderBoard[4].score + "��";
        rank17.text = "17��      " + leaderBoard[3].name + "      " + leaderBoard[3].score + "��";
        rank18.text = "18��      " + leaderBoard[2].name + "      " + leaderBoard[2].score + "��";
        rank19.text = "19��      " + leaderBoard[1].name + "      " + leaderBoard[1].score + "��";
        rank20.text = "20��      " + leaderBoard[0].name + "      " + leaderBoard[0].score + "��";

        p_rank1.text = "1��             " + personal_leaderBoard[9].score + "��";
        p_rank2.text = "2��             " + personal_leaderBoard[8].score + "��";
        p_rank3.text = "3��             " + personal_leaderBoard[7].score + "��";
        p_rank4.text = "4��             " + personal_leaderBoard[6].score + "��";
        p_rank5.text = "5��             " + personal_leaderBoard[5].score + "��";
        p_rank6.text = "6��             " + personal_leaderBoard[4].score + "��";
        p_rank7.text = "7��             " + personal_leaderBoard[3].score + "��";
        p_rank8.text = "8��             " + personal_leaderBoard[2].score + "��";
        p_rank9.text = "9��             " + personal_leaderBoard[1].score + "��";
        p_rank10.text = "10��           " + personal_leaderBoard[0].score + "��";
    }

    void Update()
    {
        if (leaderBoard.Count > 1)
            setValue();

    }
}
