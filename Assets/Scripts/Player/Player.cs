//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class Player : MonoBehaviour
//{

//    private CharacterController controller;
//    private Vector3 direction;
//    public float forwardSpeed;

//    public int jumpCount, totalScore;
//    public bool playerActive = false;

//    Rigidbody characterRGBD;
//    public Vector3 characterPos;

//    public float timer1, spawnTimer, scoreTimer;
//    private float gravityValue = -1.81f;

//    public GameObject deathParticle;
//    public Animator anim;
//    [SerializeField]
//    GameObject playerChild;

//    SavingManager savingManager;

//    public GameObject pauseGo;

//    public GameObject coinSounds;

//    // Start is called before the first frame update
//    void Start()
//    {
//        savingManager = GetComponent<SavingManager>();
//        savingManager = FindObjectOfType<SavingManager>();
//        //RunTrigger
//        characterRGBD = GetComponent<Rigidbody>();
//        playerChild = GameObject.Find("PlayerChild");
//        anim.SetTrigger("IdleTriggerBoy");

//        //FOR JUMP SOUND
//        coinSounds = GameObject.Find("SoundsGO");

//        //pauseGo = GameObject.Find("PauseGO");

//        timer1 = 0f;
//        jumpCount = 1;
//        totalScore = 0;


//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if (playerActive)
//        {

//            scoreTimer += Time.deltaTime;
//            if (scoreTimer >= 0.33f)
//            {
//                totalScore++;
//                GameObject.Find("Score").GetComponent<Text>().text = "Score:" + totalScore.ToString();
//                scoreTimer = 0;
//            }


//            anim.SetTrigger("RunTriggerBoy");

//            characterPos = transform.position;
//            characterRGBD.velocity = new Vector3(characterRGBD.velocity.x, characterRGBD.velocity.y, forwardSpeed);
//            //characterDust.transform.position = new Vector3(0, characterPos.y, characterPos.z);

//            if (totalScore <= 50)
//            {
//                characterPos = transform.position;
//                characterRGBD.velocity = new Vector3(characterRGBD.velocity.x, characterRGBD.velocity.y, forwardSpeed);

//            }

//            if (totalScore >= 50)
//            {
//                forwardSpeed = -8.5f;

//                characterPos = transform.position;
//                characterRGBD.velocity = new Vector3(characterRGBD.velocity.x, characterRGBD.velocity.y, forwardSpeed);
//            }

//            if (totalScore >= 100)
//            {
//                forwardSpeed = -9;
//                characterPos = transform.position;
//                characterRGBD.velocity = new Vector3(characterRGBD.velocity.x, characterRGBD.velocity.y, forwardSpeed);
//            }

//            if (totalScore >= 150)
//            {
//                forwardSpeed = -9.5f;
//                characterPos = transform.position;
//                characterRGBD.velocity = new Vector3(characterRGBD.velocity.x, characterRGBD.velocity.y, forwardSpeed);
//            }


//            if (totalScore >= 200)
//            {
//                forwardSpeed = -10;
//                characterPos = transform.position;
//                characterRGBD.velocity = new Vector3(characterRGBD.velocity.x, characterRGBD.velocity.y, forwardSpeed);
//            }

//            if (totalScore >= 400)
//            {
//                forwardSpeed = -11;
//                characterPos = transform.position;
//                characterRGBD.velocity = new Vector3(characterRGBD.velocity.x, characterRGBD.velocity.y, forwardSpeed);
//            }

//            if (totalScore >= 600)
//            {
//                forwardSpeed = -11.5f;
//                characterPos = transform.position;
//                characterRGBD.velocity = new Vector3(characterRGBD.velocity.x, characterRGBD.velocity.y, forwardSpeed);
//            }

//            if (totalScore >= 800)
//            {
//                forwardSpeed = -12;
//                characterPos = transform.position;
//                characterRGBD.velocity = new Vector3(characterRGBD.velocity.x, characterRGBD.velocity.y, forwardSpeed);
//            }

//            timer1 += Time.deltaTime;

//            if (Input.GetMouseButtonUp(0))
//            {
//                if (timer1 < 0.15f)
//                //if (timer1 < 1f)
//                {
//                    if (jumpCount <= 2)
//                    {
//                        characterJump();
//                    }
//                    timer1 = 0;
//                }
//                else
//                {
//                    if (jumpCount <= 2)
//                    {
//                        characterJump();
//                    }
//                    //StartCoroutine(changeColliders());
//                }

//                timer1 = 0;
//            }
//        }



//    }

//    void OnCollisionEnter(Collision coll)
//    {
//        if (coll.gameObject.tag == "ground")
//        {
//            jumpCount = 1;
//            //isCrouch = true;

//            //Debug.Log("GROUND!!!");
//        }

//        if (coll.gameObject.tag == "death")
//        {
//            savingManager.SaveCoins();
//            savingManager.Deaths();

//            pauseGo.active = false;

//            coinSounds.GetComponentInChildren<CoinSounds>().deathSound();
//            Instantiate(deathParticle, characterPos, Quaternion.Euler(90, 0, 0));

//            Camera.main.GetComponent<CameraFollow>().enabled = false;

//            playerActive = false;
//            Destroy(this.playerChild);

//            StartCoroutine(showDeathMenu());
//        }

//    }


//    private void FixedUpdate()
//    {
//        //gravity
//        characterRGBD.AddForce(Vector3.down * 50, ForceMode.Impulse);
//        //characterRGBD.AddForce(Vector3.down * 5, ForceMode.Impulse);
//    }

//    void characterJump()
//    {

//        if (jumpCount == 1)
//        {
//            //characterSpeaker.PlayOneShot(vars.jumpSound);
//            characterRGBD.velocity = new Vector3(characterRGBD.velocity.x, 9f, characterRGBD.velocity.z);
//            //characterRGBD.AddForce(Vector3.up * 400, ForceMode.Impulse);
//            coinSounds.GetComponentInChildren<CoinSounds>().jumpSound();

//            StartCoroutine(startWalkAnim(jumpCount));
//        }
//        else if (jumpCount == 2)
//        {
//            //characterSpeaker.PlayOneShot(vars.jumpSound);
//            characterRGBD.velocity = new Vector3(characterRGBD.velocity.x, 9.5f, characterRGBD.velocity.z);
//            //characterRGBD.AddForce(Vector3.up * 500, ForceMode.Impulse);
//            coinSounds.GetComponentInChildren<CoinSounds>().jumpSound();
//            StartCoroutine(startWalkAnim(jumpCount));
//        }
//        jumpCount++;
//    }

//    public IEnumerator startWalkAnim(int count)
//    {
//        yield return new WaitForSeconds(0.4f * count);
//        //GetComponent<Animation>().Play(vars.characters[selectedCharacter].walkAnimation.name);
//    }

//    IEnumerator showDeathMenu()
//    {
//        yield return new WaitForSeconds(1.2f);
//        Camera.main.GetComponent<UIs>().restartMenuUI.SetActive(true);

//    }

//}






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


    }

    // Update is called once per frame
    void Update()
    {
        HInput = Input.GetAxis("Horizontal");
        if (playerActive)
        {

            scoreTimer += Time.deltaTime;
            if (scoreTimer >= 0.33f)
            {
                totalScore++;
                GameObject.Find("Score").GetComponent<Text>().text = "" + totalScore.ToString();
                scoreTimer = 0;
            }


            anim.SetTrigger("RunTriggerBoy");

            characterPos = transform.position;
            characterRGBD.linearVelocity = new Vector3(characterRGBD.linearVelocity.x, characterRGBD.linearVelocity.y, forwardSpeed);
            //characterDust.transform.position = new Vector3(0, characterPos.y, characterPos.z);

            if (totalScore <= 50)
            {
                characterPos = transform.position;
                characterRGBD.linearVelocity = new Vector3(characterRGBD.linearVelocity.x, characterRGBD.linearVelocity.y, forwardSpeed);

            }

            if (totalScore >= 50)
            {
                forwardSpeed = -8.5f;

                characterPos = transform.position;
                characterRGBD.linearVelocity = new Vector3(characterRGBD.linearVelocity.x, characterRGBD.linearVelocity.y, forwardSpeed);
            }

            if (totalScore >= 100)
            {
                forwardSpeed = -9;
                characterPos = transform.position;
                characterRGBD.linearVelocity = new Vector3(characterRGBD.linearVelocity.x, characterRGBD.linearVelocity.y, forwardSpeed);
            }

            if (totalScore >= 150)
            {
                forwardSpeed = -9.5f;
                characterPos = transform.position;
                characterRGBD.linearVelocity = new Vector3(characterRGBD.linearVelocity.x, characterRGBD.linearVelocity.y, forwardSpeed);
            }


            if (totalScore >= 200)
            {
                forwardSpeed = -10;
                characterPos = transform.position;
                characterRGBD.linearVelocity = new Vector3(characterRGBD.linearVelocity.x, characterRGBD.linearVelocity.y, forwardSpeed);
            }

            if (totalScore >= 400)
            {
                forwardSpeed = -11;
                characterPos = transform.position;
                characterRGBD.linearVelocity = new Vector3(characterRGBD.linearVelocity.x, characterRGBD.linearVelocity.y, forwardSpeed);
            }

            if (totalScore >= 600)
            {
                forwardSpeed = -11.5f;
                characterPos = transform.position;
                characterRGBD.linearVelocity = new Vector3(characterRGBD.linearVelocity.x, characterRGBD.linearVelocity.y, forwardSpeed);
            }

            if (totalScore >= 800)
            {
                forwardSpeed = -12;
                characterPos = transform.position;
                characterRGBD.linearVelocity = new Vector3(characterRGBD.linearVelocity.x, characterRGBD.linearVelocity.y, forwardSpeed);
            }

            timer1 += Time.deltaTime;

            if (Input.GetMouseButtonUp(0) && !swap)
            {
                swap = true;
                if (timer1 < 0.15f)
                //if (timer1 < 1f)
                {
                    if (jumpCount <= 3)
                    {
                        characterJump();
                    }
                    timer1 = 0;
                }
                else
                {
                    if (jumpCount <= 3)
                    {
                        characterJump();
                    }
                    //StartCoroutine(changeColliders());
                }

                timer1 = 0;
            }


            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
                startTouchPosition = Input.GetTouch(0).position;

            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                endTouchPosition = Input.GetTouch(0).position;
                float inputX = startTouchPosition.x - endTouchPosition.x;
                float inputY = startTouchPosition.y - endTouchPosition.y;
                float slope = inputY / inputX;
                //if ((endTouchPosition.x > startTouchPosition.x) && characterRGBD.transform.position.x > -1.75f - 0.369f)
                //    characterRGBD.transform.position = new Vector3(characterRGBD.transform.position.x - 1.75f, characterRGBD.transform.position.y, this.transform.position.z);

                //if ((endTouchPosition.x < startTouchPosition.x) && characterRGBD.transform.position.x < 1.75f - 0.369f)
                //    characterRGBD.transform.position = new Vector3(characterRGBD.transform.position.x + 1.75f, characterRGBD.transform.position.y, this.transform.position.z);

                //if ((endTouchPosition.y > startTouchPosition.y))
                //     characterJump();

                float fDistance = Mathf.Sqrt(Mathf.Pow((endTouchPosition.y - startTouchPosition.y), 2) + Mathf.Pow((endTouchPosition.x - startTouchPosition.x), 2));
                if (fDistance <= (Screen.width / 15f))
                {
                    characterJump();
                    return;
                }

                if (inputX < 0 && inputY >= 0 && slope > -1 && slope <= 0 && characterRGBD.transform.position.x > -1.75f - 0.385f)
                {
                    characterRGBD.transform.position = new Vector3(characterRGBD.transform.position.x - 1.75f, characterRGBD.transform.position.y, this.transform.position.z);
                }
                else if (inputX < 0 && inputY <= 0 && slope >= 0 && slope < 1 && characterRGBD.transform.position.x > -1.75f - 0.385f)
                {
                    characterRGBD.transform.position = new Vector3(characterRGBD.transform.position.x - 1.75f, characterRGBD.transform.position.y, this.transform.position.z);
                }

                else if (inputX > 0 && inputY >= 0 && slope < 1 && slope >= 0 && characterRGBD.transform.position.x < 1.75f - 0.385f)
                {
                    characterRGBD.transform.position = new Vector3(characterRGBD.transform.position.x + 1.75f, characterRGBD.transform.position.y, this.transform.position.z);
                }
                else if (inputX > 0 && inputY <= 0 && slope > -1 && slope <= 0 && characterRGBD.transform.position.x < 1.75f - 0.385f)
                {
                    characterRGBD.transform.position = new Vector3(characterRGBD.transform.position.x + 1.75f, characterRGBD.transform.position.y, this.transform.position.z);
                }


                //else if (inputX >= 0 && inputY < 0 && slope > 1)
                //{
                //    characterJump();
                //}
                //else if (inputX <= 0 && inputY < 0 && slope > -1)
                //{
                //    characterJump();
                //}

                else if (inputX >= 0 && inputY < 0 && slope < -1)
                {
                    //characterRGBD.velocity = new Vector3(characterRGBD.velocity.x, 0.38f, characterRGBD.velocity.z);
                }
                //characterDuck(); 0.381
                //duck
                //return SwipeDirection.Duck;

                else if (inputX <= 0 && inputY < 0 && slope > 1)
                {
                    //characterRGBD.velocity = new Vector3(characterRGBD.velocity.x, 0.38f, characterRGBD.velocity.z);
                    //duck
                    //return SwipeDirection.Duck;
                }


                //characterRGBD.AddForce(Vector3.down * 50, ForceMode.Impulse);
            }

        }



    }
    public void characterDuck()
    {
        //characterRGBD.gameObject.GetComponent<Animator>().enabled = false;
        //characterRGBD.gameObject.GetComponent<Animation>().Play("Duck");
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
            Instantiate(deathParticle, characterPos, Quaternion.Euler(90, 0, 0));

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
