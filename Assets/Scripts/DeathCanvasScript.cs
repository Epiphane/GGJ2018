using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathCanvasScript : MonoBehaviour {

    public BackendScript backend;

    public Image background;
    public Text deathReason;
    public Transform toDrop;

    public float animationTime = 0.0f;

	void Start () {
        gameObject.SetActive(false);

        Reset ();
	}

    void Reset() {
        animationTime = 0;
        background.color = new Color(0, 0, 0, 0);
        toDrop.localPosition = new Vector3(0, 1250, 0);
        
    }

    float EaseOutBounce(float t, float start, float height, float d) {
        t /= d;
        if (t < 0) return start;
        if (t > 1) return start - height;

        if (t < (1 / 2.75)) {
            return start - height * (7.5625f * t * t);
        } else if (t < (2 / 2.75)) {
            t -= (1.5f / 2.75f);
            return start - height * (7.5625f * t * t + .75f);
        } else if (t < (2.5 / 2.75)) {
            t -= (2.25f / 2.75f);
            return start - height * (7.5625f * t * t + .9375f);
        } else {
            t -= (2.625f / 2.75f);
            return start - height * (7.5625f * t * t + .984375f);
        }
    }
	
	void Update () {
        animationTime += Time.deltaTime;

        float opacity = Mathf.Clamp(animationTime / 1.0f, 0, 1);
        background.color = new Color(0, 0, 0, opacity * 0.9f);

        if (animationTime > 1.0f) {
            toDrop.localPosition = new Vector3(0, EaseOutBounce(animationTime - 1.5f, 1250, 1250, 2), 0);
        }
    }

    public void ShowDeath(string reason) {
        deathReason.text = reason;

        gameObject.SetActive(true);
    }
}
