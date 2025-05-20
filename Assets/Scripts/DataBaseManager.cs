using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;

public class DataBaseManager : MonoBehaviour
{
    [SerializeField] private string UserID;
    [SerializeField] private StudentSO studentSO;
    [SerializeField] private Authentification authentification;
    private DatabaseReference reference;

    // Start is called before the first frame update
    private void Awake()
    {
        UserID = SystemInfo.deviceUniqueIdentifier;
    }

    private void Start()
    {
        reference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void UpdloadStudent()
    {
        Student newStudent = studentSO.GetBasicStudentData();
        Mail newauthentification = authentification.GetBasicData();
        string json = JsonUtility.ToJson(newStudent);

        reference.Child("Students").Child(UserID)./*Child(newStudent.nickName).*/SetRawJsonValueAsync(json);
    }
}

[System.Serializable]
public class Student
{
    public string name;
    public int id;
    public int score;

    public Student(string name, int id,int score)
    {
        this.name = name;
        this.id = id;
        this.score = score;
    }
}
[System.Serializable]
public class Mail
{
    public string email;
    public string password;
    public int score;
    public Mail(string email, string password, int score)
    {
        this.email = email;
        this.password = password;
        this.score = score;
    }
}
