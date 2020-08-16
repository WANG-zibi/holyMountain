using UnityEngine;
using UnityEngine.UI;

public class DialogController_JiaoCheng : MonoBehaviour
{

    Canvas dialogs;
    bool isAvtive;

    private void Start() {
        dialogs = transform.GetChild(0).GetComponent<Canvas>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player"){
            if(other.name == "testCosette(Clone)"){
                if(other.isTrigger == true)
                    return;
            }
            dialogs.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "Player"){
            if(other.name == "testCosette(Clone)"){
                if(other.isTrigger == true)
                    return;
            }
            dialogs.gameObject.SetActive(false);
        }
    }
}
