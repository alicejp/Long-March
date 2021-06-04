using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Hint : MonoBehaviour
{
    [SerializeField] float waitToClose = 0.5f;
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

    public void CloseTheLabel()
    {
        gameObject.SetActive(false);
    }
}
