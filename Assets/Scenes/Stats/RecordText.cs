using System.Collections;
using System.Collections.Generic;
using PRNET_Unity;
using TMPro;
using UnityEngine;

public class RecordText : MonoBehaviour
{
    public TextMeshProUGUI TextNick, TextSurvived, TextDate;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void Init(GetReply response)
    {
        TextNick.SetText(response.Name);
        TextSurvived.SetText($"{response.SurvivedTime.Seconds / 60:00}:{response.SurvivedTime.Seconds % 60:00}");
        TextDate.SetText(response.PlayedDate.ToDateTime().ToString("dd.MM.yy H:mm"));
    }

    public void Init(string nick, string survived, string date)
    {
        TextNick.SetText(nick);
        TextSurvived.SetText(survived);
        TextDate.SetText(date);
    }
}