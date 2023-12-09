using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

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


    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<BoxCollider2D>();

        int i = Random.Range(0, sprite.Length);
        spriteRenderer.sprite = sprite[i];

        Vector2 S = spriteRenderer.sprite.bounds.size;
        if (spawnType == spawnCondition.Tree)
            collider.size = new Vector2(S.x / 2f, S.y);
        else
            collider.size = S / 2f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x + Time.fixedDeltaTime * -GameManager.Instance._gameSpeed, transform.position.y, transform.position.z);
        //rb.velocity = Vector2.left * GameManager.Instance._gameSpeed * Time.fixedDeltaTime;

        if (this.transform.position.x <= -15f)
            Destroy(this.gameObject);
    }
}
