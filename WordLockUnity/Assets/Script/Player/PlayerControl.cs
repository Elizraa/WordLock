using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameState;

public class PlayerControl : MonoBehaviour
{

    public float health;
    public int manaPlayer;
    int manaLeft;

    [HideInInspector]
    public bool invul;

    public GameObject boss;

    // Start is called before the first frame update
    void Start()
    {
        PlayerUI.playerUI.SetMaxHealth(health);
        PlayerUI.playerUI.SetMaxMana(manaPlayer);
        manaLeft = manaPlayer;
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
    }

    public void TakeHealiing(float heal)
    {
        health += heal;
        PlayerUI.playerUI.UpdateHealth(health);
    }

    public void UpdateMana(int mana)
    {
        if((manaLeft - mana) > manaPlayer)
        manaLeft -= mana;
        PlayerUI.playerUI.UpdateMana(manaLeft, manaPlayer);
    }
}
