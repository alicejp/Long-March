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

    [Header("Speed")]
    [SerializeField] float moveSpeed = 7f;
    [SerializeField] float maxDistanceDelta = 10f;
    [SerializeField] Vector2 deathKick = new Vector2(0, 25f);

    [Header("Particles")]
    [SerializeField] ParticleSystem crashParticles;

    private bool moveAutomatically = false;

    Coroutine coroutine;

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

    // Update is called once per frame
    void Update()
    {
        if (FindObjectOfType<GameSession>().IsGamePaused)
        {
            return;
        }

        if (LayerMaskManager.TouchedTheBoat && FindObjectOfType<GameSession>().ShouldMoveToZLightHouse == false)
        {
            FindObjectOfType<GameSession>().ShouldMoveToZLightHouse = true;
            // If we touch the boat, then we hide the boat
            FindObjectOfType<Boat>().gameObject.SetActive(false);
            moveAutomatically = true;
            return;
        }

        if (FindObjectOfType<GameSession>().ShouldMoveToZLightHouse)
        {
            MoveForwardToZombieLand();
            return;
        }

        if (FindObjectOfType<GameSession>().LightHouseSwitchIsOn)
        {
            SwitchSprite(true);
            MoveForwardToFormosa();
            return;
        }

        FireByButton();
        Move();
        GetEssentials();
        Die();
    }
    private void Move()
    {

        if (moveAutomatically)
        {
            return;
        }

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

        UpdateAnimatorIsRunningState();
    }

    public void SwitchSprite(bool shouldBeShip)
    {
        moveAutomatically = shouldBeShip;
        if (shouldBeShip)
        {
             //change sprite to boat
            GetComponent<Animator>().SetBool("IsSailing", true);
            GetComponent<Animator>().SetBool("IsRunning", false);
            return;
        }

        GetComponent<Animator>().SetBool("IsSailing", false);
        GetComponent<Animator>().SetBool("IsRunning", true);
    }

    public void MoveForwardToFormosa()
    {
        FindObjectOfType<Ocean>().SetCollider2DEnable(false);
        
        GameObject formosa = GameObject.FindGameObjectWithTag("Formosa");
        if (formosa)
        {
            Debug.Log("Yes, found formosa : " + formosa.transform.position);
            FindObjectOfType<Player>().MoveAutomatically(formosa.transform);
        }
    }

    public void MoveForwardToZombieLand()
    {
        FindObjectOfType<Ocean>().SetCollider2DEnable(false);
        GameObject zombieLand = GameObject.FindGameObjectWithTag("ZombieLand");
        if (zombieLand)
        {
            Debug.Log("Yes, found zombieLand : " + zombieLand.transform.position);
            FindObjectOfType<Player>().MoveAutomatically(zombieLand.transform);

            if (transform.position == zombieLand.transform.position)
            {
                FindObjectOfType<GameSession>().ShouldMoveToZLightHouse = false;
            }
        }
    }

    public void MoveAutomatically(Transform target)
    {
        moveAutomatically = true;
        MoveTowardToTheTarget(target);
    }

    private void MoveTowardToTheTarget(Transform target)
    {
        float step = maxDistanceDelta * Time.deltaTime;
        var movingToward = Vector3.MoveTowards(transform.position, target.position, step);
        transform.position = movingToward;
        Debug.Log("MoveTowardToTheTargetd : " + movingToward);
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
        //If the player touches 
        if (LayerMaskManager.TouchedTheHazradAndWater)
        {
            if (moveAutomatically)
            {
                //We are on the boat, so it is fine
            }
            else
            {
                GetComponent<Animator>().SetTrigger("Dying");
                GetComponent<Rigidbody2D>().velocity = deathKick;
                if (crashParticles)
                {
                    crashParticles.Play();
                }
                
                StartCoroutine(FindObjectOfType<GameSession>().YouLose());
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
        if (moveAutomatically)
        {
            GetComponent<Animator>().SetBool("IsRunning", false);
            return;
        }
        bool hasHorizontalSpeed = Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) > Mathf.Epsilon; 
        GetComponent<Animator>().SetBool("IsRunning", hasHorizontalSpeed);
    }

    private void FacingTheRightSide(float horizontalInput)
    {
        float sign = Mathf.Sign(horizontalInput);
        transform.localScale = new Vector2(sign * 0.8f, 0.8f);
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
