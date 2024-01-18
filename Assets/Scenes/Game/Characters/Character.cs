using UnityEngine;

namespace Assets.Scenes.Game
{
    public abstract class Character : MonoBehaviour
    {
        public float HealthPoints;
        public float HealRegen;
        public float MovementSpeed;
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

        public void Slow(float slow)
        {
            MovementSpeed = MovementSpeed * (1 - slow);
        }
        public abstract void OnDeath();

        public abstract void OnDamage();

    }
}