using UnityEngine;

namespace Assets.Scenes.Game
{
    public class Weapon : MonoBehaviour
    {
        public float FireRate;
        public float Damage;
        public float ProjectileSpeed;
        public float levelDamage;
        public float levelFireRate;
        public float levelProjectileSpeed;

        public GameObject Projectile;

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

        public void IncreaseWeapon()
        {
            FireRate += levelFireRate;
            Damage += levelDamage;
            ProjectileSpeed += levelProjectileSpeed;
        }
    }


}
