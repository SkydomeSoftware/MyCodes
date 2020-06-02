using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class CameraFollow : MonoBehaviour
{

    public Transform player;
    public float distanaceFromGround;
    public float FollowSpeed = 5f;
    public float LastXposition;
    public float XOffset;
    private void FixedUpdate()
    {
        float groundHigh = player.transform.GetComponent<CharacterController>().GroundHigh;
        LastXposition = this.gameObject.transform.position.x;

        if(LastXposition <= player.transform.position.x+ XOffset)
        {
            transform.position = Vector3.Slerp(transform.position, new Vector3(player.position.x+ XOffset, groundHigh + distanaceFromGround+1, -23), FollowSpeed * Time.deltaTime);
        }
    }
}


