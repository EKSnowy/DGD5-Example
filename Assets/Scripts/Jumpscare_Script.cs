using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Jumpscare_Script : MonoBehaviour
{
    public GameObject GameOver;
    public GameObject Jumpscare;
    
    public float Intensity = .25f;
    
    //A countdown, while this is > 0 the screen shakes
    public float ShakeTimer = 2f;
    
    //Where it was before it shook
    public Vector3 StartPos;

    private void Start()
    {
        //Record our start position
        StartPos = transform.position;
    }

    void Update()
    {
            if (ShakeTimer > 0)
            {
               
                ShakeTimer -= Time.deltaTime;
                
                //Calculate how much shake we want this frame
                Vector3 shake = new Vector3(Random.Range(-Intensity, Intensity),
                    Random.Range(-Intensity, Intensity));
                
                //Set its position to be its start position plus the shake offset
                transform.position = StartPos + shake;
            }
            else 
            {
                transform.position = StartPos;
                
                transform.position = new Vector3(5, 10, -5);
                GameOver.SetActive(true);
                Jumpscare.SetActive(false);

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    SceneManager.LoadScene("GameScene");
                }
                
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    SceneManager.LoadScene("StartScene");
                }
                
            }
    }
}
