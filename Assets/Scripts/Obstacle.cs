using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public enum spawnCondition // your custom enumeration
    {
        Above,
        Under,
        Tree,
        All
    };

    [SerializeField]
    private Sprite[] sprite;

    private SpriteRenderer spriteRenderer;

    public spawnCondition spawnType;

    private BoxCollider2D collider;

    private Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<BoxCollider2D>();

        int i = Random.Range(0, sprite.Length);
        spriteRenderer.sprite = sprite[i];

        Vector2 S = spriteRenderer.sprite.bounds.size;
        collider.size = S;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = Vector2.left * GameManager.Instance._gameSpeed * Time.fixedDeltaTime;

        if (this.transform.position.x <= -15f)
            Destroy(this.gameObject);
    }
}
