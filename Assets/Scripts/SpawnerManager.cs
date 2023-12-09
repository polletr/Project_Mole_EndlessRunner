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

    Queue<GameObject> myQueue = new Queue<GameObject>();

    private bool isSpawning = false;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        AddToQueue();
    }

    private void AddToQueue()
    {
        timer += Time.deltaTime;

        if (Mathf.Abs(timer % smallRockInterval) <= 0.02f)
        {
            myQueue.Enqueue(smallRock);
        }

        if (Mathf.Abs(timer % largeRockInterval) <= 0.02f)
        {
            myQueue.Enqueue(largeRock);
        }

        if (Mathf.Abs(timer % treeInterval) <= 0.02f)
        {
            myQueue.Enqueue(tree);
        }

        if (Mathf.Abs(timer % goldenWormInterval) <= 0.02f)
        {
            myQueue.Enqueue(goldenWorm);
        }

        if (!isSpawning)
        {
            StartCoroutine(SpawnObstacle());
        }
    }

    IEnumerator SpawnObstacle()
    {
        isSpawning = true;

        while (myQueue.Count > 0)
        {
            GameObject obj = myQueue.Dequeue();

            float spawnInterval = 0;

            if (obj == smallRock)
            {
                spawnInterval = Random.Range(smallRockInterval - 2.0f, smallRockInterval + 2.0f);
            }
            else if (obj == largeRock)
            {
                spawnInterval = Random.Range(largeRockInterval - 2.0f, largeRockInterval + 2.0f);
            }
            else if (obj == tree)
            {
                spawnInterval = Random.Range(treeInterval - 3.0f, treeInterval + 2.0f);
            }
            else if (obj == goldenWorm)
            {
                spawnInterval = goldenWormInterval;
            }

            float elapsedTime = 0;

            while (elapsedTime < spawnInterval)
            {
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // Set spawn bounds based on the object type
            SetSpawnBounds(obj.GetComponent<Obstacle>());

            Instantiate(obj, new Vector2(transform.position.x, Random.Range(maxBound, minBound)), Quaternion.identity);
        }

        yield return new WaitForSeconds(1.0f); // Introduce a delay after spawning all objects
        isSpawning = false;
    }

    void SetSpawnBounds(Obstacle obstacle)
    {
        switch (obstacle.spawnType)
        {
            case Obstacle.spawnCondition.Under:
                minBound = GameManager.Instance._minY;
                maxBound = -0.5f;
                break;
            case Obstacle.spawnCondition.Above:
                minBound = 0.5f;
                maxBound = GameManager.Instance._maxY;
                break;
            case Obstacle.spawnCondition.Tree:
                minBound = 3.05f;
                maxBound = 3.05f;
                break;
            case Obstacle.spawnCondition.All:
                minBound = GameManager.Instance._minY;
                maxBound = GameManager.Instance._maxY;
                break;
            default:
                break;
        }
    }

}

