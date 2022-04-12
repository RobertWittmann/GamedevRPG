using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.SceneManagement
{
    public class PersistentObjectSpawner : MonoBehaviour
    {
        [SerializeField] GameObject persistenObjectPrefab;

        static bool hasSpawned = false;

        private void Awake()
        {
            if (hasSpawned) return;

            SpawnPersistentObjects();

            hasSpawned = true;
        }

        private void SpawnPersistentObjects()
        {
            // GameObject persistentObject = Instantiate(persistenObjectPrefab);
            DontDestroyOnLoad(Instantiate(persistenObjectPrefab));
        }
    }
}