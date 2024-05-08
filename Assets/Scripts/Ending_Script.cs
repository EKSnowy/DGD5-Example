using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

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

    public Player_Controller PC;

    public GameObject screenOff;
    public GameObject screenOn;

    public GameObject End;

    public AudioSource AS;
    public AudioSource Ambience;

    public bool atComputer;

    void Start()
    {
        End.SetActive(false);
        
        startRot = Eyes.transform.rotation;
        
        PC.DMOff();
    }
    
    void Update()
    {
        if (Physics.Raycast(Eyes.transform.position, Eyes.transform.forward,
                out RaycastHit hit, 20))
        {
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
        
        ///////// Teleports to Window \\\\\\\\\\\\
        
        if (Input.GetKey(KeyCode.W) && canTPWindow)
        {
            Eyes.transform.rotation = startRot;
            Eyes.transform.Rotate(0,-90,0);
            
            transform.position = WindowTP.transform.position;
        
            PC.setMove();
            canTPWindow = false;
        }
        
        if (Input.GetKey(KeyCode.W) && canTPComputer)
        {
            Eyes.transform.rotation = startRot;
            Eyes.transform.Rotate(0,180,0);
            
            transform.position = ComputerTP.transform.position;
        
            PC.setMove();
            
            screenOn.SetActive(true);
            canTPComputer = false;

            atComputer = true;
        }

        if (atComputer)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            
            PC.endGame();
        }
    }

    public void onQuit()
    {
        AS.Play();
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
