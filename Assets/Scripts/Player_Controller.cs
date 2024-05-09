using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

public class Player_Controller : MonoBehaviour
{
    public Camera Eyes;
    public float MouseSensitivity = 1;

    public GameObject StartPos;
    public GameObject DoorTP;
    
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

    public DM_Script DM;
    public bool atDoor = false;
    
    public Vector3 cameraMovement;

    public GameObject interactDoor;
    public GameObject interactCloset;

    public bool canTPDoor;
    public bool canTPCloset;

    public AudioSource AS;

    public GameObject Hints;
    public float hintTimer;
    public bool showHints = true;

    public Game_Manager GM;
   
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        ShortFlashlight.SetActive(false);
        Flashlight.SetActive(false);
        startRot = Eyes.transform.rotation;

        TPCooldown = .5f;
        hintTimer = 20f;
        
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
                    AS.volume = .2f;
                    AS.pitch = (Random.Range(.5f,.8f));
                    AS.Play();
                    
                    ShortFlashlight.SetActive(true);
                    ShortToggle = false;
                    
                    isFlashing = true;

                    if (atCloset)
                    {
                        if(GM.Accelerate() == 0)
                            CM.goBack(.2f);
                        
                        if(GM.Accelerate() == 1)
                            CM.goBack(.25f);
                        
                        if(GM.Accelerate() == 2)
                            CM.goBack(.3f);
                    }
                    
                    if(atDoor)
                        DM.goBack();
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
                AS.volume = .2f;
                AS.pitch = (Random.Range(.8f,1f));
                AS.Play();
                
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

            atDoor = true;
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
            atDoor = false;
            canMove = true;
            canShortFlash = false;

            isFlashing = false;
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

        if (showHints)
        {
            hintTimer -= Time.deltaTime;

            if (hintTimer < 0)
            {
                Hints.SetActive(false);
                showHints = false;
            }

            if (Input.GetKeyDown(KeyCode.H))
            {
                Hints.SetActive(false);
                showHints = false;
            }
        }
        
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