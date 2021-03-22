using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    //public Transform target;
    public float moveSpeed, delayMove, damageWeapon;
    public Rigidbody2D rb;
    public ParticleSystem particle;
    public Animator anim;

    int tuyulMiniWallCounter = 0;

    public AudioClip melayang, nabrakTembok;

    void Start()
    {
        //FaceTarget();
        if (anim != null) anim.Play("SpawnWeapon");
        StartCoroutine(GoAttack());
    }

    IEnumerator GoAttack()
    {
        yield return new WaitForSeconds(delayMove);
        if(melayang != null)
            SoundManager.soundManager.PlayAudio(melayang);
        rb.velocity = moveSpeed * transform.up;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerControl>().TakeDamage(damageWeapon);
        }
        else if (collision.CompareTag("Wall"))
            StartCoroutine(WallCollision());
    }

    IEnumerator WallCollision()
    {
        if(gameObject.name == "TuyulMini(Clone)")
        {
            if(tuyulMiniWallCounter++ == 0)
            {
                yield break;
            }
            yield return new WaitForSeconds(0.5f);
            Destroy(gameObject);
            yield break;
        }

        SoundManager.soundManager.PlayAudio(nabrakTembok);

        rb.velocity = Vector2.zero;
        particle.Play();
        anim.Play("DisappearWeapon");
        GetComponent<PolygonCollider2D>().enabled = false;
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}

