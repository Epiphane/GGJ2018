using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TooltipPlacer : MonoBehaviour
{

    public GameObject attachedObject;

    public Text myWords;

	// How many ticks has it been since we started the text garble animation
	public int textAnimTicks;
	// How many ticks should we wait to start the text garble animation
	public int textAnimInitialDelay = 0;
	// How many ticks should it take for text to garble in
	private int textAnimLength = 20;
	// how many frames per anim tick
	public int textAnimTickFrames = 0;
	// frames till next tick
	public int frames = 0;

	// Currently displayed garbled string
	public string garbledMessage;
	// What we want to garble to
	public string garbledTarget;

	// Persistent, actual message from server
	public string message;

    // Late update, since apparently changing the rect size during animation messes with the position of my precious rectangles
    void LateUpdate()
    {
        RectTransform my_transform = (RectTransform)transform;
        my_transform.position = Camera.main.WorldToScreenPoint(attachedObject.transform.position);
    }

	void Update()
	{
		if (textAnimInitialDelay > 0) {
			textAnimInitialDelay--;
			return;
		}

		// Garblin' garblin' garblin'
		if (--frames < 0  && garbledTarget.Length > 0) {
			// It's garblin' time
			frames = textAnimTickFrames;

			float percent_progress = (float) textAnimTicks / (float) textAnimLength;

			// Chance to get correct character = percent_progress ^ 5
			float chance_correct = Mathf.Pow(percent_progress, 5.0f) + 0.3f;

			// Choose a random character to turn into either the wrong character, or the right character
			int curr_char_index = Random.Range(0, garbledMessage.Length);
			char replacing_char = ' ';

			if (chance_correct > Random.Range (0.0f, 1.0f)) {
				// correct
				replacing_char = garbledTarget[curr_char_index];
			} else {
				// random
				replacing_char = (char)('A' + Random.Range (0,61));
			}

			int after_char_index = curr_char_index + 1;

			string post_string = "";

			if (after_char_index < garbledMessage.Length)
				post_string = garbledMessage.Substring(after_char_index);

			garbledMessage = garbledMessage.Substring (0, curr_char_index) + replacing_char + post_string;

			myWords.text = garbledMessage;

			textAnimTicks++;
		}
	}

    public void SetText(string words)
    {
		message = words;
    }

	public void OpenTooltip()
	{
		if (GetComponent<Animator>().GetBool("tooltipOpen") == false)
			StartAnimatingGarble ("Incoming transmission...", message, 80);
		
		GetComponent<Animator> ().SetBool ("tooltipOpen", true);
	}

	public void CloseTooltip()
	{
		if (GetComponent<Animator>().GetBool("tooltipOpen") == true)
			StartAnimatingGarble (message, "", 0);
		
		GetComponent<Animator> ().SetBool ("tooltipOpen", false);
	}

	public void StartAnimatingGarble(string start, string destination, int start_delay)
	{
		textAnimTicks = 0;
		textAnimInitialDelay = start_delay;

		// Pad out the shorter string with spaces
		while (start.Length < destination.Length) {
			start += " ";
		}

		while (destination.Length < start.Length) {
			destination += " ";
		}

		myWords.text = start;
		garbledMessage = start;
		garbledTarget = destination;
	}
}
