using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Flickerboy : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<SpriteRenderer> ().color = new Color (1.0f, 1.0f, 1.0f, Random.Range (0.8f, 1.0f));

		if (Input.GetKeyDown (KeyCode.Return)) {
			SceneManager.LoadScene ("Spaceship");
		}
	}
}
