using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GlobalSceneManager globalSceneManager;

    public void OnJugarButton()
    {
        globalSceneManager.LoadScene("Game");
    }

    public void OnPuntajesButton()
    {
        globalSceneManager.LoadScene("Results");
    }

    public void OnSalirButton()
    {
        Application.Quit();
    }
}
