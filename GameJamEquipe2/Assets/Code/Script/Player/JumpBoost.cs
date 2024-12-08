using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JumpBoost : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private Image loadingImage;
    [SerializeField] private TextMeshProUGUI loadingText;
    [Range(0, 1)]
    public float loadingProgress = 0;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && loadingProgress < 100)
        {
            loadingProgress += 1 * Time.deltaTime;
            
        }
        else
        {
            loadingProgress -= 1 * Time.deltaTime;
            
        }

        if (loadingProgress > 100)
        {
            loadingProgress = 100;
        }
        if (loadingProgress < 0)
        {
            loadingProgress = 0;
        }

        loadingImage.fillAmount = loadingProgress;
        if (loadingProgress < 1)
        {
            loadingText.text = Mathf.RoundToInt(loadingProgress * 100) + "%";
        }


        playerMovement.jumpForce += (loadingProgress / 100);
    }
}