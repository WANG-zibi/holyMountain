using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCubeTrigger : MoveCube
{
    public bool isStart;

    bool isBack;

    protected override void Move(){

        if(isStart){
            Vector3 moveDir = (positions[index].position - lastPosition).normalized;

            transform.Translate(moveDir * Time.deltaTime * moveSpeed);
            if(player != null){
                if(player.GetComponent<Rigidbody2D>().velocity.x == 0){
                    player.Translate(moveDir * Time.deltaTime * moveSpeed);
            }
        }

            if(Vector3.Distance(transform.position,positions[index].position) < 0.1)
            {
                if(isBack){
                    Refresh();
                    return;
                }
                
                lastPosition = positions[index].position;
                index++;
            }
            if(index == positions.Length)
            {
                index = 0;
                isBack = true;
            }
        }
    }


    void Refresh(){
        isStart = false;
        isBack = false;
        for(int i = 0; i < positions.Length; i++){
            positions[i] = NavPosition.GetChild(i);
        }
        lastPosition = transform.position;
    }
}
