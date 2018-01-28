using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterLimitLabel : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (!interestedField.isActiveAndEnabled)
			GetComponent<Text>().text = "";
	}

	public InputField interestedField;
	public void InputUpdated(string newStuff) {
		GetComponent<Text>().text = string.Format("{0}/{1}", newStuff.Length, interestedField.characterLimit);
	}
}
