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

	// Whether or not we prompt for user input after this
	public bool shouldShowInput;
	
	// Update is called once per frame
	void Update () {
		if (Input.anyKeyDown && finalMessage.Length > 0) {
			revealedMessage = finalMessage;
			GetComponent<Text> ().text = revealedMessage;
			ShowInput ();
		}

		// Showing regular message. Close on enter press.
		if (!shouldShowInput && Input.GetKeyDown(KeyCode.Return) && revealedMessage == finalMessage) {
			DoneForNow ();
			FindObjectOfType<DeathHandler> ().ChickenOut ();
		}

		if (finalMessage.Length > 0 && !revealedMessage.Equals(finalMessage) && --framesToNextTick < 0) {
			framesToNextTick = FRAMES_PER_TICK;

			char char_to_reveal = finalMessage [charactersRevealed++];
			revealedMessage += char_to_reveal;

			if (char_to_reveal == '\n')
				framesToNextTick = FRAMES_PER_PAUSE;

			if (char_to_reveal == '.')
				framesToNextTick = FRAMES_PER_PAUSE / 2;

			GetComponent<Text> ().text = revealedMessage;

			if (finalMessage == revealedMessage)
				ShowInput ();
		}
	}

	public InputField textInput;
	void ShowInput()
	{
		if (!shouldShowInput)
			return;
		
		textInput.gameObject.SetActive (true);
		textInput.Select();
		textInput.ActivateInputField();
	}

	public void ShowTextIncrementally(string message, bool shouldShowInput)
	{
		this.shouldShowInput = shouldShowInput;
		revealedMessage = "";
		finalMessage = message;
		charactersRevealed = 0;
	}

	public void DoneForNow()
	{
		GetComponent<Text> ().text = "";
		finalMessage = "";
		revealedMessage = "";

		textInput.gameObject.SetActive (false);
	}
}
