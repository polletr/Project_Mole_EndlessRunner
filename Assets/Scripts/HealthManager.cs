using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthManager : Singleton<HealthManager>
{
    [SerializeField]
    private int maxLives;

    public int currentLives;
    public Image heartPrefab; // The heart prefab to instantiate
    public Transform heartsParent; // The parent transform for the instantiated hearts

    // Start is called before the first frame update
    void Start()
    {
        currentLives = maxLives;
        InitializeHearts();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        UpdateHearts();

        if (currentLives <= 0)
        {
            AudioManager.Instance.StopMusic();
            AudioManager.Instance.ToggleSFX();
            SceneManager.LoadSceneAsync("GameOver");
            AudioManager.Instance.PlaySFX("GameOver", 0.1f);
        }
    }

    // Instantiate hearts based on the maxLives
    void InitializeHearts()
    {
        for (int i = 0; i < maxLives; i++)
        {
            Image heart = Instantiate(heartPrefab, heartsParent);
        }
    }

    // Update the hearts based on the currentLives
    void UpdateHearts()
    {
        Image[] hearts = heartsParent.GetComponentsInChildren<Image>();

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentLives)
            {
                hearts[i].enabled = true; // Show the heart
            }
            else
            {
                hearts[i].enabled = false; // Hide the heart
            }
        }
    }

    public void DepleteLife()
    {
        currentLives--;
    }
}
