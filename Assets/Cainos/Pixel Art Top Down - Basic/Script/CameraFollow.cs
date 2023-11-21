using UnityEngine;

namespace Assets.Cainos.Pixel_Art_Top_Down___Basic.Script
{
    //let camera follow target
    public class CameraFollow : MonoBehaviour
    {
        public Transform Target;
        public float LerpSpeed = 1.0f;

        private Vector3 _offset;

        private Vector3 _targetPos;

        private void Start()
        {
            if (Target == null) return;

            _offset = transform.position - Target.position;
        }

        private void Update()
        {
            if (Target == null) return;

            _targetPos = Target.position + _offset;
            transform.position = Vector3.Lerp(transform.position, _targetPos, LerpSpeed * Time.deltaTime);
        }

    }
}
