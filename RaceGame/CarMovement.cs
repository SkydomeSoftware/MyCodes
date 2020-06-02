using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{


    public float MaximumEngineForce;
    public float MaximumReverseEngineForce;
    public float MaximumSteeringTorque;
    public float RevercePower;
    public float Acceleration;
    public float Deceleration;


    float m_TargetEnginePower = 0f;
    float m_EnginePower = 1f;
    float m_SteeringDirection = 0f;

    Rigidbody2D m_CarBody;


    void Awake()
    {
        m_CarBody = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        UpdateEnginePower();
    }
    void UpdateEnginePower()
    {
        float acceleration = Acceleration;
        if (m_TargetEnginePower == 0)
        {
            acceleration = Deceleration;
        }
        m_EnginePower = Mathf.MoveTowards(m_EnginePower, m_TargetEnginePower, acceleration * Time.deltaTime);
    }
    private void FixedUpdate()
    {
        ApplyEngineForce();
        ApplySteeringForce();
    }
    void ApplyEngineForce()
    {
        float maximumEngineForce = MaximumEngineForce;

        if (m_EnginePower < 0f)
        {
            maximumEngineForce = MaximumReverseEngineForce;
        }
        m_CarBody.AddForce(transform.up * m_EnginePower * MaximumEngineForce, ForceMode2D.Force);
    }
    void ApplySteeringForce()
    {
        m_CarBody.AddTorque(m_SteeringDirection * MaximumSteeringTorque, ForceMode2D.Force);
    }
    public void SetEnginePower(float enginePower)
    {
        m_TargetEnginePower = Mathf.Clamp(enginePower, -1f, 1f);
    }
    public void SetSteeringDirection(float steeringDirection)
    {
        m_SteeringDirection = Mathf.Clamp( steeringDirection, -1f ,1f);
    }
}
