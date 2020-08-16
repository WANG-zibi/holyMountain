using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCube : Cube
{
    public Transform center;
    
    public float speed;

    float z;

    private void Update() {
        Move();
    }

    void Move(){
        z += speed * Time.deltaTime;

        float x = transform.rotation.eulerAngles.x;
        float y = transform.rotation.eulerAngles.y;
        transform.rotation = Quaternion.Euler(x,y,z);

        if(z >= 360){
            z = 0;
        }
    }
}
