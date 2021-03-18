using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Dialogue
{

	[Header("Kalo atas bawah yang ngomong orangnya sama ", order = 0)]
	[Space(-10, order = 1)]
	[Header("Gak usah diisi imagenya", order = 2)]
	[Space(-10, order = 3)]
	[Header("Cukup yang paling atas aja.", order = 4)]
	[Space(10, order = 5)]

	public GameState.SceneState stateAfterThis;
	public Sentences[] sentences;

}
