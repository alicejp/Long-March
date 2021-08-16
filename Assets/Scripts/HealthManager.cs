using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [SerializeField] int minHealthPoint = 0;
    [SerializeField] int currentPoint = 300;
    // Start is called before the first frame update
    void Start()
    {
        UpdateUI(currentPoint);
    }

    

    public int GetCurrentHealthPoint()
    {
        return currentPoint;
    }

    public void ReduceHealthPoint(int damage)
    {
        if (currentPoint > minHealthPoint) {
            currentPoint -= damage;
            HealthDealer();
            UpdateUI(currentPoint);
        }
    }

    private void HealthDealer()
    {
        if (currentPoint <= minHealthPoint)
        {
            StartCoroutine(FindObjectOfType<GameSession>().YouLose());
        }
    }

    public void UpdateUI(int currentPoint)
    {
        var displayes = FindObjectsOfType<HealthDisplay>();
        foreach (HealthDisplay display in displayes)
        {
            if ( currentPoint >= 250 )
            {
                if (display.tag == "Health1" || display.tag == "Health2"|| display.tag == "Health3")
                {
                    display.HightLight();
                }else
                {
                    display.SetColorToClear();
                }
            }
            else if (currentPoint < 250 && currentPoint >= 150)
            {
                if (display.tag == "Health1" || display.tag == "Health2")
                {
                    display.HightLight();
                }
                else
                {
                    display.SetColorToClear();
                }
            }
            else if (currentPoint < 150 && currentPoint > 0)
            {
                if (display.tag == "Health1")
                {
                    display.HightLight();
                }
                else
                {
                    display.SetColorToClear();
                }
            }
            else if (currentPoint <= 0)
            {
                display.SetColorToClear();
            }

            if (display.tag == "HealthText")
            {
                display.UpdateText(currentPoint);
            }
        }
    }
}
