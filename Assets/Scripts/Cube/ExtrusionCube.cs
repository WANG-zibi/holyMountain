using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtrusionCube : Cube
{
    bool isEx;
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.transform.tag == "Player"){
            testCharacter c = other.transform.GetComponent<testCharacter>();
            if(!c.isTrap){
                isEx = true;
                c.isTrap = true;
            }else{
                if(!isEx)
                    other.transform.GetComponent<testCharacter>().Dead();
            }
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        if(other.transform.tag == "Player"){
            testCharacter c = other.transform.GetComponent<testCharacter>();
            c.isTrap = false;
        }
    }
}
