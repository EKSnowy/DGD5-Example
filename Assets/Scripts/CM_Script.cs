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
    
    void Start()
    {
        
    }

    
    void Update()
    {
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

            if (attackTimer <= 0)
            {
                Debug.Log("CM Death");
                //SceneManager.LoadScene("GameOverCM");
            }
        }
    }

    public void goBack(float num)
    {
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
}
