using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Sentences
{

	public string name;

	public Image profile;

	[TextArea(3, 10)]
	public string kalimat;

}
