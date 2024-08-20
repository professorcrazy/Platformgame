using UnityEngine;
namespace premade {
    public class PlayerController : MonoBehaviour
    {
        private Rigidbody2D rb;
        public float speed = 3f;
        private float inputX;
        [SerializeField] float jumpForce = 5f;
        bool facingRight = true;
        [SerializeField] private Transform groundCheck;
        [SerializeField] private float checkRadius = 0.2f;
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private bool isGrounded;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            inputX = Input.GetAxisRaw("Horizontal");
            if (inputX < 0 && facingRight)
            {
                Flip();
            }
            if (inputX > 0 && !facingRight)
            {
                Flip();
            }

            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                Jump();
            }
        }

        private void FixedUpdate()
        {
            rb.velocity = new Vector2(inputX * speed, rb.velocity.y);

            isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);
        }

        void Jump()
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        void Flip()
        {
            facingRight = !facingRight;
            Vector3 tempScale = transform.localScale;
            tempScale.x *= -1;
            transform.localScale = tempScale;
        }

    }
}