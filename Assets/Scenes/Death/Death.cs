using Google.Protobuf.WellKnownTypes;
using PRNET_Unity;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scenes.Death
{
    public class DeathMenu : MonoBehaviour
    {
        async void Start()
        {
            Invoke("Ok", 3f);

            var myNick = PlayerDataHolder.Instance.PlayerNick;
            var myTime = PlayerDataHolder.Instance.PlayerSurvivedTime;
            var request = new SubmitRequest { Name = myNick, SurvivedTime = Duration.FromTimeSpan(myTime) };
            var reply = await HighScoresHandler.Instance.Client.SubmitAsync(request);
            Debug.Log("submission " + reply.Success);
        }

        void Ok()
        {
            SceneManager.LoadScene("MenuScene");
        }
    }
}