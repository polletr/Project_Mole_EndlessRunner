using System.Collections;
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
    private UnityEvent OnDigging;

    [SerializeField]
    private UnityEvent OnDiggingSound;

    [SerializeField]
    private UnityEvent StopDiggingSound;

    [SerializeField]
    private UnityEvent OnAboveground;

    private bool onTheDirt;

    private bool invincible;
    private float invincibilityTimer = 2f; // Duration of invincibility in seconds
    private float timer = 0f; // Timer for tracking invincibility duration

    private Rigidbody2D rb;
    public HealthManager healthManager;

    [SerializeField]
    private float rotationSpeed = 5f;

    [SerializeField]
    private float maxAngleRotation = 45f;

    private bool isBlinking = false;
    [SerializeField]
    private SpriteRenderer playerSpriteRenderer;

    public Animator animator;

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
            animator.SetBool("IsDigging", true);
            rb.velocity = new Vector2(0f, -speed);
        }

        if (Input.GetKeyDown(KeyCode.Space) && onTheDirt)
        {
            OnDiggingSound.Invoke();
        }

        if ((!onTheDirt) || (HealthManager.Instance.currentLives == 0))
        {
            StopDiggingSound.Invoke();
            animator.SetBool("IsDigging", false);
        }

        transform.position = new Vector2(-15f, transform.position.y);

        if (rb.velocity.y < 0f)
        {
            float targetRotation = Mathf.Lerp(0f, -maxAngleRotation, Mathf.Abs(rb.velocity.y) / rotationSpeed);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f, 0f, targetRotation), Time.deltaTime * rotationSpeed);
        }
        else if (rb.velocity.y > 0f)
        {
            float targetRotation = Mathf.Lerp(0f, maxAngleRotation, Mathf.Abs(rb.velocity.y) / rotationSpeed);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f, 0f, targetRotation), Time.deltaTime * rotationSpeed);
        }
        else
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f, 0f, 0f), Time.deltaTime * rotationSpeed);
        }
    }

    private void FixedUpdate()
    {
        DigMovement();
        AirMovement();

        if (invincible)
        {
            // Use Time.fixedDeltaTime for consistent timing
            timer += Time.fixedDeltaTime;

            if (timer >= invincibilityTimer)
            {
                invincible = false;
                timer = 0f; // Reset the timer when invincibility is over
            }
        }
    }

    private void AirMovement()
    {
        if (!onTheDirt && !isDigging)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.AddForce(Vector2.up * flapSpeed * Time.fixedDeltaTime, ForceMode2D.Impulse);
                AudioManager.Instance.PlaySFX("Flap");
            }
        }
    }

    private void DigMovement()
    {
        if (isDigging)
        {
            if (!Input.GetKey(KeyCode.Space))
            {
                rb.AddForce(Vector2.up * emergeSpeed * Time.fixedDeltaTime, ForceMode2D.Impulse);
            }
        }

        float clampedY = Mathf.Clamp(transform.position.y, GameManager.Instance._minY, GameManager.Instance._maxY);
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle") && !invincible)
        {
            healthManager.DepleteLife();
            invincible = true;
            AudioManager.Instance.PlaySFX("Damage", 0.5f);
            StartCoroutine(BlinkPlayerSprite());
        }

        if (collision.gameObject.CompareTag("Collectible"))
        {
            Destroy(collision.gameObject);
            CollectibleManager.Instance.collectibleCount++;
            AudioManager.Instance.PlaySFX("Collectible", 0.5f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Dirt"))
        {
            rb.gravityScale = 1f;
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

    IEnumerator BlinkPlayerSprite()
    {
        isBlinking = true;
        float blinkDuration = 2f;
        float blinkInterval = 0.2f;
        float elapsedTime = 0f;

        while (elapsedTime < blinkDuration)
        {
            playerSpriteRenderer.enabled = !playerSpriteRenderer.enabled;
            yield return new WaitForSeconds(blinkInterval);
            elapsedTime += blinkInterval;
        }

        playerSpriteRenderer.enabled = true;
        isBlinking = false;
        invincible = false;
    }
}
