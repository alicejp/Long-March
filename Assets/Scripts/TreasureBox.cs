using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TreasureBox : MonoBehaviour
{

    [SerializeField] GameObject quizLabel;
    [SerializeField] GameObject badLuckLabel;
    [SerializeField] GameObject hitLabel;
    [SerializeField] bool isOpen = false;

    Color greyoutColor = new Color(1, 1, 1, 0.25f);
    // Start is called before the first frame update
    void Start()
    {
        quizLabel.SetActive(false);
        badLuckLabel.SetActive(false);
        hitLabel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateUI();
        DestoryDealer();
    }

    private void UpdateUI()
    {
        GetComponent<SpriteRenderer>().color = isOpen ? greyoutColor : Color.white;
    }

    private void DestoryDealer()
    {
        if (!quizLabel)
        {
            Destroy(gameObject);
        }
        else if (!badLuckLabel)
        {
            Destroy(gameObject);
        }
    }

    private bool HasEnoughKeys
    {
        get
        {
            return FindObjectOfType<KeyManager>().HasEnoughKeys();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.gameObject.GetComponent<Player>();

        if (!player)
        {
            return;
        }

        if (isOpen)
        {
            return;
        }
        
        if (HasEnoughKeys)
        {
            FindObjectOfType<KeyManager>().ResetToOriginal();

            if (tag == "Gold")
            {
                quizLabel.SetActive(true);
            }
            else if (tag == "Glisten")
            {
                badLuckLabel.SetActive(true);
            }

            isOpen = true;
        }
        else
        {
            hitLabel.SetActive(true);
        }
    }
}
