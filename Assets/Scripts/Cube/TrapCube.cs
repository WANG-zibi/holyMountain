using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapCube : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.transform.tag == "Player")
        {
            other.transform.GetComponent<testCharacter>().Dead();
        }
    }
    private void OnCollisionStay2D(Collision2D other) 
    {
        if(other.transform.tag == "Player")
        {
            other.transform.GetComponent<testCharacter>().Dead();
        }
    }
}
