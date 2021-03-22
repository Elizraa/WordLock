using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryTelling : MonoBehaviour
{
    public Animator anim;
    public GameObject infoPanel;
    private void Awake()
    {
        if (!PlayerPrefs.HasKey("FirstOpened"))
        {
            PlayerPrefs.SetInt("FirstOpened", 1);
            StartStory();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        infoPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StartStory()
    {
        anim.Play("StoryTelling");
    }

    public void StartButton()
    {
        ScreenFader.screenFader.ChangeScene(0);
        PlayerPrefs.DeleteKey("FirstOpened");
    }

    public void InfoButton()
    {
        infoPanel.SetActive(true);
    }

    public void CloseInfoButton()
    {
        infoPanel.SetActive(false);
    }

}
