﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenFader : MonoBehaviour
{
    public static ScreenFader screenFader;
    public Animator anim;

    private void Awake()
    {
        if (screenFader != null)
            Destroy(gameObject);
        else
        {
            screenFader = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ChangeScene(int index)
    {
        StartCoroutine(LoadScene(index));
    }

    IEnumerator LoadScene(int index)
    {
        Time.timeScale = 1f;
        anim.Play("FadeIn");
        SoundManager.soundManager.FadeOut();
        yield return new WaitForSecondsRealtime(1f);
        SceneManager.LoadScene(index);
        anim.Play("FadeOut");
        SoundManager.soundManager.FadeIn();
    }
}
