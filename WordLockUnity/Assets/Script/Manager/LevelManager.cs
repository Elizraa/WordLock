using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static GameState;

public class LevelManager : MonoBehaviour
{
    public static LevelManager levelManager;
    public SceneState sceneState;
    public DialogueTrigger[] DialoguePhase;
    public Animator animPlayer;

    public GameObject pausePanel, fightPanel, inputBox;

    private void Awake()
    {
        if (levelManager == null)
            levelManager = this;
        pausePanel.SetActive(false);
    }

    private void Start()
    {
        SetState(SceneState.Normal);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale != 0)
                Pause();
            else
                Resume();
        }
    }

    public void StartTalking(int index)
    {
        DialoguePhase[index].TriggerDialogue();
    }

    public void SetState(SceneState state)
    {
        sceneState = state;
        if (state == SceneState.Fighting)
        {
            fightPanel.SetActive(true);
            inputBox.SetActive(false);
        }
        else if(state == SceneState.SpellWrite)
        {
            inputBox.SetActive(true);
        }
        else
            fightPanel.SetActive(false);
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        pausePanel.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
    }

    public void Retry()
    {
        ScreenFader.screenFader.ChangeScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMen()
    {
        ScreenFader.screenFader.ChangeScene(0);
    }
}
