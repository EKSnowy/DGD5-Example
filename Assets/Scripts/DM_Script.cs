using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DM_Script : MonoBehaviour
{
    public GameObject Door;
    public GameObject DoorOpened;

    public bool doorOpen = false;

    public float Pos0Timer;

    public float attackTimer;

    public bool isAttacking;
    public bool attack1;
    public bool attack2;
    public bool attack3;
    public bool attack4;

    public GameObject Hand1;
    public GameObject Hand2;
    public GameObject Hand3;
    public GameObject Hand4;

    public Game_Manager GM;

    public Audio_Script AS;
    public bool toggle = true;
    public bool toggle2 = true;
    public int random;
    
    void Start()
    {
        Pos0Timer = Random.Range(20f,40f);
        attackTimer = Random.Range(2f, 3f);
    }

    
    void Update()
    {
        if (!doorOpen)
        {
            Pos0Timer -= Time.deltaTime;

            if (Pos0Timer < 0)
            {
               DoorOpened.SetActive(true);
               Door.SetActive(false);
               doorOpen = true; 
            }
        }

        if (doorOpen)
        {
            Randomize();
            
            attackTimer -= Time.deltaTime;
            
            if (attackTimer < 0)
            {
                if (!attack1)
                {
                    attack1 = true;
                    attackTimer = Random.Range(1f, 2f);
                    
                    Hand1.SetActive(true);

                    isAttacking = true;
                }
                
                else if (!attack2)
                {
                    attack2 = true;
                    attackTimer = Random.Range(1f, 2f);
                    
                    Hand2.SetActive(true);
                }
                
                else if (!attack3)
                {
                    attack3 = true;
                    attackTimer = Random.Range(1f, 2f);
                    
                    Hand3.SetActive(true);
                }
                
                else if (!attack4)
                {
                    attack4 = true;
                    attackTimer = Random.Range(1f, 2f);
                    
                    Hand4.SetActive(true);
                }
                else
                {
                    //Debug.Log("DM Attack");
                    SceneManager.LoadScene("GameOverDM");
                }
            }
            
        }
        
        if (doorOpen && Input.GetKeyDown(KeyCode.Space) && !isAttacking)
        {
            PlaySound();
            
            DoorOpened.SetActive(false);
            Door.SetActive(true);
            doorOpen = false;
            toggle = true;
            toggle2 = true;
            
            ResetTimer();
            attackTimer = Random.Range(1f, 2f);
        }
    }

    public void goBack()
    {
        if (attack4)
        {
            attack4 = false;
            //attackTimer = Random.Range(3f, 5f);
                    
            Hand4.SetActive(false);
        }
                
        else if (attack3)
        {
            attack3 = false;
            //attackTimer = Random.Range(3f, 5f);
                    
            Hand3.SetActive(false);
        }
                
        else if (attack2)
        {
            attack2 = false;
            //attackTimer = Random.Range(3f, 5f);
                    
            Hand2.SetActive(false);
        }
                
        else if (attack1)
        {
            attack1 = false;
            //attackTimer = Random.Range(2f, 5f);
                    
            Hand1.SetActive(false);
            
            isAttacking = false;
        }
    }

    public void ResetTimer()
    {
        if (GM.Accelerate() == 0)
        {
            Pos0Timer = Random.Range(20f, 40f);
        }
        
        else if (GM.Accelerate() == 1)
        {
            Pos0Timer = Random.Range(20f, 30f);
        }
        
        else if (GM.Accelerate() == 2)
        {
            Pos0Timer = Random.Range(10f, 20f);
        }
    }
    
    public void Randomize()
    {
        if (toggle)
        {
            random = Random.Range(0,2);
            AS.SoundAudio(random);
           
            toggle = false;
        }
        
    }
    
    public void PlaySound()
    {
        ///Plays closing door sound
        if (toggle2)
        { 
            AS.SoundAudio(2);
            toggle2 = false;
        }
    }
}
