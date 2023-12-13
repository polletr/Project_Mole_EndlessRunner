using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Obstacle : MonoBehaviour
{
    public enum spawnCondition // your custom enumeration
    {
        Above,
        Under,
        Tree,
        Log,
        All
    };

    [SerializeField]
    private Sprite[] sprite;

    private SpriteRenderer spriteRenderer;

    public spawnCondition spawnType;

    private BoxCollider2D collider;

    private SpriteMask mask;


    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<BoxCollider2D>();

        mask = GetComponentInChildren<SpriteMask>();

        int i = Random.Range(0, sprite.Length);
        spriteRenderer.sprite = sprite[i];

        Vector2 S = spriteRenderer.sprite.bounds.size;
        if (spawnType == spawnCondition.Tree)
            collider.size = new Vector2(S.x / 2f, S.y);
        if (spawnType == spawnCondition.Log)
            collider.size = new Vector2(S.x / 10f, S.y);
        else
            collider.size = S / 2f;

        mask.transform.localScale = S * 7f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x + Time.fixedDeltaTime * -GameManager.Instance._gameSpeed, transform.position.y, transform.position.z);
        //rb.velocity = Vector2.left * GameManager.Instance._gameSpeed * Time.fixedDeltaTime;
        if (this.transform.position.x <= -20f)
            Destroy(this.gameObject);
    }
}
