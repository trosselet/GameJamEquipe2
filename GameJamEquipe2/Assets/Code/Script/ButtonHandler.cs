using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    // Fonction appel�e par le bouton Play
    public void PlayGame()
    {
        // Charger la sc�ne de jeu (remplacez "GameScene" par le nom de votre sc�ne de jeu)
        SceneManager.LoadScene("Level Double Jumps");
    }

    // Fonction appel�e par le bouton Select Level
    public void SelectLevel()
    {
        // Charger la sc�ne de s�lection des niveaux (remplacez "LevelSelectScene" par le nom de votre sc�ne)
        SceneManager.LoadScene("LevelSelectScene");
    }

    // Fonction appel�e par le bouton Quit
    public void QuitGame()
    {
        // Quitter l'application
        Debug.Log("Quit Game"); // Pour tester dans l'�diteur (ne fermera pas l'�diteur)
        Application.Quit();
    }
}
