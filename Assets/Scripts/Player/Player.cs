using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HardRunner.Managers;
using UnityEditor.Rendering.LookDev;
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

        AudioManager.Instance.PlayRandomGameplaySound();
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
        HandleTouchInput();
        HandleSmoothHorizontalMovement();
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

        if (EventSystem.current.IsPointerOverGameObject())
            return;


#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
            startTouchPosition = Input.mousePosition;

        if (Input.GetMouseButtonUp(0))
            ProcessSwipe(Input.mousePosition);
#else
    if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        startTouchPosition = Input.GetTouch(0).position;

    if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        ProcessSwipe(Input.GetTouch(0).position);
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
    private void PlayCoinPickupVfx()
    {
        StopCoroutine(HandleCoinVfx());
        StartCoroutine(HandleCoinVfx());
    }
    IEnumerator HandleCoinVfx()
    {
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
            AudioManager.Instance.PlayJumpSound();

            if (anim != null)
                anim.SetBool("Jump", false);

        }

        if (coll.gameObject.tag == "death")
        {

            pauseGo.active = false;

            //coinSounds.GetComponentInChildren<CoinSounds>().deathSound();
            Instantiate(deathParticle, transform.position, Quaternion.Euler(90, 0, 0)); // FIX
            //Instantiate(deathParticle, characterPos, Quaternion.Euler(90, 0, 0));

            Camera.main.GetComponent<CameraFollow>().enabled = false;

            playerActive = false;
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


    private void FixedUpdate()
    {
        //gravity
        characterRGBD.AddForce(Vector3.down * 50, ForceMode.Impulse);
        //Vector3 HMove = transform.right * HInput * 5 * Time.fixedDeltaTime * 2;
        //characterRGBD.AddForce(HMove, ForceMode.Impulse);
        //characterRGBD.AddForce(Vector3.down * 5, ForceMode.Impulse);
    }

    void characterJump()
    {
        if (jumpCount > 3) return;

        characterRGBD.linearVelocity = new Vector3(characterRGBD.linearVelocity.x, 10f, characterRGBD.linearVelocity.z);
        isJumping = true;

        if (anim != null)
            anim.SetBool("Jump", true);

        StartCoroutine(startWalkAnim(jumpCount));

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




}
