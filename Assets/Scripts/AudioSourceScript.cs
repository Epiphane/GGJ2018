using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceScript : MonoBehaviour {

    public AudioSource audioSource;
    public GameObject pingPrefab;

    public float repeatDelay = 2.0f;
    public bool isActive = false;

    public int numPings = 5;
    public float delayAddPerPing = 0.5f;

    public GameObject player;

    private float cooldown = 0.0f;

	// Use this for initialization
	void Start () {
        if (audioSource == null) {
            audioSource = GetComponent<AudioSource>();
        }

        audioSource.playOnAwake = false;

        cooldown = Random.Range(0, repeatDelay);
        player = GameObject.FindGameObjectWithTag("Player");
    }
	
	// Update is called once per frame
	void Update () {
        if (!isActive) {
            Vector3 dist = player.transform.position - transform.position;

            if (Mathf.Abs(dist.x) < 7.5f && Mathf.Abs(dist.y) < 5.0f) {
                isActive = true;
            }
        }

        if (!isActive)
            return;

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
        //numPings--;
    }

    void SpawnLight () {
        GameObject ping = GameObject.Instantiate(pingPrefab);

        ping.transform.position = transform.position;
        ping.transform.parent = transform;
    }
}
