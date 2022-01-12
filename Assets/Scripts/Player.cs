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
    GameSession gameSession;
    Rigidbody2D myRigidbody2D;
    Animator animator;

    private float VelocityX
    {
        get
        {
            return myRigidbody2D.velocity.x;
        }
    }
    private float VelocityY
    {
        get
        {
            return myRigidbody2D.velocity.y;
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

    private void Awake()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        gameSession = FindObjectOfType<GameSession>();
    }

    void Update()
    {
        if (gameSession.IsGamePaused)
        {
            return;
        }

        if (LayerMaskManager.TouchedTheBoat && gameSession.ShouldMoveToZLightHouse == false)
        {
            gameSession.ShouldMoveToZLightHouse = true;
            // If we touch the boat, then we hide the boat
            FindObjectOfType<Boat>().gameObject.SetActive(false);
            moveAutomatically = true;
            return;
        }

        if (gameSession.ShouldMoveToZLightHouse)
        {
            MoveForwardToZombieLand();
            return;
        }

        if (gameSession.LightHouseSwitchIsOn)
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
            animator.SetBool("IsSailing", true);
            animator.SetBool("IsRunning", false);
            return;
        }

        animator.SetBool("IsSailing", false);
        animator.SetBool("IsRunning", true);
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
                gameSession.ShouldMoveToZLightHouse = false;
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
            animator.SetBool("IsShooting", false);
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

        animator.SetBool("IsShooting", true);
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
        myRigidbody2D.velocity = HorizontalSpeedByInput;
        UpdateAnimatorIsRunningState();
        FacingTheRightSide(HorizontalInput);
    }

    private void MovePlayerVertically()
    {
        myRigidbody2D.velocity = VerticalSpeedByInput;
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
                animator.SetTrigger("Dying");
                myRigidbody2D.velocity = deathKick;
                if (crashParticles)
                {
                    crashParticles.Play();
                }
                
                StartCoroutine(gameSession.YouLose());
            }            
        }
    }

    private void GetEssentials()
    {
        if (LayerMaskManager.TouchedTheBook)
        {
            gameSession.GotTheEssential(TagName.Book);
        }

        if (LayerMaskManager.TouchedTheCoconut)
        {
            gameSession.GotTheEssential(TagName.Coconut);
        }

        if (LayerMaskManager.TouchedTheSeed)
        {
            gameSession.GotTheEssential(TagName.Seed);
        }
    }
    private void UpdateAnimatorIsRunningState()
    {
        if (moveAutomatically)
        {
            animator.SetBool("IsRunning", false);
            return;
        }
        bool hasHorizontalSpeed = Mathf.Abs(myRigidbody2D.velocity.x) > Mathf.Epsilon;
        animator.SetBool("IsRunning", hasHorizontalSpeed);
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
