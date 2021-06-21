using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("SFX")]
    [SerializeField] AudioClip shootSound;
    [SerializeField] [Range(0, 1)] float shootSoundVolume = 0.25f;

    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 100f;
    [SerializeField] float projectileFiringPeriod = 1f;
    [SerializeField] bool ableToFire = true;

    [SerializeField] float moveSpeed = 7f;

    Coroutine coroutine;

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

        FireByButton();
        Move();
        GetEssentials();
        Die();
    }

    private float VelocityX
    {
        get
        {
            return GetComponent<Rigidbody2D>().velocity.x;
        }
    }

    private float VelocityY
    {
        get
        {
            return GetComponent<Rigidbody2D>().velocity.y;
        }
    }

    private float HorizontalInput
    {
        get
        {
            return Input.GetAxis("Horizontal");
        }
    }

    private float VerticalInput
    {
        get
        {
            return Input.GetAxis("Vertical");
        }
    }

    private bool Fire
    {
        get
        {
            return Input.GetKeyDown(KeyCode.Space);
        }
    }

    private bool StopFire
    {
        get
        {
            return Input.GetKeyUp(KeyCode.Space);
        }
    }

    private Vector2 HorizontalSpeedByInput
    {
        get
        {
            return new Vector2(HorizontalInput * moveSpeed, VelocityY);
        }
    }

    private Vector2 VerticalSpeedByInput
    {
        get
        {
            return new Vector2(VelocityX, VerticalInput * moveSpeed);
        }
    }

    private PlayerLayerMaskManager LayerMaskManager
    {
        get
        {
            return GetComponent<PlayerLayerMaskManager>();
        }
    }

    private void Move()
    {
        if (Mathf.Abs(HorizontalInput) > Mathf.Epsilon)
        {
            MovePlayerHorizontally();
        }
        else if (Mathf.Abs(VerticalInput) > Mathf.Epsilon)
        {
            MovePlayerVertically(); 
        }

        if (LayerMaskManager.TouchedTheTunnel)
        {
            FindObjectOfType<LevelLoader>().LoadTunnelScene();
        }

        if (LayerMaskManager.TouchedTheBoat)
        {
            var velocity = GetComponent<Rigidbody2D>().velocity;            
        }
        
        UpdateAnimatorIsRunningState();
    }

    public void MoveAutomatically(Vector2 velocity)
    {
        GetComponent<Rigidbody2D>().velocity = velocity;
    }

    private void FireByButton()
    {
        if (!ableToFire) return;

        if (Fire)
        {
            coroutine = StartCoroutine(FireBubble());
        }

        if(StopFire)
        {
            GetComponent<Animator>().SetBool("IsShooting", false);
            StopCoroutine(coroutine);
        }
    }

    private IEnumerator FireBubble()
    {
        FireBubbles();
        if (shootSound != null)
        {
            AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootSoundVolume);
        }
        yield return new WaitForSeconds(projectileFiringPeriod);
    }

    private void FireBubbles()
    {
        if (projectilePrefab == null)
        {
            return;
        }

        GetComponent<Animator>().SetBool("IsShooting", true);
        GameObject icePrefab = Instantiate(projectilePrefab, transform.position, Quaternion.identity) as GameObject;
        
        if (VerticalInput != 0f)
        {
            icePrefab.GetComponent<Ice>().SetSpeed(new Vector2(0, projectileSpeed * Time.deltaTime), Mathf.Sign(VelocityY));
        }
        else
        {
            icePrefab.GetComponent<Ice>().SetSpeed(new Vector2(projectileSpeed * Time.deltaTime, 0), Mathf.Sign(VelocityX));
        }
    }

    private void MovePlayerHorizontally()
    {
        GetComponent<Rigidbody2D>().velocity = HorizontalSpeedByInput;
        UpdateAnimatorIsRunningState();
        FacingTheRightSide(HorizontalInput);
    }

    private void MovePlayerVertically()
    {
        GetComponent<Rigidbody2D>().velocity = VerticalSpeedByInput;
        UpdateAnimatorIsRunningState();
    }

    private void Die()
    {
        if (LayerMaskManager.TouchedTheHazradAndWater)
        {
            if (LayerMaskManager.TouchedTheBoat)
            {
                //We are on the boat, so it is fine
            }
            else
            {
                //TODO: death kick
                FindObjectOfType<GameSession>().YouLose();
            }            
        }
    }

    private void GetEssentials()
    {
        if (LayerMaskManager.TouchedTheBook)
        {
            FindObjectOfType<GameSession>().GotTheEssential(TagName.Book);
        }

        if (LayerMaskManager.TouchedTheCoconut)
        {
            FindObjectOfType<GameSession>().GotTheEssential(TagName.Coconut);
        }

        if (LayerMaskManager.TouchedTheSeed)
        {
            FindObjectOfType<GameSession>().GotTheEssential(TagName.Seed);
        }
    }


    private void UpdateAnimatorIsRunningState()
    {
        bool hasHorizontalSpeed = Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) > Mathf.Epsilon; 
        GetComponent<Animator>().SetBool("IsRunning", hasHorizontalSpeed);
    }

    private void FacingTheRightSide(float horizontalInput)
    {
        float sign = Mathf.Sign(horizontalInput);
        transform.localScale = new Vector2(sign, 1f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var zombie = collision.gameObject.GetComponent<Zombie>();
        if (zombie)
        {
            FindObjectOfType<HealthManager>().ReduceHealthPoint(zombie.GetHitPoint);
            //VFX
        }
    }
}
