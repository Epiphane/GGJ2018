using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantDeathScript : MonoBehaviour {

    public AudioSource deathSound;
    public string deathMessage = "You have died of unknown causes!";

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            other.GetComponent<PlayerScript>().Die(deathMessage);

            if (deathSound)
                deathSound.Play();
        }
    }
}
