using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Show How to play label when the botton on click.
public class HowButton : MonoBehaviour
{
    [SerializeField] GameObject howLabel;


    void Start()
    {
        Button btn = gameObject.GetComponent<Button>();
        btn.onClick.AddListener(ShowHowToPlayLabel);
        howLabel.SetActive(false);
    }
    private void ShowHowToPlayLabel()
    {
        howLabel.SetActive(true);
    }
}
