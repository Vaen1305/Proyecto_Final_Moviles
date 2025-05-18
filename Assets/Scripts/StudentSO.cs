using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Student", menuName = "ScriptableObjects/Example/Student")]
public class StudentSO : ScriptableObject
{
    [SerializeField] private string playerName;
    [SerializeField] private int id;
    [SerializeField] private int score;

    public string PlayerName => playerName;
    public int Id => id;
    public int Score => score;

    public void SetName(string newName)
    {
        playerName = newName;
    }
    public void SetId(int newId)
    {
        id = newId;
    }
    public void SetScore(int newScore)
    {
        score = newScore;
    }
    public Student GetBasicStudentData()
    {
        return new Student(playerName, id, score);
    }
}
