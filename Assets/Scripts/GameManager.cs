using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    private float minY;
    [SerializeField]
    private float maxY;

    public float _minY
    {
        get { return minY; }
    }
    public float _maxY
    {
        get { return maxY; }
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
