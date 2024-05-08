using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Game_Manager : MonoBehaviour
{
    public AudioSource AS;

    public TextMeshPro TimeText;
    public int time;
    public float timer;
    public float minutes;

    public bool toggle;
    
    void Start()
    {
        
    }
    
    void Update()
    {
        timer += Time.deltaTime;

        minutes = Mathf.FloorToInt(timer / 60);

        if (minutes > 0)
        {
            TimeText.text = (minutes + ":00");

            if (minutes == 6)
            {
                Debug.Log("Win");
            }
        }
        
    }
}
