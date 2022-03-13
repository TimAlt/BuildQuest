using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Player : MonoBehaviour
{
    private Rigidbody rb;
    private float vInput;
    private float hInput;

    private float prevHeight = 0;
    private bool grounded = true;

    private int points = 0;
    private float gTimer = 0;
    private float charge = 0;

    public float moveSpeed;
    public float jumpForce;
    public Animator playerAnim;
    public Animator staffAnim;
    public Slider slider;

    public Transform firePosition;
    public GameObject fireball;

    public GameObject flame;
    public GameObject skele;

    public TextMeshProUGUI pointsTxt;
    public TextMeshProUGUI timeTxt;
    public TextMeshProUGUI scoreTxt;
    public GameObject menuBtn;


    private float hp = 100f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        World.player = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        vInput = Input.GetAxisRaw("Vertical");
        hInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetMouseButtonDown(0))
        {
            ChargeUp();
        }
        else if (Input.GetMouseButton(0))
        {
            if (charge >= 1.4f)
            {
                Instantiate(skele, transform.position, transform.rotation);
                Die();
            }
            charge += Time.deltaTime;
            slider.value += Time.deltaTime;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            Attack();
        }

        if (Input.GetKeyDown(KeyCode.Space) && grounded == true)
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        gTimer += Time.fixedDeltaTime;
        Vector3 moveForce = new Vector3(hInput, 0, vInput).normalized;
        if (moveForce.magnitude > .5f)
        {
            rb.AddForce(moveForce * moveSpeed * Time.fixedDeltaTime);
            playerAnim.SetBool("Moving", true);
        }
        else
        {
            playerAnim.SetBool("Moving", false);
        }

        if (Mathf.Abs(transform.position.y - prevHeight) <= .01f)
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }
        prevHeight = transform.position.y;

        if (transform.position.y <= -4f)
        {
            Die();
        }
    }

    private void ChargeUp()
    {
        staffAnim.ResetTrigger("Attack");
        staffAnim.SetTrigger("ChargeUp");
    }

    private void Attack()
    {
        staffAnim.ResetTrigger("ChargeUp");
        staffAnim.SetTrigger("Attack");
        
        Plane tempPlane = new Plane(Vector3.up, 0);
        float distance;
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (tempPlane.Raycast(camRay, out distance))
        {
            Vector3 clickPosition;
            clickPosition = camRay.GetPoint(distance);
            Vector3 staffPosition = firePosition.position;
            Vector3 targetPosition = new Vector3(clickPosition.x, staffPosition.y, clickPosition.z);
            GameObject newFireball = Instantiate(fireball, staffPosition, Quaternion.identity);
            Vector3 force = (targetPosition - staffPosition).normalized;
            float forceMultiplier = 1200f + (2400f * slider.value);
            newFireball.GetComponent<Rigidbody>().AddForce(force * forceMultiplier);
            newFireball.GetComponent<Fireball>().damage = 10 + (120 * slider.value);
        }

        slider.value = 0;
        charge = 0;
    }

    public void AddPoints(int p)
    {
        points += p;
    }

    private void Die()
    {
        Instantiate(flame, transform.position, transform.rotation);
        menuBtn.SetActive(true);
        Destroy(gameObject);
    }

    private void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GG"))
        {
            pointsTxt.text = points.ToString();
            TimeSpan formattedTime = TimeSpan.FromSeconds(gTimer);
            timeTxt.text = formattedTime.ToString();
            int newScore = (int)((points * 500f) / gTimer);
            scoreTxt.text = newScore.ToString();
            menuBtn.SetActive(true);
            this.enabled = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("EnemyFireball"))
        {
            hp -= 10;
            Destroy(collision.gameObject);
            if (hp <= 0)
            {
                Die();
            }
        }
    }
}
