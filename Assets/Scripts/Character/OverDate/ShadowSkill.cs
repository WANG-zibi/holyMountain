using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowSkill : Character
{
    public GameObject hall;
    public float dis;
    
    Material material;
    
    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        
        rb.gravityScale = 3f;
    }
    void Start()
    {
        

        material = GetComponent<Renderer>().material;
        if(levelCnt == 6){
            material.SetFloat("_DragInterval",10);
        }else if(levelCnt == 7){
            material.SetFloat("_DragInterval",1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (this.rb.gravityScale == 0f) Debug.Log("kale");
        material.color = new Color(material.color.r,material.color.g,material.color.b,(dis-(Vector3.Distance(transform.position,hall.transform.position)))/dis+0.2f);
        JumpControl();
        RushTimeContrl();
    }
    private void FixedUpdate()
    {
        GroundMovement();
        Jump();
        Rush();
    }

    public override void Dead(){
        hall.GetComponent<HallSkill>().RetractShadow();
    }
}
