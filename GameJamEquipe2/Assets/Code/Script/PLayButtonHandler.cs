using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButtonHandler : MonoBehaviour
{
    public void PlayGame()
    {
        Debug.Log("PlayGame appel� !");
        SceneManager.LoadScene("Level Double Jumps");
    }
}
