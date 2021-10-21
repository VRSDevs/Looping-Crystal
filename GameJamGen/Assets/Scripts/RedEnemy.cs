using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedEnemy : MonoBehaviour
{
    public Transform target;
    public float triggerRange;
    public float speed;

    private float distance;
    private Vector2 direction;
    private Vector2 moveVec;
    private Rigidbody2D rb;
    private bool facingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position, target.position);
        if(distance <= triggerRange)
        {
            direction = target.position - transform.position;

            moveVec = direction.normalized * speed * Time.fixedDeltaTime;
            rb.velocity = moveVec;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }

        if (moveVec.x < 0 && facingRight)
        {
            Flip();
        }
        else if (moveVec.x > 0 && !facingRight)
        {
            Flip();
        }

    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 newScale = transform.localScale;
        newScale.x *= -1;
        transform.localScale = newScale;
    }
}
