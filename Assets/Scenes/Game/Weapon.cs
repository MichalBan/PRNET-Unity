using UnityEngine;

namespace Assets.Scenes.Game
{
    public class Weapon : MonoBehaviour
    {
        public float FireRate;
        public float Damage;
        public float ProjectileSpeed;

        public GameObject Projectile;

        void DoFireBolt()
        {
            var mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            float x = mouseWorldPos.x - transform.position.x;
            float y = mouseWorldPos.y - transform.position.y;
            float angle = Mathf.Atan2(y, x) * Mathf.Rad2Deg;
            var rotation = Quaternion.Euler(new Vector3(0, 0, angle)); 
        
            var spawnedBolt = Instantiate(Projectile, gameObject.transform.position, rotation);
            spawnedBolt.GetComponent<Rigidbody2D>().velocity = (mouseWorldPos - transform.position).normalized * ProjectileSpeed;
            spawnedBolt.GetComponent<Projectile>().SetDamage(Damage);
        }
    }
}
