using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public static PlayerUI playerUI;

    public Slider health, currentEnergy;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMaxHealth(float maxHealth)
    {
        health.maxValue = maxHealth;
        health.value = maxHealth;
    }

    public void UpdateHealth(float currentHealth)
    {
        health.value = currentHealth;
        //if 0 gameOver
    }

    public void SetMaxEnergy(float maxEnergy)
    {
        currentEnergy.maxValue = maxEnergy;
        currentEnergy.value = maxEnergy;
    }

    public void UpdateEnergy(float energy)
    {
        currentEnergy.value = energy;
    }
}
