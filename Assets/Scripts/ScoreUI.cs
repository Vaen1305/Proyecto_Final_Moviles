using UnityEngine;
using TMPro;
public class ScoreUI : MonoBehaviour
{
    public TMP_Text scoresText;
    void Start()
    {
        ShowTopScores();
    }

    void ShowTopScores()
    {
        scoresText.text = "";
        for (int i = 0; i < 5; i++)
        {
            int score = PlayerPrefs.GetInt($"score_{i}", 0);
            string name = PlayerPrefs.GetString($"name_{i}", "Jugador");
            scoresText.text += $"{i + 1}. {name}: {score}\n";
        }
    }
}
