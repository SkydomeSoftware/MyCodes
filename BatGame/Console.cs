using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System;
[RequireComponent(typeof(RectTransform))]
public class Console : MonoBehaviour
{
  

    public TMP_InputField inputFieldConsole;
    public InputField firstLine, value;
    public Scrollbar scrollbarConsole;
    public Camera playerCamera;

    public GameObject player, world, backGround;


    private RectTransform rectTransform;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta;
    }

    public void OpenCloseinputFieldConsole()
    {
        if (this.gameObject.activeSelf)
        {
            this.gameObject.gameObject.SetActive(false);
        }
        else
        {
            this.gameObject.gameObject.SetActive(true);
        }
    }

    public void copyToClipBoard()
    {
        GUIUtility.systemCopyBuffer = inputFieldConsole.text;
    }

    public void ClearinputFieldConsole()
    {
        inputFieldConsole.text = "";
    }

    public void Changevalue()
    {
       string command = "";
       string Svalue = "";
        float value = 1;
       string commandInput = firstLine.text;

        if (commandInput.Contains(" "))
        {
            Svalue = commandInput.Substring(commandInput.IndexOf(" "));
            command = commandInput.Substring(0, commandInput.IndexOf(" ") + 1);
        }
        else
        {
            command = commandInput;
        }
        command = command.Trim(' ');
        command = command.ToLower();

        if (Svalue != "")
            value = float.Parse(Svalue.Substring(Svalue.IndexOf(" ")));

        Debug.Log(command);
        Debug.Log(value);

        switch (command)
        {

            case "playerinfo":
                inputFieldConsole.text += "playerPosition: " + "<color=red>X" + player.transform.position.x.ToString("0.00") + "</color >" + "<color=blue> Y" + player.transform.position.y.ToString("0.00") + "</color>" + "<color=green> Z" + player.transform.position.z.ToString("0.00") + "</color>";
                inputFieldConsole.text += "<color=yellow>Available Commands:</color>" + System.Environment.NewLine;
                inputFieldConsole.text += "For example in Command enter: <color=blue>AnimationSpeed </color><color=red>1</color>" + System.Environment.NewLine + System.Environment.NewLine;
                inputFieldConsole.text += "Commands:" + System.Environment.NewLine;
                inputFieldConsole.text += "AnimationSpeed: " + player.GetComponent<Animator>().speed + System.Environment.NewLine;
                inputFieldConsole.text += "SoundSpeed: " + player.GetComponent<AudioSource>().pitch + System.Environment.NewLine;
                inputFieldConsole.text += "FlyUpForce: " + player.GetComponent<CharacterController>().flyForce + System.Environment.NewLine;
                inputFieldConsole.text += "FlySidesForce: " + player.GetComponent<CharacterController>().pushForce + System.Environment.NewLine;
                inputFieldConsole.text += "LinearDrag: " + player.GetComponent<Rigidbody2D>().drag + System.Environment.NewLine;
                inputFieldConsole.text += "playerGravity: " + player.GetComponent<CharacterController>().gravityScale+ System.Environment.NewLine;
                inputFieldConsole.text += "playerMaxHP: " + player.GetComponent<CharacterController>().playerMaxHP + System.Environment.NewLine;
                inputFieldConsole.text += "playerHP: " + player.GetComponent<CharacterController>().playerHP + System.Environment.NewLine;
                inputFieldConsole.text += "playerEnergy: " + player.GetComponent<CharacterController>().playerEnergy + System.Environment.NewLine;
                inputFieldConsole.text += "playerMaxEnergy: " + player.GetComponent<CharacterController>().playerMaxEnergy + System.Environment.NewLine;
                
                break;



            //player
            case "animationspeed":
                inputFieldConsole.text += "Changed " + "AnimationSpeed from <color=red>" + player.GetComponent<Animator>().speed  + "</color> to: <color=green>" + value + "</color>"+ System.Environment.NewLine;
                player.GetComponent<Animator>().speed = value;
                
                break;
            case "soundspeed":
                inputFieldConsole.text += "Changed " + "SoundSpeed from <color=red>" + player.GetComponent<AudioSource>().pitch + "</color> to: <color=green>" + value + "</color>" + System.Environment.NewLine;
                player.GetComponent<AudioSource>().pitch = value;
               
                break;

            case "flyupforce":
                inputFieldConsole.text += "Changed " + "FlyUpForce from <color=red>" + player.GetComponent<CharacterController>().flyForce + "</color> to: <color=green>" + value + "</color>" + System.Environment.NewLine;
                player.GetComponent<CharacterController>().flyForce = value;
               
                break;

            case "linearMoveForce":
                inputFieldConsole.text += "Changed " + "linearMoveForce from <color=red>" + player.GetComponent<CharacterController>().pushForce + "</color> to: <color=green>" + value + "</color>" + System.Environment.NewLine;
                player.GetComponent<CharacterController>().pushForce = value;
               
                break;


            case "gravityScale":
                inputFieldConsole.text += "Changed " + "gravityScale from <color=red>" + player.GetComponent<CharacterController>().gravityScale + "</color> to: <color=green>" + value + "</color>" + System.Environment.NewLine;
                player.GetComponent<CharacterController>().gravityScale = value;
               
                break;

            case "lineardrag":
                inputFieldConsole.text += "Changed " + "LinearDrag from <color=red>" + player.GetComponent<Rigidbody2D>().drag + "</color> to: <color=green>" + value + "</color>" + System.Environment.NewLine;
                player.GetComponent<Rigidbody2D>().drag = value;
            
                break;

            case "playerHP":
                inputFieldConsole.text += "Changed " + "playerHP from <color=red>" + player.GetComponent<Rigidbody2D>().drag + "</color> to: <color=green>" + value + "</color>" + System.Environment.NewLine;
                player.GetComponent<CharacterController>().playerHP = value;
                
                break;

            case "playerMaxHP":
                inputFieldConsole.text += "Changed " + "playerHP from <color=red>" + player.GetComponent<Rigidbody2D>().drag + "</color> to: <color=green>" + value + "</color>" + System.Environment.NewLine;
                player.GetComponent<CharacterController>().playerMaxHP = value;
                player.GetComponent<CharacterController>().hpSlider.maxValue = value;
                break;

            case "playerEnergy":
                inputFieldConsole.text += "Changed " + "playerHP from <color=red>" + player.GetComponent<Rigidbody2D>().drag + "</color> to: <color=green>" + value + "</color>" + System.Environment.NewLine;
                player.GetComponent<CharacterController>().playerEnergy = value;
               
                break;

            case "playerMaxEnergy":
                inputFieldConsole.text += "Changed " + "playerHP from <color=red>" + player.GetComponent<Rigidbody2D>().drag + "</color> to: <color=green>" + value + "</color>" + System.Environment.NewLine;
                player.GetComponent<CharacterController>().playerMaxEnergy= value;
                player.GetComponent<CharacterController>().energySlider.maxValue = value;
                break;

            //world

            case "worldinfo":
                inputFieldConsole.text += "<color=red>Don't use spacebar to enter command</color>" + System.Environment.NewLine;
                inputFieldConsole.text += "For example in Command enter: <color=blue>SmallMountainsPositionY</color> and in value: <color=red>0,1</color>" + System.Environment.NewLine + System.Environment.NewLine;
                inputFieldConsole.text += "SkyPosition <b><color=red>Y:</color></b> " + backGround.transform.GetChild(0).GetComponent<Transform>().position.y.ToString("0.00") + " <b><color=green> Z: </color></b>" + backGround.transform.GetChild(0).GetComponent<Transform>().position.z.ToString("0.00") + System.Environment.NewLine;
                inputFieldConsole.text += "Clouds1Position <b><color=red>Y:</color></b> " + backGround.transform.GetChild(1).GetComponent<Transform>().position.y.ToString("0.00") + " <b><color=green> Z: </color></b>" + backGround.transform.GetChild(1).GetComponent<Transform>().position.z.ToString("0.00") + System.Environment.NewLine;
                inputFieldConsole.text += "Clouds2Position <b><color=red>Y:</color></b> " + backGround.transform.GetChild(2).GetComponent<Transform>().position.y.ToString("0.00") + " <b><color=green> Z: </color></b>" + backGround.transform.GetChild(2).GetComponent<Transform>().position.z.ToString("0.00") + System.Environment.NewLine;
                inputFieldConsole.text += "BigMountainsPosition <b><color=red>Y:</color></b> " + backGround.transform.GetChild(3).GetComponent<Transform>().position.y.ToString("0.00") + "<b><color=green> Z: </color></b>" + backGround.transform.GetChild(3).GetComponent<Transform>().position.z.ToString("0.00") + System.Environment.NewLine;
                inputFieldConsole.text += "SmallMountainsPosition <b><color=red>Y:</color></b> " + backGround.transform.GetChild(4).GetComponent<Transform>().position.y.ToString("0.00") + " <b><color=green> Z: </color></b>" + backGround.transform.GetChild(4).GetComponent<Transform>().position.z.ToString("0.00") + System.Environment.NewLine;
                inputFieldConsole.text += "EarthPosition <b><color=red>Y:</color></b> " + backGround.transform.GetChild(5).GetComponent<Transform>().position.y.ToString("0.00") + " <b><color=green> Z: </color></b>" + backGround.transform.GetChild(5).GetComponent<Transform>().position.z.ToString("0.00") + System.Environment.NewLine;

                break;

            case "skypositiony":
                inputFieldConsole.text += "Changed " + "SkyPosition" + "from " + backGround.transform.GetChild(0).GetComponent<Transform>().position.y.ToString("0.00") + " to: " + value + System.Environment.NewLine;
                backGround.transform.GetChild(0).GetComponent<Transform>().position = new Vector3 (backGround.transform.GetChild(0).GetComponent<Transform>().position.x, value, backGround.transform.GetChild(0).GetComponent<Transform>().position.z);
               
                break;

            case "clouds1positionyY":
                inputFieldConsole.text += "Changed " + "Clouds1PositionY" + "from " + backGround.transform.GetChild(1).GetComponent<Transform>().position.y.ToString("0.00") + " to: " + value + System.Environment.NewLine;
                backGround.transform.GetChild(1).GetComponent<Transform>().position = new Vector3(backGround.transform.GetChild(0).GetComponent<Transform>().position.x, value, backGround.transform.GetChild(1).GetComponent<Transform>().position.z);
              
                break;


            case "clouds2positiony":
                inputFieldConsole.text += "Changed " + "Clouds2PositionY" + "from " + backGround.transform.GetChild(2).GetComponent<Transform>().position.y.ToString("0.00") + " to: " + value + System.Environment.NewLine;
                backGround.transform.GetChild(2).GetComponent<Transform>().position = new Vector3(backGround.transform.GetChild(0).GetComponent<Transform>().position.x, value, backGround.transform.GetChild(2).GetComponent<Transform>().position.z);
              
                break;

            case "bigmountainspositiony":
                inputFieldConsole.text += "Changed " + "BigMountainsPositionY" + "from " + backGround.transform.GetChild(3).GetComponent<Transform>().position.y.ToString("0.00") + " to: " + value + System.Environment.NewLine;
                backGround.transform.GetChild(3).GetComponent<Transform>().position = new Vector3(backGround.transform.GetChild(0).GetComponent<Transform>().position.x, value, backGround.transform.GetChild(3).GetComponent<Transform>().position.z);
               
                break;

            case "smallmountainspositiony":
                inputFieldConsole.text += "Changed " + "SmallMountainsPositionY" + "from " + backGround.transform.GetChild(4).GetComponent<Transform>().position.y.ToString("0.00") + " to: " + value + System.Environment.NewLine;
                backGround.transform.GetChild(4).GetComponent<Transform>().position = new Vector3(backGround.transform.GetChild(0).GetComponent<Transform>().position.x, value, backGround.transform.GetChild(4).GetComponent<Transform>().position.z);
                
                break;

            case "earthpositiony":
                inputFieldConsole.text += "Changed " + "EarthPositionY" + "from " + backGround.transform.GetChild(5).GetComponent<Transform>().position.y.ToString("0.00") + " to: " + value + System.Environment.NewLine;
                backGround.transform.GetChild(5).GetComponent<Transform>().position = new Vector3(backGround.transform.GetChild(0).GetComponent<Transform>().position.x, value, backGround.transform.GetChild(5).GetComponent<Transform>().position.z);
                
                break;







            case "skypositionz":
                inputFieldConsole.text += "Changed " + "SkyPositionZ" + "from " + backGround.transform.GetChild(0).GetComponent<Transform>().position.z.ToString("0.00") + " to: " + value + System.Environment.NewLine;
                backGround.transform.GetChild(0).GetComponent<Transform>().position = new Vector3(backGround.transform.GetChild(0).GetComponent<Transform>().position.x, backGround.transform.GetChild(0).GetComponent<Transform>().position.y, value);
              
                break;
                

            case "clouds1positionz":
                inputFieldConsole.text += "Changed " + "SkyPositionZ" + "from " + backGround.transform.GetChild(1).GetComponent<Transform>().position.z.ToString("0.00") + " to: " + value + System.Environment.NewLine;
                backGround.transform.GetChild(1).GetComponent<Transform>().position = new Vector3(backGround.transform.GetChild(1).GetComponent<Transform>().position.x, backGround.transform.GetChild(1).GetComponent<Transform>().position.y, value);
           
                break;


            case "clouds2positionz":
                inputFieldConsole.text += "Changed " + "SkyPositionZ" + "from " + backGround.transform.GetChild(2).GetComponent<Transform>().position.z.ToString("0.00") + " to: " + value + System.Environment.NewLine;
                backGround.transform.GetChild(2).GetComponent<Transform>().position = new Vector3(backGround.transform.GetChild(2).GetComponent<Transform>().position.x, backGround.transform.GetChild(2).GetComponent<Transform>().position.y, value);
               
                break;


            case "bigmountainspositionz":
                inputFieldConsole.text += "Changed " + "SkyPositionZ" + "from " + backGround.transform.GetChild(3).GetComponent<Transform>().position.z.ToString("0.00") + " to: " + value + System.Environment.NewLine;
                backGround.transform.GetChild(3).GetComponent<Transform>().position = new Vector3(backGround.transform.GetChild(3).GetComponent<Transform>().position.x, backGround.transform.GetChild(3).GetComponent<Transform>().position.y, value);
              
                break;

            case "smallmountainspositionz":
                inputFieldConsole.text += "Changed " + "SkyPositionZ" + "from " + backGround.transform.GetChild(4).GetComponent<Transform>().position.z.ToString("0.00") + " to: " + value + System.Environment.NewLine;
                backGround.transform.GetChild(4).GetComponent<Transform>().position = new Vector3(backGround.transform.GetChild(4).GetComponent<Transform>().position.x, backGround.transform.GetChild(4).GetComponent<Transform>().position.y, value);
                
                break;

            case "earthpositionz":
                inputFieldConsole.text += "Changed " + "SkyPositionZ" + "from " + backGround.transform.GetChild(5).GetComponent<Transform>().position.z.ToString("0.00") + " to: " + value + System.Environment.NewLine;
                backGround.transform.GetChild(5).GetComponent<Transform>().position = new Vector3(backGround.transform.GetChild(5).GetComponent<Transform>().position.x, backGround.transform.GetChild(5).GetComponent<Transform>().position.y, value);
              
                break;


                //camera
            case "camerainfo":
                inputFieldConsole.text += "<color=yellow>Don't use spacebar to enter command</color>" + System.Environment.NewLine;
                inputFieldConsole.text += "For example in Command enter: <color=blue>FollowSpeed</color> and in value: <color=red>1</color>" + System.Environment.NewLine + System.Environment.NewLine;
                inputFieldConsole.text += "Commands:" + System.Environment.NewLine;
                inputFieldConsole.text += "CameraDistance: " + playerCamera.GetComponent<Camera>().orthographicSize + System.Environment.NewLine;
                inputFieldConsole.text += "FollowSpeed: " + playerCamera.GetComponent<CameraFollow>().FollowSpeed + System.Environment.NewLine;
                inputFieldConsole.text += "DistanceFromGround: " + playerCamera.GetComponent<CameraFollow>().distanaceFromGround + System.Environment.NewLine;
                break;


            case "cameradistance":
                inputFieldConsole.text += "Changed " + "CameraDistance" + "from " + playerCamera.GetComponent<Camera>().orthographicSize + " to: " + value + System.Environment.NewLine;
                playerCamera.GetComponent<Camera>().orthographicSize = value;

                break;

            case "followspeed":
                inputFieldConsole.text += "Changed " + "FollowSpeed" + "from " + playerCamera.GetComponent<CameraFollow>().FollowSpeed + " to: " + value + System.Environment.NewLine;
                playerCamera.GetComponent<CameraFollow>().FollowSpeed = value;

                break;

            case "distnceFromGround":
                inputFieldConsole.text += "Changed " + "DistnaceFromGround" + "from " + playerCamera.GetComponent<CameraFollow>().distanaceFromGround + " to: " + value + System.Environment.NewLine;
                playerCamera.GetComponent<CameraFollow>().distanaceFromGround = value;

                break;

                
            default:
                inputFieldConsole.text += "Bad name or value. Try use (,) not (.) in value"+ System.Environment.NewLine;
                break;

        }
        StartCoroutine(BarUpdate());
     
    }

    public IEnumerator BarUpdate()
    {
        yield return null;
        scrollbarConsole.value = 1f;
    }
}






