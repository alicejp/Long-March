using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyDisplay : MonoBehaviour
{
    Color greyoutColor = new Color(1, 1, 1, 0.25f);

    // Start is called before the first frame update
    void Start()
    {
        int keyCount = FindObjectOfType<KeyManager>().GetKeyCount();
        UpdatUI(keyCount);
    }

    public void UpdatUI(int keyCount)
    {
        GetComponent<Image>().color = greyoutColor;
        if (keyCount == 1)
        {
            if (tag == "Key1")
            {
                GetComponent<Image>().color = Color.white;
            }
            else
            {
                GetComponent<Image>().color = greyoutColor;
            }
        }
        else if (keyCount == 2)
        {
            if (tag == "Key1" || tag == "Key2")
            {
                GetComponent<Image>().color = Color.white;
            }
            else
            {
                GetComponent<Image>().color = greyoutColor;
            }
        }
        else if (keyCount == 3)
        {
            if (tag == "Key1" || tag == "Key2" || tag == "Key3")
            {
                GetComponent<Image>().color = Color.white;
            }
            else
            {
                GetComponent<Image>().color = greyoutColor;
            }
               
        }
    }

    public void UpdateEssentialDisplay(TagName tagName, bool gotIt = true)
    {
        if (tag == tagName.ToString())
        {
            GetComponent<Image>().color = gotIt ? Color.white : greyoutColor;
        }
    }
}

public enum TagName
{
    General,
    Book,
    Coconut,
    Seed,
    Coin
}
