using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideCheck : MonoBehaviour
{

    public Aura aura;
    Vector3 lastVelocity;
    // Start is called before the first frame update
    void Start()
    {
        aura = GetComponentInParent<Aura>();
    }

    // Update is called once per frame
    void Update()
    {
        lastVelocity = aura.rb.velocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Wall"))
            return;
        var speed = lastVelocity.magnitude;
        Vector3 wallNormal = collision.contacts[0].normal;
        aura.direction = Vector3.Reflect(lastVelocity.normalized, wallNormal);
        aura.rb.velocity = aura.direction * Mathf.Max(speed, 0f);
        
    }
}
