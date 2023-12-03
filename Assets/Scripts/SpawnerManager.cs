using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class SpawnerManager : Singleton<SpawnerManager>
{
    [SerializeField]
    private float obstacleSpeed;

    [Header("Small Rock")]
    [SerializeField]
    private GameObject smallRock;
    [SerializeField]
    private float smallRockInterval;

    [Header("Large Rock")]
    [SerializeField] 
    private GameObject largeRock;
    [SerializeField]
    private float largeRockInterval;

    private float timer;

    private GameObject obstacleObj;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        SpawnLoop();
    }

    private void SpawnLoop()
    {
        timer += Time.fixedDeltaTime;
        Debug.Log("timer");

        if (Mathf.Abs(timer % smallRockInterval) <= 0.02f)
        {
            obstacleObj = smallRock;
            SpawnGroundObstacle();
        }
        
        if (Mathf.Abs(timer % largeRockInterval) <= 0.02f)
        {
            obstacleObj = largeRock;
            SpawnGroundObstacle();
        }


    }

    private void SpawnGroundObstacle()
    {
        GameObject spawnedObstacle = Instantiate(obstacleObj, new Vector2(transform.position.x, Random.Range(-0.5f, GameManager.Instance._minY)), Quaternion.identity);

        Rigidbody2D obstacleRB = spawnedObstacle.GetComponent<Rigidbody2D>();

        obstacleRB.velocity = Vector2.left * obstacleSpeed;
    }

}
