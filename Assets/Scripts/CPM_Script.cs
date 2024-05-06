using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

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
    
    void Start()
    {
        Pos0Timer = Random.Range(10f, 20f);
    }
    
    void Update()
    {
        Vector3 vel = RB.velocity;

        if (!atPos0 && !atAttack)
        {
           Pos0Timer -= Time.deltaTime; 
           
           if (Pos0Timer < 0)
           {
               atPos0 = true;
           }
        }
        
        /// Continuously moves forwards until certain point \\\
        if (atPos0)
        {
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
                atAttack = false;
        }

        else if (atAttack)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -2.1f);
            
            HandClosed.SetActive(false);
            HandOpened.SetActive(true);
            
            attackTimer -= Time.deltaTime;

            if (attackTimer < 0)
            {
                Debug.Log("CPM Attack");
                atAttack = false;
            }

            if (isFlashed)
            {
                transform.position = new Vector3 (transform.position.x,transform.position.y,
                    -2.2f);

                atAttack = false;
            }
        }
        
        if ((isFlashed && PC.getFlashing() && atPos0))
        {
            vel.z = (-Speed * 2) * Time.deltaTime;
        
            RB.velocity = vel;
        }
        else
        {
            isFlashed = false;
        }

        if (transform.position.z > -1.6f && atPos0)
        {
            Debug.Log("reset");
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
}
