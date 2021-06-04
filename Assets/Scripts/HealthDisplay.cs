using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    Text healthText;
    // Start is called before the first frame update
    void Start()
    {
        healthText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateText(int healthPoint)
    {
        if (healthText)
        {
            healthText.text = healthPoint.ToString();
        }
    }

    public void HightLight()
    {
        if (GetComponent<Image>())
        {
            GetComponent<Image>().color = Color.white;
        }
    }

    public void SetColorToClear()
    {
        if (GetComponent<Image>())
        {
            GetComponent<Image>().color = Color.clear;
        }
    }
}
