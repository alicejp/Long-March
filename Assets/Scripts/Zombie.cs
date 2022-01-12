using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    public float maxDistanceDelta = 10f;
    public float waitTime = 30f;
    public bool isHit = false;
    public int maximumHit = 3;
    public int currentHit = 0;
    public GameObject projectileVFX;
    public float durationOfExplosion = 1f;
    public int hitPoint = 20;

    //Use it to tune the zombie's volume
    public float distanceWithPlayer = 20f;

    Player thePlayer;
    GameSession gameSession;
    Animator animator;
    Collider2D myCollider2D;

    private void Awake()
    {
        thePlayer = FindObjectOfType<Player>();
        gameSession = FindObjectOfType<GameSession>();
        animator = GetComponent<Animator>();
        myCollider2D = GetComponent<Collider2D>();
    }

    private Transform PlayerTransform
    {
        get
        {
            return thePlayer.transform;
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

    void Update()
    {
        if (gameSession.IsGamePaused)
        {
            return;
        }

        if (gameSession.LightHouseSwitchIsOn)
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

        distanceWithPlayer = Vector3.Distance(player.position, movingToward);
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

    // Shoot projectile
    private void PlayProjectileVFX()
    {
        if (projectileVFX)
        {
            GameObject visualEffect = Instantiate(projectileVFX, transform.position, Quaternion.identity);
            Destroy(visualEffect, durationOfExplosion);
        }
    }

    //Update Animator's shouldDuck variable
    private IEnumerator DuckFor30Secs()
    {
        animator.SetBool("shouldDuck", true);
        yield return new WaitForSeconds(waitTime);
        animator.SetBool("shouldDuck", false);
        currentHit = 0;
    }

    //Zombie should stop when reaching Hazards and Ocean
    private bool ShouldStop()
    {
        //Afraid of water
        return myCollider2D.IsTouchingLayers(LayerMask.GetMask("Hazards", "Ocean"));
    }
}
