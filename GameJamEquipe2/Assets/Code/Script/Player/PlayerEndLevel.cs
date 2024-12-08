using TMPro;
using UnityEngine;

public class PlayerEndLevel : MonoBehaviour
{
    [SerializeField] private float timer;
    [SerializeField] private bool timerInProgress = false;
    [SerializeField] private TextMeshProUGUI timerText;

    private void Start()
    {
        timerInProgress = true;
    }

    private void Update()
    {
        if (timerInProgress)
        {
            timer += Time.deltaTime;

            timerText.text = timer.ToString("F2");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            timerInProgress = false;

            Destroy(other.gameObject);
        }
    }
}
