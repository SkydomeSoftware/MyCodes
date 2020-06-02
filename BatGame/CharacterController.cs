using System;
using System.Collections;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;



public class CharacterController : MonoBehaviour
{




    public bool wPressed, sPressed, aPressed, dPressed;



    //playerStats
    public bool playerStats;
    public float playerHP;
    public float playerEnergy;
    public float playerMaxHP;
    public float playerMaxEnergy;
    public float flyForce;
    public float pushForce;
    public float linearMoveForce;
    public float gravityScale;

   //Saves
    public int[] savedLevels;
    public float[] arrayPlayerMaxHP;
    public float[] arrayPlayerMaxEnergy;
    public float[] arrayFlyForce;
    public float[] arrayPushForce;
    public float[] arrayLinearMoveForce;
    public float[] arrayGravityScale;

    //Sensors
    public bool distanceSensors;
    public float downDistance, topDistance, leftDistance, rightDistance, downLeftDistance, downRightDistance, playerDistance, GroundHigh;
    float colliderRadious;

   

    LayerMask terrainLayer, EnemyLayer, waterLayer, foodLayer;

    //GameObjects
    [HideInInspector] public Transform player;
    [HideInInspector] public Animator playerAnimator;
    [HideInInspector] public Slider energySlider, hpSlider;
    [HideInInspector] public AudioSource audioSource;
    [HideInInspector] public GameObject console, endGameOverScreen;
    [HideInInspector] public TMP_Text textGameOverScreen;
  


    public AudioClip[] audioClips;


    //canvas
    public Button up, down, left, right;
    public TMP_Text scoreText;
    public Image energySliderFill, hpSliderFill;
    public UnityEngine.Experimental.Rendering.Universal.Light2D globalLight;
    public UnityEngine.Experimental.Rendering.Universal.Light2D playerLight;
    public TMP_Text textDistance;

    public bool GameEnd;
    private void FixedUpdate()
    {
        UpdateKeyInput();
        CheckDistances();
        AnimationController();
        CheckEnemies();
        UpdateEnergy();
        UpdateHP();
        UpdateDistance();
        lightningSystem();
    }
    private void Start()
    {
       CreateButton(up, 'w');
       CreateButton(down, 's');
       CreateButton(left, 'a');
       CreateButton(right, 'd');
       CreatePlayer();
       LoadSave();
    }
    public void CreatePlayer()
    {
        player = this.gameObject.transform;
        playerAnimator = this.gameObject.transform.GetComponent<Animator>();
        audioSource = this.gameObject.transform.GetComponent<AudioSource>();
        colliderRadious = this.gameObject.GetComponent<CircleCollider2D>().radius;
        energySlider = player.GetChild(0).transform.Find("Slider Energy").GetComponent<Slider>();
        hpSlider = player.GetChild(0).transform.Find("Slider Health").GetComponent<Slider>();
        console = player.GetChild(0).transform.Find("Console").gameObject;
        endGameOverScreen = player.GetChild(0).transform.Find("end game screen").gameObject;
        scoreText = endGameOverScreen.transform.transform.Find("Text Your Score").GetComponent<TMP_Text>();
        textGameOverScreen = endGameOverScreen.transform.transform.Find("Text Last Score").GetComponent<TMP_Text>();
        terrainLayer = LayerMask.GetMask("Terrain");
        EnemyLayer = LayerMask.GetMask("Enemy");
        waterLayer = LayerMask.GetMask("WaterLayer");
        foodLayer = LayerMask.GetMask("Food");
        GameEnd = false;
    }

    public void LoadSave()
    {
        savedLevels[0] = PlayerPrefs.GetInt("PlayerMaxHP");
        savedLevels[1] = PlayerPrefs.GetInt("PlayerMaxEnergy");
        savedLevels[2] = PlayerPrefs.GetInt("FlyForce");
        savedLevels[3] = PlayerPrefs.GetInt("PushForce");
        savedLevels[4] = PlayerPrefs.GetInt("LinearMoveForce");
        savedLevels[5] = PlayerPrefs.GetInt("GravityScale");
        textGameOverScreen.text = "Last Score: " + PlayerPrefs.GetFloat("Last endGameOverScreen").ToString("0.00m");
        LoadStats();
    }

    public void LoadStats()
    {
        playerMaxHP = arrayPlayerMaxHP[savedLevels[0]];
        playerMaxEnergy = arrayPlayerMaxEnergy[savedLevels[1]];
        energySlider.maxValue = arrayPlayerMaxEnergy[savedLevels[1]];
        playerEnergy = arrayPlayerMaxEnergy[savedLevels[1]];
        flyForce = arrayFlyForce[savedLevels[2]];
        pushForce = arrayPushForce[savedLevels[3]];
        linearMoveForce = arrayLinearMoveForce[savedLevels[4]];
        gravityScale = arrayGravityScale[savedLevels[5]];
    }

    public void CheckDistances()
    {
        downDistance = CheckDistance(Vector2.down);
        topDistance = CheckDistance(Vector2.up);
        rightDistance = CheckDistance(Vector2.right);
        leftDistance = CheckDistance(Vector2.left);
        downLeftDistance = CheckDistance(new Vector2(-1, -1));
        rightDistance = CheckDistance(new Vector2(1, -1));
    }

    public float CheckDistance(Vector2 direction)
    {
        RaycastHit2D hitDirection = Physics2D.Raycast(transform.position, direction, 4f, terrainLayer);
       
        
        if(hitDirection.collider != null)
        {
            if (direction == Vector2.down)
            {
                GroundHigh = hitDirection.collider.transform.position.y;
            }
            return Vector2.Distance(transform.position, hitDirection.point);
        }
        else { return 100; }
    }

    public void CreateButton(Button button, char key)
    {
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.callback.RemoveAllListeners();
        entry.eventID = EventTriggerType.PointerDown;
        entry.callback.AddListener((eventData) => { CheckPressedButtonEnter(key); });
        button.gameObject.AddComponent<EventTrigger>().triggers.Add(entry);
        
        EventTrigger.Entry exit = new EventTrigger.Entry();
        exit.eventID = EventTriggerType.PointerUp;
        exit.callback.AddListener((eventData) => { CheckPressedButtonExit(key); });
        button.gameObject.AddComponent<EventTrigger>().triggers.Add(exit);
    }
    public void CheckPressedButtonEnter(char pressedButton)
    {
      switch(pressedButton)
        {
            case 'w': wPressed = true; break;
            case 's': sPressed = true; break;
            case 'a': aPressed = true; break;
            case 'd': dPressed = true; break;
        }
    }
    public void CheckPressedButtonExit(char pressedButton)
    {
        switch (pressedButton)
        {
            case 'w': wPressed = false; break;
            case 's': sPressed = false; break;
            case 'a': aPressed = false; break;
            case 'd': dPressed = false; break;
        }
    }
    public void UpdateKeyInput()
    {

        if (Input.GetAxis("Vertical") > 0 || wPressed)
        {
            if (playerEnergy > 1)
            {
                playerAnimator.SetBool("WEntered", true);
                playerEnergy -= 1f;
                FlyUp();
            }
        }
        else
        {
            playerAnimator.SetBool("WEntered", false);
        }

        if (Input.GetAxis("Vertical") < 0 || sPressed)
        {
            playerAnimator.SetBool("SEntered", true);
            FallDown();
        }
        else
        {
            playerAnimator.SetBool("SEntered", false);
        }

        if (Input.GetAxis("Horizontal") > 0 || dPressed)
        {
            player.GetComponent<SpriteRenderer>().flipX = false;
            playerAnimator.SetBool("DEntered", true);
            if (downDistance > colliderRadious && topDistance > colliderRadious + 0.1f && player.gameObject.GetComponent<Rigidbody2D>().velocity.x < 1f)
            {
                MoveSides(Vector3.right);
            }
        }
        else
        {
            playerAnimator.SetBool("DEntered", false);
        }

        if (Input.GetAxis("Horizontal") < 0 || aPressed)
        {
            player.GetComponent<SpriteRenderer>().flipX = true;
            playerAnimator.SetBool("AEntered", true);
            if (downDistance > colliderRadious && topDistance > colliderRadious + 0.1f)
            {
                MoveSides(Vector3.left);

            }
        }
        else
        {
            playerAnimator.SetBool("AEntered", false);
        }

        if (Input.GetKeyDown(KeyCode.BackQuote))
        {
            if (console.activeSelf)
            {
                console.SetActive(false);
            }
            else
            {
                console.SetActive(true);
            }
        }

        if (Input.GetKeyDown(KeyCode.KeypadEnter) && console.activeSelf)
        {
            console.GetComponent<Console>().Changevalue();
        }
    }

    public void PlayAudio(int audioID)
    {
        audioSource.PlayOneShot(audioClips[audioID]);
    }

    IEnumerator endGame()
    {
        yield return new WaitForSeconds(1.5f);
        endGameOverScreen.SetActive(true);
        scoreText.GetComponent<TMP_Text>().text = "NEW SCORE: " + playerDistance.ToString("0.00m");
        PlayerPrefs.SetFloat("Last endGameOverScreen", playerDistance);
    }

    public void LoadLevel(int levelID)
    {
        Application.LoadLevel(levelID);
    }

    public void lightningSystem()
    {
        if (topDistance < 10 && globalLight.intensity >= 0.1)
        {
            globalLight.intensity -= 0.1f;
            playerLight.gameObject.SetActive(true);
        }
        else if (topDistance > 10 && globalLight.intensity <= 1)
        {

            globalLight.intensity += 0.1f;
            playerLight.gameObject.SetActive(false);
        }
    }

    public void UpdateHP()
    {
        hpSlider.value = playerHP;
    }
    public void UpdateEnergy()
    {
        energySlider.value = playerEnergy;

        if (playerEnergy < playerMaxEnergy && !Input.GetKey(KeyCode.W))

            if (playerEnergy <= 5 && downDistance <= 0.30 && !GameEnd)
            {
                GameEnd = true;
                StartCoroutine(endGame());
            }
    }

    public void UpdateDistance()
    {
        playerDistance = player.transform.position.x;
        textDistance.text = playerDistance.ToString("0.00m");
    }
    public void AnimationController()
    {
        playerAnimator.SetFloat("GroundDistance", downDistance);
        playerAnimator.SetFloat("TopDistance", topDistance);

        if (topDistance <= colliderRadious + 0.02f)
        {
            player.GetComponent<SpriteRenderer>().flipY = true;
            player.GetComponent<Rigidbody2D>().gravityScale = 0;
        }
        else
        {
            player.GetComponent<SpriteRenderer>().flipY = false;
            player.GetComponent<Rigidbody2D>().gravityScale = gravityScale;
        }
    }
    public void CheckEnemies()
    {
        RaycastHit2D EnemyHit = Physics2D.CircleCast(transform.position, colliderRadious + 0.03f, transform.forward, colliderRadious + 0.03f, EnemyLayer);
        {
            if (EnemyHit.transform != null)
            {
                playerHP -= 45;
                Destroy(EnemyHit.transform.gameObject);
            }
            if (playerHP <= 0)
            {
                Application.LoadLevel(0);
            }
        }
        RaycastHit2D WaterHit = Physics2D.CircleCast(transform.position, colliderRadious + 0.03f, transform.forward, colliderRadious + 0.03f, waterLayer);
        {

            if (WaterHit.transform != null)
            {
                playerEnergy = 1;
                playerAnimator.SetBool("isSink", true);
            }
        }
        RaycastHit2D FoodHit = Physics2D.CircleCast(transform.position, colliderRadious + 0.03f, transform.forward, colliderRadious + 0.03f, foodLayer);
        {

            if (FoodHit.transform != null)
            {
                playerEnergy += 45;
                audioSource.PlayOneShot(audioClips[3]);
                Destroy(FoodHit.transform.gameObject);
            }
        }
    }

    public void FlyUp()
    {

        player.GetComponent<Rigidbody2D>().velocity = new Vector2(this.GetComponent<Rigidbody2D>().velocity.x, flyForce);
    }

    public void FallDown()
    {
        player.GetComponent<Rigidbody2D>().velocity = new Vector2(this.GetComponent<Rigidbody2D>().velocity.x, -flyForce * 1.5f);
    }

    public void MoveSides(Vector3 direction)
    {

        player.transform.position += direction * linearMoveForce * Time.deltaTime;
        player.GetComponent<Rigidbody2D>().AddForce(direction * pushForce);
    }
}

