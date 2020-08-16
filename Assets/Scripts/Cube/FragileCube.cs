using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragileCube : Cube
{
    public float fragileTime;
    public float rebuildTime;
    float timer;
    [HideInInspector]
    public bool isPlayerOn;

    CubeManager cubeManager;

    void Start() {
        cubeManager = GameObject.Find("GameManager").GetComponent<CubeManager>();
    }
    void Update()
    {
        if(isPlayerOn)
        {
            if(timer < fragileTime)
            {
                timer += Time.deltaTime;
            }
            else
            {
                timer = 0;
                Crushed();
            }
        }
        

    }

    void Crushed()
    {
        cubeManager.AddRebuildCube(gameObject,rebuildTime);
        gameObject.SetActive(false);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.transform.tag == "Player" && other.transform.position.y > transform.position.y)
        {
            isPlayerOn = true;
        }
    }

}
