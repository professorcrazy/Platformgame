using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform a;
    public Transform b;
    public float speed = 3f;
    Rigidbody2D rb;

    Vector3 target;
    bool targetA = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        transform.position = a.position;
        target = b.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Vector2.Distance(transform.position, target) < 0.1f)
        {
            if (target == a.position) {
                target = b.position;
            }
            else
            {
                target = a.position;
            }
        }
        rb.velocity = (target - transform.position).normalized * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.collider.attachedRigidbody.AddForce((collision.transform.position - transform.position) * 10000f, ForceMode2D.Force);
            Debug.Log(collision.transform.position - transform.position);
        }
    }
}
