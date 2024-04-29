using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player_Controller : MonoBehaviour
{
    public Camera Eyes;
    public float MouseSensitivity = 1;

    public GameObject StartPos;
    public GameObject DoorTP;
    public GameObject WindowTP;
    public GameObject ClosetTP;

    public GameObject Flashlight;
    public GameObject ShortFlashlight;
    public bool canFlash = true;
    public bool canShortFlash = false;
    public bool ShortToggle = true;

    public bool canMove = true;
    public bool canTP = true;

    public Quaternion startRot;
    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
        
        ShortFlashlight.SetActive(false);
        Flashlight.SetActive(false);
        startRot = Eyes.transform.rotation;
    }

    
    void Update()
    {
        if (canMove)
        {
            float xRot = Input.GetAxis("Mouse X") * MouseSensitivity;
            float yRot = -Input.GetAxis("Mouse Y") * MouseSensitivity;
            transform.Rotate(0,xRot,0);
            Eyes.transform.Rotate(yRot,0,0);
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (canShortFlash)
            {
                if (ShortToggle)
                {
                    ShortFlashlight.SetActive(true);
                    ShortToggle = false;
                }
                
                else
                {
                    ShortToggle = true;
                    ShortFlashlight.SetActive(false);
                }

                return;
            }
            
            if (!canFlash)
            {
                canFlash = true;
                Flashlight.SetActive(false);
            }
            else
            {
                Flashlight.SetActive(true);
                canFlash = false;
            }
        }

        if (Input.GetKey(KeyCode.A) && canTP)
        {
            Eyes.transform.rotation = startRot;
            Eyes.transform.Rotate(0,-90,0);
            transform.position = WindowTP.transform.position;

            canMove = false;
            canShortFlash = true;
            canTP = false;
            
            Flashlight.SetActive(false);
        }
        if (Input.GetKey(KeyCode.W) && canTP)
        {
            Eyes.transform.rotation = startRot;
            transform.position = ClosetTP.transform.position;
            
            canMove = false;
            canShortFlash = true;
            canTP = false;
            
            Flashlight.SetActive(false);
        }
        if (Input.GetKey(KeyCode.D) && canTP)
        {
            Eyes.transform.rotation = startRot;
            transform.position = DoorTP.transform.position;
            
            canMove = false;
            canShortFlash = true;
            canTP = false;
            
            Flashlight.SetActive(false);
        }
        if (Input.GetKey(KeyCode.S) && !canTP)
        {
            Eyes.transform.rotation = startRot;
            transform.position = StartPos.transform.position;
            
            canMove = true;
            canShortFlash = false;
            canTP = true;
            
            ShortFlashlight.SetActive(false);
            Flashlight.SetActive(false);  
            
        }
    }
    
}