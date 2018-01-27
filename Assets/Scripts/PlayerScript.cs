using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    public DeathCanvasScript deathCanvas;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Die (string reason) {
        deathCanvas.ShowDeath(new Vector2(transform.position.x, transform.position.y), reason);

        GetComponent<PlayerMovementScript>().Stop();
    }
}
