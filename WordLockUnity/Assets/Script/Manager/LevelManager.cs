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

    private void Awake()
    {
        if (levelManager == null)
            levelManager = this;
    }

    private void Start()
    {
        sceneState = SceneState.Talking;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartTalking(int index)
    {
        DialoguePhase[index].TriggerDialogue();
    }
}
