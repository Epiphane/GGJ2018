using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathHandler : MonoBehaviour {

	const int IN_GAME = 0;
	const int AWAITING_INPUT = 1;
	const int CONTEMPLATING_DEATH = 2;

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

		currState = AWAITING_INPUT;

		transform.Find ("DeathMessage").GetComponent<OneCharAtATime> ().ShowTextIncrementally ("CATASTROPHIC SYSTEM FAILURE:\n\n" + reason + "\n\nDRONE UNRECOVERABLE\n\nSEND TRANSMISSION FROM CURRENT LOCATION:");
		transform.Find ("DeathOverlay").GetComponent<Image> ().color = new Color (0.2f, 0.2f, 0.2f, 0.8f);
		SetBGVisible (true);
	}

	public void SubmitDeathMessage(string message) {
		if (currState == AWAITING_INPUT) {
			currState = IN_GAME;
			transform.Find ("InputField").gameObject.SetActive (false);
			GameObject.FindObjectOfType<BackendScript>().ReportDeath(deathPosition, message);

			SetBGVisible(false);
			transform.Find ("DeathMessage").GetComponent<OneCharAtATime> ().DoneForNow ();
		}
	}

	public GameObject buttonLayer;

	public void ShowConfirmation(Vector2 pos) {
		SetBGVisible (true);
			// set buttons to visible
		currState = CONTEMPLATING_DEATH;
		deathPosition = pos;
		buttonLayer.SetActive (true);
	}

	public void ChickenOut() {
		currState = IN_GAME;
		SetBGVisible (false);
		buttonLayer.SetActive (false);
	}

	public void EasyWayOut()  {
		buttonLayer.SetActive (false);
	}

	public void SetBGVisible(bool visible) {
		transform.Find ("DeathOverlay").GetComponent<Image> ().color = new Color (0.2f, 0.2f, 0.2f, visible ? 0.8f : 0.0f);
	}
}
