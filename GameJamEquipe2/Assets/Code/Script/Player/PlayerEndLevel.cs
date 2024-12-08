using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerEndLevel : MonoBehaviour
{
    [SerializeField] private float timer;
    [SerializeField] private bool timerInProgress = false;
    [SerializeField] private TextMeshProUGUI timerText;
    
    public GameObject LastCheckpoint { get; set; }


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

        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            SceneManager.LoadScene("Level/Scenes/BoostLevel");
            return;
        }
        
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            SceneManager.LoadScene("Level/Scenes/TranslateurLevel");
        }
    }

    private void FixedUpdate()
    {
        if (transform.position.y <= -20)
        {
            transform.position = LastCheckpoint.transform.position;
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
