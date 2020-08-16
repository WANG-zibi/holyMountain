using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCube : Cube
{
    public Transform NavPosition;
    public float moveSpeed;
    
    protected int index = 0;

    protected Transform[] positions;
    Transform CollisionPoint;
    protected Transform player;
    protected Vector3 lastPosition;
    void Start()
    {
        CollisionPoint = transform.GetChild(0);
        lastPosition = transform.position;
        positions = new Transform[NavPosition.childCount];
        for(int i = 0; i < positions.Length; i++){
            positions[i] = NavPosition.GetChild(i);
        }
    }

    void Update()
    {
        Move();
    }
    protected virtual void Move()
    {
        Vector3 moveDir = (positions[index].position - lastPosition).normalized;

        transform.Translate(moveDir * Time.deltaTime * moveSpeed);
        if(player != null){
            if(player.GetComponent<Rigidbody2D>().velocity.x == 0){
                player.Translate(moveDir * Time.deltaTime * moveSpeed);
            }
        }

        if(Vector3.Distance(transform.position,positions[index].position) < 0.1)
        {
            lastPosition = positions[index].position;
            index++;
        }
        if(index == positions.Length)
        {
            index = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.transform.tag == "Player" && Physics2D.OverlapCircle(other.transform.GetChild(0).transform.position, 0.1f, LayerMask.GetMask("ground"))){
            player = other.transform;
        }
    }

    private void OnCollisionStay2D(Collision2D other) {
        if(other.transform.tag == "Player" && !Physics2D.OverlapCircle(other.transform.GetChild(0).transform.position, 0.1f, LayerMask.GetMask("ground"))){
            player = null;
        }
        if(other.transform.tag == "Player" && Physics2D.OverlapCircle(other.transform.GetChild(0).transform.position, 0.1f, LayerMask.GetMask("ground"))){
            player = other.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        if(other.transform.tag == "Player"){
            player = null;
        }
    }
}
