using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapTrigger : Cube
{
    public float gravity;
    Transform trap0;
    Transform trap1;
    Transform des;
    Vector3 pos;
    bool isDes;
    float t;

    private void Start() {
        trap0 = transform.GetChild(0);
        trap1 = transform.GetChild(1);
        des = transform.GetChild(2);
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player"){
			AudioPoolMgr.AudioPlay("Trap", AudioClipName.砸);
			if(other.name == "testCosette(Clone)"){
                if(other.isTrigger == true)
                    return;
            }            
            trap0.GetComponent<Rigidbody2D>().gravityScale = gravity;
            trap1.GetComponent<Rigidbody2D>().gravityScale = gravity;
        }
    }

    private void Update() {
        if(Vector3.Distance(des.position,trap1.position) <= 0.5f || Vector3.Distance(des.position,trap0.position) <= 0.5f || trap0.position.y < -30){
            isDes = true;
        }

        if(isDes){
            if(t < 0.5f){
                t += Time.deltaTime;
            }else{
                isDes = false;
                Destroy(gameObject);
            }
        }
    }
}
