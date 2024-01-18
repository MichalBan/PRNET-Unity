using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scenes.Game
{
    public class Shuriken : MonoBehaviour
    {
        // Start is called before the first frame update
        private float _damage;
        public void SetDamage(float damage)
        {
            _damage = damage;
        }

        // Update is called once per frame
        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Projectile")
            {
                return;
            }

            if (collision.gameObject.tag == "Enemy")
            {
                collision.gameObject.GetComponent<Character>().TakeDamage(_damage);
            }

            Destroy(gameObject);
        }
    }
}
