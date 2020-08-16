using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCubeCircle : Cube
{
    public Transform center;
    public float r;
    public float speed;

    float angle;

    private void Update() {
        Move();
    }

    void Move(){
        angle += speed * Time.deltaTime;

        float x = center.position.x + Mathf.Cos(angle) * r;
        float y = center.position.y + Mathf.Sin(angle) * r;

        transform.position = new Vector3(x,y,transform.position.z);

        if(angle >= 2*Mathf.PI){
            angle = 0;
        }
    }
}
