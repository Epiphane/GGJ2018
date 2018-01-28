using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerMovementScript : MonoBehaviour {

    public AudioMixer volumeMixer;
    public AudioSource audioSource;
    public GameObject wohPrefab;
    public int wohsPerLoop = 4;

    public SpriteRenderer droneSprite;
    public Transform sprite;

    private bool moving = false;
    private float lastTime = 0.0f;

    private float opacity;

	// Use this for initialization
	void Start () {
	    if (!audioSource) {
            audioSource = GetComponent<AudioSource>();
        }
        Resume();
    }

    public void Stop() {
        //audioSource.Stop();
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        this.enabled = false;
        volumeMixer.SetFloat("InGameVolume", -80);
    }

    public void Resume() {
        volumeMixer.SetFloat("InGameVolume", 0);
        audioSource.volume = 0;
        droneSprite.color = new Color(1, 1, 1, 0);
        this.enabled = true;
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 velocity = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);

        GetComponent<Rigidbody2D>().velocity = velocity * 0.5f;
        sprite.rotation = Quaternion.FromToRotation(Vector3.up, velocity);

        float MIN_TO_MOVE = 0.5f;
        float speed = velocity.magnitude;

        bool newMoving = (speed > MIN_TO_MOVE);
        if (newMoving != moving) {
            if (newMoving) {
                audioSource.Play();
                opacity = 0;
            }
            else {
                audioSource.Stop();
                audioSource.volume = 0;
                droneSprite.color = new Color(1, 1, 1, 0);
            }
            moving = newMoving;
        }
        if (moving) {
            audioSource.volume = Mathf.Clamp((speed - MIN_TO_MOVE) / (1 - MIN_TO_MOVE), 0, 1) / 2;

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

            opacity += 2 * Time.deltaTime;
            if (opacity > Mathf.PI * 2) {
                opacity -= Mathf.PI * 2;
            }
            droneSprite.color = new Color(1, 1, 1, audioSource.volume * 2); // Mathf.Pow(audioSource.volume * (Mathf.Sin(opacity) + 1) / 2, 2));
        }

    }

    public void Woh (Vector3 playerDirection) {
        GameObject woh = GameObject.Instantiate(wohPrefab);

        Vector3 right = Quaternion.AngleAxis(90.0f, Vector3.forward) * playerDirection;

        woh.transform.position = transform.position - playerDirection.normalized * 0.1f + right.normalized * Random.Range(-0.05f, 0.05f);
        woh.transform.rotation = Quaternion.FromToRotation(Vector3.up, playerDirection);
        woh.transform.localScale = Vector3.one * 0.125f;
    }
}
