using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Student", menuName = "ScriptableObjects/Example/Student")]
public class StudentSO : ScriptableObject
{
    [SerializeField] private string playerName;
    [SerializeField] private int id;
    public string PlayerNameName => playerName;
    public int Id => id;
    public void SetName(string newName)
    {
        playerName = newName;
    }
    public void SetId(int newId)
    {
        id = newId;
    }
    public Student GetBasicStudentData()
    {
        return new Student(name, id);
    }
}
