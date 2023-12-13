using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    private float minY;
    [SerializeField]
    private float maxY;
    [SerializeField]
    private float gameSpeed;

    private float timer;

    [SerializeField]
    private float increaseSpeedTimer;

    [SerializeField]
    private float increaSpeedMultiplier;

    public float _minY
    {
        get { return minY; }
    }
    public float _maxY
    {
        get { return maxY; }
    }
    public float _gameSpeed
    {
        get { return gameSpeed; }
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        IncreaseSpeedOverTime();
    }

    private void IncreaseSpeedOverTime()
    {
        if (Mathf.Abs(timer % increaseSpeedTimer) <= 0.02f)
        {
            gameSpeed *= increaSpeedMultiplier;
        }
    }
}
