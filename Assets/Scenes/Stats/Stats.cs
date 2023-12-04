using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Grpc.Net.Client;
using PRNET_Unity;
using Cysharp.Net.Http;

public class Stats : MonoBehaviour
{
    public Button ButtonSend, ButtonBack;
    public TMP_InputField InputText;
    public TextMeshProUGUI Text;

    private GrpcChannel _channel;
    private Greeter.GreeterClient _client;

    // Start is called before the first frame update
    void Start()
    {
        ButtonSend.onClick.AddListener(Send);
        ButtonBack.onClick.AddListener(Back);
        InputText.onValueChanged.AddListener(Limit);

        var options = new GrpcChannelOptions();
        var handler = new YetAnotherHttpHandler();
        handler.Http2Only = true;
        options.HttpHandler = handler;
        options.DisposeHttpClient = true;
        _channel = GrpcChannel.ForAddress("http://localhost:5076", options);
        _client = new Greeter.GreeterClient(_channel);
    }

    private void Limit(string inputString)
    {
        if (inputString.Length > 20)
        {
            InputText.text = inputString[..20];
        }
    }

    private void OnDestroy()
    {
        _channel.Dispose();
    }

    async void Send()
    {
        var reply = await _client.SayHelloAsync(new HelloRequest { Name = InputText.text });
        Text.SetText(reply.Value + "\n" + reply.Message);
    }

    void Back()
    {
        SceneManager.LoadScene("MenuScene");
    }
}