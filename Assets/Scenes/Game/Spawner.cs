using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scenes.Game
{
    public class Spawner : MonoBehaviour
    {
        public GameObject EnemyStatue;
        public GameObject EnemyBandit;
        public float SpawnAcceleration;
        public float StatueInterval;
        public float BanditInterval;
        public int InitialBandits;
        public GameObject[] SpawnPoints;

        private GameObject _player;

        // Start is called before the first frame update
        void Start()
        {
            _player = GameObject.FindGameObjectWithTag("MainCamera");

            InvokeRepeating("SpawnStatue", 5.0f, StatueInterval);
            InvokeRepeating("SpawnBandit", 1.0f, BanditInterval);
            InvokeRepeating("AcceletateSpawns", 15.0f, 15.0f);

            for (var i = 0; i < InitialBandits; ++i)
            {
                Invoke("SpawnBandit", i);
            }
        }

        void AcceletateSpawns()
        {
            CancelInvoke("SpawnStatue");
            CancelInvoke("SpawnBandit");
            StatueInterval /=  (1 + SpawnAcceleration);
            BanditInterval /=  (1 + SpawnAcceleration);
            InvokeRepeating("SpawnStatue", 5.0f, StatueInterval);
            InvokeRepeating("SpawnBandit", 1.0f, BanditInterval);
        }

        // Update is called once per frame
        void Update()
        {
        }

        Vector2 GetSpawnPoint()
        {
            List<Vector2> validSpawns = new();

            foreach (var spawn in SpawnPoints)
            {
                if (Vector2.Distance(spawn.transform.position, _player.transform.position) > 10.0f)
                {
                    validSpawns.Add(spawn.transform.position);
                }
            }

            if (validSpawns.Count > 0)
            {
                return validSpawns[Random.Range(0, validSpawns.Count)];
            }

            return Vector2.zero;
        }

        void SpawnStatue()
        {
            Instantiate(EnemyStatue, GetSpawnPoint(), Quaternion.identity);
        }

        void SpawnBandit()
        {
            Instantiate(EnemyBandit, GetSpawnPoint(), Quaternion.identity);
        }
    }
}