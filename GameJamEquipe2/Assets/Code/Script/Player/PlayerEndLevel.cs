using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerEndLevel : MonoBehaviour
{
    [SerializeField] private float timer;
    [SerializeField] private bool timerInProgress = false;
    [SerializeField] private TextMeshProUGUI timerText;


    // Start is called before the first frame update
    void Start()
    {
        timerInProgress = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerInProgress)
        {
            timer += Time.deltaTime;
            timerText.text = timer.ToString();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Finish")
        {
            timerInProgress = false;
            Destroy(other.gameObject);
        }
    }
}
