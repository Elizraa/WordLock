using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public static PlayerUI playerUI;

    public Slider health, currentEnergy;

    public Text healthLeft, manaLeft, spellNeeded;

    public GameObject[] spellBox;
    public string[] spellText;

    public InputField spell;
    public GameObject page1, page2;

    int currentSpell;
    bool canChange = true;
    PlayerControl player;

    private void Awake()
    {
        if (playerUI == null)
            playerUI = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentSpell = 0;
        player = GetComponent<PlayerControl>();
    }

    // Update is called once per frame
    void Update()
    {
        if (LevelManager.levelManager.sceneState == GameState.SceneState.SpellWrite)
        {
            spell.Select();
            spell.ActivateInputField();
        }
        GetCurrentSpell();

    }

    public void SetMaxHealth(float maxHealth)
    {
        health.maxValue = maxHealth;
        health.value = maxHealth;
        healthLeft.text = maxHealth + " / " + maxHealth;
    }

    public void UpdateHealth(float currentHealth)
    {
        health.value = currentHealth;
        healthLeft.text = currentHealth + " / " + health.maxValue;
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

    public void SetMaxMana(int maxMana)
    {
        manaLeft.text = "Mana : " + maxMana + " / " + maxMana;
    }

    public void UpdateMana(int mana, int maxMana)
    {
        manaLeft.text = "Mana : " + mana + " / " + maxMana;
    }

    void GetCurrentSpell()
    {
        if (!canChange) return;
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            spellBox[currentSpell].SetActive(false);
            currentSpell = (currentSpell - 2 >= 0 ? currentSpell - 2 : currentSpell);
            spellBox[currentSpell].SetActive(true);
            spellNeeded.text = spellText[currentSpell];
            if(currentSpell < 2)
            {
                page1.SetActive(true);
                page2.SetActive(false);
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            spellBox[currentSpell].SetActive(false);
            currentSpell = (currentSpell + 2 <= 3 ? currentSpell + 2 : currentSpell);
            spellBox[currentSpell].SetActive(true);
            spellNeeded.text = spellText[currentSpell];
            if (currentSpell > 1)
            {
                page1.SetActive(false);
                page2.SetActive(true);
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            spellBox[currentSpell].SetActive(false);
            currentSpell = (currentSpell % 2 == 0 ? currentSpell : currentSpell - 1);
            spellBox[currentSpell].SetActive(true);
            spellNeeded.text = spellText[currentSpell];
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            spellBox[currentSpell].SetActive(false);
            currentSpell = (currentSpell % 2 == 1 ? currentSpell : currentSpell + 1);
            spellBox[currentSpell].SetActive(true);
            spellNeeded.text = spellText[currentSpell];
        }
    }

    public void OnValueChange(string input)
    {
        if(input == "")
        {
            canChange = true;
            return;
        }
        canChange = false;
        if (input.Length <= spellNeeded.text.Length && input == spellNeeded.text.Substring(0, input.Length))
        {
            spellNeeded.color = Color.white;
        }
        else
            spellNeeded.color = Color.red;
    }

    public void DoneTyping(string input)
    {
        if(input == spellNeeded.text)
        {
            switch (currentSpell)
            {
                case 0:
                    player.FlyingKeris();
                    break;
                case 1:
                    player.TouchOfFire();
                    break;
                case 2:
                    player.TakeHealiing(5);
                    break;
                case 3:
                    player.Komet();
                    break;
            }
        }
        canChange = true;
        spell.text = "";
    }

}
