using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadLuck : MonoBehaviour
{
    [SerializeField] float waitToClose = 2f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MessageCloseCountDown());
    }

    private IEnumerator MessageCloseCountDown()
    {
        yield return new WaitForSeconds(waitToClose);
        gameObject.SetActive(false);
    }
}
