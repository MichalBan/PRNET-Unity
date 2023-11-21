using System;
using UnityEngine;

namespace Assets.Scenes.Game
{
    public class Movement : MonoBehaviour
    {
        public float MoveSpeed;

        private Rigidbody2D _body;
        private Animator _anim;

        // Start is called before the first frame update
        void Start()
        {
            _body = GetComponent<Rigidbody2D>();
            _anim = GetComponent<Animator>();
        }

        public void UpdateMovement()
        {
            float xSpeed = 0;
            float ySpeed = 0;

            if (Input.GetKey("w"))
            {
                ySpeed += MoveSpeed;
            }

            if (Input.GetKey("s"))
            {
                ySpeed -= MoveSpeed;
            }

            if (Input.GetKey("a"))
            {
                xSpeed -= MoveSpeed;
            }

            if (Input.GetKey("d"))
            {
                xSpeed += MoveSpeed;
            }

            if (xSpeed != 0 && ySpeed != 0)
            {
                xSpeed *= 0.707f;
                ySpeed *= 0.707f;
            }

            _body.velocity = new Vector2(xSpeed, ySpeed);

            _anim.SetBool("isRun", _body.velocity.magnitude > 0.1);
            var scale = transform.localScale;
            scale.x = xSpeed > 0 ? Math.Abs(scale.x) : -Math.Abs(scale.x);
            transform.localScale = scale;
        }
    }
}