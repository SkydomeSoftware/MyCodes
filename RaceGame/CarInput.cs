using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CarMovement))]


public class CarInput : MonoBehaviour
{
    CarMovement m_carMovement;

    private void Awake()
    {
        m_carMovement = GetComponent<CarMovement>();
    }
    protected void SetSteeringDirection(float steeringDirection)
    {
        m_carMovement.SetSteeringDirection(steeringDirection);
    }
    protected void SetEnginePower(float enginePower)
    {
        m_carMovement.SetEnginePower(enginePower);
    }
}
