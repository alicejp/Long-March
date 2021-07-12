using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButttonDisplay : MonoBehaviour
{
    private void Start()
    {
        Button btn = gameObject.GetComponent<Button>();
        btn.onClick.AddListener(CloseOnClick);
    }
    private void CloseOnClick()
    {
        gameObject.transform.parent.gameObject.SetActive(false);
        FindObjectOfType<GameSession>().IsGamePaused = false;
    }
}
