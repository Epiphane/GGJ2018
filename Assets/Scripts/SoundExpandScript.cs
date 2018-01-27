using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Relies on SoundFadeScript
public class SoundExpandScript : MonoBehaviour {

    public SoundFadeScript fade;
    private Vector3 finalScale = Vector3.zero;

    // Use this for initialization
    void Start() {
        if (!fade) {
            fade = GetComponent<SoundFadeScript>();
        }

        finalScale = transform.localScale;
        transform.localScale = Vector3.zero;
    }

    // Update is called once per frame
    void Update() {
        transform.localScale = (1 - fade.opacity) * finalScale;
    }
}
