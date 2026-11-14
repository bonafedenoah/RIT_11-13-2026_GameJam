using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 10f;

    [SerializeField] private float playerHalfHeight = 1.5f;

    private Vector2 playerDirection;

    private Vector2 playerVelocity;

    private Vector2 playerPosition;

    private Camera mainCamera;

    private Rigidbody2D rb;

    AudioManager audioManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCamera = Camera.main;

        rb=GetComponent<Rigidbody2D>();

        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
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
            if (Keyboard.current.wKey.isPressed)
            {
                PlayerRawInput.y += 1f;
            }

            if (Keyboard.current.sKey.isPressed)
            {
                PlayerRawInput.y -= 1f;
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
}
