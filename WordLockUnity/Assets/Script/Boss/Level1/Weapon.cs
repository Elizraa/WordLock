using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform positionToMoveTo;
    public float durationMove, delayMove, damageWeapon;

    void Start()
    {
        StartCoroutine(LerpPosition(positionToMoveTo.position, durationMove));
    }

    IEnumerator LerpPosition(Vector3 targetPosition, float duration)
    {
        float time = 0;
        Vector2 startPosition = transform.position;
        yield return new WaitForSeconds(delayMove);
        while (time < duration)
        {
            transform.position = Vector2.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerControl>().TakeDamage(damageWeapon);
        }
    }
}

