using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenEnemy : MonoBehaviour
{
    [SerializeField] private Transform chekcGround;
    [SerializeField] private LayerMask whatIsGround;
    public float groundRadius = 2f;
    public float speed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        transform.Translate(Vector3.right * Time.deltaTime * speed);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(chekcGround.position, groundRadius, whatIsGround);
        if (colliders.Length == 0)
        {
            transform.Rotate(Vector3.forward, -90);
            transform.Translate(1, 0.05f, 0);
           
        }
       
    }
}
