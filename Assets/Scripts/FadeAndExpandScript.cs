using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeAndExpandScript : MonoBehaviour {

    // Lifetime in seconds
    public float lifetime = 1.0f;
    new public SpriteRenderer renderer;

    private Vector3 finalScale = Vector3.zero;
    private float lifeRemaining = 1.0f;

    // Use this for initialization
    void Start() {
        lifeRemaining = lifetime;

        finalScale = transform.localScale;
        transform.localScale = Vector3.zero;
    }

    // Update is called once per frame
    void Update() {
        lifeRemaining -= Time.deltaTime;

        float opacity = lifeRemaining / lifetime;
        if (lifeRemaining < 0) {
            GameObject.Destroy(gameObject);
        }

        transform.localScale = (1 - opacity) * finalScale;
        renderer.color = new Color(1, 1, 1, opacity);
    }
}
