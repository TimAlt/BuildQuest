using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Collider trigger;
    public GameObject enemy00;

    private float timeSinceLastSpawn = 0f;
    private const float spawnInterval = 90f;
    private int extraMinions = 0;
    // Start is called before the first frame update
    void Start()
    {
        Spawn();
        Spawn();
        Spawn();
    }

    private void FixedUpdate()
    {
        timeSinceLastSpawn += Time.fixedDeltaTime;
        if (timeSinceLastSpawn >= spawnInterval)
        {
            timeSinceLastSpawn = 0;
            if (extraMinions < 3)
            {
                extraMinions += 1;
                Spawn();
            }
            
        }
    }

    private void Spawn()
    {
        Vector3 randomPoint = new Vector3(Random.Range(-8, 8), 0, Random.Range(-8, 8)) + transform.position;
        GameObject newEnemy = Instantiate(enemy00, randomPoint, Quaternion.identity);
        newEnemy.GetComponent<Enemy00>().trigger = trigger;
    }
}
