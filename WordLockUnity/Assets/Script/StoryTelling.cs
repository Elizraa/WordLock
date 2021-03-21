using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryTelling : MonoBehaviour
{
    public GameObject canvasStory;
    public AudioSource mainMenu;
    public AnimationClip story;

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("FirstOpened"))
        {
            PlayerPrefs.SetInt("FirstOpened", 1);
            StartStory();
        }
        else
            canvasStory.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StartStory()
    {
        mainMenu.Stop();
        canvasStory.SetActive(true);
        Invoke("StartBGM", story.length);
    }

    public void StartButton()
    {
        ScreenFader.screenFader.ChangeScene(0);
        PlayerPrefs.DeleteKey("FirstOpened");
    }

    void StartBGM()
    {
        mainMenu.Play();
    }

}
