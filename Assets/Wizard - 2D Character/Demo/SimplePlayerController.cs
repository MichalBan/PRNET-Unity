using UnityEngine;

namespace Assets.Wizard___2D_Character.Demo
{
    public class SimplePlayerController : MonoBehaviour
    {
        public float MovePower = 10f;
        public float JumpPower = 15f; //Set Gravity Scale in Rigidbody2D Component to 5

        private Rigidbody2D _rb;
        private Animator _anim;
        Vector3 _movement;
        private int _direction = 1;
        bool _isJumping = false;
        private bool _alive = true;


        // Start is called before the first frame update
        void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            _anim = GetComponent<Animator>();
        }

        private void Update()
        {
            Restart();
            if (_alive)
            {
                Hurt();
                Die();
                Attack();
                Jump();
                Run();

            }
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            _anim.SetBool("isJump", false);
        }


        void Run()
        {
            Vector3 moveVelocity = Vector3.zero;
            _anim.SetBool("isRun", false);


            if (Input.GetAxisRaw("Horizontal") < 0)
            {
                _direction = -1;
                moveVelocity = Vector3.left;

                transform.localScale = new Vector3(_direction, 1, 1);
                if (!_anim.GetBool("isJump"))
                    _anim.SetBool("isRun", true);

            }
            if (Input.GetAxisRaw("Horizontal") > 0)
            {
                _direction = 1;
                moveVelocity = Vector3.right;

                transform.localScale = new Vector3(_direction, 1, 1);
                if (!_anim.GetBool("isJump"))
                    _anim.SetBool("isRun", true);

            }
            transform.position += moveVelocity * MovePower * Time.deltaTime;
        }
        void Jump()
        {
            if ((Input.GetButtonDown("Jump") || Input.GetAxisRaw("Vertical") > 0)
            && !_anim.GetBool("isJump"))
            {
                _isJumping = true;
                _anim.SetBool("isJump", true);
            }
            if (!_isJumping)
            {
                return;
            }

            _rb.velocity = Vector2.zero;

            Vector2 jumpVelocity = new Vector2(0, JumpPower);
            _rb.AddForce(jumpVelocity, ForceMode2D.Impulse);

            _isJumping = false;
        }
        void Attack()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                _anim.SetTrigger("attack");
            }
        }
        void Hurt()
        {
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                _anim.SetTrigger("hurt");
                if (_direction == 1)
                    _rb.AddForce(new Vector2(-5f, 1f), ForceMode2D.Impulse);
                else
                    _rb.AddForce(new Vector2(5f, 1f), ForceMode2D.Impulse);
            }
        }
        void Die()
        {
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                _anim.SetTrigger("die");
                _alive = false;
            }
        }
        void Restart()
        {
            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                _anim.SetTrigger("idle");
                _alive = true;
            }
        }
    }
}