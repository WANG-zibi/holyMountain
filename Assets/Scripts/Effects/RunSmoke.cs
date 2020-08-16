using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunSmoke : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject thisObject;
    static GameObject instance;
    void Start()
    {
        instance = Instantiate(thisObject);
    }
    public GameObject GetObject()
    {
        return instance;
    }
    public void SetFalseThis()
    {
        instance.SetActive(false);
    }

    public void SetTrue()
    {
        //instance.SetActive(true);
    }

}
