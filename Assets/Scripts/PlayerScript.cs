using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    public DeathCanvasScript deathCanvas;
    public DeathCanvasScript sendTransmissionCanvas;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButton("SendTransmission")) {
            SendTransmission();
        }
	}

    public void Die (string reason) {
        deathCanvas.ShowDeath(new Vector2(transform.position.x, transform.position.y), reason);

        GetComponent<PlayerMovementScript>().Stop();
    }

    public void SendTransmission() {
        sendTransmissionCanvas.player = this;
        sendTransmissionCanvas.ShowDeath(new Vector2(transform.position.x, transform.position.y), "");

        GetComponent<PlayerMovementScript>().Stop();
    }

    public void Resume() {
        GetComponent<PlayerMovementScript>().Resume();
    }
}
