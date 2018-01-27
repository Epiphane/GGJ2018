using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFadeScript : MonoBehaviour {

    // Lifetime in seconds
    public float lifetime = 1.0f;
    new public SpriteRenderer renderer;

    private float lifeRemaining = 1.0f;

    public float opacity {
        get {
            return lifeRemaining / lifetime;
        }
    }

    // Use this for initialization
    void Start() {
        if (!renderer) {
            renderer = GetComponent<SpriteRenderer>();
        }

        lifeRemaining = lifetime;
    }

    // Update is called once per frame
    void Update() {
        lifeRemaining -= Time.deltaTime;

        if (lifeRemaining < 0) {
            GameObject.Destroy(gameObject);
        }

        renderer.color = new Color(1, 1, 1, opacity);
    }
}
