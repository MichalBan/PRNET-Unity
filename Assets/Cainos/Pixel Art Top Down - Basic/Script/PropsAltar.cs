using System.Collections.Generic;
using UnityEngine;

//when something get into the alta, make the runes glow
namespace Assets.Cainos.Pixel_Art_Top_Down___Basic.Script
{

    public class PropsAltar : MonoBehaviour
    {
        public List<SpriteRenderer> Runes;
        public float LerpSpeed;

        private Color _curColor;
        private Color _targetColor;

        private void OnTriggerEnter2D(Collider2D other)
        {
            _targetColor = new Color(1, 1, 1, 1);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            _targetColor = new Color(1, 1, 1, 0);
        }

        private void Update()
        {
            _curColor = Color.Lerp(_curColor, _targetColor, LerpSpeed * Time.deltaTime);

            foreach (var r in Runes)
            {
                r.color = _curColor;
            }
        }
    }
}
