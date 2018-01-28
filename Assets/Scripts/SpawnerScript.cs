using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour {

    public GameObject[] thingsToSpawn;
    public Transform[] spawnLocations;
    public float spawnCooldown = 1.0f;

    private float spawnTime = 0;
    private int spawnNdx;

	// Use this for initialization
	void Start () {
        spawnTime = 0;
        spawnNdx = 0;
    }
	
	// Update is called once per frame
	void Update () {
		if (spawnTime <= 0) {
            Spawn();
            spawnTime = spawnCooldown;
        }
        spawnTime -= Time.deltaTime;
	}

    public void Spawn() {
        // Pick anything but the last spawn point
        spawnNdx = (spawnNdx + (int)Random.Range(1, spawnLocations.Length - 1)) % spawnLocations.Length;

        GameObject spawned = GameObject.Instantiate(thingsToSpawn[Random.Range(0, thingsToSpawn.Length)]);
        spawned.transform.position = spawnLocations[spawnNdx].position;
        spawned.transform.rotation = Quaternion.AngleAxis(Random.Range(0, 360), Vector3.forward);
    }
}
