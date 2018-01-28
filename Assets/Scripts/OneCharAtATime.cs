using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OneCharAtATime : MonoBehaviour {


	int FRAMES_PER_TICK = 1;
	int FRAMES_PER_PAUSE = 20;
	int framesToNextTick = 0;
	int charactersRevealed = 0;

	// text thus far
	string revealedMessage = "";
	string finalMessage = "";
	
	// Update is called once per frame
	void Update () {
		if (Input.anyKeyDown) {
			revealedMessage = finalMessage;
			GetComponent<Text> ().text = revealedMessage;
			FinishedText ();
		}

		if (finalMessage.Length > 0 && !revealedMessage.Equals(finalMessage) && framesToNextTick-- < 0) {
			framesToNextTick = FRAMES_PER_TICK;

			char char_to_reveal = finalMessage [charactersRevealed++];
			revealedMessage += char_to_reveal;

			if (char_to_reveal == '\n')
				framesToNextTick = FRAMES_PER_PAUSE;

			GetComponent<Text> ().text = revealedMessage;

			if (finalMessage == revealedMessage)
				FinishedText ();
		}
	}

	public InputField textInput;
	void FinishedText()
	{
		textInput.gameObject.SetActive (true);
		textInput.Select();
		textInput.ActivateInputField();
	}

	public void ShowTextIncrementally(string message)
	{
		revealedMessage = "";
		finalMessage = message;
		charactersRevealed = 0;
	}

	public void DoneForNow()
	{
		GetComponent<Text> ().text = "";
		finalMessage = "";
		revealedMessage = "";
	}
}
