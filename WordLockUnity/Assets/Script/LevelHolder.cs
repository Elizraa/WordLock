﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelHolder : MonoBehaviour
{
    public Button[] level;

    // Start is called before the first frame update
    void Start()
    {
        UnlockLevel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoToLevel(int index)
    {
        ScreenFader.screenFader.ChangeScene(index);
    }

    void UnlockLevel()
    {
        for(int i = 1; i < level.Length; i++)
        {
            if (PlayerPrefs.HasKey("Level" + i))
            {
                level[i].interactable = true;
            }
            else
                break;
        }
    }
}
