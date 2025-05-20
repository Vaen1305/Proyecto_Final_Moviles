using Firebase;
using Firebase.Database;
using System;
using System.Collections;
using UnityEngine;

public class DatabaseHandler : MonoBehaviour
{
    private string userID;
    private DatabaseReference reference;
    [SerializeField] private StudentSO studentSO;
    [SerializeField] private Authentification authentification;

    private void Awake()
    {
        userID = SystemInfo.deviceUniqueIdentifier;
    }


    // Start is called before the first frame update
    void Start()
    {
        reference = FirebaseDatabase.DefaultInstance.RootReference;


        Invoke(nameof(GetUserInfo), 1f);
    }
    /*
    private void CreateUser()
    {
        User newUser = new User("Pedro", "Piedrito", 9781235);
        string json = JsonUtility.ToJson(newUser);


        reference.Child("users").Child(userID).SetRawJsonValueAsync(json);
    }
    */

    public void UpdateFirstName(string newName,string newId, int newScore)
    {
        authentification.SetMail(newName);
        authentification.SetPassword(newId);
        authentification.SetScore(newScore);

        var studentRef = reference.Child("users").Child(userID).Child(newId.ToString());

        Mail newStudent = new Mail(newName, newId, newScore);
        string json = JsonUtility.ToJson(newStudent);
        studentRef.SetRawJsonValueAsync(json);
    }
    public void SaveScoretoFirebase(string playerName, int score)
    {
        var scoresRef = FirebaseDatabase.DefaultInstance.RootReference.Child("scores").Push();
        scoresRef.Child("name").SetValueAsync(playerName);
        scoresRef.Child("score").SetValueAsync(score);
    }

    public void UpdateStudentScoreInFirebase(int userId, int newScore)
    {
        reference.Child("users").Child(userID).Child(userId.ToString()).Child("score").SetValueAsync(newScore);
    }

    public IEnumerator GetFirstName(Action<string> onCallBack)
    {
        var userNameData = reference.Child("users").Child(userID).Child("firstName").GetValueAsync();


        yield return new WaitUntil(predicate: () => userNameData.IsCompleted);


        if (userNameData != null)
        {
            DataSnapshot snapshot = userNameData.Result;
            if (snapshot != null && snapshot.Value != null)
            {
                string name = snapshot.Value.ToString();
                authentification.SetMail(name);
                onCallBack?.Invoke(name);
            }
            else
            {
                Debug.LogWarning("");
                onCallBack?.Invoke("");
            }
        }
    }


    private IEnumerator GetLastName(Action<string> onCallBack)
    {
        var userNameData = reference.Child("users").Child(userID).Child("lastName").GetValueAsync();


        yield return new WaitUntil(predicate: () => userNameData.IsCompleted);


        if (userNameData != null)
        {
            DataSnapshot snapshot = userNameData.Result;
            onCallBack?.Invoke("snapshot.Value.ToString()");
        }
    }
    

    /*private IEnumerator GetCodeID(Action<int> onCallBack)
    {
        var userNameData = reference.Child("users").Child(userID).GetValueAsync();


        yield return new WaitUntil(predicate: () => userNameData.IsCompleted);


        if (userNameData != null)
        {
            DataSnapshot snapshot = userNameData.Result;
            //(int) -> Casting
            //int.Parse -> Parsing
            //https://teamtreehouse.com/community/when-should-i-use-int-and-intparse-whats-the-difference
            onCallBack?.Invoke(int.Parse(snapshot.Value.ToString()));
        }
    }*/


    public void GetUserInfo()
    {
        StartCoroutine(GetFirstName(PrintData));
        StartCoroutine(GetLastName(PrintData));
        //StartCoroutine(GetCodeID(PrintData));
    }


    private void PrintData(string name)
    {
        Debug.Log(name);
    }


    /*private void PrintData(int code)
    {
        Debug.Log(code);
    }*/
}



public class User
{
    public string firstName;
    public string lastName;
    // public int codeID;


    public User(string firstName, string lastName/*,int codeID*/)
    {
        this.firstName = firstName;
        this.lastName = lastName;
        //this.codeID = codeID;
    }
}
