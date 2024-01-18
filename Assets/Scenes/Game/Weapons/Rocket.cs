using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scenes.Game
{
    public class Rocket : MonoBehaviour
    {
        public GameObject explosionPrefab;
        private float _damage;
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

            // Zdefiniuj promieñ ra¿enia
            float blastRadius = 5f;
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, blastRadius);

            foreach (Collider2D collider in colliders)
            {
                if (collider.gameObject.tag == "Enemy")
                {
                    Character enemyCharacter = collider.gameObject.GetComponent<Character>();
                    if (enemyCharacter != null)
                    {
                        // Zadaj obra¿enia przeciwnikowi
                        enemyCharacter.TakeDamage(_damage);
                    }
                }
            }

            // Utwórz efekt wybuchu w pozycji rakiety, ale nie przypisuj go do zmiennej
            GameObject explosionInstance = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
            Destroy(explosionInstance, 1f);
            
        }
    }

}
