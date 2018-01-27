using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerMovementScript : MonoBehaviour {

    public AudioMixer volumeMixer;
    public AudioSource audioSource;
    public GameObject wohPrefab;

    private bool moving = false;
    private float lastTime = 0.0f;

	// Use this for initialization
	void Start () {
	    if (!audioSource) {
            audioSource = GetComponent<AudioSource>();
        }
        volumeMixer.SetFloat("InGameVolume", 0);
    }

    public void Stop() {
        //audioSource.Stop();
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        this.enabled = false;
        volumeMixer.SetFloat("InGameVolume", -80);
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 velocity = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);

        GetComponent<Rigidbody2D>().velocity = velocity;

        float MIN_TO_MOVE = 0.5f;
        float speed = velocity.magnitude;

        bool newMoving = (speed > MIN_TO_MOVE);
        if (newMoving != moving) {
            if (newMoving) {
                audioSource.Play();
            }
            else {
                audioSource.Stop();
            }
            moving = newMoving;
        }
        if (moving) {
            audioSource.volume = Mathf.Clamp((speed - MIN_TO_MOVE) / (1 - MIN_TO_MOVE), 0, 1);

            int wohsPerLoop = 4;
            float newTime = audioSource.time;
            if (newTime < lastTime) {
                // Looped!
                Woh(velocity);
            }
            else {
                float interval = audioSource.clip.length / wohsPerLoop;
                for (float t = interval; t < audioSource.clip.length; t += interval) {
                    if (newTime >= t && lastTime < t) {
                        Woh(velocity);
                    }
                }
            }
            lastTime = newTime;
        }
	}

    public void Woh (Vector3 playerDirection) {
        GameObject woh = GameObject.Instantiate(wohPrefab);

        woh.transform.position = transform.position;
        woh.transform.rotation = Quaternion.FromToRotation(Vector3.up, playerDirection);
    }
}
