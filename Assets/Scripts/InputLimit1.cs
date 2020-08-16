using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputLimit1 : MonoBehaviour
{
    KeyCode JUMP;
    KeyCode RUSH;
    KeyCode CHANGE;
    bool isS;
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player" && other.name != "testShadow(Clone)"){
            if(isS){
                isS = false;
                JUMP = other.GetComponent<testCharacter>().JUMP;
                Debug.Log(JUMP);
                RUSH = other.GetComponent<testCharacter>().RUSH;
                CHANGE = other.GetComponent<testCharacter>().CHANGE;

                other.GetComponent<testCharacter>().JUMP = KeyCode.None;
                other.GetComponent<testCharacter>().RUSH = KeyCode.None;
                other.GetComponent<testCharacter>().CHANGE = KeyCode.None;
                other.GetComponent<testCharacter>().CANINPUT = false;
            }
            

        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "Player" && other.name != "testShadow(Clone)"){
            other.GetComponent<testCharacter>().JUMP = JUMP;
            Debug.Log(JUMP);
            other.GetComponent<testCharacter>().RUSH = RUSH;
            other.GetComponent<testCharacter>().CHANGE = CHANGE;
            other.GetComponent<testCharacter>().CANINPUT = true;
            //isS = true;
        }
    }
}
