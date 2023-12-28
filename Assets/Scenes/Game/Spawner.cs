using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scenes.Game
{
    public class Spawner : MonoBehaviour
    {
        public GameObject EnemyStatue;
        public GameObject EnemyBandit;
        public GameObject EnemyArcher;
        public GameObject PowerUpSpeed;
        public float SpawnAcceleration;
        public float StatueInterval;
        public float BanditInterval;
        public float ArcherInterval;
        public float PowerUpSpeedInterval;
        public int InitialBandits;
        public int InitialArchers;
        public GameObject[] SpawnPoints;

        private GameObject _player;

        // Start is called before the first frame update
        void Start()
        {
            _player = GameObject.FindGameObjectWithTag("MainCamera");

            InvokeRepeating("SpawnStatue", 5.0f, StatueInterval);
            InvokeRepeating("SpawnBandit", 1.0f, BanditInterval);
            //InvokeRepeating("SpawnArcher", 5.0f, BanditInterval);
            InvokeRepeating("SpawnPowerUpSpeed", 15f, PowerUpSpeedInterval);
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
            CancelInvoke("SpawnPowerUpSpeed");
            StatueInterval /=  (1 + SpawnAcceleration);
            BanditInterval /=  (1 + SpawnAcceleration);
            PowerUpSpeedInterval /= (1 + SpawnAcceleration);
            InvokeRepeating("SpawnStatue", 5.0f, StatueInterval);
            InvokeRepeating("SpawnBandit", 1.0f, BanditInterval);
            InvokeRepeating("SpawnPowerUpSpeed", 15f, PowerUpSpeedInterval);
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
                    Debug.Log("Dodano punkt spawnu: " + spawn.transform.position);
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
            Debug.Log("Wartoœæ EnemyStatue: " + EnemyStatue); // SprawdŸ, czy PowerUpBoot nie jest null-em
            Instantiate(EnemyStatue, GetSpawnPoint(), Quaternion.identity);
        }

        void SpawnBandit()
        {
            Debug.Log("Wartoœæ EnemyBandit: " + EnemyBandit); // SprawdŸ, czy PowerUpBoot nie jest null-em
            Instantiate(EnemyBandit, GetSpawnPoint(), Quaternion.identity);
        }

        void SpawnPowerUpSpeed()
        {
            Debug.Log("Wartoœæ PowerUpSpeed: " + PowerUpSpeed); // SprawdŸ, czy PowerUpBoot nie jest null-em
            Instantiate(PowerUpSpeed, GetSpawnPoint(), Quaternion.identity);
        }
    }
}