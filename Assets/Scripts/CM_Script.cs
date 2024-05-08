using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CM_Script : MonoBehaviour
{
    public Rigidbody RB;
    
    public float Speed = 15f;
    
    public float attackTimer = 3f;

    public bool canAttack = false;

    public Game_Manager GM;

    public Audio_Script AS;

    public int random;
    public bool toggle = true;
    
    void Start()
    {
        
    }

    
    void Update()
    {
        Quicken();
        
        /// Continuously moves forwards until certain point \\\
        Vector3 vel = RB.velocity;
        vel.z = -Speed * Time.deltaTime;
        
        RB.velocity = vel;
        
        /// When at point, timer goes down till attack \\\
        if (transform.localPosition.z < 25.8f)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y,
                25.8f);
            
            attackTimer -= Time.deltaTime;
            canAttack = true;
            
            Randomize();

            if (attackTimer <= 0)
            {
                Debug.Log("CM Death");
                //SceneManager.LoadScene("GameOverCM");
            }
        }
    }

    public void goBack(float num)
    {
        toggle = true;
        
        if (transform.localPosition.z < 30.2)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y,
                transform.localPosition.z + num);
        }

        attackTimer = 3f;
        canAttack = false;
    }

    public bool isAttacking()
    {
        return canAttack;
    }
    
    public void Quicken()
    {
        if (GM.Accelerate() == 0)
        {
            Speed = 15f;
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
            random = Random.Range(0,2);
            AS.SoundAudio(random);
           
            toggle = false;
        }
        
    }
}
