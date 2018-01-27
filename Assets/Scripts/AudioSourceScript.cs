using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceScript : MonoBehaviour {

    public AudioSource audioSource;
    public GameObject pingPrefab;

    public float repeatDelay = 2.0f;
    public bool playOnAwake = true;

    public int numPings = 5;
    public float delayAddPerPing = 0.5f;

    private float cooldown = 0.0f;

	// Use this for initialization
	void Start () {
        if (audioSource == null) {
            audioSource = GetComponent<AudioSource>();
        }

        audioSource.playOnAwake = false;

        if (!playOnAwake) {
            cooldown = repeatDelay;
        }
	}
	
	// Update is called once per frame
	void Update () {
        cooldown -= Time.deltaTime;
        if (cooldown <= 0) {
            Ping();
        }
    }

    public void Ping() {
        if (numPings <= 0) {
            return;
        }

        audioSource.Play();

        SpawnLight();
        repeatDelay += delayAddPerPing;
        cooldown = repeatDelay;
        numPings--;
    }

    void SpawnLight () {
        GameObject ping = GameObject.Instantiate(pingPrefab);

        ping.transform.position = transform.position;
        ping.transform.parent = transform;
    }
}
