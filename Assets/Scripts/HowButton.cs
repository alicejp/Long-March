using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HowButton : MonoBehaviour
{
    [SerializeField] GameObject howLabel;
    // Start is called before the first frame update
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
