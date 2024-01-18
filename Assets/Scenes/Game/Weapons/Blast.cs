using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scenes.Game
{
    public class Blast : MonoBehaviour
    {
        private float _damage;
        public void SetDamage(float damage)
        {
            _damage = damage;
        }

        public void XD()
        {
            Collider2D myCollider = GetComponent<Collider2D>();

            if (myCollider != null)
            {
                // Pobierz pozycjê i promieñ collidera
                Vector2 position = myCollider.bounds.center;
                float radius = myCollider.bounds.extents.magnitude;

                // SprawdŸ kolizje z innymi obiektami tylko w obrêbie collidera obiektu
                Collider2D[] colliders = Physics2D.OverlapCircleAll(position, radius);

                foreach (Collider2D collider in colliders)
                {
                    if (collider.gameObject.tag == "Enemy")
                    {
                        collider.gameObject.GetComponent<Character>().TakeDamage(_damage);
                    }
                }
            }
            else
            {
                Debug.LogError("Brak komponentu Collider2D na obiekcie.");
            }
            Destroy(gameObject);
        }


    }

}
