using UnityEngine;

namespace Assets.Bandits___Pixel_Art.Demo
{
    public class SensorBandit : MonoBehaviour {

        private int _mColCount = 0;

        private float _mDisableTimer;

        private void OnEnable()
        {
            _mColCount = 0;
        }

        public bool State()
        {
            if (_mDisableTimer > 0)
                return false;
            return _mColCount > 0;
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            _mColCount++;
        }

        void OnTriggerExit2D(Collider2D other)
        {
            _mColCount--;
        }

        void Update()
        {
            _mDisableTimer -= Time.deltaTime;
        }

        public void Disable(float duration)
        {
            _mDisableTimer = duration;
        }
    }
}
