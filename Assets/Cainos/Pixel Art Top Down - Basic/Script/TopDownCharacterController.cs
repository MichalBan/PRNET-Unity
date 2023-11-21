using UnityEngine;

namespace Assets.Cainos.Pixel_Art_Top_Down___Basic.Script
{
    public class TopDownCharacterController : MonoBehaviour
    {
        public float Speed;

        private Animator _animator;

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }


        private void Update()
        {
            Vector2 dir = Vector2.zero;
            if (Input.GetKey(KeyCode.A))
            {
                dir.x = -1;
                _animator.SetInteger("Direction", 3);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                dir.x = 1;
                _animator.SetInteger("Direction", 2);
            }

            if (Input.GetKey(KeyCode.W))
            {
                dir.y = 1;
                _animator.SetInteger("Direction", 1);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                dir.y = -1;
                _animator.SetInteger("Direction", 0);
            }

            dir.Normalize();
            _animator.SetBool("IsMoving", dir.magnitude > 0);

            GetComponent<Rigidbody2D>().velocity = Speed * dir;
        }
    }
}
