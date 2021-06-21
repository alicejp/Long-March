using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyDisplay : MonoBehaviour
{
    Color greyoutColor = new Color(1, 1, 1, 0.25f);

    public void UpdateKeyDisplay(string[] tagNames, bool gotIt = true)
    {
        foreach (String tagName in tagNames)
        {
            if (tag == tagName)
            {
                GetComponent<Image>().color = gotIt ? Color.white : greyoutColor;
                break;
            }

            GetComponent<Image>().color = greyoutColor;
        }
    }
}
