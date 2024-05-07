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

    public bool isFlashing;

    public bool canMove = true;
    
    public float TPCooldown;
    public bool triggerCooldown;
    
    public Quaternion startRot;

    public GameObject ClosetClose;
    public GameObject ClosetOpen;
    public GameObject ClosetSliOpen;

    public CM_Script CM;
    public bool atCloset = false;

    public Vector3 cameraMovement;

    public GameObject interactDoor;
    public GameObject interactCloset;

    public bool canTPDoor;
    public bool canTPCloset;
    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
        
        ShortFlashlight.SetActive(false);
        Flashlight.SetActive(false);
        startRot = Eyes.transform.rotation;

        TPCooldown = .5f;
        
        interactDoor.SetActive(false);
        interactCloset.SetActive(false);
    }

    
    void Update()
    {
        if (canMove)
        {
            float xRot = Input.GetAxis("Mouse X") * MouseSensitivity;
            float yRot = -Input.GetAxis("Mouse Y") * MouseSensitivity;

            Vector3 eRot = Eyes.transform.localRotation.eulerAngles;
            eRot.x += yRot;

            if (eRot.x < -180)
                eRot.x += 360;

            if (eRot.x > 180)
                eRot.x -= 360;

            eRot = new Vector3(Mathf.Clamp(eRot.x, -90, 90), 0, 0);
            Eyes.transform.localRotation = Quaternion.Euler(eRot);
            
            transform.Rotate(0,xRot,0);
            //Eyes.transform.Rotate(yRot,0,0);
            
            cameraMovement = Eyes.velocity;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (canShortFlash)
            {
                if (ShortToggle)
                {
                    ShortFlashlight.SetActive(true);
                    ShortToggle = false;
                    
                    isFlashing = true;
                    
                    if(atCloset)
                        CM.goBack(.2f);
                }
                
                else
                {
                    ShortToggle = true;
                    ShortFlashlight.SetActive(false);
                    
                    isFlashing = false;
                }

                return;
            }
            
            if (!canFlash)
            {
                canFlash = true;
                Flashlight.SetActive(false);
                
                isFlashing = false;
            }
            else
            {
                Flashlight.SetActive(true);
                canFlash = false;
                
                isFlashing = true;
            }
        }

        if (triggerCooldown)
        {
            TPCooldown -= Time.deltaTime;
        }
        
        ////If hovering over area, shows prompt to go\\\\\
        
            if (Physics.Raycast(Eyes.transform.position, Eyes.transform.forward,
                    out RaycastHit hit, 20))
            {
                if (hit.transform.gameObject.tag == "Door")
                {
                    interactDoor.SetActive(true);
                    canTPDoor = true;
                }
                else
                {
                    interactDoor.SetActive(false);
                    canTPDoor = false;
                }
                
                if (hit.transform.gameObject.tag == "Closet")
                {
                    interactCloset.SetActive(true);
                    canTPCloset = true;
                }
                else
                {
                    interactCloset.SetActive(false);
                    canTPCloset = false;
                }
            }
            
        ///////// Teleports to Window \\\\\\\\\\\\
    
        // if (Input.GetKey(KeyCode.A) && canTP)
        // {
        //     Eyes.transform.rotation = startRot;
        //     Eyes.transform.Rotate(0,-90,0);
        //     transform.position = WindowTP.transform.position;
        //
        //     canMove = false;
        //     canShortFlash = true;
        //     canTP = false;
        //     
        //     Flashlight.SetActive(false);
        //
        //     triggerCooldown = true;
        // }
        
        ///////// Teleports to Closet \\\\\\\\\\\\
        
        if (Input.GetKey(KeyCode.W) && canTPCloset)
        {
            Eyes.transform.rotation = startRot;
            transform.position = ClosetTP.transform.position;
            
            ClosetClose.SetActive(false);
            ClosetOpen.SetActive(true);

            atCloset = true;
            canMove = false;
            canShortFlash = true;
            canTPCloset = false;
            
            Flashlight.SetActive(false);
            
            triggerCooldown = true;
        }
        
        ///////// Teleports to Door \\\\\\\\\\\\
        
        if (Input.GetKey(KeyCode.W) && canTPDoor)
        {
            Eyes.transform.rotation = startRot;
            transform.position = DoorTP.transform.position;
            
            canMove = false;
            canShortFlash = true;
            canTPDoor = false;
            
            Flashlight.SetActive(false);

            triggerCooldown = true;
        }
        
        ///////// Teleports to Start \\\\\\\\\\\\
        
        if (Input.GetKey(KeyCode.S) && !canTPDoor && !canTPCloset && TPCooldown <= 0)
        {
            Eyes.transform.rotation = startRot;
            transform.position = StartPos.transform.position;
            
            TPCooldown = .5f;
            triggerCooldown = false;
            
            ClosetClose.SetActive(true);
            ClosetOpen.SetActive(false);

            atCloset = false;
            canMove = true;
            canShortFlash = false;
            
            ShortFlashlight.SetActive(false);
            Flashlight.SetActive(false);
            
        }
        
        ////////// Visual and Audio Indicator when CM is Attacking \\\\\\\\\\\
        if (CM.isAttacking() && !atCloset)
        {
            ClosetClose.SetActive(false);
            ClosetSliOpen.SetActive(true);
        }
        else
            ClosetSliOpen.SetActive(false);
    }

    public Vector3 getCamMov()
    {
        return cameraMovement;
    }

    public bool getFlashing()
    {
        return isFlashing;
    }
}