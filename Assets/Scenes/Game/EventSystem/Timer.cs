using System;
using TMPro;
using UnityEngine;

namespace Assets.Scenes.Game
{
    public class Timer : MonoBehaviour
    {
        public TextMeshProUGUI Text;

        // Start is called before the first frame update
        void Start()
        {
            PlayerDataHolder.Instance.PlayerSurvivedTime = TimeSpan.Zero;
        }

        // Update is called once per frame
        void Update()
        {
            var delta = TimeSpan.FromSeconds(Time.deltaTime);
            PlayerDataHolder.Instance.PlayerSurvivedTime = 
                PlayerDataHolder.Instance.PlayerSurvivedTime.Add(delta);
            DisplayTime(PlayerDataHolder.Instance.PlayerSurvivedTime);
        }

        void DisplayTime(TimeSpan timeToDisplay)
        {
            Text.text = $"{timeToDisplay.Minutes:00}:{timeToDisplay.Seconds:00}";
        }
    }
}