using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameState;

public class LevelManager : MonoBehaviour
{
    public static LevelManager levelManager;
    public SceneState sceneState;
    public DialogueTrigger[] DialoguePhase;

    private void Awake()
    {
        if (levelManager == null)
            levelManager = this;
    }

    private void Start()
    {
        sceneState = SceneState.Talking;
        StartTalking(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartTalking(int index)
    {
        DialoguePhase[0].TriggerDialogue();
    }
}
