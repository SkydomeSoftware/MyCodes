using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCollider : MonoBehaviour
{
    public LayerMask EnemyLayer;
    CarMovement m_carMovement;
    public float DistanceLeft, DistanceRight, DistanceUp;



    private void Awake()
    {
        m_carMovement = GetComponent<CarMovement>();
    }
    private void Start()
    {
        EnemyLayer = LayerMask.GetMask("Enemy");
    }
    public void DrawTires()
    {
        Debug.DrawRay(transform.position, transform.rotation*Vector2.up*0.03f  , Color.black, 100f);
    }
    public void CheckCollider(Vector2 direction, Color color, ref float distance)
    {
        RaycastHit2D hit = Physics2D.Raycast(this.gameObject.transform.localPosition, transform.rotation * direction, EnemyLayer);

        if (hit.transform != null)
        {
            distance = hit.distance;
            Debug.DrawRay(transform.position, transform.rotation * direction * hit.distance, color, 0.01f, false);
        }
    }
    public void AutoDrive()
    {

        
        if(DistanceUp>1.6f)
        {
            m_carMovement.SetEnginePower(100);
        }
        else if (DistanceUp < 0.9f)
        {
            m_carMovement.SetEnginePower(0);
        }
        if (DistanceLeft <  2f )
        {
            m_carMovement.SetSteeringDirection(-0.6f);
        }
        else if(DistanceRight<2f)
        {
            m_carMovement.SetSteeringDirection(0.6f);
        }
        else
        {
            m_carMovement.SetSteeringDirection(0f);
        }
        
        Debug.Log("autodrive");
    }
    public void Update()
    {
        AutoDrive();
        DrawTires();
        CheckCollider(Vector2.up, Color.green,ref DistanceUp);
        CheckCollider(new Vector2(-0.75f,1), Color.red, ref DistanceLeft);
        CheckCollider(new Vector2(0.75f, 1), Color.blue,ref  DistanceRight);
    }

}
