using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuzzyRounder : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

     
    void CheckRoundFloat(float target, ref float outVal)
    {
        if (Mathf.Abs(outVal - target) < 0.1f)
        {
            outVal = target;
        }
    }



    public void DoIt() {
        BoxCollider2D[] colliders = FindObjectsOfType<BoxCollider2D>() as BoxCollider2D[];
        foreach (BoxCollider2D collider in colliders)
        {
            Vector2 size = collider.size;
            CheckRoundFloat(1.0f, ref size.x);
            CheckRoundFloat(0.2f, ref size.x);
            CheckRoundFloat(1.0f, ref size.y);
            CheckRoundFloat(0.2f, ref size.y);
            collider.size = size;

            Vector2 pos = collider.offset;

            CheckRoundFloat(0.2f, ref pos.x);
            CheckRoundFloat(0.4f, ref pos.x);
            CheckRoundFloat(-0.2f, ref pos.x);
            CheckRoundFloat(-0.4f, ref pos.x);
            CheckRoundFloat(0.2f, ref pos.y);
            CheckRoundFloat(0.4f, ref pos.y);
            CheckRoundFloat(-0.2f, ref pos.y);
            CheckRoundFloat(-0.4f, ref pos.y);

            collider.offset = pos;
        }    
    }
}
