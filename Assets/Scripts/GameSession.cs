using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSession : MonoBehaviour
{
    [SerializeField] int money;
    [SerializeField] GameObject winLabel;
    [SerializeField] GameObject loseLabel;
    [SerializeField] GameObject seedLabel;
    [SerializeField] GameObject foodLabel;
    [SerializeField] GameObject bookLabel;
    [SerializeField] GameObject infoLabel;
    [SerializeField] float showingLabelTime = 2f;

    [SerializeField] int minimumCoinRequirement = 500;
    [SerializeField] bool hasGeneral = false;
    [SerializeField] bool hasCoconut = false;
    [SerializeField] bool hasBook = false;
    [SerializeField] bool hasSeed = false;
    [SerializeField] bool hasEnoughCoin = false;
    [SerializeField] bool reachFormosa = false;

    [SerializeField] bool gamePause = false;
    [SerializeField] bool lightHouseSwitchIsOn = false;

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

    // Start is called before the first frame update
    void Start()
    {
        SetAllLabelsOff();
    }

    private void Update()
    {
        WinDealer();
    }

    private void WinDealer()
    {
        if (HasEveryThingToWin())
        {
            YouWin();
        }
    }

    public bool HasEveryThingToWin()
    {
        bool hasEssentials = (hasSeed && hasCoconut && hasBook && hasEnoughCoin);

        return (hasEssentials && hasGeneral && reachFormosa);
    }

    /// <summary>
    /// Gain money by crashing the coin sprite
    /// </summary>
    /// <param name="amount"></param>
    public void GainMoney(int amount)
    {
        money += amount;
        FindObjectOfType<MoneyDisplay>().UpdateDisplay(money);
        ReachRequirementDealer();
    }

    /// <summary>
    /// Display the current amount on the screen
    /// </summary>
    /// <returns></returns>
    public int GetCurrentMoneyAmount()
    {
        return money;
    }

    public void YouWin()
    {
        //TODO:VFX
        winLabel.SetActive(true);
    }

    public void YouLose()
    {
        //TODO:VFX
        loseLabel.SetActive(true);
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

    public void UnlockGeneral()
    {
        if (HasAlreadyGotIt(TagName.General))
        {
            return;
        }

        hasGeneral = true;
        FindObjectOfType<KeyManager>().UpdateEssentialDisplay(TagName.General);
    }

    private void ReachRequirementDealer()
    {
        if (money >= minimumCoinRequirement)
        {
            hasEnoughCoin = true;
            FindObjectOfType<KeyManager>().UpdateEssentialDisplay(TagName.Coin);
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
            default:
                break;
        }
        FindObjectOfType<KeyManager>().UpdateEssentialDisplay(tagName);
        StartCoroutine(ShowEncouragingMessage(tagName));
    }

    private bool HasAlreadyGotIt(TagName tagName)
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
            default:
                return false;
        }
    }

    private IEnumerator ShowEncouragingMessage(TagName tagName)
    {
        switch (tagName)
        {
            case TagName.Book:
                bookLabel.SetActive(true);
                break;
            case TagName.Coconut:
                foodLabel.SetActive(true);
                break;
            case TagName.Seed:
                seedLabel.SetActive(true);
                break;
            default:
                break;
        }

        IsGamePaused = true;

        yield return new WaitForSeconds(showingLabelTime);
        SetAllLabelsOff();
        IsGamePaused = false;
    }

    private void SetAllLabelsOff()
    {
        if (winLabel)
        {
            winLabel.SetActive(false);
        }

        if (loseLabel)
        {
            loseLabel.SetActive(false);
        }

        if (seedLabel)
        {
            seedLabel.SetActive(false);
        }

        if (foodLabel)
        {
            foodLabel.SetActive(false);
        }

        if (bookLabel)
        {
            bookLabel.SetActive(false);
        }

        if (infoLabel)
        {
            infoLabel.SetActive(false);
        }

        
    }

    public void ShowInfoLabel()
    {
        if (infoLabel)
        {
            infoLabel.SetActive(true);
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
