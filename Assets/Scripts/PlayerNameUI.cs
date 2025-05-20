using UnityEngine;
using TMPro;

public class PlayerNameUI : MonoBehaviour
{
    public TMP_InputField playerNameInputField;
    public TMP_InputField playerIdInputField;
    public TMP_Text playerNameText;
    public DatabaseHandler databaseHandler;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(databaseHandler.GetFirstName(UpdateNameUI));
    }

    public void UpdateNameUI(string playerName)
    {
        playerNameText.text = playerName;
        playerNameInputField.text = playerName;
    }

    public void OnChangeNameButton()
    {
        string newName = playerNameInputField.text;
        //string newId = int.Parse(playerIdInputField.text);
        int newScore = 0;
        //databaseHandler.UpdateFirstName(newName, newId, newScore);
        playerNameText.text = newName;
    }
}
