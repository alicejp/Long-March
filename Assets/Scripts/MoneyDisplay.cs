using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyDisplay : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Text>().text = "0";
    }

    public void UpdateDisplay(int aomunt)
    {
        GetComponent<Text>().text = aomunt.ToString();
    }
}
