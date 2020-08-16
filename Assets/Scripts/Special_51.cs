using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Special_51 : MonoBehaviour
{
    public KeyCode CHNAGE;
    public GameObject cosette;
    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "Player"){
            
            other.GetComponent<testCharacter>().CHANGE = CHNAGE;
            Destroy(cosette);
        }
    }
}
