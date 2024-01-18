using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scenes.Game
{
    public class Fog : MonoBehaviour
    {
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

            if (collision.gameObject.tag == "Enemy")
            {
                collision.gameObject.GetComponent<Character>().TakeDamage(_damage);
            }

            if (collision.gameObject.tag == "Border")
            {
                Destroy(gameObject);
            }

            
        }
    }
}
