using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CPM_Script : MonoBehaviour
{
    public Rigidbody RB;
    
    public float Speed = 10f;

    public Player_Controller PC;

    public float Pos0Timer;
    public bool atPos0;

    public float attackTimer = 5f;
    public bool atAttack = false;

    public bool isFlashed;

    public GameObject HandClosed;
    public GameObject HandOpened;

    public float resetTimer;

    public Game_Manager GM;

    public Audio_Script AS;
    public bool toggle = true;
    public int random;
    
    void Start()
    {
        Pos0Timer = Random.Range(10f, 20f);

        resetTimer = 5f;
    }
    
    void Update()
    {
        Quicken();
        
        Vector3 vel = RB.velocity;

        if (!atPos0 && !atAttack)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y,
                -44.2f);
            
            resetTimer = 5f;
            
            Pos0Timer -= Time.deltaTime; 
           
           if (Pos0Timer < 0)
           {
               atPos0 = true;
           }
        }
        
        /// Continuously moves forwards until certain point \\\
        if (atPos0)
        {
            resetTimer -= Time.deltaTime;
            
            HandClosed.SetActive(true);
            HandOpened.SetActive(false);
            
            attackTimer = 5f;
            
            vel.z = Speed * Time.deltaTime;
                        
            RB.velocity = vel;

            if (transform.position.z > -2.1f)
            {
                atPos0 = false;
                atAttack = true;
            }
            else
            {
               atAttack = false; 
            }
            
            if (transform.localPosition.z < -44.03f && resetTimer < 0)
            {
                atPos0 = false;
                ResetTimer();
            }
        }

        else if (atAttack)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y,
                -41.08f);
            
            HandClosed.SetActive(false);
            HandOpened.SetActive(true);
            Randomize();
            
            attackTimer -= Time.deltaTime;

            if (attackTimer < 0)
            {
                Debug.Log("CPM Attack");
                //SceneManager.LoadScene("GameOverCPM");
                
                atAttack = false;
            }

            if (isFlashed)
            {
                transform.localPosition = new Vector3 (transform.localPosition.x,transform.localPosition.y,
                    -41.2f);

                atAttack = false;
                atPos0 = true;
            }
        }
        
        if ((isFlashed && PC.getFlashing() && atPos0))
        {
            toggle = true;
            
            vel.z = (-Speed * 2) * Time.deltaTime;
        
            RB.velocity = vel;
        }
        else
        {
            isFlashed = false;
        }
    }
    
    private void OnTriggerStay(Collider other)
    {
        Light_Script light = other.gameObject.GetComponent<Light_Script>();
        
        if (light != null)
        {
            isFlashed = true;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        isFlashed = false;
    }
    
    public void ResetTimer()
    {
        if (GM.Accelerate() == 0)
        {
            Pos0Timer = Random.Range(10f, 20f);
        }
        
        else if (GM.Accelerate() == 1)
        {
            Pos0Timer = Random.Range(10f, 15f);
            Speed = 20f;
        }
        
        else if (GM.Accelerate() == 2)
        {
            Pos0Timer = Random.Range(8f, 10f);
            Speed = 30f;
        }
    }
    
    public void Quicken()
    {
        if (GM.Accelerate() == 0)
        {
            Speed = 10f;
        }
        
        else if (GM.Accelerate() == 1)
        {
            Speed = 25f;
        }
        
        else if (GM.Accelerate() == 2)
        {
            Speed = 30f;
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

}
