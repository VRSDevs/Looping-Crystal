using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public float timeTillDeath;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DeathCountDown());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator DeathCountDown()
    {
        yield return new WaitForSeconds(timeTillDeath);
        Die();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Die();
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
