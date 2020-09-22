using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;

    public GameObject pauseMenuUI;

    public GameObject SettingsWindow;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Paused();
            }
        }
    }
    void Paused()
    {
        // Empecher les mouvements en pause
        PlayerMovement.instance.enabled = false;
        // Activer notre menu pause / l'afficher
        pauseMenuUI.SetActive(true);
        // Arrêter le temps
        Time.timeScale = 0;
        // Changer le statu du jeu
        gameIsPaused = true;
    }

    public void Resume()
    {
        // Réactiver les mouvements une fois la pause fini
        PlayerMovement.instance.enabled = true;
        // Activer notre menu pause / l'afficher
        pauseMenuUI.SetActive(false);
        // Arrêter le temps
        Time.timeScale = 1;
        // Changer le statu du jeu
        gameIsPaused = false;
    }

    public void OpenSettingsWindow()
    {
        SettingsWindow.SetActive(true);
    }

    public void CloseSettingsWindow()
    {
        SettingsWindow.SetActive(false);
    }

    public void LoadMainMenu()
    {
        //Permet au jeu de ne pas garder les objets
        DontDestroyOnLoadScene.instance.RemoveFromDontDestroyOnLoad();
        // Toutes les valeurs reprennent leurs valeurs de base avant de retourner au main menu
        Resume();
        // retour au menu principal
        SceneManager.LoadScene("MainMenu");
    }
}
