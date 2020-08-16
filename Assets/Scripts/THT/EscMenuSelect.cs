using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscMenuSelect : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject cam;
    bool isActive;
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        this.GetComponent<Canvas>().worldCamera = cam.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {    
                isActive = !isActive;      
        }
        if(isActive)
        {
            Time.timeScale = 0f;
            this.GetComponent<Canvas>().sortingLayerName = "Player";
  
        }
        else if(!isActive)
        {
            Time.timeScale = 1f;
            this.GetComponent<Canvas>().sortingLayerName = "Default";
        }


        if (Input.GetAxisRaw("Vertical") < 0 && isActive)
        {

            transform.GetChild(2).gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(16f, 256f);
        }

        else if (Input.GetAxisRaw("Vertical") > 0 && isActive)
        {
            transform.GetChild(2).gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(16f, 398f);
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (transform.GetChild(2).gameObject.GetComponent<RectTransform>().anchoredPosition.y == 398f)
            {
                this.GetComponent<Canvas>().sortingLayerName = "Default";
                Time.timeScale = 1f;
                isActive = false;
            }
            else if (transform.GetChild(2).gameObject.GetComponent<RectTransform>().anchoredPosition.y == 256f)
            {
                SceneManager.LoadScene(0);
            }
        }

    }
}
