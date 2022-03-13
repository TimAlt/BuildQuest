using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy00 : MonoBehaviour
{
    private Rigidbody rb;

    public float moveSpeed;
    public Animator wizardAnim;
    public Animator staffAnim;

    public Transform firePosition;
    public GameObject fireball;
    public Collider trigger;
    public Spawner spawner;

    private float minDistance;
    private float timeBetweenAttacks = 3f;
    private float timeSinceLastAttack = 0;
    private float hodlTime = .5f;
    private bool charging = false;
    private float hodling = 0;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        minDistance = Random.Range(3f, 8f);
        timeBetweenAttacks = Random.Range(1.2f, 3f);
    }


    private void FixedUpdate()
    {
        if (charging == true)
        {
            hodling += Time.fixedDeltaTime;
            if (hodling > hodlTime)
            {
                hodling = 0;
                charging = false;
                Attack();
            }
        }
        Vector3 playerPosition = World.player.transform.position;
        if (trigger.bounds.Contains(playerPosition))
        {
            if (Vector3.Distance(transform.position, playerPosition) >= minDistance)
            {
                Vector3 moveForce = (playerPosition - transform.position).normalized;
                rb.AddForce(moveForce * moveSpeed * Time.fixedDeltaTime);
                wizardAnim.SetBool("Moving", true);
            }
            else
            {
                wizardAnim.SetBool("Moving", false);
            }
        }
        else
        {
            wizardAnim.SetBool("Moving", false);
        }

        if (Vector3.Distance(transform.position, playerPosition) < 22f)
        {
            timeSinceLastAttack += Time.fixedDeltaTime;
            if (timeSinceLastAttack >= timeBetweenAttacks)
            {
                timeSinceLastAttack = 0;
                timeBetweenAttacks = Random.Range(1.2f, 3f);
                ChargeUp();
            }
        }

        if (transform.position.y <= -2f)
        {
            //Trigger here
        }
    }

    private void ChargeUp()
    {
        staffAnim.ResetTrigger("Attack");
        staffAnim.SetTrigger("ChargeUp");
        charging = true;
    }

    private void Attack()
    {
        staffAnim.ResetTrigger("ChargeUp");
        staffAnim.SetTrigger("Attack");

        Vector3 offsetVector = new Vector3(Random.Range(-1, 1), 0, Random.Range(-1, 1)).normalized * Random.Range(0, 4f);
        Vector3 targetVector = World.player.transform.position + offsetVector;

        Vector3 staffPosition = firePosition.position;
        Vector3 targetPosition = new Vector3(targetVector.x, staffPosition.y, targetVector.z);
        GameObject newFireball = Instantiate(fireball, staffPosition, Quaternion.identity);
        Vector3 force = (targetPosition - staffPosition).normalized;
        newFireball.GetComponent<Rigidbody>().AddForce(force * 2200f);
    }
}
