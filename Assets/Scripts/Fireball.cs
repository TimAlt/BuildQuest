using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float damage;
    public GameObject boom;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("EnemyFireball"))
        {
            Instantiate(boom, collision.transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
