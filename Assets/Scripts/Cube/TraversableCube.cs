using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraversableCube : Cube
{
    new BoxCollider2D collider;
    
    private void Start() 
    {
        collider = GetComponents<BoxCollider2D>()[0];
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player"){
            if(other.name == "testCosette(Clone)"){
                if(other.isTrigger == true)
                    return;
            }
            collider.isTrigger = true;
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        if(other.tag == "Player"){
            if(other.name == "testCosette(Clone)"){
                if(other.isTrigger == true)
                    return;
            }            
            collider.isTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "Player"){
            if(other.name == "testCosette(Clone)"){
                if(other.isTrigger == true)
                    return;
            }
            collider.isTrigger = false;
        }
    }

    // private void OnCollisionEnter2D(Collision2D other) {
    //     if(other.transform.tag == "Player"){
    //         collider.isTrigger = false;
    //     }
    // }
}
