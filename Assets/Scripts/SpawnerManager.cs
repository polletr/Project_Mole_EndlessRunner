using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpawnSettings
{
    public float initialSpawnInterval;
    public float minSpawnInterval;
    public float intervalReductionRate;
}

public class SpawnerManager : Singleton<SpawnerManager>
{
    [Header("Collectible")]
    [SerializeField] private GameObject goldenWorm;
    [SerializeField] private SpawnSettings goldenWormSpawnSettings;
    [SerializeField] private float goldenWormStartDelay;

    [Header("Coin")]
    [SerializeField] private GameObject coin;
    [SerializeField] private SpawnSettings coinSpawnSettings;
    [SerializeField] private float coinStartDelay;

    [Header("Tree")]
    [SerializeField] private GameObject tree;
    [SerializeField] private SpawnSettings treeSpawnSettings;
    [SerializeField] private float treeStartDelay;

    [Header("Log")]
    [SerializeField] private GameObject log;
    [SerializeField] private SpawnSettings logSpawnSettings;
    [SerializeField] private float logStartDelay;

    [Header("Small Rock")]
    [SerializeField] private GameObject smallRock;
    [SerializeField] private SpawnSettings smallRockSpawnSettings;
    [SerializeField] private float smallRockStartDelay;

    [Header("Large Rock")]
    [SerializeField] private GameObject largeRock;
    [SerializeField] private SpawnSettings largeRockSpawnSettings;
    [SerializeField] private float largeRockStartDelay;

    private float timer;
    private float minBound;
    private float maxBound;
    private GameObject obstacleObj;
    private Queue<GameObject> myQueue = new Queue<GameObject>();
    private bool isSpawning = false;

    // Other existing variables...

    void Start()
    {
        timer = 0;
    }

    void FixedUpdate()
    {
        AddToQueue();
    }

    private void AddToQueue()
    {
        timer += Time.deltaTime;

        EnqueueObjectIfTimeElapsed(smallRock, smallRockSpawnSettings, smallRockStartDelay);
        EnqueueObjectIfTimeElapsed(largeRock, largeRockSpawnSettings, largeRockStartDelay);
        EnqueueObjectIfTimeElapsed(tree, treeSpawnSettings, treeStartDelay);
        EnqueueObjectIfTimeElapsed(log, logSpawnSettings, logStartDelay);
        EnqueueObjectIfTimeElapsed(coin, coinSpawnSettings, coinStartDelay);
        EnqueueObjectIfTimeElapsed(goldenWorm, goldenWormSpawnSettings, goldenWormStartDelay);

        if (!isSpawning)
        {
            StartCoroutine(SpawnObstacle());
        }
    }

    private void EnqueueObjectIfTimeElapsed(GameObject obj, SpawnSettings settings, float startDelay)
    {
        if (timer >= startDelay)
        {
            if (Mathf.Abs((timer - startDelay) % settings.initialSpawnInterval) <= 0.02f)
            {
                myQueue.Enqueue(obj);
            }
        }
    }

    IEnumerator SpawnObstacle()
    {
        isSpawning = true;

        while (myQueue.Count > 0)
        {
            GameObject obj = myQueue.Dequeue();

            float spawnInterval = Random.Range(0.5f, 2f);

            float elapsedTime = 0;

            while (elapsedTime < spawnInterval)
            {
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            SetSpawnBounds(obj.GetComponent<Obstacle>());

            Instantiate(obj, new Vector2(transform.position.x, Random.Range(maxBound, minBound)), Quaternion.identity);
        }

        yield return null;
        isSpawning = false;
    }

    void SetSpawnBounds(Obstacle obstacle)
    {
        switch (obstacle.spawnType)
        {
            case Obstacle.spawnCondition.Under:
                minBound = GameManager.Instance._minY;
                maxBound = -3f;
                break;
            case Obstacle.spawnCondition.Above:
                minBound = 1f;
                maxBound = GameManager.Instance._maxY;
                break;
            case Obstacle.spawnCondition.Tree:
                minBound = 1f;
                maxBound = 1f;
                break;
            case Obstacle.spawnCondition.Log:
                minBound = 5f;
                maxBound = 5f;
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

