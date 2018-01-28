using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInSound : MonoBehaviour {

    public AudioSource sound;

	// Use this for initialization
	void Start () {
        sound = GetComponent<AudioSource>();
        sound.volume = 0;
    }
	
	// Update is called once per frame
	void Update () {
		if (sound.volume < 1) {
            sound.volume += Time.deltaTime;
        }
	}
}
