﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAI : MonoBehaviour
{
    private Transform _playerTransform;
    private bool _isFollowingPlayer = false;
    [SerializeField]
    private float _speed = 1.0f;
    [SerializeField]
    private float _timeToDie = 5.0f;
    [SerializeField]
    private AudioClip[] _spawnAudio;

    public float TimeToDie { get { return _timeToDie; } set { _timeToDie = value; } }

    private void Start()
    {
        if (_spawnAudio.Length > 0)
        {
            var spawnAudioIndex = Random.Range(0, _spawnAudio.Length);

            var audioSource = GetComponent<AudioSource>();
            audioSource.clip = _spawnAudio[spawnAudioIndex];
            audioSource.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_isFollowingPlayer)
        {
            var vectorToPlayer = _playerTransform.position - transform.position;
            //Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(vectorToPlayer), 45.0f * Time.deltaTime);

            transform.Translate(vectorToPlayer * _speed * Time.deltaTime);
        }
    }

    public void StartFollowingPlayer()
    {
        var playerGO = GameObject.FindGameObjectWithTag(Tags.Player);

        if (playerGO)
        {
            _playerTransform = playerGO.transform;
            _isFollowingPlayer = true;

            StartCoroutine(CountDown());
        }
    }

    private IEnumerator CountDown()
    {
        while (_timeToDie > 0.0f)
        {
            yield return new WaitForSecondsRealtime(1.0f);

            _timeToDie -= 1.0f;
        }

        Destroy(gameObject);
    }
}
