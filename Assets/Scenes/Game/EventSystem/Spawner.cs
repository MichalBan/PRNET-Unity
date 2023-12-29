using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scenes.Game
{
    [System.Serializable]
    public class SpawnInfo
    {
        public GameObject Enemy;
        public int Initial;
        public float Start;
        public float End;
        public float Interval;
        public float Acceleration;
    }

    public class Spawner : MonoBehaviour
    {
        public SpawnInfo[] SpawnInfos;
        public GameObject[] SpawnPoints;

        private GameObject _player;

        // Start is called before the first frame update
        void Start()
        {
            _player = GameObject.FindGameObjectWithTag("MainCamera");

            foreach (var spawnInfo in SpawnInfos)
            {
                StartCoroutine(StartSpawning(spawnInfo));
            }

            InvokeRepeating(nameof(AccelerateSpawns), 0.0f, 15.0f);
        }

        // Update is called once per frame
        void Update()
        {
        }

        void AccelerateSpawns()
        {
            foreach (var spawnInfo in SpawnInfos)
            {
                spawnInfo.Interval /= 1 + spawnInfo.Acceleration;
            }
        }

        Vector2 GetSpawnPoint()
        {
            List<Vector2> validSpawns = new();

            foreach (var spawn in SpawnPoints)
            {
                if (Vector2.Distance(spawn.transform.position, _player.transform.position) > 10.0f)
                {
                    validSpawns.Add(spawn.transform.position);
                    Debug.Log("Dodano punkt spawnu: " + spawn.transform.position);
                }
            }

            if (validSpawns.Count > 0)
            {
                return validSpawns[Random.Range(0, validSpawns.Count)];
            }

            return SpawnPoints[0].transform.position;
        }

        IEnumerator StartSpawning(SpawnInfo info)
        {
            for (var i = 0; i < info.Initial; ++i)
            {
                Spawn(info.Enemy);
                yield return new WaitForSeconds(1f);
            }

            yield return new WaitForSeconds(info.Start);

            while (info.End == 0 || PlayerDataHolder.Instance.PlayerSurvivedTime.TotalSeconds < info.End)
            {
                Spawn(info.Enemy);
                yield return new WaitForSeconds(info.Interval);
            }
        }

        void Spawn(GameObject enemy)
        {
            Instantiate(enemy, GetSpawnPoint(), Quaternion.identity);
        }
    }
}