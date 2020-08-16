using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End : MonoBehaviour
{
    // Start is called before the first frame update
    bool isKeyDown = false;

    private void OnEnable()
    {
        
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isKeyDown)
            {
                isKeyDown = true;
                Time.timeScale = 0f;
            }
            else if (isKeyDown)
            {
                isKeyDown = false;
                Time.timeScale = 1f;
            }
            //Application.Quit();
        }

    }
}
