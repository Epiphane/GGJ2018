using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextInputBlinker : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	public int FRAMES_PER_BLINK = 30;
	public int framesTillNextBlink = 0;

	bool shown = true;

	void Update () {
		if (framesTillNextBlink-- < 0) {
			framesTillNextBlink = FRAMES_PER_BLINK;

			shown = !shown;

			var color = GetComponent<Image> ().color;
			if (shown) {
				color.a = 1.0f;
			} else {
				color.a = 0.0f;	
			}
			GetComponent<Image> ().color = color;
		}
	}

	public float character_size = 23.0f;

	public Text inputText;

	public void TextFieldUpdated(string newStuff) {
		TextGenerator textGen = new TextGenerator();
		TextGenerationSettings generationSettings = inputText.GetGenerationSettings(inputText.rectTransform.rect.size); 
		float width = textGen.GetPreferredWidth(newStuff, generationSettings);
		//float height = textGen.GetPreferredHeight(newStuff, generationSettings);

		RectTransform rectTransform = (RectTransform)transform;
		Vector2 pos = rectTransform.anchoredPosition;
		pos.x = width;
		rectTransform.anchoredPosition = pos;
	}
}
