using UnityEngine;
namespace premade {
    public class PlayerController : MonoBehaviour {

        private Rigidbody2D rb;
        public float speed = 3f;
        private float inputX;
        [SerializeField] float jumpForce = 5f;
        bool facingRight = true;
        [SerializeField] private Transform groundCheck;
        [SerializeField] private float checkRadius = 0.2f;
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private bool isGrounded;

        bool isJumping;
        [SerializeField] float jumpTime = 0.5f;
        float jumpTimeLeft;

        [SerializeField] private float coyoteTime = 0.2f;
        float coyoteTimeLeft;
        [SerializeField] float jumpBufferTime = 0.2f;
        [SerializeField] float jumpBufferCounter;

        [SerializeField] ParticleSystem jumpEffect;


        private void Start() {
            rb = GetComponent<Rigidbody2D>();
        }

        private void Update() {
            inputX = Input.GetAxisRaw("Horizontal");
            if (inputX < 0 && facingRight)
            {
                Flip();
            }
            if (inputX > 0 && !facingRight)
            {
                Flip();
            }

            if (isGrounded) {
                coyoteTimeLeft = coyoteTime;
            }
            else { 
                coyoteTimeLeft -= Time.deltaTime;
            }

            if (Input.GetButtonDown("Jump")) {
                jumpBufferCounter = jumpBufferTime;
            }
            else {
                jumpBufferCounter -= Time.deltaTime;
            }

            if (jumpBufferCounter > 0f && coyoteTimeLeft > 0f) {
                
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                jumpEffect.Play();
                isJumping = true;
                jumpTimeLeft = jumpTime;
                coyoteTimeLeft = 0;
                jumpBufferCounter = 0;
                
            }

            if (Input.GetButton("Jump") && isJumping) {
                if (jumpTimeLeft > 0) {
                    jumpTimeLeft -= Time.deltaTime;
                    rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                }
            }
            if (Input.GetButtonUp("Jump")) {
                isJumping = false;
            }
        }

        private void FixedUpdate() {
            rb.velocity = new Vector2(inputX * speed, rb.velocity.y);

            isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);
            if (rb.velocity.y < 0f) {
                rb.velocity += Vector2.up * (Physics2D.gravity.y * 1.5f * Time.fixedDeltaTime);
            }
        }

        void Flip() {
            facingRight = !facingRight;
            Vector3 tempScale = transform.localScale;
            tempScale.x *= -1;
            transform.localScale = tempScale;
        }

    }
}