using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JumpBoost : MonoBehaviour
{
    [SerializeField] private Image loadingImage;
    [SerializeField] private TextMeshProUGUI loadingText;
    [Range(0, 1)]
    public float loadingProgress = 0;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        loadingImage.fillAmount = loadingProgress;
        if (loadingProgress < 1 || loadingProgress != 0)
        {
            loadingText.text = Mathf.RoundToInt(loadingProgress * 100) + "%";
        }
    }
}
