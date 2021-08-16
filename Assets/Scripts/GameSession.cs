using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSession : MonoBehaviour
{
    [SerializeField] bool gamePause = false;
    [SerializeField] bool lightHouseSwitchIsOn = false;
    [SerializeField] bool touchedTheBoat = false;

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

        if (Input.GetKeyDown(KeyCode.L))
        {
            StartCoroutine(YouLose());
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            WinDealer(true);
        }

        //Has everything but the player is not in the tunnel
        if (Input.GetKeyDown(KeyCode.J))
        {
            hasSeed = hasCoconut = hasBook = hasEnoughCoin = hasGeneral = true;
        }

        // Pause the game
        if (Input.GetKeyDown(KeyCode.P))
        {
            PauseTheGame();
        }
    }

    public void PauseTheGame()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    private void WinDealer(bool setTrueFordebugUsed = false)
    {
        if (setTrueFordebugUsed)
        {
            FindObjectOfType<LevelController>().ShowWinLabel();
            IsGamePaused = true;
            return;
        }

        if (HasEveryThingToWin() && FindObjectOfType<LevelLoader>().IsPlayerInTheTunnel())
        {
            IsGamePaused = true;
            FindObjectOfType<LevelController>().ShowWinLabel();
        }
    }

    public bool HasEveryThingToWin()
    {
        bool hasEssentials = (hasSeed && hasCoconut && hasBook && hasEnoughCoin);

        return (hasEssentials && hasGeneral && reachFormosa);
    }

    public IEnumerator YouLose()
    {
        Time.timeScale = 0.2f;
        yield return new WaitForSecondsRealtime(2f);
        FindObjectOfType<LevelController>().ShowLoseLabel();
        
        Time.timeScale = 1f;
        IsGamePaused = true;
        FindObjectOfType<SoundEffects>().GameDeathMusic();
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

    public bool ShouldMoveToZLightHouse
    {
        set
        {
            touchedTheBoat = value;
        }

        get
        {
            return touchedTheBoat;
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
