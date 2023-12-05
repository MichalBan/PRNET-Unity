using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using PRNET_Unity;
using Grpc.Core;

public class Stats : MonoBehaviour
{
    public Button ButtonGetAll, ButtonGetMy, ButtonBack;
    public Transform Content;
    public RecordText RecordPrefab;

    // Start is called before the first frame update
    void Start()
    {
        ButtonGetAll.onClick.AddListener(GetAll);
        ButtonGetMy.onClick.AddListener(GetMy);
        ButtonBack.onClick.AddListener(Back);
    }

    private void GetAll()
    {
        var call = HighScoresHandler.Instance.Client.GetAll(new GetAllRequest());
        SetTextFromStream(call);
    }

    private void GetMy()
    {
        var myNick = PlayerDataHolder.Instance.PlayerNick;
        var call = HighScoresHandler.Instance.Client.GetMy(new GetMyRequest { Name = myNick });
        SetTextFromStream(call);
    }

    private async void SetTextFromStream(AsyncServerStreamingCall<GetReply> call)
    {
        for (var i = Content.childCount - 1; i >= 0; --i)
        {
            Destroy(Content.GetChild(i).gameObject);
        }

        Instantiate(RecordPrefab, Content).Init("Nick", "Survived", "Date");
        await foreach (var response in call.ResponseStream.ReadAllAsync())
        {
            Instantiate(RecordPrefab, Content).Init(response);
        }
    }

    private void Back()
    {
        SceneManager.LoadScene("MenuScene");
    }
}