using Assets.Scenes.Game;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scenes.Game
{
    public class PowerUpSpeed : MonoBehaviour
    {
        public SpriteRenderer powerUpSprite;
        private Rigidbody2D _body;
        public float increase = 4f;

        void Start()
        {
            _body = GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            _body.bodyType = RigidbodyType2D.Static;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Player")
            {
                GameObject playerObject = collision.gameObject;
                Movement movementScript = playerObject.GetComponent<Movement>();

                if (movementScript)
                {
                    movementScript.MoveSpeed += increase;
                    HideSprite();

                    Invoke("ResetSpeed", 10f);
                }
            }
        }

        private void ResetSpeed()
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject)
            {
                Movement movementScript = playerObject.GetComponent<Movement>();
                if (movementScript)
                {
                    // Przywrócenie pierwotnej prêdkoœci ruchu
                    movementScript.MoveSpeed -= increase;
                }
            }
        }

        void HideSprite()
        {
            // Wy³¹cz SpriteRenderer, aby ukryæ sprite
            if (powerUpSprite != null)
            {
                powerUpSprite.enabled = false;
            }
        }
    }
}