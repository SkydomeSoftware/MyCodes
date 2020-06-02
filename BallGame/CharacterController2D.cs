using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using UnityEditor;
[RequireComponent(typeof(Rigidbody2D))]
public class CharacterController2D : MonoBehaviour
{
    public float jumpHeight, pushForce, rotationForce, moveSpeed;
    public bool upCollider, downCollider, leftCollider, rightCollider, rightUpCollider, leftUpCollider,downRightCollider,downLeftCollider, platformCollider, enemyCollider;
    public float UpColliderDistance, DownColliderDIstance, LeftColliderDistance, RightColliderDistance;
    public bool isAIGoing;
    public bool isFalling;
    public bool AIGoLeft, AIGoRight;
    public Toggle TupCollider, TdownCollider, TleftCollider, TrightCollider, TrightUpCollider, TleftUpCollider, TdownRightCollider, TdownLeftCollider, TenemyCollider;
  

    public bool doublejump;
    public int Coins;
    public Text CoinsText;

    public Camera PlayerCamera;
    public TextMesh UpText;
    public Vector3 cameraOffset, UpTextOffset;
    public float FollowSpeed;
    public float angularDrag;
    public LayerMask GroundLayer, EnemyLayer, PlatformLayer, CoinLayer, DirectionLayer, InfoLayer, FinishLayer;



    public void UIToggles()
    {
        TupCollider.isOn = upCollider;
        TdownCollider.isOn = downCollider;
        TleftCollider.isOn = leftCollider;
        TrightCollider.isOn = rightCollider;
        TrightUpCollider.isOn = rightUpCollider;
        TleftUpCollider.isOn = leftUpCollider;
        TdownRightCollider.isOn = downRightCollider;
        TdownLeftCollider.isOn = downLeftCollider;
        TenemyCollider.isOn = enemyCollider;
    }


    private void Start()
    {
        GroundLayer = LayerMask.GetMask("Ground");
        EnemyLayer = LayerMask.GetMask("Enemy");
        PlatformLayer = LayerMask.GetMask("Platform");
        CoinLayer = LayerMask.GetMask("Coin");
        DirectionLayer = LayerMask.GetMask("Direction");
        InfoLayer = LayerMask.GetMask("Info");
        FinishLayer = LayerMask.GetMask("Finish");
    }

   

   
    public void Move(Vector3 direction, float rotationspeed, float force)
    {
        transform.position += direction * moveSpeed * Time.deltaTime;
        transform.GetComponent<Rigidbody2D>().AddForce(direction * force);
        transform.GetComponent<Rigidbody2D>().AddTorque(rotationspeed, ForceMode2D.Impulse);
    }



    public void jump()
    {
        if(downCollider == true || doublejump == false)
        {
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(this.GetComponent<Rigidbody2D>().velocity.x, jumpHeight);
            doublejump = true;
        }
        if(downCollider== false || doublejump == true)
        {
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(this.GetComponent<Rigidbody2D>().velocity.x, jumpHeight);
            doublejump = false;
        }
    }
    public bool CheckCollider(Vector2 direction, float distance)
    {
        Debug.DrawRay(transform.position, direction* distance, Color.green);
        if (Physics2D.Raycast(transform.position, direction, distance, GroundLayer))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool IsFalling()
    {
        if (this.GetComponent<Rigidbody2D>().velocity.y < -0.05)
        {
            cameraOffset.y = 0;
            return true;

        }
        else
        {
            cameraOffset.y = 3;
            return false;
        }
    }
    public bool IsEnemyNearbly()
    {
        if (Physics2D.Raycast(transform.position, new Vector2(0.7f, 0), 3, EnemyLayer))
        {

            return true;
        }
        if (Physics2D.Raycast(transform.position, new Vector2(-0.7f, 0), -3, EnemyLayer))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool IsInfoNearbly()
    {
        if (Physics2D.Raycast(transform.position, new Vector2(0.7f, 0), 3, InfoLayer))
        {

            return true;
        }
        if (Physics2D.Raycast(transform.position, new Vector2(-0.7f, 0), -3, InfoLayer))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool isDoubleWallInLeft()
    {

        if (Physics2D.Raycast(transform.position + new Vector3(0, 1), new Vector2(-0.8f, 1f), 1.5f, GroundLayer) == true)
        {
            leftUpCollider = true;

            if (leftCollider && AIGoLeft)
            {
                return true;
            }
        }
        else
        {
            leftUpCollider = false;
            return false;
        }
        return false;
    }
    public bool isDoubleWallInRight()
    {

        if (Physics2D.Raycast(transform.position + new Vector3(0, 1), new Vector2(0.8f, 1f), 1.5f, GroundLayer) == true)
        {
            rightUpCollider = true;

            if (rightCollider && AIGoRight)
            {
                return true;
            }
        }
        else
        {
            rightUpCollider = false;
            return false;
        }
        return false;
    }
    public bool isWallLeftDown()
    {
        if (CheckCollider(new Vector2(-1f, -1f), 1.5f))
        {
           
            downLeftCollider = true;
            return true;
        }
        else
        {
         
            downLeftCollider = false;
            return false;
        }
    }
    public bool isWallRightDown()
    {
        if (CheckCollider(new Vector2(1f, -1f), 1.5f))
        {
            downRightCollider = true;
            return true;
        }
        else
        {
            downRightCollider = false;
            return false;
        }
    }

    public IEnumerator DoubleJumpAI()
    {
        jump();
        yield return new WaitForSeconds(0.5f);
        jump();
    }
    public void Go()
    {
        if (!rightCollider && AIGoRight == true)
        {
            Move(Vector3.right, 0.5f * -1, 1f);
        }
        if (!leftCollider && AIGoLeft == true)
        {
            Move(Vector3.left, 0.5f * 1, 1f);
        }
        if (isDoubleWallInLeft() && downCollider == true)
        {
            StartCoroutine(DoubleJumpAI());
        }
        if (isDoubleWallInRight() && downCollider == true)
        {
            StartCoroutine(DoubleJumpAI());
        }
        else if (rightCollider==true && downCollider == true && AIGoRight)
        {
            jump();
        }
        else if (leftCollider == true && downCollider == true && AIGoLeft)
        {
            jump();
        }

        if (!isWallRightDown() && AIGoRight && downCollider)
        {
            Debug.DrawRay(transform.position, new Vector2(1f, -0.8f) * 1.5f, Color.red, 20f);
            jump();
        }

        if (!isWallLeftDown() && AIGoLeft && downCollider)
        {
            Debug.DrawRay(transform.position, new Vector2(-1f, -0.8f) * 1.5f, Color.yellow, 20f);
            jump();
        }

        if(IsEnemyNearbly()&& downCollider==true)
        { 
            jump();
        }
            RaycastHit2D DirectionHit = Physics2D.CircleCast(transform.position, 1f, transform.forward, 1f, DirectionLayer);
            {
                if (DirectionHit.transform != null)
                {
                    if (DirectionHit.transform.gameObject.name == "left")
                    {
                        AIGoLeft = true;
                        AIGoRight = false;
                    }
                    else
                    {
                        AIGoLeft = false;
                        AIGoRight = true;
                    }
                }
            }
    }
    public void Update()
        {
        downCollider =  CheckCollider(Vector2.down, DownColliderDIstance);
        leftCollider = CheckCollider(Vector2.left, LeftColliderDistance);
        rightCollider = CheckCollider(Vector2.right, RightColliderDistance);
        upCollider = CheckCollider(Vector2.up, UpColliderDistance);
        isFalling = IsFalling();
        enemyCollider = IsEnemyNearbly();
        followPlayer();
        CheckLayer();
        UIToggles();
        if (isAIGoing)
        {
            Go();
        }
     


        if (Input.GetKey(KeyCode.D) && rightCollider == false)
        {
            Move(Vector3.right, rotationForce * -1, pushForce);
        }
        if (Input.GetKey(KeyCode.A) && leftCollider == false)
        {
            Move(Vector3.left, rotationForce, pushForce);
        }
        if (Input.GetKeyDown(KeyCode.W) && downCollider == true)
        {
            jump();
        }
        else if (Input.GetKeyDown(KeyCode.W) && downCollider == false && doublejump == true)
        {
            jump();
            doublejump = false;
        }
        if (Input.GetMouseButtonUp(0))
        {
            
            isAIGoing = true;
            this.transform.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        }
    }
    public void CheckLayer()
    {

        RaycastHit2D PlatformHit = Physics2D.CircleCast(transform.position, 1f, transform.forward, 1f, PlatformLayer);
        {
            if(PlatformHit.transform != null)
            {
                transform.parent = PlatformHit.transform;
                platformCollider = true;
            }
            else
            {
                transform.parent = null;
                platformCollider = false;
            }
        }
        RaycastHit2D CoinHit = Physics2D.CircleCast(transform.position, 1f, transform.forward, 1f, CoinLayer);
        {
            if (CoinHit.transform !=null)
            {
                Destroy(CoinHit.transform.gameObject);
                Coins += 1;
                CoinsText.text = Coins.ToString();
            
            }
        }

        RaycastHit2D InfoHit = Physics2D.CircleCast(transform.position, 1f, transform.forward, 1f, InfoLayer);
        {
            if (InfoHit.transform != null && InfoHit.transform.gameObject.GetComponent<Info>().entered == false)
            {
                InfoHit.transform.gameObject.GetComponent<Info>().textmesh.gameObject.SetActive(true);
                InfoHit.transform.gameObject.GetComponent<Info>().textmesh.text = InfoHit.transform.gameObject.GetComponent<Info>().info;

                this.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
                isAIGoing = false;
                InfoHit.transform.gameObject.GetComponent<Info>().entered = true;
            }
        }

        RaycastHit2D FinishHit = Physics2D.CircleCast(transform.position, 1f, transform.forward, 1f, FinishLayer);
        {
            if (FinishHit.transform != null)
            {
                FinishHit.transform.gameObject.GetComponent<FinishMap>().OpenNextLevel();
             
            }
        }

        RaycastHit2D EnemyHit = Physics2D.CircleCast(transform.position, 0.7f, transform.forward, 0.7f, EnemyLayer);
        {
            if (EnemyHit.transform != null)
            {
                enemyCollider = true;
                Application.LoadLevel(Application.loadedLevel);
            } 
        }
    }
    public void followPlayer()
    {
        if(AIGoRight)
        PlayerCamera.transform.position = Vector3.Slerp(PlayerCamera.transform.position, transform.position + cameraOffset, FollowSpeed * Time.deltaTime);
        if(AIGoLeft)
        PlayerCamera.transform.position = Vector3.Slerp(PlayerCamera.transform.position, transform.position + new Vector3(cameraOffset.x*(-3), cameraOffset.y, cameraOffset.z) , FollowSpeed * Time.deltaTime);
        UpText.transform.position = Vector3.Slerp(UpText.transform.position, transform.position + UpTextOffset, 15 * Time.deltaTime);
    }

}