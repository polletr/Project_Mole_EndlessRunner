using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private GameObject cam;
    [SerializeField] private float parallaxEffect;
    [SerializeField] private float boundary;
    [SerializeField] private Vector2 respawnPos;
    [SerializeField] private Vector2 startpos;
    private float length;


    // Start is called before the first frame update
    void Start()
    {
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
       float dist = Mathf.Repeat(Time.time * parallaxEffect, length);
        transform.position = new Vector3(startpos.x + dist, transform.position.y, transform.position.z);

        // Check if the background has reached the left boundary
        if (transform.position.x < boundary)
        {
            startpos.x += respawnPos.x;
        }
        // Check if the background has reached the right boundary
        else if (transform.position.x > boundary + length)
        {
            startpos.x -= respawnPos.x;
        }
    }
}
