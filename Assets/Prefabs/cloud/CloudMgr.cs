using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMgr : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject wenhao;
    static public CloudMgr instance;
    public GameObject shengluehao;
    public GameObject gantanhao;
    static GameObject wh, gth, slh;
    bool isActiveW;
    bool isActiveS;
    bool isActiveG;
    private void Awake()
    {
        wh = Instantiate(wenhao);
        gth = Instantiate(shengluehao);
        slh = Instantiate(gantanhao);
        wh.SetActive(false);
        gth.SetActive(false);
        slh.SetActive(false);
        instance = this;
    }

    private void Update()
    {
        if(isActiveG)
        {
            
            Invoke("DestoyCloudG",0.8f);
            isActiveG = false;
        }
        if(isActiveS)
        {
            Invoke("DestoyCloudS", 0.8f);
            isActiveS = false;
        }
        if(isActiveW)
        {
            Invoke("DestoyCloudW", 0.8f);
            isActiveW = false;
        }
        
    }
    public void SetCloudTrue(string name,Vector3 pos)
    {
        if(name == "wenhao")
        {
            wh.SetActive(true);
            wh.transform.position = new Vector2(pos.x,pos.y + 1.8f);
            isActiveW = true;
        }
        if(name == "gantanhao")
        {
            gth.SetActive(true);
            gth.transform.position = new Vector2(pos.x, pos.y + 1.8f);
            isActiveG = true;
        }
        if(name == "shengluehao")
        {
            slh.SetActive(true);
            slh.transform.position = new Vector2(pos.x, pos.y + 1.8f);
            isActiveS = true;
        }
    }


    void DestoyCloudW()
    {
        wh.SetActive(false);
    }
    void DestoyCloudS()
    {
        slh.SetActive(false);
    }
    void DestoyCloudG()
    {
        gth.SetActive(false);
    }

}
