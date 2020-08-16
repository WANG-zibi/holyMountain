using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragileTrap : Cube
{
    public float fragileTime;
    public float speed;

    float timer;
    bool isPlayerOn;
    bool isDown;
    Vector3 pos;

    void Start()
    {
        pos = transform.position;
    }

    void Update()
    {
        if(isPlayerOn)
        {
            if(timer < fragileTime)
            {
                timer += Time.deltaTime;
            }
            else
            {
                isPlayerOn = false;
                timer = 0;
                isDown = true;
            }
        }

        if(isDown)
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime);
        }

        if(transform.position.y < -30){
            isDown = false;
            transform.position = pos;
            gameObject.SetActive(false);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.transform.tag == "Player" && other.transform.position.y > transform.position.y)
        {
            isPlayerOn = true;
        }
    }
}
