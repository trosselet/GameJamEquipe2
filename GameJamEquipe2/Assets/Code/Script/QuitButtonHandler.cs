using UnityEngine;

public class QuitButtonHandler : MonoBehaviour
{
    public void QuitGame()
    {
        Debug.Log("Quit Game");

        Application.Quit();
    }
}
