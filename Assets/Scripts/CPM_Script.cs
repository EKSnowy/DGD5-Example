using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPM_Script : MonoBehaviour
{
    public Rigidbody RB;
    
    public float Speed = 10f;

    public Player_Controller PC;
    
    void Start()
    {
        
    }
    
    void Update()
    {
        /// Continuously moves forwards until certain point \\\
        Vector3 vel = RB.velocity;
        vel.z = Speed * Time.deltaTime;
        
        RB.velocity = vel;

        if (PC.getFlashing())
        {
            vel.z = (-Speed * 2) * Time.deltaTime;
        
            RB.velocity = vel;
        }
        
    }
}
