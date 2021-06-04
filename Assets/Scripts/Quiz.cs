using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quiz : MonoBehaviour
{
    [SerializeField] float waitForDismissTime = 2f;
    [SerializeField] GameObject wrongLabel;
    [SerializeField] GameObject generalLabel;
    // Start is called before the first frame update
    void Start()
    {
        wrongLabel.SetActive(false);
        generalLabel.SetActive(false);
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
            StartCoroutine(UnlockGeneral());
        }else if ((Input.GetKey(KeyCode.Alpha1)) || (Input.GetKey(KeyCode.Alpha2)))
        {
            StartCoroutine(ShowWrongMessage());
        }
    }

    private IEnumerator UnlockGeneral()
    {
        generalLabel.SetActive(true);
        yield return new WaitForSeconds(waitForDismissTime);
        generalLabel.SetActive(false);
        FindObjectOfType<GameSession>().UnlockGeneral();
        gameObject.SetActive(false);
    }

    private IEnumerator ShowWrongMessage()
    {
        wrongLabel.SetActive(true);
        yield return new WaitForSeconds(waitForDismissTime);
        wrongLabel.SetActive(false);
        gameObject.SetActive(false);
    }
}
