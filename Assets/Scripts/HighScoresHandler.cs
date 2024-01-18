using Cysharp.Net.Http;
using Grpc.Net.Client;
using PRNET_Unity;
using UnityEngine;

public class HighScoresHandler : MonoBehaviour
{
    public static HighScoresHandler Instance { get; private set; }
    private GrpcChannel _channel;
    public HighScores.HighScoresClient Client { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        var options = new GrpcChannelOptions();
        var handler = new YetAnotherHttpHandler();
        handler.Http2Only = true;
        options.HttpHandler = handler;
        options.DisposeHttpClient = true;
        _channel = GrpcChannel.ForAddress("https://prnet-server.azurewebsites.net/", options);
        Client = new HighScores.HighScoresClient(_channel);

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void OnDestroy()
    {
        _channel?.Dispose();
    }
}