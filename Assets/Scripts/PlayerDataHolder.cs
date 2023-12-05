using System;
using UnityEngine;

public class PlayerDataHolder : MonoBehaviour
{
    public static PlayerDataHolder Instance { get; private set; }

    public string PlayerNick;
    public TimeSpan PlayerSurvivedTime;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}