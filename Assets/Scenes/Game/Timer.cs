using TMPro;
using UnityEngine;

namespace Assets.Scenes.Game
{
    public class Timer : MonoBehaviour
    {
        public float Time;
        public TextMeshProUGUI Text;

        // Start is called before the first frame update
        void Start()
        {
            Time = 0;
        }

        // Update is called once per frame
        void Update()
        {
            Time += UnityEngine.Time.deltaTime;
            DisplayTime(Time);
        }

        void DisplayTime(float timeToDisplay)
        {
            float minutes = Mathf.FloorToInt(timeToDisplay / 60);
            float seconds = Mathf.FloorToInt(timeToDisplay % 60);
            Text.text = $"{minutes:00}:{seconds:00}";
        }
    }
}
