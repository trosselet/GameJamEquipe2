using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    // Fonction appelée par le bouton Play
    public void PlayGame()
    {
        // Charger la scène de jeu (remplacez "GameScene" par le nom de votre scène de jeu)
        SceneManager.LoadScene("Level Double Jumps");
    }

    // Fonction appelée par le bouton Select Level
    public void SelectLevel()
    {
        // Charger la scène de sélection des niveaux (remplacez "LevelSelectScene" par le nom de votre scène)
        SceneManager.LoadScene("LevelSelectScene");
    }

    // Fonction appelée par le bouton Quit
    public void QuitGame()
    {
        // Quitter l'application
        Debug.Log("Quit Game"); // Pour tester dans l'éditeur (ne fermera pas l'éditeur)
        Application.Quit();
    }
}
