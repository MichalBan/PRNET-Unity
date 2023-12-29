using UnityEngine;

namespace Assets.Scenes.Game
{
    public class Projectile : MonoBehaviour
    {
        private float _damage;

        public void SetDamage(float damage)
        {
            _damage = damage;
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                collision.gameObject.GetComponent<Character>().TakeDamage(_damage);
            }
            Destroy(gameObject);
        }
    }
}