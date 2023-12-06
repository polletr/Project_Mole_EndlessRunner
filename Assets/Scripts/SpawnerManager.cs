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

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        SpawnLoop();
    }

    private void SpawnLoop()
    {
        timer += Time.deltaTime;

        if (Mathf.Abs(timer % Random.Range(smallRockInterval, smallRockInterval + 2.0f)) <= 0.02f)
        {
            myQueue.Enqueue(smallRock);
        }

        if (Mathf.Abs(timer % Random.Range(largeRockInterval, largeRockInterval + 4.0f)) <= 0.02f)
        {
            myQueue.Enqueue(largeRock);
        }

        if (Mathf.Abs(timer % Random.Range(treeInterval, treeInterval + 7.0f)) <= 0.02f)
        {
            myQueue.Enqueue(tree);
        }

        /*        if (Mathf.Abs(timer % goldenWormInterval) <= 0.02f)
                {
                    obstacleObj = goldenWorm;
                    SpawnObstacle();
                }
        */
        StartCoroutine(SpawnObstacle());
    }

    IEnumerator SpawnObstacle()
    {
        yield return new WaitForSeconds(2f);

        if (myQueue.Count > 0)
        {

            Debug.Log(myQueue.Count);

            GameObject obj = myQueue.Dequeue();

            if (obj.GetComponent<Obstacle>().spawnType.ToString() == "Under")
            {
                minBound = GameManager.Instance._minY;
                maxBound = -0.5f;
            }
            else if (obj.GetComponent<Obstacle>().spawnType.ToString() == "Above")
            {
                minBound = 0.5f;
                maxBound = GameManager.Instance._maxY;
            }
            else if (obj.GetComponent<Obstacle>().spawnType.ToString() == "Tree")
            {
                minBound = 3.05f;
                maxBound = 3.05f;
            }
            else
            {
                minBound = GameManager.Instance._minY;
                maxBound = GameManager.Instance._maxY;
            }


            GameObject spawnedObstacle = Instantiate(obj, new Vector2(transform.position.x, Random.Range(maxBound, minBound)), Quaternion.identity);

        }
    }

    IEnumerator WaitForFunction()
    {
        yield return new WaitForSeconds(3);
    }

}

