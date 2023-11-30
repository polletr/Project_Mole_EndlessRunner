using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    private float speed;

    [SerializeField]
    private float emergeSpeed;

    [SerializeField]
    private float flapSpeed;

    private bool isDigging;

    [SerializeField]
    private float minY;
    [SerializeField]
    private float maxY;

    [SerializeField]
    private UnityEvent OnDigging;

    [SerializeField]
    private UnityEvent OnAboveground;

    private bool onTheDirt;


    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && (onTheDirt || isDigging))
        {
            OnDigging.Invoke();
            rb.velocity = new Vector2(0f, -speed);

        }

        DigMovement();
        AirMovement();
    }

    private void AirMovement()
    {
        if (!onTheDirt && !isDigging)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.AddForce(Vector2.up * flapSpeed, ForceMode2D.Force);
            }
        }
    }

    private void DigMovement()
    {
        if (isDigging)
        {
            if (!Input.GetKey(KeyCode.Space))
            {
                //rb.velocity = new Vector2(0f, digSpeed);
                rb.AddForce(Vector2.up * emergeSpeed, ForceMode2D.Impulse);

            }
        }

        // Clamp the position within boundaries
        float clampedY = Mathf.Clamp(transform.position.y, minY, maxY);
        transform.position = new Vector2(transform.position.x, clampedY);

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Dirt"))
        {
            rb.gravityScale = 0f;

            isDigging = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Dirt"))
        {
            Debug.Log("Exit");
            rb.gravityScale = 1f;
            //rb.velocity = new Vector2(0f, emergeSpeed);
            //rb.AddForce(Vector2.up * emergeSpeed, ForceMode2D.Impulse);

            OnAboveground.Invoke();
            isDigging = false;
            onTheDirt = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Dirt"))
        {
            onTheDirt = true;
        }

    }

}
