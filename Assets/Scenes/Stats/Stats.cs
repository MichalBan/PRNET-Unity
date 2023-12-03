using System;
using System.Net.Http;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Grpc.Core;
using Grpc.Net.Client;
using PRNET_Unity;
using Grpc.Net.Client.Web;
using Cysharp.Net.Http;
using UnityEditor.PackageManager;
using Unity.VisualScripting;
using static PRNET_Unity.Greeter;

public class Stats : MonoBehaviour
{
    public Button ButtonSend, ButtonRequest, ButtonBack;
    public TMP_InputField InputRequest;
    public TextMeshProUGUI Text;

    private GrpcChannel channel;
    private Greeter.GreeterClient client;

    // Start is called before the first frame update
    void Start()
    {
        ButtonSend.onClick.AddListener(Send);
        ButtonRequest.onClick.AddListener(Request);
        ButtonBack.onClick.AddListener(Back);


        var options = new GrpcChannelOptions();
        //var handler = new GrpcWebHandler(GrpcWebMode.GrpcWeb, new HttpClientHandler());
        var handler = new YetAnotherHttpHandler();
        handler.Http2Only = true;
        options.HttpHandler = handler;
        //options.Credentials = ChannelCredentials.Insecure;
        options.DisposeHttpClient = true;
        channel = GrpcChannel.ForAddress("http://localhost:5076", options);
        client = new GreeterClient(channel);
    }


    async void SendTutorial()
    {
        var reply = await client.SayHelloAsync(new HelloRequest { Name = InputRequest.text });
        Text.SetText(reply.Message);
    }

    private void OnDestroy()
    {
        channel.Dispose();
    }

    void Send()
    {
        SendTutorial();
    }

    void Request()
    {
        Text.SetText("Request Clicked\n" + InputRequest.text);
    }

    void Back()
    {
        SceneManager.LoadScene("MenuScene");
    }
}