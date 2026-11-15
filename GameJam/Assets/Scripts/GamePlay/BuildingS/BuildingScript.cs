using UnityEngine;
using UnityEngine.SceneManagement;

public class BuildingScript : MonoBehaviour
{
    private BuildingSpawner buildingSpawner;

    private void Start()
    {
        buildingSpawner = GameObject.FindGameObjectWithTag("BuildingManager").GetComponent<BuildingSpawner>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "EnemyBullet")
        {
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
            buildingSpawner.CurrentBuildings--;
        }
    }
}
