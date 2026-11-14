using System.Collections;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private int pointValue = 50;
    [SerializeField] private float speed = 1.0f;
    private ScoreManager scoreManager;
    private Vector2 direction = Vector2.right;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        scoreManager = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 velocity = direction * speed;
        Vector2 position = rb.position + velocity * Time.deltaTime;

        rb.MovePosition(position);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PlayerBullet")
        {
            scoreManager.AddPlayerScore(pointValue);
            Destroy(this.gameObject);
            Destroy(collision.gameObject);
        }
    }
}
