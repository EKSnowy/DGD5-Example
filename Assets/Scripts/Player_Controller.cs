using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    public Camera Eyes;
    public Rigidbody RB;
    public float MouseSensitivity = 1;
    public float WalkSpeed = 10;
    public float JumpPower = 7;
    public List<GameObject> Floors;
    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }

    
    void Update()
    {
        float xRot = Input.GetAxis("Mouse X") * MouseSensitivity;
        float yRot = -Input.GetAxis("Mouse Y") * MouseSensitivity;
        transform.Rotate(0,xRot,0);
        Eyes.transform.Rotate(yRot,0,0);

        if (WalkSpeed > 0)
        {
            Vector3 move = Vector3.zero;
            if (Input.GetKey(KeyCode.W))
                move += transform.forward;
            if (Input.GetKey(KeyCode.S))
                move -= transform.forward;
            if (Input.GetKey(KeyCode.A))
                move -= transform.right;
            if (Input.GetKey(KeyCode.D))
                move += transform.right;
            move = move.normalized * WalkSpeed;
            if (JumpPower > 0 && Input.GetKeyDown(KeyCode.Space) && OnGround())
                move.y = JumpPower;
            else
                move.y = RB.velocity.y;
            RB.velocity = move;
        }
    }

    public bool OnGround()
    {
        return Floors.Count > 0;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!Floors.Contains(other.gameObject))
            Floors.Add(other.gameObject);
    }

    private void OnCollisionExit(Collision other)
    {
        Floors.Remove(other.gameObject);
    }
}