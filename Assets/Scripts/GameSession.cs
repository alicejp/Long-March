using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSession : MonoBehaviour
{
    [SerializeField] bool gamePause = false;
    [SerializeField] bool lightHouseSwitchIsOn = false;

    [SerializeField] bool hasGeneral = false;
    [SerializeField] bool hasCoconut = false;
    [SerializeField] bool hasBook = false;
    [SerializeField] bool hasSeed = false;
    [SerializeField] bool hasEnoughCoin = false;
    [SerializeField] bool reachFormosa = false;

    private void Awake()
    {
        //singleton
        var gameSessions = FindObjectsOfType<GameSession>();
        if (gameSessions.Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Update()
    {
        WinDealer();
    }

    private void WinDealer()
    {
        if (HasEveryThingToWin())
        {
            FindObjectOfType<LevelController>().ShowWinLabel();
            ResetEverything();
        }
    }

    public bool HasEveryThingToWin()
    {
        bool hasEssentials = (hasSeed && hasCoconut && hasBook && hasEnoughCoin);

        return (hasEssentials && hasGeneral && reachFormosa);
    }

    public void YouLose()
    {
        //TODO:VFX
        FindObjectOfType<LevelController>().ShowLoseLabel();
        ResetEverything();
    }

    public bool ReachFormosa
    {
        set
        {
            reachFormosa = value;
        }

        get
        {
            return reachFormosa;
        }
    }

    public bool LightHouseSwitchIsOn
    {
        set
        {
            lightHouseSwitchIsOn = value;
        }

        get
        {
            return lightHouseSwitchIsOn;
        }
    }


    public void GotTheEssential(TagName tagName)
    {
        if (HasAlreadyGotIt(tagName))
        {
            return;
        }

        switch (tagName)
        {
            case TagName.Book:
                hasBook = true; break;
            case TagName.Coconut:
                hasCoconut = true; break;
            case TagName.Seed:
                hasSeed = true; break;
            case TagName.General:
                hasGeneral = true; break;
            default:
                break;
        }
        FindObjectOfType<KeyManager>().UpdateEssentialDisplay(tagName);
        IsGamePaused = true;
        StartCoroutine(FindObjectOfType<LevelController>().ShowEncouragingMessage(tagName));
        IsGamePaused = false;
    }

    public bool HasAlreadyGotIt(TagName tagName)
    {
        switch (tagName)
        {
            case TagName.Book:
                return hasBook;
            case TagName.Coconut:
                return hasCoconut;
            case TagName.Seed:
                return hasSeed;
            case TagName.General:
                return hasGeneral;
            case TagName.Coin:
                return hasEnoughCoin;
            default:
                return false;
        }
    }

    public bool HasEnoughCoin
    {
        get
        {
            return hasEnoughCoin;
        }
        
        set
        {
            hasEnoughCoin = value;
        }
    }

    public bool IsGamePaused
    {
        set
        {
            gamePause = value;
        }

        get
        {
            return gamePause;
        }
    }
    public void ResetEverything()
    {
        Destroy(gameObject);
    }
}
