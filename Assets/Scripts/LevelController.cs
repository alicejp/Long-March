using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] GameObject winLabel;
    [SerializeField] GameObject loseLabel;
    [SerializeField] GameObject seedLabel;
    [SerializeField] GameObject foodLabel;
    [SerializeField] GameObject bookLabel;
    [SerializeField] GameObject infoLabel;
    [SerializeField] GameObject commanderLabel;
    [SerializeField] GameObject mapLabel;
    [SerializeField] float showingLabelTime = 2f;
    // Start is called before the first frame update
    void Start()
    {
        SetAllLabelsOff();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowWinLabel()
    {
        if (GetComponent<AudioSource>())
        {
            GetComponent<AudioSource>().Play();
        }
        winLabel.SetActive(true);
    }

    public void ShowLoseLabel()
    {
        //TODO:VFX
        loseLabel.SetActive(true);
    }

    public void ShowInfoLabel(string tagName = "Hint")
    {
        if (tagName == "Map" && mapLabel)
        {
           mapLabel.SetActive(true);
           FindObjectOfType<GameSession>().IsGamePaused = true;
        }
        else
        {
            if (infoLabel)
            {
                infoLabel.SetActive(true);
                FindObjectOfType<GameSession>().IsGamePaused = true;
            }
        }
    }

    public IEnumerator ShowEncouragingMessage(TagName tagName)
    {
        switch (tagName)
        {
            case TagName.Book:
                bookLabel.SetActive(true);
                break;
            case TagName.Coconut:
                foodLabel.SetActive(true);
                break;
            case TagName.Seed:
                seedLabel.SetActive(true);
                break;
            case TagName.General:
                commanderLabel.SetActive(true);
                break;
            default:
                break;
        }

        yield return new WaitForSeconds(showingLabelTime);
        SetAllLabelsOff();
    }

    private void SetAllLabelsOff()
    {
        if (winLabel)
        {
            winLabel.SetActive(false);
        }

        if (loseLabel)
        {
            loseLabel.SetActive(false);
        }

        if (seedLabel)
        {
            seedLabel.SetActive(false);
        }

        if (foodLabel)
        {
            foodLabel.SetActive(false);
        }

        if (bookLabel)
        {
            bookLabel.SetActive(false);
        }

        if (infoLabel)
        {
            infoLabel.SetActive(false);
        }

        if (commanderLabel)
        {
            commanderLabel.SetActive(false);
        }

        if (mapLabel)
        {
            mapLabel.SetActive(false);
        }
    }
}
