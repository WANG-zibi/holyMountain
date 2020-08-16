using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscMenu : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject[] player;
    bool flag;
    bool isActive;
    void Start()
    {
        flag = true;
    }

    void Set_flag()
    {
        flag = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Vertical") < 0 && flag)
        {

            flag = false;
            transform.GetChild(2).gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(16f, 256f);

            Invoke("Set_flag", 0.3f);
        }

        if (Input.GetAxisRaw("Vertical") > 0 && flag)
        {
            flag = false;
            transform.GetChild(2).gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(16f, 398f);

            Invoke("Set_flag", 0.3f);

        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isActive)
            {
                isActive = true;
                transform.GetChild(2).gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(16f, 398f);
                Time.timeScale = 0f;
            }
            if (isActive)
            {
                isActive = false;
            }
        }



        if (Input.GetKeyDown(KeyCode.Return))
        {

            if (transform.GetChild(2).gameObject.GetComponent<RectTransform>().anchoredPosition.y == 398f)
            {
                this.GetComponent<Canvas>().sortingLayerName = "Default";
                foreach (GameObject tmp in player)
                {
                    tmp.SetActive(true);
                }
            }
            if (transform.GetChild(2).gameObject.GetComponent<RectTransform>().anchoredPosition.y == 256f)
            {
                SceneManager.LoadScene(0);
            }
        }
    }
}
