using Firebase.Firestore;
using UnityEngine;

[FirestoreData]
public struct CharacterData
{
    [FirestoreProperty]
    public string Name { get; set; }
    [FirestoreProperty]
    public string Description { get; set; }
    [FirestoreProperty]
    public int Attack { get; set; }
    [FirestoreProperty]
    public int Defense { get; set; }
}

[FirestoreData]
public class UserData
{
    [FirestoreProperty]
    public string Email { get; set; }

    [FirestoreProperty]
    public int Score { get; set; }
}
