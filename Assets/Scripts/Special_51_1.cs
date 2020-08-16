using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Special_51_1 : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "Player"){
            other.GetComponent<testCharacter>().CHANGE = KeyCode.None;
        }
    }
}
