using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] int coinValue = 100;
    [SerializeField] AudioClip pickUpSFX;
    [SerializeField] GameObject pickupVFX;
    [SerializeField] float soundVolumn = 0.25f;
    [SerializeField] float durationOfExplosion = 0.5f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.gameObject.GetComponent<Player>();

        if (!player)
        {
            return;
        }

        PlaySFX();
        PlayVFX();
        FindObjectOfType<MoneyDisplay>().GainMoney(coinValue);
        Destroy(gameObject);
    }

    private void PlayVFX()
    {
        if (pickupVFX)
        {
            GameObject visualEffect = Instantiate(pickupVFX, transform.position, Quaternion.identity);
            Destroy(visualEffect, durationOfExplosion);
        }
    }

    private void PlaySFX()
    {
        if (pickUpSFX)
        {
            AudioSource.PlayClipAtPoint(pickUpSFX, Camera.main.transform.position, soundVolumn);
        }
    }
}
