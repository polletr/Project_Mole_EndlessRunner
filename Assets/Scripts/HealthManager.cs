using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    [SerializeField]
    private int maxLives;

    private int currentLives;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    // Start is called before the first frame update
    void Start()
    {
        currentLives = maxLives;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        foreach (Image img in hearts)
        {
            img.sprite = emptyHeart;
        }
        for (int i = 0; i < currentLives; i++)
        {
            hearts[i].sprite = fullHeart;
        }

        if (currentLives <= 0)
        {
            SceneManager.LoadSceneAsync("GameOver");
        }
    }

    public void DepleteLife()
    {
        currentLives--;
    }
}
