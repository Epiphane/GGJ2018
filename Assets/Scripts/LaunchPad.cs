using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchPad : MonoBehaviour {

    public TerminalScript[] terminals;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            int activated = 0;
            for (int i = 0; i < terminals.Length; i++) {
                if (terminals[i].hasBeenActivatedBefore) {
                    activated++;
                }
            }

            if (activated == 0) {
                FindObjectOfType<OneCharAtATime>().ShowTextIncrementally("The ZZ TRITON was discovered here, with no information about the crew.\n\nPlease find all the control terminals and investigate the incident to report on what happened to the TRITON.\n\n<PRESS [ENTER] TO CONTINUE>", false);
                return;
            }

            if (activated < terminals.Length) {
                FindObjectOfType<OneCharAtATime>().ShowTextIncrementally("Please investigate all terminals before exiting the vessel.\n\n<PRESS [ENTER] TO CONTINUE>", false);
                return;
            }

            // All found!
            FindObjectOfType<OneCharAtATime>().ShowTextIncrementally("Data collected. Initiating launch sequence...", false);
        }
    }
}
