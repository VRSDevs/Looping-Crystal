using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowEnemy : MonoBehaviour
{
    public Transform target;
    public Transform shootPos;
    public float shootSpeed;
    public float range;
    public float reload;
    public GameObject projectile;


    private float distToTarget;
    private Vector2 shootDir;
    private Vector2 shootVec;
    private bool canShoot = true;
    private bool facingRight = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        distToTarget = Vector2.Distance(target.position, transform.position);
        

        if(distToTarget <= range)
        {
            shootDir = (target.position - transform.position);
            shootVec = shootDir.normalized * shootSpeed * Time.fixedDeltaTime;
            if(shootDir.x < 0 && facingRight)
            {
                Flip();
            }
            else if(shootDir.x > 0 && !facingRight)
            {
                Flip();
            }
            if (canShoot)
            {
                StartCoroutine(Shoot());
            }
        }
    }

    private IEnumerator Shoot()
    {
        canShoot = false;
        yield return new WaitForSeconds(reload);
        GameObject newProjectile = Instantiate(projectile, shootPos.position, Quaternion.identity);
        newProjectile.GetComponent<Rigidbody2D>().velocity = shootVec;

        canShoot = true;
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 newScale = transform.localScale;
        newScale.x *= -1;
        transform.localScale = newScale;
    }
}
