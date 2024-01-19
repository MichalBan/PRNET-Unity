using Assets.Scenes.Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class LevelSystem : MonoBehaviour
{
    public int level;
    public float currentXp;
    public float requiredXp;
    //private GameObject _XpBar;
    private float lerpTimer;
    private float delayTimer;
    [Header("UI")]
    public Image XpFrontImageBar;
    public Image XpBackImageBar;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI xpText;
    [Header("Multipliers")]
    [Range(1f, 300f)]
    public float additionMultiplier = 300;
    [Range(2f, 4f)]
    public float powerMultiplier = 2;
    [Range(7f, 14f)]
    public float divisionMultiplier = 7;
    // Start is called before the first frame update
    void Start()
    {
        levelText.text = "Level " + level;
        //_XpBar = GameObject.Find("XpBar");
        //_XpBar.GetComponent<Slider>().value = currentXp / requiredXp;
        XpFrontImageBar.fillAmount = currentXp / requiredXp;
        XpBackImageBar.fillAmount = currentXp / requiredXp;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateXpUI();
        if (Input.GetKeyDown(KeyCode.Equals))
        { 
            GainExperienceFlatRace(20);
        }

        if (currentXp >= requiredXp)
        {
            LevelUp();
        }
    }

    public void UpdateXpUI()
    {
        float xpFraction = currentXp / requiredXp;
        float FXP = XpFrontImageBar.fillAmount;
        if (FXP < xpFraction)
        {
            delayTimer += Time.deltaTime;
            XpBackImageBar.fillAmount = xpFraction;
            if(delayTimer > 3)
            {
                lerpTimer += Time.deltaTime;
                float percentComplete = lerpTimer / 4;
                XpFrontImageBar.fillAmount = Mathf.Lerp(FXP, XpBackImageBar.fillAmount, percentComplete);
            } 
        }
        xpText.text = currentXp + "/" + requiredXp;
    }

    public void GainExperienceFlatRace(float XpGained)
    {
        currentXp += XpGained;
        lerpTimer = 0f;
        delayTimer = 0f;
    }
    
    public void LevelUp()
    {
        level++;
        XpBackImageBar.fillAmount = 0f;
        XpFrontImageBar.fillAmount = 0f;
        currentXp = Mathf.RoundToInt(currentXp - requiredXp);
        GetComponent<Player>().IncreaseHealth();
        GetComponent<Weapon>().IncreaseWeapon();
        requiredXp = CalculateNextLevelXp();
        levelText.text = "Level " + level;
    }
    // Runescape Algorithm (https://oldschool.runescape.wiki/w/Experience)
    private int CalculateNextLevelXp()
    {
        int solveForRequiredXp = 0;
        for (int levelCycle = 1; levelCycle <= level; levelCycle++)
        {
            solveForRequiredXp += (int)Mathf.Floor(levelCycle + additionMultiplier * Mathf.Pow(powerMultiplier, levelCycle / divisionMultiplier));
        }
        return solveForRequiredXp / 4;
    }
}
