using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverUi;

    public static GameOverManager instance;
    // Singleton de GameOverManager
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de GameOverManager dans la scène");
            return;
        }
        instance = this;
    }

    public void OnPlayerDeath()
    {
        if (CurrentSceneManager.instance.isPlayerPresentByDefault)
        {
            DontDestroyOnLoadScene.instance.RemoveFromDontDestroyOnLoad();
        }
        gameOverUi.SetActive(true);
    }

    public void RetryButton()
    {
        // Retire les pièces récuperer dans le niveau
        Inventory.instance.RemoveCoins(CurrentSceneManager.instance.coinsPickedUpInThisSceneCount);
        // Recharge la Scène
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        PlayerHealth.instance.Respawn();
        // Désactvier le menu GameOver apres un respawn
        gameOverUi.SetActive(false);

    }
    public void MainMenuButton()
    {
        // retour au menu principal

    }
    public void QuitButton()
    {
        // fermer le jeu
        Application.Quit();
    }
}
