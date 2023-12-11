using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private GameObject cam;
    [SerializeField] private float speedDifference;
    [SerializeField] private float boundary;
    [SerializeField] private Vector2 respawnPos;
    [SerializeField] private Vector2 startpos;

    private float speed;
    private float length;

    // Start is called before the first frame update
    void Start()
    {
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        speed = GameManager.Instance._gameSpeed - speedDifference;

        float dist = Mathf.Repeat(Time.fixedTime * -speed, length);
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

    // Draw gizmos in the scene view
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(startpos, 0.5f); // Adjust the sphere size as needed

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(respawnPos + startpos, 0.5f); // Adjust the sphere size as needed
    }
}
