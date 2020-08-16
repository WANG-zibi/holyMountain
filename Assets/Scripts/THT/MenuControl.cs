using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuControl : MonoBehaviour
{
    bool flag;
    bool flag1;
    float a;
    GameObject Press;
    GameObject []menu;
    void Start()
    {
        flag = true;
        flag1 = false;
        a = 1;
        Press = GameObject.Find("Press");
        menu = GameObject.FindGameObjectsWithTag("Menu");
    }

    void Set_flag()
    {
        flag = true;
    }

    void Set_flag1()
    {
        flag1 = false;
    }
    void Update()
    {
        //Debug.Log(Input.GetAxisRaw("Horizontal"));

        if (Input.anyKeyDown)
        {
            AudioPoolMgr.AudioPlay("skill", AudioClipName.点击音);
            flag1 = true;
            
        }

        if(flag1)
        {
            if (Press.GetComponent<SpriteRenderer>().color.a < 0.1)
            {
                Press.SetActive(false);
                a += (Time.deltaTime);
                foreach(GameObject tmp in menu)
                {
                    tmp.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, a);
                }
                

                Invoke("Set_flag1", 1.5f);
            }
            else
            {
                a -= (Time.deltaTime);
                Press.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, a);
            }   
            
        }

        if (Input.GetAxisRaw("Vertical") < 0 && flag && menu[0].GetComponent<Transform>().position.y != -2)
        {

            AudioPoolMgr.AudioPlay("skill", AudioClipName.点击音);
            flag = false;
            menu[0].GetComponent<Transform>().position = new Vector3(0, menu[0].GetComponent<Transform>().position.y - 1,0);
            
            Invoke("Set_flag",0.3f);

        }

        if (Input.GetAxisRaw("Vertical") > 0 && flag && menu[0].GetComponent<Transform>().position.y != 0)
        {
            AudioPoolMgr.AudioPlay("skill", AudioClipName.点击音);
            flag = false;
            menu[0].GetComponent<Transform>().position = new Vector3(0, menu[0].GetComponent<Transform>().position.y + 1, 0);
            
            Invoke("Set_flag", 0.3f);

        }

        if(Input.GetKeyDown(KeyCode.Return))
        {
            AudioPoolMgr.AudioPlay("skill", AudioClipName.关卡切换音);
            if (menu[0].GetComponent<Transform>().position.y == 0)
            {
                SceneManager.LoadScene(2);
            }

            if (menu[0].GetComponent<Transform>().position.y == -1)
            {
                SceneManager.LoadScene(1);
            }

            if (menu[0].GetComponent<Transform>().position.y == -2)
            {

            }
        }



    }
}
