using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CollectibleManager : Singleton<CollectibleManager>
{
    public int collectibleCount;
    public TextMeshProUGUI collectibleText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        collectibleText.text = ":   " + collectibleCount.ToString();
    }

}
