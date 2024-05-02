using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WM_Script : MonoBehaviour
{
    public Player_Controller PC;
    public Vector3 noMovement = new Vector3(0, 0, 0);
    
    public GameObject Pos1;
    public GameObject Pos2;

    public GameObject WindowClosed;
    public GameObject WindowOpen;

    public float Pos0Timer;
    public float Pos1Timer;
    public float attackTimer;

    public bool atPos0;
    public bool atPos1 = false;
    public bool atPos2 = false;

    public float resetTimer;
    
    void Start()
    {
        atPos0 = true;

        Pos0Timer = Random.Range(30f,40f);
    }
    
    void Update()
    {
        if (atPos0)
        {
            resetTimer = 5f;
            attackTimer = 5f;
            
            Pos1.SetActive(false);
            Pos2.SetActive(false);
            
            WindowClosed.SetActive(true);
            WindowOpen.SetActive(false);
            
            Pos0Timer -= Time.deltaTime;

            if (Pos0Timer < 0)
            {
                atPos1 = true;
                Pos1Timer = Random.Range(3f,4f);
                
                atPos0 = false;
            }
        }
        
        else if (atPos1)
        {
            Pos1.SetActive(true);
            
            if (PC.getCamMov() == noMovement && !PC.getFlashing())
            {
                resetTimer -= Time.deltaTime;

                if (resetTimer <= 0)
                {
                    Pos0Timer = Random.Range(20f, 30f);
                    
                    atPos1 = false;
                    atPos0 = true;
                }
            }
            else
            {
               Pos1Timer -= Time.deltaTime; 
            }
            
            if (Pos1Timer < 0)
            {
                atPos2 = true;
                atPos1 = false;
                
                resetTimer = 3f;
            }
        }
        
        else if (atPos2)
        {
            Pos1.SetActive(false);
            Pos2.SetActive(true);
            
            WindowClosed.SetActive(false);
            WindowOpen.SetActive(true);

            if (PC.getCamMov() == noMovement && !PC.getFlashing())
            {
                resetTimer -= Time.deltaTime;

                if (resetTimer <= 0)
                {
                    Pos0Timer = Random.Range(20f, 30f);
                    
                    atPos2 = false;
                    atPos0 = true;
                }
            }
            else
            {
                attackTimer -= Time.deltaTime;
            }
            
            if (attackTimer < 0)
            {
                atPos2 = false;
                
                Debug.Log("WM attack");
            }
        }
        
    }

}
