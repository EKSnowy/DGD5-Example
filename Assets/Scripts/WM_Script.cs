using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WM_Script : MonoBehaviour
{
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
    
    void Start()
    {
        atPos0 = true;

        Pos0Timer = Random.Range(9f,12f);
    }
    
    void Update()
    {
        if (atPos0)
        {
            WindowClosed.SetActive(true);
            WindowOpen.SetActive(false);
            
            Pos0Timer -= Time.deltaTime;

            if (Pos0Timer < 0)
            {
                atPos1 = true;
                Pos1Timer = Random.Range(2f,4f);
                
                atPos0 = false;
            }
        }
        
        else if (atPos1)
        {
            Pos1.SetActive(true);
            
            Pos1Timer -= Time.deltaTime;

            if (Pos1Timer < 0)
            {
                atPos2 = true;
                attackTimer = 5f;
                
                atPos1 = false;
            }
        }
        
        else if (atPos2)
        {
            Pos1.SetActive(false);
            Pos2.SetActive(true);
            
            WindowClosed.SetActive(false);
            WindowOpen.SetActive(true);
            
            attackTimer -= Time.deltaTime;

            if (attackTimer < 0)
            {
                atPos2 = false;
                
                Debug.Log("WM attack");
            }
        }
        
    }

}
