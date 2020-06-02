using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float linearMoveForce, pushForce;
    public bool isFlying, isFollowPlayer;
    public float minXOffset, maxXOffset, minYoffset, maxYOffset;
    public float MovementTimeUpDown, MovementTimeSides, minMovementTime, maxMovementTime;
    public float MovementTypeSides, MovementTypeUpDown;
    public float downDistance, topDistance, leftDistance, rightDistance;
    public LayerMask TerrainLayer, CameraWall;
    GameObject GameObjectEnemy;
    

    public void Start()
    {
        GameObjectEnemy = this.gameObject;
        if (isFlying)
        {
            this.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
        }
        minXOffset = GameObjectEnemy.transform.position.x - 0.8f;
        maxXOffset = GameObjectEnemy.transform.position.x + 0.8f;
        minYoffset = GameObjectEnemy.transform.position.y - 0.2f;
        maxYOffset = GameObjectEnemy.transform.position.y + 0.5f;
    }
    private void Update()
    {
        DirectionDrawSides();
        CheckColliders();
        DirectionDrawUpDown();
    }


    public void DirectionDrawSides()
    {
        MovementTimeSides -= Time.deltaTime;
        if (MovementTimeSides <= 0.1f)
        {
            MovementTypeSides = Random.Range(0, 3);
            MovementTimeSides = Random.Range(minMovementTime, maxMovementTime);
            if (MovementTypeSides == 0)
            {
                MovementTimeSides = 0.5f;
            }
                
        }

        if (MovementTypeSides == 1)//move left
        {
            if (GameObjectEnemy.transform.position.x <= minXOffset)
            {
                MovementTypeSides = 2;
            }
            else
            {
                AImovement(Vector3.left);
            }
        }

        if (MovementTypeSides == 2)//move right
        {
            if (GameObjectEnemy.transform.position.x >= maxXOffset)
            {
                MovementTypeSides = 1;
            }
            else
            {
                AImovement(Vector3.right);
            }
        }
    }
    public void DirectionDrawUpDown()
    {
        MovementTimeUpDown -= Time.deltaTime;
        if (MovementTimeUpDown <= 0.1f)
        {
            MovementTypeUpDown = Random.Range(0, 3);
            MovementTimeUpDown = Random.Range(minMovementTime, maxMovementTime);
            if (MovementTypeUpDown == 0)
            {
                MovementTimeUpDown = 0.5f;
            }
        }

        if (MovementTypeUpDown == 1)//move left
        {
            if (GameObjectEnemy.transform.position.y <= minYoffset)
            {
                MovementTypeUpDown = 2;
            }
            else
            {
                AImovement(new Vector3(0, Random.Range(-100f, 0f) / 100, 0));
            }
        }

        if (MovementTypeUpDown == 2)//move right
        {
            if (GameObjectEnemy.transform.position.y >= maxYOffset)
            {
                MovementTypeUpDown = 1;
            }
            else
            {
                AImovement(new Vector3(0, Random.Range(0f, 100f) / 100, 0));
            }
        }
    }
    public void AImovement(Vector3 direction)
    {
        if (direction == Vector3.left)
        {
            GameObjectEnemy.GetComponent<SpriteRenderer>().flipX = true;
        }
        if (direction == Vector3.right)
        {
            GameObjectEnemy.GetComponent<SpriteRenderer>().flipX = false;
        }
        GameObjectEnemy.transform.position += direction * linearMoveForce * Time.deltaTime;
        GameObjectEnemy.GetComponent<Rigidbody2D>().AddForce(direction * pushForce);
    }
    public void CheckColliders()
    {
        RaycastHit2D hitDown = Physics2D.Raycast(transform.position, Vector2.down, 4f, TerrainLayer);
        {
            if (hitDown.collider != null)
                downDistance = Vector2.Distance(transform.position, hitDown.point);
            else
                downDistance = 100;
        }

        RaycastHit2D hitTop = Physics2D.Raycast(transform.position, Vector2.up, 4f, TerrainLayer);
        {
            if (hitTop.collider != null)
            
                topDistance = Vector2.Distance(transform.position, hitTop.point);
            else
                topDistance = 100;
        }

        RaycastHit2D hitRight = Physics2D.Raycast(transform.position, Vector2.right, 4f, TerrainLayer);
        {
            if (hitRight.collider != null)
                rightDistance = Vector2.Distance(transform.position, hitRight.point);
            else
            {
                rightDistance = 100;
            }
              
        }

        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, Vector2.left, 4f, TerrainLayer);
        {
            if (hitLeft.collider != null)
            {
                leftDistance = Vector2.Distance(transform.position, hitLeft.point);
            }
            else
            {
                leftDistance = 100;
            }
        }

        RaycastHit2D hitWall = Physics2D.Raycast(transform.position, Vector2.left, 0.1f, CameraWall);
        {
            if (hitWall.collider != null)
            {
                Destroy(this.gameObject);
            }
          
        }
    }
}
