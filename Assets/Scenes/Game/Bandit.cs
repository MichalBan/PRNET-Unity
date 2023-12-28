using Assets.Scenes.Game;
using UnityEngine;

namespace Assets.Bandits___Pixel_Art.Demo
{
    [RequireComponent(typeof(FollowPlayer))]
    public class Bandit : Character
    {
        public float Damage;
        public float SlashCooldown;
        public float SlashRange;
        public float XpOnDeath;
        private FollowPlayer _followPlayer;
        private Animator _animator;
        private Rigidbody2D _body2d;
        private GameObject _target;
        LevelSystem playerLevel;
        private float _slashCooldownLeft;

        // Use this for initialization
        void Start()
        {
            _animator = GetComponent<Animator>();
            _body2d = GetComponent<Rigidbody2D>();
            _target = GameObject.FindGameObjectWithTag("Player");
            _followPlayer = GetComponent<FollowPlayer>();
            playerLevel = _target.GetComponent<LevelSystem>();
            _slashCooldownLeft = 0f;
        }

        // Update is called once per frame
        void Update()
        {
            if (HealthPoints > 0)
            {
                _followPlayer.MoveTowardsPlayer();

                _animator.SetInteger("AnimState", 2);
                if (_body2d.velocity.x > 0)
                    _body2d.transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
                else if (_body2d.velocity.x < 0)
                    _body2d.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

                if (_slashCooldownLeft > 0)
                {
                    _slashCooldownLeft -= Time.deltaTime;
                }
                else if (IsInSlashRange())
                {
                    TrySlash();
                }
            }
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
            _body2d.bodyType = RigidbodyType2D.Static;
            _animator.SetTrigger("Death");
            playerLevel.GainExperienceFlatRace(XpOnDeath);
            Invoke("Die", 0.25f);

        }

        void Die()
        {
            Destroy(gameObject);
        }

        void TrySlash()
        {
            _animator.SetTrigger("Attack");
            _slashCooldownLeft = SlashCooldown;
            Invoke("SlashDamgage", 0.5f);
        }

        void SlashDamgage()
        {
            if (IsInSlashRange())
            {
                _target.GetComponent<Player>().TakeDamage(Damage);
            }
        }

        private bool IsInSlashRange()
        {
            return Vector2.Distance(_target.transform.position, transform.position) < SlashRange;
        }
    }
}