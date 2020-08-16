using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtrusionCubeTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player")
        {
            for(int i = 0; i < transform.childCount; i++){
                MoveCubeTrigger m = transform.GetChild(i).GetComponent<MoveCubeTrigger>();
                if(m != null)
                    m.isStart = true;
            }
        }
    }
}
