using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Start_Script : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject controlsScreen;

    public bool start = false;
    
    void Start()
    {
        controlsScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            SceneManager.LoadScene("GameScene");
        }
    }

    public void onControlClick()
    {
        controlsScreen.SetActive(true);
    }
    
    public void onStartClick()
    {
        start = true;
    }
    
    public void onExitClick()
    {
        controlsScreen.SetActive(false);
    }
    
}
