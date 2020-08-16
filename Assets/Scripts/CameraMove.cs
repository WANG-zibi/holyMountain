using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform dis;
    public Transform ori;

    GameObject player;
    KeyCode JUMP;
    KeyCode SKILL;
    KeyCode CHANGE;
    KeyCode RUSH;
    bool isBack = false;
    Vector3 dir;
    private void Start() {
        dir = (dis.position - transform.position).normalized;
        isBack = false; 
    }
    
    void Update()
    {

        if(PlayerPrefs.GetInt("Dead") == 0){
            
            if(player == null){
                player = GameObject.FindGameObjectWithTag("Player");
                JUMP = player.GetComponent<testCharacter>().JUMP;
                RUSH = player.GetComponent<testCharacter>().RUSH;
                SKILL = player.GetComponent<testCharacter>().SKILL;
                CHANGE = player.GetComponent<testCharacter>().CHANGE;

                player.GetComponent<testCharacter>().JUMP = KeyCode.None;
                player.GetComponent<testCharacter>().RUSH = KeyCode.None;
                player.GetComponent<testCharacter>().SKILL = KeyCode.None;
                player.GetComponent<testCharacter>().CHANGE = KeyCode.None;
                player.GetComponent<testCharacter>().CANINPUT = false;
            }

            if(!isBack){
                
                GetComponent<CharacterMgr>().isFollow = false;
                transform.Translate(dir*0.13f);
                if(Vector3.Distance(transform.position, dis.position) < 5f){
                    isBack = true;
                }
            }else{
                transform.Translate(-dir * 0.13f);
                if(Vector3.Distance(transform.position, ori.position) < 5f){
                    GetComponent<CharacterMgr>().isFollow = true;

                    player.GetComponent<testCharacter>().JUMP = JUMP;
                    player.GetComponent<testCharacter>().RUSH = RUSH;
                    player.GetComponent<testCharacter>().SKILL = SKILL;
                    player.GetComponent<testCharacter>().CHANGE = CHANGE;
                    player.GetComponent<testCharacter>().CANINPUT = true;

                    transform.GetComponent<CameraMove>().enabled = false;
                }
            }
        }
        
    }
}
