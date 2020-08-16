using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.SceneManagement;

public class PassPoint : MonoBehaviour
{
    bool isRange;
    public LightController LC;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && !collision.GetComponent<testShadow>())
        {
            if(collision.name == "testCosette(Clone)"){
                if(collision.isTrigger == true)
                    return;
            }
            //控制对话框播放，最好别改
            PlayerPrefs.SetInt("Dead",0);
            LC.isRange = true;
            Invoke("cut",1f);
        }
    }
    void cut()
    {
        if(SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCountInBuildSettings-1)
        {
           
            DontDestroyOnLoad(AudioPoolMgr.thisInstance);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            Application.Quit();
        }
    }

}
