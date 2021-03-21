using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameState;

public class PlayerControl : MonoBehaviour
{

    public float health;
    public int manaPlayer;
    int manaLeft;
    float maxHealth;

    [HideInInspector]
    public bool invul;

    public GameObject boss, keris;

    public float damageFlyingKeris, damageFire, damageKomet;

    // Start is called before the first frame update
    void Start()
    {
        PlayerUI.playerUI.SetMaxHealth(health);
        PlayerUI.playerUI.SetMaxMana(manaPlayer);
        manaLeft = manaPlayer;
        maxHealth = health;
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
        PlayerUI.playerUI.UpdateHealth(health);
        if (health <= 0) LevelManager.levelManager.GameOver();
    }


    public void UpdateMana(int mana)
    {
        if((manaLeft - mana) > manaPlayer)
        manaLeft -= mana;
        PlayerUI.playerUI.UpdateMana(manaLeft, manaPlayer);
    }
    public void TakeHealiing(float heal)
    {
        health += heal;
        if (health > maxHealth) health = maxHealth;
        PlayerUI.playerUI.UpdateHealth(health);
    }

    void FlyingKeris()
    {
       
    }

    void TouchOfFire()
    {

    }

    void Komet()
    {

    }
}
