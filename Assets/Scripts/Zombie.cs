using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    [SerializeField] float maxDistanceDelta = 10f;
    [SerializeField] float waitTime = 30f;
    [SerializeField] bool isHit = false;
    [SerializeField] int maximumHit = 3;
    [SerializeField] int currentHit = 0;
    [SerializeField] GameObject projectileVFX;
    [SerializeField] float durationOfExplosion = 1f;
    [SerializeField] int hitPoint = 20;
    private Transform PlayerTransform
    {
        get
        {
            return FindObjectOfType<Player>().transform;
        }
    }

    public int GetHitPoint
    {
        get
        {
            return hitPoint;
        }
    }

    private bool IsHit
    {
        get
        {
            isHit = currentHit >= maximumHit;
            return isHit;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (FindObjectOfType<GameSession>().IsGamePaused)
        {
            return;
        }

        if (FindObjectOfType<GameSession>().LightHouseSwitchIsOn)
        {
            return;
        }

        if (IsHit || ShouldStop())
        {
            return;
        }

        MoveTowardToTheTarget(PlayerTransform);
    }

    private void MoveTowardToTheTarget(Transform player)
    {
        float step = maxDistanceDelta * Time.deltaTime;
        var movingToward = Vector3.MoveTowards(transform.position, player.position, step);
        transform.position = movingToward;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var projectile = collision.gameObject.GetComponent<Ice>();
        if (projectile)
        {
            if (IsHit)
            {
                StartCoroutine(DuckFor30Secs());
            }
            projectile.Melt();
            PlayProjectileVFX();
            currentHit++;
        }
    }

    private void PlayProjectileVFX()
    {
        if (projectileVFX)
        {
            GameObject visualEffect = Instantiate(projectileVFX, transform.position, Quaternion.identity);
            Destroy(visualEffect, durationOfExplosion);
        }
    }

    private IEnumerator DuckFor30Secs()
    {
        GetComponent<Animator>().SetBool("shouldDuck", true);
        yield return new WaitForSeconds(waitTime);
        GetComponent<Animator>().SetBool("shouldDuck", false);
        currentHit = 0;
    }
    private bool ShouldStop()
    {
        //Afraid of water
        return GetComponent<Collider2D>().IsTouchingLayers(LayerMask.GetMask("Hazards", "Ocean"));
    }
}
