using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CollectibleManager : Singleton<CollectibleManager>
{
    public int collectibleCount;
    public TextMeshProUGUI collectibleText;
    private bool isCollectibleCount20Reached = false;
    private bool isCollectibleCount40Reached = false;
    private bool isCollectibleCount60Reached = false;
    private bool isCollectibleCount80Reached = false;
    private bool isCollectibleCount100Reached = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        collectibleText.text = ":   " + collectibleCount.ToString();

        if (collectibleCount == 20 && !isCollectibleCount20Reached)
        {
            HealthManager.Instance.currentLives++;
            isCollectibleCount20Reached = true;
        }

        if (collectibleCount == 40 && !isCollectibleCount40Reached)
        {
            HealthManager.Instance.currentLives++;
            isCollectibleCount40Reached = true;
        }

        if (collectibleCount == 60 && !isCollectibleCount60Reached)
        {
            HealthManager.Instance.currentLives++;
            isCollectibleCount60Reached = true;
        }

        if (collectibleCount == 80 && !isCollectibleCount80Reached)
        {
            HealthManager.Instance.currentLives++;
            isCollectibleCount80Reached = true;
        }

        if (collectibleCount == 100 && !isCollectibleCount100Reached)
        {
            HealthManager.Instance.currentLives++;
            isCollectibleCount100Reached = true;
        }
    }
}
