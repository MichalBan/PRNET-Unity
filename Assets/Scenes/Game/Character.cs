using UnityEngine;

namespace Assets.Scenes.Game
{
    public abstract class Character : MonoBehaviour
    {
        public float HealthPoints;
        public void TakeDamage(float damage)
        {
            if (HealthPoints <= 0)
            {
                return;
            }

            HealthPoints -= damage;

            if (HealthPoints <= 0)
            {
                OnDeath();
            }
            else
            {
                OnDamage();
            }
        }

        public abstract void OnDeath();

        public abstract void OnDamage();
    }
}