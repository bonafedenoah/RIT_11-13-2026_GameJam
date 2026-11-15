using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int health = 10;

    public int Health
    {
        get { return health; }
    }

    [SerializeField] private float speed = 10f;

    [SerializeField] private float playerHalfHeight = 1.5f;

    [SerializeField] private SpriteRenderer spriteRenderer;

    private bool reversedControls = true;

    private Vector2 playerDirection;

    private Vector2 playerVelocity;

    private Vector2 playerPosition;

    private Camera mainCamera;

    private Rigidbody2D rb;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCamera = Camera.main;

        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        ReadInput();
    }

    private void FixedUpdate()
    {
        playerVelocity = playerDirection * speed;

        playerPosition = rb.position + playerVelocity * Time.fixedDeltaTime;

        StopMoving();

        rb.MovePosition(playerPosition);
    }

    private void ReadInput()
    {
        Vector2 PlayerRawInput = Vector2.zero;

        if (Keyboard.current != null)
        {
            if (reversedControls == false)
            {
                if (Keyboard.current.wKey.isPressed)
                {
                    PlayerRawInput.y += 1f;
                }

                if (Keyboard.current.sKey.isPressed)
                {
                    PlayerRawInput.y -= 1f;
                }

                if (Keyboard.current.iKey.isPressed)
                {
                    reversedControls = true;
                }

            }
            else
            {
                if (Keyboard.current.wKey.isPressed)
                {
                    PlayerRawInput.y -= 1f;
                }
                if (Keyboard.current.sKey.isPressed)
                {
                    PlayerRawInput.y += 1f;
                }

                if (Keyboard.current.iKey.isPressed)
                {
                    reversedControls = false;
                }
            }
        }

        playerDirection = PlayerRawInput.normalized;
    }

    private void StopMoving()
    {
        float camHeight = mainCamera.orthographicSize;
        float maxY = camHeight - playerHalfHeight - 0.5f;
        float minY = -camHeight + playerHalfHeight + 0.5f;


        playerPosition.y = Mathf.Clamp(playerPosition.y, minY, maxY);
    }

    public void ToggleControlsFlip()
    {
        reversedControls = !reversedControls;

        if (reversedControls == true)
        {
            spriteRenderer.color = Color.white;
        }
        else
        {
            spriteRenderer.color = Color.black;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "EnemyBullet")
        {
            Destroy(collision.gameObject);
            health--;
        }
    }
}
