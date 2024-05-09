using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public Game_Manager GM;

    public Audio_Script AS;
    public int random;
    
    public bool toggle = true;
    public bool toggle2 = true;
    public bool toggle3 = true;
    
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

            Randomize();
            
            if (PC.getCamMov() == noMovement && !PC.getFlashing())
            {
                resetTimer -= Time.deltaTime;

                if (resetTimer <= 0)
                {
                    ResetTimer();
                    PlaySound(4);
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
            PlaySound(3);
            
            WindowClosed.SetActive(false);
            WindowOpen.SetActive(true);

            if (PC.getCamMov() == noMovement && !PC.getFlashing())
            {
                resetTimer -= Time.deltaTime;

                if (resetTimer <= 0)
                {
                    ResetTimer();
                    PlaySound(4);
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

                //Debug.Log("WM Attack");
                SceneManager.LoadScene("GameOverWM");
            }
        }
    }

    public void ResetTimer()
    {
        toggle = true;
        toggle2 = true;
        toggle3 = true;
        
        if (GM.Accelerate() == 0)
        {
            Pos0Timer = Random.Range(20f, 30f);
        }
        
        else if (GM.Accelerate() == 1)
        {
            Pos0Timer = Random.Range(20f, 25f);
        }
        
        else if (GM.Accelerate() == 2)
        {
            Pos0Timer = Random.Range(18f, 20f);
        }
    }

    public void Randomize()
    {
        if (toggle)
        {
           random = Random.Range(0,3);
           AS.SoundAudio(random);
           
           toggle = false;
        }
        
    }

    public void PlaySound(int index)
    {
        ///Plays opening window sound
        if (index == 3)
        {
            if (toggle2)
            { 
                AS.SoundAudio(index);
                
                toggle2 = false;
            }
            
        }
        
        ///Plays closing window sound
        if (index == 4)
        { 
            if (toggle3)
            { 
                AS.SoundAudio(index);
                
                toggle3 = false;
            }
        }
    }

}
