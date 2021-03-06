﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("SendTransmission")) {
            SendTransmission();
        }
	}

    public void Die (string reason) {
		FindObjectOfType<DeathHandler>().ShowDeath(new Vector2(transform.position.x, transform.position.y), reason);

        GetComponent<PlayerMovementScript>().Stop();
    }

    public void SendTransmission() {
		FindObjectOfType<DeathHandler>().ShowConfirmation(new Vector2(transform.position.x, transform.position.y));
        this.enabled = false;

        GetComponent<PlayerMovementScript>().Stop();
    }

    public void Resume() {
        this.enabled = true;
        GetComponent<PlayerMovementScript>().Resume();
    }
}
