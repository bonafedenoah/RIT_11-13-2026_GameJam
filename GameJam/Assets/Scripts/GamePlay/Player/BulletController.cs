using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private float speed = 15f;
    [SerializeField] private float lifeTime = 5f;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerBullet")
        {
            Destroy(this.gameObject);
            Destroy(collision.gameObject);
        }
    }
}