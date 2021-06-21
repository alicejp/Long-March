using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EssentialsDisplay : MonoBehaviour
{
    [SerializeField] bool isHighLighted = false;
    Color greyoutColor = new Color(1, 1, 1, 0.25f);
    // Start is called before the first frame update
    void Start()
    {
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        GetComponent<Image>().color = isHighLighted ? Color.white : greyoutColor;
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
    Coin,
    Default
}
