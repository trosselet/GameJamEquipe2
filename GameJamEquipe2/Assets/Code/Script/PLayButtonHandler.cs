using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButtonHandler : MonoBehaviour
{
    public void PlayGame()
    {
        Debug.Log("PlayGame appelé !");
        SceneManager.LoadScene("Level Double Jumps");
    }
}
