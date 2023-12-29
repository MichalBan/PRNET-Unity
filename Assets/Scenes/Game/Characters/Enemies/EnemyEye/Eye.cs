using System;
using UnityEngine;

namespace Assets.Scenes.Game
{
    [RequireComponent(typeof(FollowPlayer))]
    public class Eye : Character
    {
        public float BiteDamage;
        public float BiteCooldown;
        public float SpeedMultiplier;
        public float BiteRange;
        public float ChargeRange;
        public float XpOnDeath;

        private FollowPlayer _followPlayer;
        private Rigidbody2D _body;
        private Animator _animator;
        private GameObject _target;
        LevelSystem playerLevel;
        private float _biteCooldownLeft;

        // Start is called before the first frame update
        void Start()
        {
            _body = GetComponent<Rigidbody2D>();
            _followPlayer = GetComponent<FollowPlayer>();
            _animator = GetComponent<Animator>();
            _target = GameObject.FindGameObjectWithTag("Player");
            playerLevel = _target.GetComponent<LevelSystem>();
            _biteCooldownLeft = 0f;
        }

        // Update is called once per frame
        void Update()
        {
            if (HealthPoints > 0)
            {
                _followPlayer.MoveTowardsPlayer();

                var scale = _body.transform.localScale;
                if (_body.velocity.x > 0)
                    scale.x = Math.Abs(scale.x);
                else if (_body.velocity.x < 0)
                    scale.x = -Math.Abs(scale.x);
                _body.transform.localScale = scale;

                if (_biteCooldownLeft > 0)
                {
                    _biteCooldownLeft -= Time.deltaTime;
                }
                else if (IsInRange(ChargeRange))
                {
                    _animator.SetTrigger("Attack");
                    Invoke("Bite", 0.5f);
                    _followPlayer.MoveSpeed *= SpeedMultiplier;
                    _biteCooldownLeft = BiteCooldown;
                }
            }
        }

        private void Bite()
        {
            _followPlayer.MoveSpeed /= SpeedMultiplier;
            if (IsInRange(BiteRange))
            {
                _target.GetComponent<Player>().TakeDamage(BiteDamage);
            }
        }

        private bool IsInRange(float range)
        {
            return Vector2.Distance(_target.transform.position, GetComponent<CircleCollider2D>().transform.position) < range;
        }

        public override void OnDamage()
        {
            if (!_animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            {
                _animator.SetTrigger("Hurt");
            }
        }

        public override void OnDeath()
        {
            _body.bodyType = RigidbodyType2D.Static;
            _animator.SetTrigger("Death");
            playerLevel.GainExperienceFlatRace(XpOnDeath);
            Invoke("Die", 0.25f);
        }

        void Die()
        {
            Destroy(gameObject);
        }
    }
}