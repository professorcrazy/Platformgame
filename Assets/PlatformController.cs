using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    public float speed = 5f;
    private float inputX;
    Rigidbody2D rb;
    //Dette er en kommentar
    [SerializeField] private bool facingRight = true;
    [SerializeField] private float jumpForce = 5f;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private float checkRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);
        inputX = Input.GetAxisRaw("Horizontal");
        if(inputX < 0 && facingRight)
        {
            Flip();
        }
        if(inputX > 0 && !facingRight)
        {
            Flip();
        }
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(inputX * speed, rb.velocity.y);
    }

    void Flip()
    {
        Vector3 tempScale = transform.localScale;
        tempScale.x *= -1f;
        transform.localScale = tempScale;
        facingRight = !facingRight;
    }

}
