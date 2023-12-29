using UnityEngine;

namespace Assets.Scenes.Game
{
    public class FollowPlayer : MonoBehaviour
    {
        public float MoveSpeed;

        private Rigidbody2D _body;
        private GameObject _target;

        void Start()
        {
            _body = GetComponent<Rigidbody2D>();
            _target = GameObject.FindGameObjectWithTag("Player");
        }

        public void MoveTowardsPlayer()
        {
            if (_target == null)
            {
                return;
            }

            Vector2 direction = _target.transform.position - transform.position;
            _body.velocity = direction.normalized * MoveSpeed;
        }
    }
}