using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public static SoundManager soundManager;

    public AudioSource mainAudio;
    public AudioSource mainBGM;

    public float duration;

    private void Awake()
    {
        if (soundManager != null)
            Destroy(gameObject);
        else
        {
            soundManager = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void PlayBGM(AudioClip BGM)
    {
        mainBGM.clip = BGM;
        mainBGM.Play();
    }

    public void PlayAudio(AudioClip sfx)
    {
        mainAudio.PlayOneShot(sfx);
    }

    public void FadeIn()
    {
        StartCoroutine(FadeAudioSource.StartFade(mainBGM, duration, 0.75f));
    }

    public void FadeOut()
    {
        StartCoroutine(FadeAudioSource.StartFade(mainBGM, duration, 0f));
    }
}

public static class FadeAudioSource
{

    public static IEnumerator StartFade(AudioSource audioSource, float duration, float targetVolume)
    {
        float currentTime = 0;
        float start = audioSource.volume;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        yield break;
    }
}
