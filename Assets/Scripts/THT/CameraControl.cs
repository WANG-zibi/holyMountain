using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    GameObject[] player;

    void Update()
    {
        player = GameObject.FindGameObjectsWithTag("Player");
        Debug.Log(player.Length);
        if (player.Length == 2)
        {
            foreach (GameObject tmp in player)
                if (tmp.name == "testShadow(Clone)")
                        this.GetComponent<CinemachineVirtualCamera>().Follow = tmp.transform;

        }
         else
            this.GetComponent<CinemachineVirtualCamera>().Follow = player[0].transform;
        
    }
}
