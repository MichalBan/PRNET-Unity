using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scenes.Game
{
    public class Rocket : MonoBehaviour
    {
        public GameObject explosionPrefab;
        private float _damage;
        private float pos;
        public void SetDamage(float damage)
        {
            _damage = damage;
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Projectile")
            {
                return;
            }

           
            ContactPoint2D contactPoint = collision.GetContact(0);
            
            Vector2 offset = new Vector2(0.0f, -1.0f);
            Vector2 explosionPosition = contactPoint.point + offset;
            GameObject explosionInstance = Instantiate(explosionPrefab, explosionPosition, Quaternion.identity);
            Destroy(gameObject);
            Collider2D myCollider = explosionInstance.GetComponent<Collider2D>();
            Vector2 position = myCollider.bounds.center;
            float radius = myCollider.bounds.extents.magnitude;
            if (collision.gameObject.tag == "Enemy")
            {
                collision.gameObject.GetComponent<Character>().TakeDamage(_damage);
            }
            Collider2D[] colliders = Physics2D.OverlapCircleAll(position, radius);

            foreach (Collider2D collider in colliders)
            {
                if (collider.gameObject.tag == "Enemy")
                {
                    collider.gameObject.GetComponent<Character>().TakeDamage(_damage);
                }
            }
            Destroy(explosionInstance, 1f);
        }

    }

}
