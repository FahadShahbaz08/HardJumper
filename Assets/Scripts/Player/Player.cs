using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    private CharacterController controller;
    private Vector3 direction;
    public float forwardSpeed;

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

    SavingManager savingManager;

    public GameObject pauseGo;

    public GameObject coinSounds;
    private Vector2 startTouchPosition, endTouchPosition;

    float HInput;
    bool isSliding = false;
    float slideTime = 0.8f;
    public CapsuleCollider capCollider;
    private float targetXPosition;
    private float horizontalSpeed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        savingManager = GetComponent<SavingManager>();
        savingManager = FindObjectOfType<SavingManager>();
        //RunTrigger
        characterRGBD = GetComponent<Rigidbody>();
        playerChild = GameObject.Find("PlayerChild");
        anim.SetTrigger("IdleTriggerBoy");

        //FOR JUMP SOUND
        coinSounds = GameObject.Find("SoundsGO");

        //pauseGo = GameObject.Find("PauseGO");

        timer1 = 0f;
        jumpCount = 1;
        totalScore = 0;

        targetXPosition = transform.position.x;
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
        scoreTimer += Time.deltaTime;
        if (scoreTimer >= 0.33f)
        {
            totalScore++;
            GameObject.Find("Score").GetComponent<Text>().text = totalScore.ToString();
            scoreTimer = 0;
        }

        if (totalScore >= 800) forwardSpeed = -12f;
        else if (totalScore >= 600) forwardSpeed = -11.5f;
        else if (totalScore >= 400) forwardSpeed = -11f;
        else if (totalScore >= 200) forwardSpeed = -10f;
        else if (totalScore >= 150) forwardSpeed = -9.5f;
        else if (totalScore >= 100) forwardSpeed = -9f;
        else if (totalScore >= 50) forwardSpeed = -8.5f;
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
            if (delta.x < 0)
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



    public void characterDuck()
    {
        if (isSliding) return;
        print("sliding");
        isSliding = true;
        anim.SetTrigger("SlideTriggerBoy");

        capCollider.height = capCollider.height / 2f;

        StartCoroutine(StopSlide());
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
            //isCrouch = true;

            //Debug.Log("GROUND!!!");
        }

        if (coll.gameObject.tag == "death")
        {
            savingManager.SaveCoins();
            savingManager.Deaths();

            pauseGo.active = false;

            coinSounds.GetComponentInChildren<CoinSounds>().deathSound();
            Instantiate(deathParticle, transform.position, Quaternion.Euler(90, 0, 0)); // FIX
            //Instantiate(deathParticle, characterPos, Quaternion.Euler(90, 0, 0));

            Camera.main.GetComponent<CameraFollow>().enabled = false;

            playerActive = false;
            Destroy(this.playerChild);

            StartCoroutine(showDeathMenu());
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

        if (jumpCount == 1)
        {
            //characterSpeaker.PlayOneShot(vars.jumpSound);
            characterRGBD.linearVelocity = new Vector3(characterRGBD.linearVelocity.x, 10f, characterRGBD.linearVelocity.z);
            //characterRGBD.AddForce(Vector3.up * 400, ForceMode.Impulse);
            coinSounds.GetComponentInChildren<CoinSounds>().jumpSound();

            StartCoroutine(startWalkAnim(jumpCount));
        }
        else if (jumpCount == 2)
        {
            //characterSpeaker.PlayOneShot(vars.jumpSound);
            characterRGBD.linearVelocity = new Vector3(characterRGBD.linearVelocity.x, 10f, characterRGBD.linearVelocity.z);
            //characterRGBD.AddForce(Vector3.up * 500, ForceMode.Impulse);
            coinSounds.GetComponentInChildren<CoinSounds>().jumpSound();
            StartCoroutine(startWalkAnim(jumpCount));
        }

        else if (jumpCount == 3)
        {
            //characterSpeaker.PlayOneShot(vars.jumpSound);
            characterRGBD.linearVelocity = new Vector3(characterRGBD.linearVelocity.x, 10f, characterRGBD.linearVelocity.z);
            //characterRGBD.AddForce(Vector3.up * 500, ForceMode.Impulse);
            coinSounds.GetComponentInChildren<CoinSounds>().jumpSound();
            StartCoroutine(startWalkAnim(jumpCount));
        }
        jumpCount++;
    }

    public IEnumerator startWalkAnim(int count)
    {
        yield return new WaitForSeconds(0.4f * count);
    }

    IEnumerator showDeathMenu()
    {
        yield return new WaitForSeconds(1.2f);
      UIManager.instance.ShowRestartMenu();

    }




}
