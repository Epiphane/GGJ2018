using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BackendScript : MonoBehaviour {

    public GameObject entityToSpawn;
    public Transform entityTransformParent;

    public const string baseUrl = "https://fierce-taiga-67645.herokuapp.com/";
    public const string deathEntryUrl = "deathEntry";

    // Use this for initialization
    void Start() {
        StartCoroutine(SpawnEntities());
    }

    void SpawnEntity(EntityData entity) {
        GameObject e = GameObject.Instantiate(entityToSpawn);

        e.transform.position = new Vector3(entity.xPos, entity.yPos, 0);
        e.transform.parent = entityTransformParent;
    }

    [System.Serializable]
    public class EntityData {
        public string username;
        public int xPos, yPos;
        public string message;
    }

    [System.Serializable]
    public class EntityList {
        public EntityData[] entities;
    }

    IEnumerator SpawnEntities() {
        UnityWebRequest getEntities = UnityWebRequest.Get(baseUrl + deathEntryUrl);
        yield return getEntities.SendWebRequest();

        if (getEntities.isNetworkError || getEntities.isHttpError) {
            Debug.Log(getEntities.error);
        } else {
            // Show results as text
            Debug.Log(getEntities.downloadHandler.text);
            EntityData[] entities = JsonUtility.FromJson<EntityList>("{\"entities\":" + getEntities.downloadHandler.text + "}").entities;

            foreach (EntityData e in entities) {
                SpawnEntity(e);
            }
        }
    }

    IEnumerator ReportDeath(EntityData entity) {
        byte[] data = System.Text.Encoding.UTF8.GetBytes(JsonUtility.ToJson(entity));
        UnityWebRequest www = UnityWebRequest.Post(baseUrl + deathEntryUrl, data);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError) {
            Debug.Log(www.error);
        } else {
            Debug.Log("Death Reported G_G");
        }
    }
}
