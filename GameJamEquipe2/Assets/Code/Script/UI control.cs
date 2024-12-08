using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonHandler : MonoBehaviour
{
    public string[] levelNames = { "DoubleJumps", "Grapin" };

    public void PlayGame()
    {
        if (levelNames.Length > 0)
        {
            SceneManager.LoadScene(levelNames[0]);
        }
        else
        {
            Debug.LogError("Le tableau 'levelNames' est vide !");
        }
    }
    public void LoadLevelByIndex(int index)
    {
        if (index >= 0 && index < levelNames.Length)
        {
            SceneManager.LoadScene(levelNames[index]);
        }
        else
        {
            Debug.LogError("Index de niveau invalide !");
        }
    }

    public void SelectLevel()
    {
        SceneManager.LoadScene("LevelSelectScene");
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }

    public void ActivateObject(GameObject obj)
    {
        obj.SetActive(true);
    }

    public void DeactivateObject(GameObject obj)
    {
        obj.SetActive(false);
    }

    public void PrintMessage(string message)
    {
        Debug.Log(message);
    }
}
