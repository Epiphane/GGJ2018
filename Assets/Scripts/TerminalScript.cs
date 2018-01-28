using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerminalScript : MonoBehaviour {

    public SpriteRenderer sprite;
    public SpriteRenderer sound;
    public Sprite[] turningOnSprites;
    public Sprite[] turningOnSounds;
    public AudioSource audioSource;

    public bool hasBeenActivatedBefore = false;

	[Multiline]
	public string textToSay;
	public bool shownText = false;

    private float level = 0;
    private bool active = false;

	// Use this for initialization
	void Start () {
        level = 0;
    }
	
	// Update is called once per frame
	void Update () {
		if (active && level < turningOnSprites.Length - 1) {
			level += Time.deltaTime * 2;

			sprite.sprite = turningOnSprites [(int)Mathf.Floor (level)];
			sound.sprite = turningOnSounds [(int)Mathf.Floor (level)];
		} else if (active) {
			// MAAAAX POWERRRRRR
			if (!shownText) {
				shownText = true;
				FindObjectOfType<OneCharAtATime> ().ShowTextIncrementally (textToSay, false);
                hasBeenActivatedBefore = true;
            }
		}
        else if (!active && level > 0) {
            level -= Time.deltaTime * 2;
            if (level <= 0) {
                level = 0;
                shownText = false;
            }

            sprite.sprite = turningOnSprites[(int)Mathf.Floor(level)];
            sound.sprite = turningOnSounds[(int)Mathf.Floor(level)];
        }

	}

    void OnTriggerEnter2D (Collider2D other) {
        if (other.tag == "Player") {
            active = true;
            audioSource.Play();
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "Player") {
            active = false;
            audioSource.Stop();
        }
    }
}
