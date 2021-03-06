﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public TerminalScript activateAfterThis;
    public bool canSpawn = false;

    [SerializeField]
    private GameObject _monsterPrefab;

    [SerializeField]
    private float _monsterLifetime = 5.0f;

    void Update () {
        if (activateAfterThis == null || activateAfterThis.hasBeenActivatedBefore) {
            canSpawn = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (canSpawn && other.tag == Tags.Player && _monsterPrefab)
        {
            var monsterGO = Instantiate(_monsterPrefab);
            var monsterAI = monsterGO.GetComponent<MonsterAI>();

            monsterGO.transform.SetParent(transform, false);
            monsterAI.TimeToDie = _monsterLifetime;
            monsterAI.StartFollowingPlayer();
        }
    }
}
