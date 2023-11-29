using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    private float speed;
    [SerializeField]
    private float lowBoundary;
    [SerializeField]
    private float highBoundary;



    [SerializeField]
    private UnityEvent OnDigging;


    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            OnDigging.Invoke();
            if (transform.position.y > lowBoundary)
            {
                rb.AddForce(new Vector2(0, speed * Time.deltaTime), ForceMode2D.Impulse);
            }

        }

        if (transform.position.y <= lowBoundary)
        {
            transform.position = new Vector2(0f, lowBoundary);
        }

    }
}
