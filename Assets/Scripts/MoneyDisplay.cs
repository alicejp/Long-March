using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyDisplay : MonoBehaviour
{
    [SerializeField] int currentMoney = 100;
    [SerializeField] int minimumCoinRequirement = 500;

    private void Start()
    {
        UpdateDisplay();
    }

    /// <summary>
    /// Display the current amount on the screen
    /// </summary>
    /// <returns></returns>
    public int GetCurrentMoneyAmount()
    {
        return currentMoney;
    }

    /// <summary>
    /// Gain money by crashing the coin sprite
    /// </summary>
    /// <param name="amount"></param>
    public void GainMoney(int amount)
    {
        currentMoney += amount;
        UpdateDisplay();
        ReachRequirementDealer();
    }
    
    private void UpdateDisplay()
    {
        GetComponent<Text>().text = currentMoney.ToString();
    }

    private void ReachRequirementDealer()
    {
        if (currentMoney >= minimumCoinRequirement)
        {
            FindObjectOfType<GameSession>().HasEnoughCoin = true;
            FindObjectOfType<KeyManager>().UpdateEssentialDisplay(TagName.Coin);
        }
    }
}
