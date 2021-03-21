using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aura : MonoBehaviour
{
    public float moveSpeed, delayMove, damageWeapon, bulletLifetime = 5f;
    public Rigidbody2D rb;
    public LayerMask bounceMask;
    public Vector3 direction;
    Collider2D col;

    float timeSpent = 0f;
    void Start()
    {
        StartCoroutine(GoAttack());
        col = GetComponent<Collider2D>();
        direction = transform.up;
    }

    // Update is called once per frame
    void Update()
    {
        timeSpent += Time.deltaTime;
        if(timeSpent >= bulletLifetime + delayMove)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator GoAttack()
    {
        yield return new WaitForSeconds(delayMove);
        rb.velocity = moveSpeed * direction;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerControl>().TakeDamage(damageWeapon);
            Destroy(gameObject);
        }   
    }

}
