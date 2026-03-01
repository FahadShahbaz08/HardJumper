using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HardRunner.Managers;
using Solo.MOST_IN_ONE;
using System;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{

    private CharacterController controller;
    private Vector3 direction;
    [SerializeField] private float forwardSpeed;

    [SerializeField] private float startSpeed = 8f;
    [SerializeField] private float maxSpeed = 20f;
    [SerializeField] private float acceleration = 0.5f;

    private float currentSpeed;


    public int jumpCount, totalScore;
    public bool playerActive = false;
    public bool swap = false;

    Rigidbody characterRGBD;
    public Vector3 characterPos;

    public float timer1, spawnTimer, scoreTimer;
    private float gravityValue = -1.81f;

    public GameObject deathParticle;
    public Animator anim;
    [SerializeField]
    GameObject playerChild;


    public GameObject pauseGo;
    private Vector2 startTouchPosition, endTouchPosition;

    float HInput;
    bool isSliding = false;
    float slideTime = 0.8f;
    public CapsuleCollider capCollider;
    private float targetXPosition;
    private float horizontalSpeed = 10f;

    private GameSceneUiManager gameSceneUiManager;
    private CameraShake camShake;

    public bool isJumping = false;

    [SerializeField] ParticleSystem coinVfx;
    [SerializeField] float coinVfxDuration = 0.5f;


    // ================= POWER UPS =================
    [Header("Power Ups")]

    // Magnet
    [SerializeField] private float magnetDuration = 8f;
    [SerializeField] private float magnetRadiusZ = 6f;
    [SerializeField] private float magnetRadiusX = 3f;
    private bool magnetActive = false;
    private float magnetTimer;
    [SerializeField] private Slider magnetSlider;
    [SerializeField] private GameObject magnetUI;

    [SerializeField] private float magnetRadiusY = 15f; // height (NEW)
    [SerializeField] private float magnetPullSpeed = 25f;
    // Shield
    [SerializeField] private float shieldDuration = 6f;
    private bool shieldActive = false;
    private float shieldTimer;
    [SerializeField] private Slider shieldSlider;
    [SerializeField] private GameObject shieldUI;

    [SerializeField] private GameObject shieldBubble;

    // ================= HIGH JUMP POWER UP =================
    [Header("High Jump PowerUp")]
    [SerializeField] private float highJumpForce = 18f;
    [SerializeField] private float normalJumpForce = 10f;
    [SerializeField] private float highJumpDuration = 5f;

    private bool highJumpActive = false;
    private float highJumpTimer = 0f;
    private bool isGrounded = false;

    [SerializeField] private Slider highJumpSlider;
    [SerializeField] private GameObject highJumpUI;

    public static Action<int> CoinsCollecd;

    [SerializeField] ParticleSystem destructionParticle;

    private bool gameStarted = false;
    private bool canReadInput = false;
    public static int CurrentRunCoins = 0;

    // Start is called before the first frame update
    void Start()
    {
        //RunTrigger
        characterRGBD = GetComponent<Rigidbody>();
        playerChild = GameObject.Find("PlayerChild");
        anim.SetTrigger("IdleTriggerBoy");


        //pauseGo = GameObject.Find("PauseGO");

        timer1 = 0f;
        jumpCount = 1;
        totalScore = 0;

        targetXPosition = transform.position.x;

        gameSceneUiManager = FindAnyObjectByType<GameSceneUiManager>();
        camShake = Camera.main.GetComponent<CameraShake>();

        currentSpeed = startSpeed;

        AudioManager.Instance.PlayGameplayMusic();

        // Initialize powerup UI correctly
        magnetActive = false;
        shieldActive = false;

        magnetUI.SetActive(false);
        shieldUI.SetActive(false);

        magnetSlider.minValue = 0;
        magnetSlider.maxValue = magnetDuration;
        magnetSlider.value = 0;

        shieldSlider.minValue = 0;
        shieldSlider.maxValue = shieldDuration;
        shieldSlider.value = 0;

        shieldBubble.SetActive(false);

        highJumpActive = false;

        highJumpUI.SetActive(false);

        highJumpSlider.minValue = 0;
        highJumpSlider.maxValue = highJumpDuration;
        highJumpSlider.value = 0;

    }
    private void OnEnable()
    {
        GameEventManager.OnCoinCollect += PlayCoinPickupVfx;
    }
    private void OnDisable()
    {
        GameEventManager.OnCoinCollect -= PlayCoinPickupVfx;
    }

    void Update()
    {
        if (!playerActive) return;

        HandleScoreAndSpeed();
        HandleForwardMovement();

        if (canReadInput)
            HandleTouchInput();

        HandleSmoothHorizontalMovement();

        HandleMagnet();
        HandleShield();
        HandleHighJump();
    }
    public void StartGame()
    {
        CurrentRunCoins = 0;
        gameStarted = true;
        playerActive = true;
        print("Hehe here");
        StartCoroutine(EnableInputAfterDelay());
    }
    IEnumerator EnableInputAfterDelay()
    {
        yield return new WaitForSeconds(0.2f); // 0.2 sec delay
        canReadInput = true;
    }
    void HandleSmoothHorizontalMovement()
    {
        // Smoothly move towards target position
        float currentX = transform.position.x;
        float newX = Mathf.MoveTowards(currentX, targetXPosition, horizontalSpeed * Time.deltaTime);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }
    void HandleScoreAndSpeed()
    {
        // score update
        scoreTimer += Time.deltaTime;
        if (scoreTimer >= 0.33f)
        {
            totalScore++;
            //GameObject.Find("Score").GetComponent<Text>().text = totalScore.ToString();
            scoreTimer = 0;
        }

        // speed increase every frame
        if (currentSpeed > maxSpeed)
        {
            currentSpeed += acceleration * Time.deltaTime;
        }

        forwardSpeed = currentSpeed;
    }

    void HandleForwardMovement()
    {
        characterRGBD.linearVelocity = new Vector3(
            characterRGBD.linearVelocity.x,
            characterRGBD.linearVelocity.y,
            forwardSpeed
        );

        anim.SetTrigger("RunTriggerBoy");
    }
    void HandleTouchInput()
    {
#if UNITY_EDITOR
        // Mouse check
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (Input.GetMouseButtonDown(0))
            startTouchPosition = Input.mousePosition;

        if (Input.GetMouseButtonUp(0))
            ProcessSwipe(Input.mousePosition);

#else
    // Touch check with fingerId
    if (Input.touchCount > 0)
    {
        Touch touch = Input.GetTouch(0);

        // IMPORTANT FIX
        if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
            return;

        if (touch.phase == TouchPhase.Began)
            startTouchPosition = touch.position;

        if (touch.phase == TouchPhase.Ended)
            ProcessSwipe(touch.position);
    }
#endif
    }

    void ProcessSwipe(Vector2 endPos)
    {
        Vector2 delta = endPos - startTouchPosition;
        float absX = Mathf.Abs(delta.x);
        float absY = Mathf.Abs(delta.y);

        // TAP → Jump
        if (absX < Screen.width / 15f && absY < Screen.height / 15f)
        {
            characterJump();
            return;
        }

        // VERTICAL
        if (absY > absX)
        {
            if (delta.y < 0)
            {
                // Swipe Down → Slide
                characterDuck();
            }
            else
            {
                // Swipe Up → Jump
                characterJump();
            }
        }
        // HORIZONTAL
        else
        {
            if (delta.x > 0)
            {
                // Swipe Right → Move Right
                if (targetXPosition < 1.75f - 0.385f)
                    targetXPosition += 1.75f;
            }
            else
            {
                // Swipe Left → Move Left
                if (targetXPosition > -1.75f + 0.385f)
                    targetXPosition -= 1.75f;
            }

        }
    }
    public void ActivateMagnet()
    {
        magnetActive = true;
        magnetTimer = magnetDuration;

        magnetUI.SetActive(true);
        coinVfx.gameObject.SetActive(true);
        coinVfx.Play();
        AudioManager.Instance.PlayCoinPickSound();

        magnetSlider.maxValue = magnetDuration;
        magnetSlider.value = magnetDuration;
    }
    void HandleMagnet()
    {
        if (!magnetActive) return;

        magnetTimer -= Time.deltaTime;
        magnetTimer = Mathf.Clamp(magnetTimer, 0, magnetDuration);

        magnetSlider.value = magnetTimer;

        // 3D Box around player (forward, left/right, up/down)
        Vector3 boxCenter = transform.position + new Vector3(0, magnetRadiusY / 2f, magnetRadiusZ / 2f);

        Vector3 boxSize = new Vector3(
            magnetRadiusX,
            magnetRadiusY,
            magnetRadiusZ
        );

        Collider[] hits = Physics.OverlapBox(
            boxCenter,
            boxSize,
            Quaternion.identity
        );

        foreach (Collider hit in hits)
        {
            if (hit.CompareTag("Coin"))
            {
                Transform coin = hit.transform;

                coin.position = Vector3.MoveTowards(
                    coin.position,
                    transform.position + Vector3.up * 1f, // attract toward chest height
                    magnetPullSpeed * Time.deltaTime
                );
            }
        }

        if (magnetTimer <= 0)
        {
            magnetActive = false;
            magnetUI.SetActive(false);
        }
    }
    public void ActivateShield()
    {
        shieldActive = true;
        shieldTimer = shieldDuration;

        shieldUI.SetActive(true);
        coinVfx.gameObject.SetActive(true);
        coinVfx.Play();
        AudioManager.Instance.PlayCoinPickSound();
        shieldSlider.maxValue = shieldDuration;
        shieldSlider.value = shieldDuration;

        shieldBubble.SetActive(true);
    }
    void HandleShield()
    {
        if (!shieldActive) return;

        shieldTimer -= Time.deltaTime;
        shieldTimer = Mathf.Clamp(shieldTimer, 0, shieldDuration);

        shieldSlider.value = shieldTimer;

        if (shieldTimer <= 0)
        {
            shieldActive = false;
            shieldUI.SetActive(false);
            shieldBubble.SetActive(false);
        }
    }

    public void ActivateHighJump()
    {
        highJumpActive = true;
        highJumpTimer = highJumpDuration;

        highJumpUI.SetActive(true);
        coinVfx.gameObject.SetActive(true);
        coinVfx.Play();
        AudioManager.Instance.PlayCoinPickSound();

        highJumpSlider.maxValue = highJumpDuration;
        highJumpSlider.value = highJumpDuration;
    }
    void HandleHighJump()
    {
        if (!highJumpActive) return;

        highJumpTimer -= Time.deltaTime;
        highJumpTimer = Mathf.Clamp(highJumpTimer, 0, highJumpDuration);

        highJumpSlider.value = highJumpTimer;

        if (highJumpTimer <= 0)
        {
            highJumpActive = false;
            highJumpUI.SetActive(false);
        }
    }
    private void PlayCoinPickupVfx()
    {
        StopCoroutine(HandleCoinVfx());
        StartCoroutine(HandleCoinVfx());
    }
    private int currentCoins = 0;
    IEnumerator HandleCoinVfx()
    {
        currentCoins++;
        CurrentRunCoins = currentCoins;
        CoinsCollecd.Invoke(currentCoins);
        coinVfx.gameObject.SetActive(true);
        coinVfx.Play();
        yield return new WaitForSeconds(coinVfxDuration);
        coinVfx.gameObject.SetActive(false);
    }

    public void characterDuck()
    {
        //if (isSliding) return;
        //print("sliding");
        //isSliding = true;
        //anim.SetTrigger("SlideTriggerBoy");

        //capCollider.height = capCollider.height / 2f;

        //StartCoroutine(StopSlide());
    }
    IEnumerator StopSlide()
    {
        yield return new WaitForSeconds(slideTime);

        isSliding = false;

        capCollider.height = capCollider.height * 2f;
    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == "ground")
        {
            jumpCount = 1;
            isJumping = false; 
            isGrounded = true;

            if (anim != null)
                anim.SetBool("Jump", false);

        }


        if (coll.gameObject.tag == "death")
        {
            if (shieldActive)
            {
                Destroy(coll.gameObject); // break obstacle
                destructionParticle.gameObject.SetActive(true);
                destructionParticle.Play();
                AudioManager.Instance.PlayDestroySound();
                return;
            }
            pauseGo.active = false;
            shieldUI.SetActive(false);
            magnetUI.SetActive(false);
            highJumpUI.SetActive(false);
            //coinSounds.GetComponentInChildren<CoinSounds>().deathSound();
            Instantiate(deathParticle, transform.position, Quaternion.Euler(90, 0, 0)); // FIX
            //Instantiate(deathParticle, characterPos, Quaternion.Euler(90, 0, 0));

            Camera.main.GetComponent<CameraFollow>().enabled = false;

            playerActive = false;
            MOST_HapticFeedback.Generate(MOST_HapticFeedback.HapticTypes.HeavyImpact);
            Destroy(this.playerChild);
            StartCoroutine(camShake.Shake(0.5f, 0.35f));
            AudioManager.Instance.StopMusic();
            AudioManager.Instance.PlayDeathSound();
            StartCoroutine(showDeathMenu());
        }
        if(coll.gameObject.tag == "end")
        {
            MOST_HapticFeedback.Generate(MOST_HapticFeedback.HapticTypes.HeavyImpact);
            Camera.main.GetComponent<CameraFollow>().enabled = false;

            playerActive = false;
            Destroy(this.playerChild);
            
            HardRunner.Managers.LevelManager.CompleteLevel();
            gameSceneUiManager.LevelComplete();

            AudioManager.Instance.StopMusic();
        }
    }

    void OnCollisionExit(Collision coll)
    {
        if (coll.gameObject.CompareTag("ground"))
        {
            isGrounded = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "confetti")
        {
            GameEventManager.OnPlayerReachedEnd();
        }
    }
    private void FixedUpdate()
    {
        //gravity
        characterRGBD.AddForce(Vector3.down * 50, ForceMode.Impulse);
        //Vector3 HMove = transform.right * HInput * 5 * Time.fixedDeltaTime * 2;
        //characterRGBD.AddForce(HMove, ForceMode.Impulse);
        //characterRGBD.AddForce(Vector3.down * 5, ForceMode.Impulse);
    }

    //void characterJump()
    //{
    //    if (jumpCount > 3) return;

    //    characterRGBD.linearVelocity = new Vector3(characterRGBD.linearVelocity.x, 10f, characterRGBD.linearVelocity.z);
    //    isJumping = true;
    //    AudioManager.Instance.PlayJumpSound();

    //    if (anim != null)
    //        anim.SetBool("Jump", true);

    //    StartCoroutine(startWalkAnim(jumpCount));

    //    jumpCount++;
    //}

    void characterJump()
    {
        print("Is hight jump active ?" + highJumpActive);
        // HIGH JUMP ACTIVE
        if (highJumpActive)
        {
            if (!isGrounded) return;

            characterRGBD.linearVelocity = new Vector3(
                characterRGBD.linearVelocity.x,
                highJumpForce,
                characterRGBD.linearVelocity.z
            );

            AudioManager.Instance.PlayJumpSound();

            if (anim != null)
                anim.SetBool("Jump", true);

            return;
        }

        // NORMAL JUMP
        if (jumpCount > 3) return;

        characterRGBD.linearVelocity = new Vector3(
            characterRGBD.linearVelocity.x,
            normalJumpForce,
            characterRGBD.linearVelocity.z
        );

        isJumping = true;

        AudioManager.Instance.PlayJumpSound();

        if (anim != null)
            anim.SetBool("Jump", true);

        jumpCount++;
    }
    public IEnumerator startWalkAnim(int count)
    {
        yield return new WaitForSeconds(0.4f * count);
    }

    IEnumerator showDeathMenu()
    {
        yield return new WaitForSeconds(1.2f);
        gameSceneUiManager.GameOver();

    }



    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;

        Vector3 boxCenter = transform.position + new Vector3(0, magnetRadiusY / 2f, magnetRadiusZ / 2f);
        Vector3 boxSize = new Vector3(magnetRadiusX, magnetRadiusY, magnetRadiusZ);

        Gizmos.DrawWireCube(boxCenter, boxSize);
    }
}
