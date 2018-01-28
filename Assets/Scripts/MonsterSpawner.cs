using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject _monsterPrefab;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == Tags.Player && _monsterPrefab)
        {
            var monsterGO = Instantiate(_monsterPrefab);
            var monsterAI = monsterGO.GetComponent<MonsterAI>();

            monsterGO.transform.SetParent(transform, false);
            monsterAI.StartFollowingPlayer();
        }
    }
}
