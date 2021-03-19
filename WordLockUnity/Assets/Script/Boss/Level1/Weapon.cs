using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    //public Transform target;
    public float moveSpeed, delayMove, damageWeapon;
    public Rigidbody2D rb;

    void Start()
    {
        //FaceTarget();
        StartCoroutine(GoAttack());
    }

    //void FaceTarget()
    //{
    //    Vector3 targ = target.position;
    //    targ.z = 0f;

    //    Vector3 objectPos = transform.position;
    //    targ.x = targ.x - objectPos.x;
    //    targ.y = targ.y - objectPos.y;

    //    float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
    //    transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
    //}

    IEnumerator GoAttack()
    {
        yield return new WaitForSeconds(delayMove);
        rb.velocity = moveSpeed * transform.up;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerControl>().TakeDamage(damageWeapon);
        }
        else if (collision.name == "ObjectDestroyer")
            Destroy(gameObject);
    }
}

