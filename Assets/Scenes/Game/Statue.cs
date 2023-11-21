using UnityEngine;

namespace Assets.Scenes.Game
{
    [RequireComponent(typeof(FollowPlayer))]
    public class Statue : Character
    {
        public float DamagePerSecond;

        private FollowPlayer _followPlayer;
        private Rigidbody2D _body;
        private Animator _animator;

        // Start is called before the first frame update
        void Start()
        {
            _body = GetComponent<Rigidbody2D>();
            _followPlayer = GetComponent<FollowPlayer>();
            _animator = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            if (HealthPoints > 0)
            {
                _followPlayer.MoveTowardsPlayer();
            }
        }

        void OnCollisionStay2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                collision.gameObject.GetComponent<Player>().TakeDamage(DamagePerSecond * Time.deltaTime);
            }
        }

        public override void OnDamage()
        {
            _followPlayer.MoveSpeed /= 1.2f;
        }

        public override void OnDeath()
        {
            _body.bodyType = RigidbodyType2D.Static;
            _animator.SetTrigger("Fade");
            Invoke("Die", 5.0f);
        }

        void Die()
        {
            Destroy(gameObject);
        }
    }
}