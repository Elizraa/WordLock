using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossUI : MonoBehaviour
{
    public static BossUI bossUI;

    public Slider health, currentMana;

    public Image fillMana;
    public Gradient gradientMana;

    private void Awake()
    {
        if (bossUI == null)
            bossUI = this;
    }

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
        //if 0 win
    }

    public void SetMaxMana(float maxEnergy)
    {
        currentMana.maxValue = maxEnergy;
        currentMana.value = maxEnergy;
        fillMana.color = gradientMana.Evaluate(1f);
    }

    public void UpdateMana(float energy)
    {
        currentMana.value = energy;
        fillMana.color = gradientMana.Evaluate(currentMana.normalizedValue);
    }
}
