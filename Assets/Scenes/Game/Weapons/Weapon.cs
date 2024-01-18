using System;
using UnityEngine;

namespace Assets.Scenes.Game
{
    public class Weapon : MonoBehaviour
    {
        public float FireRate;
        public float Damage;
        public float RocketDamage;
        public float FogDamage;
        public float ShurikenDamage;
        public float ProjectileSpeed;
        public float levelDamage;
        public float levelFireRate;
        public float levelProjectileSpeed;

        public GameObject Projectile;
        public GameObject Rocket;
        public GameObject Shuriken;
        public GameObject Fog;
        

        void DoFireBolt()
        {
            var mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            float x = mouseWorldPos.x - transform.position.x;
            float y = mouseWorldPos.y - transform.position.y;
            float angle = Mathf.Atan2(y, x) * Mathf.Rad2Deg;
            var rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            var position = gameObject.transform.position + new Vector3(0f, 0.7f, 0f);

            var spawnedBolt = Instantiate(Projectile, position, rotation);
            spawnedBolt.GetComponent<Rigidbody2D>().velocity = (mouseWorldPos - transform.position).normalized * ProjectileSpeed;
            spawnedBolt.GetComponent<Projectile>().SetDamage(Damage);
        }

        void DoRocketBolt()
        {
            var mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            float x = mouseWorldPos.x - transform.position.x;
            float y = mouseWorldPos.y - transform.position.y;
            float angle = Mathf.Atan2(y, x) * Mathf.Rad2Deg;
            var rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            var position = gameObject.transform.position + new Vector3(0f, 0.0f, 0f);

            var spawnedBolt = Instantiate(Rocket, position, rotation);
            spawnedBolt.GetComponent<Rigidbody2D>().velocity = (mouseWorldPos - transform.position).normalized * ProjectileSpeed/2 ;
            spawnedBolt.GetComponent<Rocket>().SetDamage(RocketDamage);
        }


        void DoShuriken()
        {
            for (int i = 0; i < 5; i++)
            {
                var mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                float x = mouseWorldPos.x - transform.position.x;
                float y = mouseWorldPos.y - transform.position.y;
                float angle = Mathf.Atan2(y, x) * Mathf.Rad2Deg;

                angle += i * 90.0f;

                var rotation = Quaternion.Euler(new Vector3(0, 0, angle));

                var offset = new Vector3(UnityEngine.Random.Range(-1.0f, 1.0f), UnityEngine.Random.Range(-0.2f, 0.2f), 0f);
                var position = gameObject.transform.position + offset;

                var spawnedShuriken = Instantiate(Shuriken, position, rotation);
                spawnedShuriken.GetComponent<Rigidbody2D>().velocity = (mouseWorldPos - transform.position).normalized * ProjectileSpeed / 2;
                spawnedShuriken.GetComponent<Shuriken>().SetDamage(ShurikenDamage);
            }
        }

        void DoFog()
        {
            var mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            float x = mouseWorldPos.x - transform.position.x;
            float y = mouseWorldPos.y - transform.position.y;
            float angle = Mathf.Atan2(y, x) * Mathf.Rad2Deg;


            var rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            var position = gameObject.transform.position;

            var spawnedFog = Instantiate(Fog, position, rotation);
            spawnedFog.GetComponent<Rigidbody2D>().velocity = (mouseWorldPos - transform.position).normalized * ProjectileSpeed / 2;
            spawnedFog.GetComponent<Fog>().SetDamage(FogDamage);
            Destroy(spawnedFog, 5.0f);
        }

        public void IncreaseWeapon()
        {
            FireRate += levelFireRate;
            Damage += levelDamage;
            ProjectileSpeed += levelProjectileSpeed;
        }
    }


}
