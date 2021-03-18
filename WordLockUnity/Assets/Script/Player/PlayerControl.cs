using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    public float health;
    public bool invul;
    // Start is called before the first frame update
    void Start()
    {
        //PlayerUI.playerUI.SetMaxHealth(health);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damage)
    {
        if (invul)
            return;
        health -= damage;
        //PlayerUI.playerUI.UpdateHealth(health);
    }

    public void TakeHealiing(float heal)
    {
        health += heal;
        //PlayerUI.playerUI.UpdateHealth(health);
    }
}
