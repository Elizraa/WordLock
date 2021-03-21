using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathFromBelow : MonoBehaviour
{
    public float indicateTime = 1, damageWeapon, lifeDuration, fade;

    public SpriteRenderer sp;
    public Collider2D colCheck;
    public GameObject trueSprite;


    float timeSpent = 0;
    bool damaging = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("FadeOut", sp);
    }

    // Update is called once per frame
    void Update()
    {
        

        timeSpent += Time.deltaTime;
        if(timeSpent >= indicateTime && colCheck.enabled == false)
        {
            colCheck.enabled = true;
            trueSprite.SetActive(true);
        }
        if(timeSpent >= lifeDuration)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !damaging)
        {
            collision.GetComponent<PlayerControl>().TakeDamage(damageWeapon);
            damaging = true;
            
        }
    }

    IEnumerator FadeOut(SpriteRenderer spr)
    {
        for(float f = 1f; f >= -0.2f; f -= 0.05f)
        {
            Color c = spr.material.color;
            c.a = f;
            sp.material.color = c;
            yield return new WaitForSeconds(0.05f);
        }
    }

}
