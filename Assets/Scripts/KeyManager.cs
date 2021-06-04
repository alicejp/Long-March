using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyManager : MonoBehaviour
{
    [SerializeField] int keyCount = 1;// Game start with the player already has one key in the pocket.
    [SerializeField] int maxCount = 3;


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
        var displays = FindObjectsOfType<KeyDisplay>();
        foreach(KeyDisplay display in displays)
        {
            display.UpdatUI(keyCount);
        }
    }

    public void UpdateEssentialDisplay(TagName tagName)
    {
        var displays = FindObjectsOfType<KeyDisplay>();
        foreach (KeyDisplay display in displays)
        {
            display.UpdateEssentialDisplay(tagName);
        }
    }

    public int GetKeyCount()
    {
        return keyCount;
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
