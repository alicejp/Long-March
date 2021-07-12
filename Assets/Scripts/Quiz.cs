using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quiz : MonoBehaviour
{
    [SerializeField] GameObject wrongLabel;
    // Start is called before the first frame update
    void Start()
    {
        if (wrongLabel)
        {
            wrongLabel.SetActive(false);
        }
        GetKeyboardInput();
    }

    // Update is called once per frame
    void Update()
    {
        GetKeyboardInput();
    }

    private void GetKeyboardInput()
    {
        if (Input.GetKey(KeyCode.Alpha3))
        {
            UnlockGeneral();
        }
        else if ((Input.GetKey(KeyCode.Alpha1)) || (Input.GetKey(KeyCode.Alpha2)))
        {
            ShowWrongMessage();
        }
    }

    private void UnlockGeneral()
    {
        FindObjectOfType<GameSession>().GotTheEssential(TagName.General);
        gameObject.SetActive(false);
    }

    private void ShowWrongMessage()
    {
        wrongLabel.SetActive(true);
        gameObject.SetActive(false);
    }
}
