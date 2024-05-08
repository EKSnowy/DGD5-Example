using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Ending_Script : MonoBehaviour
{
    public Quaternion startRot;
    
    public Camera Eyes;

    public GameObject interactWindow;
    public GameObject interactComputer;

    public bool canTPWindow;
    public bool canTPComputer;

    public GameObject WindowTP;
    public GameObject ComputerTP;
    
    public GameObject screenOn;

    public GameObject End;
    
    public AudioSource Ambience;

    public bool atComputer;

    public Game_Manager GM;
    
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

    public GameObject ClosetClose;
    public GameObject ClosetOpen;
    public GameObject ClosetSliOpen;
    
    public Vector3 cameraMovement;

    public GameObject interactDoor;
    public GameObject interactCloset;

    public bool canTPDoor;
    public bool canTPCloset;

    public AudioSource AS;
    public AudioSource AS2;
    
    void Start()
    {
        End.SetActive(false);
        
        startRot = Eyes.transform.rotation;
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        ShortFlashlight.SetActive(false);
        Flashlight.SetActive(false);
        startRot = Eyes.transform.rotation;

        TPCooldown = .5f;
        
        interactDoor.SetActive(false);
        interactCloset.SetActive(false);
    }

    private void Awake()
    {
        GM.setAmp(5);
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
            
            if (hit.transform.gameObject.tag == "Window")
            {
                interactWindow.SetActive(true);
                canTPWindow = true;
            }
            else
            {
                interactWindow.SetActive(false);
                canTPWindow = false;
            }

            if (hit.transform.gameObject.tag == "Computer")
            {
                interactComputer.SetActive(true);
                canTPComputer = true;
            }
            else
            {
                interactComputer.SetActive(false);
                canTPComputer = false;
            }
        }
        
        ///////// Teleports to Closet \\\\\\\\\\\\
        
        if (Input.GetKey(KeyCode.W) && canTPCloset)
        {
            Eyes.transform.rotation = startRot;
            transform.position = ClosetTP.transform.position;
            
            ClosetClose.SetActive(false);
            ClosetOpen.SetActive(true);
            
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
            
            canMove = true;
            canShortFlash = false;
            
            ShortFlashlight.SetActive(false);
            Flashlight.SetActive(false);
            
        }
        
        
        ///////// Teleports to Window \\\\\\\\\\\\
        
        if (Input.GetKey(KeyCode.W) && canTPWindow)
        {
            Eyes.transform.rotation = startRot;
            Eyes.transform.Rotate(0,-90,0);
            
            transform.position = WindowTP.transform.position;
        
            canMove = false;
            canTPDoor = false;
            canTPCloset = false;
            TPCooldown = 0;
            
            canTPWindow = false;
        }
        
        ///////// Teleports to Computer \\\\\\\\\\\\\\
        
        if (Input.GetKey(KeyCode.W) && canTPComputer)
        {
            Eyes.transform.rotation = startRot;
            Eyes.transform.Rotate(0,180,0);
            
            transform.position = ComputerTP.transform.position;
        
            canMove = false;
            canTPDoor = false;
            canTPCloset = false;
            TPCooldown = 0;
            
            screenOn.SetActive(true);
            canTPComputer = false;

            atComputer = true;
        }

        if (atComputer)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            
            canMove = false;
            TPCooldown = 10;
        
            Flashlight.SetActive(false);
            AS.volume = 0;
        }
    }

    public void onQuit()
    {
        AS2.Play();
        
        Ambience.volume = 0;

        Cursor.visible = false;
        
        StartCoroutine(quitGame());
    }

    public IEnumerator quitGame()
    {
        End.SetActive(true);
        
        yield return new WaitForSeconds(5f);
        
        Application.Quit();
        Debug.Log("Quit");
    }
}
