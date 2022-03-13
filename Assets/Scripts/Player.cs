using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody rb;
    private float vInput;
    private float hInput;

    private float prevHeight = 0;
    private bool grounded = true;

    public float moveSpeed;
    public float jumpForce;
    public Animator playerAnim;
    public Animator staffAnim;

    public Transform firePosition;
    public GameObject fireball;
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
            newFireball.GetComponent<Rigidbody>().AddForce(force * 2400f);
        }


        

    }

    private void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce);
    }
}
