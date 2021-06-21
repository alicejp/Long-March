using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyManager : MonoBehaviour
{
    [SerializeField] int keyCount = 1;// Game start with the player already has one key in the pocket.
    [SerializeField] int maxCount = 3;

    private void Start()
    {
        UpdateKeyDisplays();
        UpdateEssentialDisplays();
    }

    private KeyDisplay[] KeyDisplays
    {
        get
        {
            return FindObjectsOfType<KeyDisplay>();
        }
    }

    /// <summary>
    /// Increase 1 when the player collect one of the key
    /// </summary>
    public void AddKeyCount()
    {
        if (keyCount == maxCount)
        {
            return;
        }

        keyCount++;
        UpdateKeyDisplays();
    }



    private void UpdateKeyDisplays()
    {
        for(int i = 0; i< KeyDisplays.Length; i++)
        {
            KeyHandler(KeyDisplays[i]);
        }
    }

    // If key count == 1, key1 is true
    // key count == 2, key1, key2 is true
    // key count == 3, key1, key2, key3 is true
    private void KeyHandler(KeyDisplay display)
    {
        if (keyCount == 1)
        {
            display.UpdateKeyDisplay(new string[] { "Key1" });
        }
        else if (keyCount == 2)
        {
            display.UpdateKeyDisplay(new string[] { "Key1", "Key2" });
        }
        else if (keyCount == 3)
        {
            display.UpdateKeyDisplay(new string[] { "Key1", "Key2", "Key3" });
        }
    }

    private void UpdateEssentialDisplays()
    {
        var displays = FindObjectsOfType<EssentialsDisplay>();
        foreach (EssentialsDisplay display in displays)
        {
            EssentialHandler(display);
        }
    }

    private void EssentialHandler(EssentialsDisplay essentialKeyDisplay)
    {
        TagName tagName = TapNameMapper(essentialKeyDisplay.tag);
        bool gotIt = FindObjectOfType<GameSession>().HasAlreadyGotIt(tagName);
        essentialKeyDisplay.UpdateEssentialDisplay(tagName, gotIt);
    }

    private TagName TapNameMapper(string displayTag)
    {
        TagName tagName = TagName.Default;

        if (displayTag == TagName.Book.ToString())
        {
            tagName = TagName.Book;
        }

        if (displayTag == TagName.Coconut.ToString())
        {
            tagName = TagName.Coconut;
        }

        if (displayTag == TagName.Coin.ToString())
        {
            tagName = TagName.Coin;
        }

        if (displayTag == TagName.General.ToString())
        {
            tagName = TagName.General;
        }

        if (displayTag == TagName.Seed.ToString())
        {
            tagName = TagName.Seed;
        }

        return tagName;
    }

    public void UpdateEssentialDisplay(TagName tagName)
    {
        var displays = FindObjectsOfType<EssentialsDisplay>();
        foreach (EssentialsDisplay display in displays)
        {
            display.UpdateEssentialDisplay(tagName);
        }
    }

    public bool HasEnoughKeys()
    {
        return keyCount >= maxCount;
    }

    public void ResetToOriginal()
    {
        keyCount = 1;
        UpdateKeyDisplays();
    }

}
