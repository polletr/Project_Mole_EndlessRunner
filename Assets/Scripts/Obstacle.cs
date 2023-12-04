using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public enum spawnCondition // your custom enumeration
    {
        Above,
        Under,
        All
    };

    [SerializeField]
    private Sprite[] sprite;

    private SpriteRenderer spriteRenderer;

    public spawnCondition spawnType;

    private BoxCollider2D collider;


    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<BoxCollider2D>();

        int i = Random.Range(0, sprite.Length);
        spriteRenderer.sprite = sprite[i];

        Vector2 S = spriteRenderer.sprite.bounds.size;
        collider.size = S;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.x <= -12f)
            Destroy(this.gameObject);
    }
}
