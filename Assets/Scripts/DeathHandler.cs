using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathHandler : MonoBehaviour {

	const int IN_GAME = 0;
	const int AWAITING_INPUT = 1;

	public int currState = IN_GAME; // can also mean title screen. Death handler sees ya. Death handler don't care.

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public Vector2 deathPosition;

	public void ShowDeath(Vector2 deathPosition, string reason) {
		this.deathPosition = deathPosition;
//
//		if (deathReason != null)
//			deathReason.text = reason;
//
//		if (message != null) {
//			animationTime = 10;
//		}
//
////		transform.Find("
//		isInputting = true;

		currState = AWAITING_INPUT;

		transform.Find ("DeathMessage").GetComponent<OneCharAtATime> ().ShowTextIncrementally ("CATASTROPHIC SYSTEM FAILURE:\n\n" + reason + "\n\nDRONE UNRECOVERABLE\n\nSEND TRANSMISSION FROM CURRENT LOCATION:");
		transform.Find ("DeathOverlay").GetComponent<Image> ().color = new Color (0.2f, 0.2f, 0.2f, 0.2f);
	}

	public void SubmitDeathMessage(string message) {
		if (currState == AWAITING_INPUT) {
			currState = IN_GAME;
			transform.Find ("InputField").gameObject.SetActive (false);
			GameObject.FindObjectOfType<BackendScript>().ReportDeath(deathPosition, message);

			transform.Find ("DeathOverlay").GetComponent<Image> ().color = new Color (0.2f, 0.2f, 0.2f, 0.0f);
			transform.Find ("DeathMessage").GetComponent<OneCharAtATime> ().DoneForNow ();
		}
	}
}
