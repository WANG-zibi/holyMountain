using UnityEngine;
using UnityEngine.UI;

public class DialogController : MonoBehaviour
{

    public KeyCode INTERACTIVE;
    Canvas[] dialogs;
    bool isAvtive;
    int dialogIndex;

    KeyCode JUMP;
    KeyCode SKILL;
    KeyCode CHANGE;
    KeyCode RUSH;

    GameObject player;

    private void Start() {
        dialogs = new Canvas[transform.childCount];
        for(int i = 0; i < dialogs.Length; i++){
            dialogs[i] = transform.GetChild(i).GetComponent<Canvas>();
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        if(PlayerPrefs.GetInt("Dead") == 0){
            
            if(other.tag == "Player" && !isAvtive){
                if(other.name == "testCosette(Clone)"){
                    if(other.isTrigger == true)
                        return;
                }
                isAvtive = true;

                JUMP = other.GetComponent<testCharacter>().JUMP;
                RUSH = other.GetComponent<testCharacter>().RUSH;
                SKILL = other.GetComponent<testCharacter>().SKILL;
                CHANGE = other.GetComponent<testCharacter>().CHANGE;

                other.GetComponent<testCharacter>().JUMP = KeyCode.None;
                other.GetComponent<testCharacter>().RUSH = KeyCode.None;
                other.GetComponent<testCharacter>().SKILL = KeyCode.None;
                other.GetComponent<testCharacter>().CHANGE = KeyCode.None;

                other.GetComponent<testCharacter>().CANINPUT = false;

                player = other.gameObject;

                dialogs[dialogIndex].gameObject.SetActive(true);
            }
        }else{
            isAvtive = false;
        }
        
    }
    private void Update() {
        if(isAvtive){
            if(Input.GetKeyDown(INTERACTIVE)){
                dialogs[dialogIndex].gameObject.SetActive(false);
                dialogIndex++;

                if(dialogIndex < dialogs.Length){
                    dialogs[dialogIndex].gameObject.SetActive(true);
                }else{
                    //isAvtive = false;
                    if(player != null){
                        player.GetComponent<testCharacter>().JUMP = JUMP;
                        player.GetComponent<testCharacter>().RUSH = RUSH;
                        player.GetComponent<testCharacter>().SKILL = SKILL;
                        player.GetComponent<testCharacter>().CHANGE = CHANGE;
                        player.GetComponent<testCharacter>().CANINPUT = true;
                    }
                    Destroy(gameObject);
                }
            }
        }
    }

}
