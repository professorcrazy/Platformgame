using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    public float speed = 5f; //float variable to controll the speed, public so it can be modified in the inspector or from powerups
    private float inputX;    //the stored value of you input

    Rigidbody2D rb; //our reference to the physics system
    [SerializeField] private bool facingRight = true; //storing our current direction (looking left or right)

    //the variables we use for jumping and to make sure we are on the ground.
    [SerializeField] private float jumpForce = 5f; //how much force we use when jumping
    [SerializeField] private Transform groundCheck; 
    [SerializeField] private float checkRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer; //the layer the gameobject is on in order for us to jump.
    [SerializeField] private bool isGrounded; //are we on the ground

    //to make our jump better we introduced 
    private bool isJumping;
    [SerializeField] private float jumpTime = 0.5f;
    private float jumpTimeCounter;

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);
        inputX = Input.GetAxisRaw("Horizontal");
        if(inputX < 0 && facingRight){
            Flip();
        }
        if(inputX > 0 && !facingRight){
            Flip();
        }
        //når knappen bliver trykket ned
        if (Input.GetButtonDown("Jump") && isGrounded){
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            //rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isJumping = true;
            isGrounded = false;
            jumpTimeCounter = jumpTime;
        }
        if (Input.GetButton("Jump") && isJumping) {
            if (jumpTimeCounter > 0) {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                jumpTimeCounter -= Time.deltaTime;
            }
        }
        if (Input.GetButtonUp("Jump")) {
            isJumping = false;
        }

        anim.SetFloat("Speed", Mathf.Abs(inputX));

    }

    

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.transform.position, checkRadius, groundLayer);
        rb.velocity = new Vector2(inputX * speed, rb.velocity.y);
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * (Physics2D.gravity * 1.5f * Time.fixedDeltaTime);
        }
    }

    void Flip()
    {
        Vector3 tempScale = transform.localScale;
        tempScale.x *= -1f;
        transform.localScale = tempScale;
        facingRight = !facingRight;
    }

}
