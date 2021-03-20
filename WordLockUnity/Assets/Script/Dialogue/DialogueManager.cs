using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

	public Text nameText, dialogueText;
	public Image profile;

	public Animator animator;

	private Queue<Sentences> sentences;

	public float spawnLetter;
	bool dialogueBegin = false;

	GameState.SceneState nextState;

	// Use this for initialization
	void Start()
	{
		sentences = new Queue<Sentences>();
	}
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && dialogueBegin)
        {
			DisplayNextSentence();
        }

	}
    public void StartDialogue(Dialogue dialogue)
	{
		dialogueBegin = true;
		animator.SetBool("IsOpen", true);


		sentences.Clear();
		nextState = dialogue.stateAfterThis;

		foreach (Sentences sentence in dialogue.sentences)
		{
			sentences.Enqueue(sentence);
		}

		DisplayNextSentence();
	}

	public void DisplayNextSentence()
	{
		if (sentences.Count == 0)
		{
			EndDialogue();
			return;
		}

		Sentences sentence = sentences.Dequeue();
		string kalimat = sentence.kalimat;
		if (nameText.text != sentence.name)
		{
			nameText.text = sentence.name;
			profile.sprite = sentence.profile;

		}
		StopAllCoroutines();
		StartCoroutine(TypeSentence(kalimat));
	}

	IEnumerator TypeSentence(string sentence)
	{
		dialogueText.text = "";
		foreach (char letter in sentence.ToCharArray())
		{
			dialogueText.text += letter;
			yield return new WaitForSeconds(spawnLetter);
		}
	}

	void EndDialogue()
	{
		dialogueBegin = false;
		animator.SetBool("IsOpen", false);
		LevelManager.levelManager.SetState(nextState);
	}

}
