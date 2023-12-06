using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class SpawnerManager : Singleton<SpawnerManager>
{
    private float obstacleSpeed;

    [Header("Collectible")]
    [SerializeField]
    private GameObject goldenWorm;
    [SerializeField]
    private float goldenWormInterval;

    [Header("Tree")]
    [SerializeField]
    private GameObject tree;
    [SerializeField]
    private float treeInterval;

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

    private float minBound;
    private float maxBound;

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

        if (Mathf.Abs(timer % smallRockInterval) <= 0.02f)
        {
            obstacleObj = smallRock;
            SpawnObstacle();
        }
        
        if (Mathf.Abs(timer % largeRockInterval) <= 0.02f)
        {
            obstacleObj = largeRock;
            SpawnObstacle();
        }

        if (Mathf.Abs(timer % treeInterval) <= 0.02f)
        {
            obstacleObj = tree;
            SpawnObstacle();
        }

/*        if (Mathf.Abs(timer % goldenWormInterval) <= 0.02f)
        {
            obstacleObj = goldenWorm;
            SpawnObstacle();
        }
*/
    }

    private void SpawnObstacle()
    {
        if (obstacleObj.GetComponent<Obstacle>().spawnType.ToString() == "Under")
        {
            minBound = GameManager.Instance._minY;
            maxBound = -0.5f;

        }
        else if (obstacleObj.GetComponent<Obstacle>().spawnType.ToString() == "Above")
        {
            minBound = 0.5f;
            maxBound = GameManager.Instance._maxY;
        }
        else if (obstacleObj.GetComponent<Obstacle>().spawnType.ToString() == "Tree")
        {
            minBound = 3.05f;
            maxBound = 3.05f;
        }
        else
        {
            minBound = GameManager.Instance._minY;
            maxBound = GameManager.Instance._maxY;
        }


        GameObject spawnedObstacle = Instantiate(obstacleObj, new Vector2(transform.position.x, Random.Range(maxBound, minBound)), Quaternion.identity);

        Rigidbody2D obstacleRB = spawnedObstacle.GetComponent<Rigidbody2D>();
        obstacleSpeed = GameManager.Instance._gameSpeed;

        obstacleRB.velocity = Vector2.left * obstacleSpeed;
    }


}
