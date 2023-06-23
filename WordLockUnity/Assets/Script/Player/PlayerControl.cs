﻿using System.Collections;
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

    public Vector2 kerisReady;

    Vector2 kerisOffset;

    public float damageFlyingKeris, damageFire, damageKomet;

    public AudioClip healingSound, kerisMelayang, kerisKenaBoss, kometSound;

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
        SoundManager.soundManager.PlayAudio(kerisKenaBoss);
        PlayerUI.playerUI.UpdateHealth(health);
        if (health <= 0) LevelManager.levelManager.GameOver();
    }


    public void UpdateMana(int mana)
    {
        if ((manaLeft - mana) > manaPlayer) return;
        manaLeft -= mana;
        PlayerUI.playerUI.UpdateMana(manaLeft, manaPlayer);
    }
    public void TakeHealiing(float heal)
    {
        if (manaLeft < 8) return;
        UpdateMana(8);
        health += heal;
        if (health > maxHealth) health = maxHealth;
        PlayerUI.playerUI.UpdateHealth(health);
        SoundManager.soundManager.PlayAudio(healingSound);
    }

    public void FlyingKeris()
    {
        StartCoroutine("KerisFly");
        
    }

    IEnumerator KerisFly()
    {
        Vector2 hitLand = boss.transform.position; kerisOffset = keris.transform.position;
        Rigidbody2D rb = keris.GetComponent<Rigidbody2D>();
        float time = 0, currentRot = 0;

        while (time < 1f)
        {
            keris.transform.position = Vector2.Lerp(kerisOffset, kerisOffset + kerisReady, time / 1f);
            time += Time.deltaTime;
            yield return null;
        }
        float rotWeapon = Mathf.Atan2(hitLand.y - keris.transform.position.y, hitLand.x - keris.transform.position.x) * Mathf.Rad2Deg - 90;

        SoundManager.soundManager.PlayAudio(kerisMelayang);
        time = 0;
        while (time < 0.5f)
        {
           
            if(time < 0.3)
            {
                keris.transform.position = Vector2.Lerp(kerisOffset + kerisReady, hitLand, time / 0.3f);
                currentRot = Mathf.Lerp(0, rotWeapon, time / 0.3f);
                rb.rotation = currentRot;
            }
            time += Time.deltaTime;
            yield return null;
        }

        SoundManager.soundManager.PlayAudio(kerisKenaBoss);

        rotWeapon = Mathf.Atan2(keris.transform.position.y - hitLand.y, keris.transform.position.x - hitLand.x) * Mathf.Rad2Deg - 90;
        AttackBoss(Random.Range(damageFlyingKeris, damageFlyingKeris + 100));
        time = 0;

        while (time < 0.3f)
        {
            keris.transform.position = Vector2.Lerp(hitLand, kerisOffset, time / 0.3f);
            currentRot = Mathf.Lerp(rotWeapon, 0, time / 0.3f);
            rb.rotation = currentRot;

            time += Time.deltaTime;
            yield return null;
        }
    }

    public void TouchOfFire()
    {
        if (manaLeft < 4) return;
        UpdateMana(4);
        AttackBoss(Random.Range(damageFire, damageFire + 100));
    }

    public void Komet()
    {
        if (manaLeft < 10) return;
        SoundManager.soundManager.PlayAudio(kometSound);
        UpdateMana(10);
        AttackBoss(Random.Range(damageKomet, damageKomet + 100));
    }
    void AttackBoss(float damage)
    {
        //Ini cacad
        if (boss.name == "TuyulWarboss")
            boss.GetComponent<TuyulWarboss>().TakeDamage(damage);
        else if (boss.name == "TilanakQueen")
            boss.GetComponent<TilanakQueen>().TakeDamage(damage);
    }
}
