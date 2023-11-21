using UnityEngine;

namespace Assets.Cainos.Pixel_Art_Top_Down___Basic.Script
{
    //animate the sprite color base on the gradient and time
    public class SpriteColorAnimation : MonoBehaviour
    {
        public Gradient Gradient;
        public float Time;

        private SpriteRenderer _sr;
        private float _timer;

        private void Start()
        {
            _timer = Time * Random.value;
            _sr = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            if (_sr)
            {
                _timer += UnityEngine.Time.deltaTime;
                if (_timer > Time) _timer = 0.0f;

                _sr.color = Gradient.Evaluate(_timer / Time);
            }
        }
    }
}
