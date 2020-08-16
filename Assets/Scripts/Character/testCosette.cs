using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testCosette : testCharacter
{
/***************************************************************************************************
    以下为    开放给外界的变量
****************************************************************************************************/
    public float wallJumpForce;
    
    [Header("左右触墙点")]
    public Transform left;
    public Transform right;

    [Header("技能数值")]
    public float skillRushTime;
    public float skillRadius;
    public float maxWallJumpTime;
    public float minWallJumpTime;

    [Header("贴墙减速下落的速度,建议1")]
    public float antiSpeed;

/***************************************************************************************************
    以下为    私有的变量
****************************************************************************************************/
    [Header("子弹")]
    GameObject bullet;

    [Header("检测子弹的bool值")]
    bool isBullet;

    [Header("检测墙壁")]
    bool isWall;
    [Header("蹬墙跳状态bool值")]
    public bool isWallJump;

    [Header("计时器")]
    float skillTimer;
    public float wallJumpPressTimer;
    bool isWallJumpPress;
    [Header("画圆相关，后面改变技能表现可以删除")]
    int positionCount;			//完成一个圆的总点数，
    float angle;				//转角，三个点形成的两段线之间的夹角
    Quaternion q;				//Quaternion四元数
    LineRenderer line;
    bool m_flag;
    private void Start() {
        init();
        positionCount = 180;
        angle = 360f / (positionCount - 1);
        line = GetComponent<LineRenderer>();
        line.positionCount = positionCount;
        m_flag = true;
    }

    void DrawCircle()
    {
        line.enabled = true;
        for (int i = 0; i < positionCount; i++)
        {
            if (i != 0)
            {
                q = Quaternion.Euler(q.eulerAngles.x, q.eulerAngles.y, q.eulerAngles.z + angle);
            }
            Vector3 forwardPosition = transform.position + q * Vector3.down * skillRadius;
            line.SetPosition(i, forwardPosition);
        }
    }

    /*
     * 初始化
     */
    public float y;
    public Vector2 v;
    public override void init(){
        base.init();
        canSkill = false;
        skillTimer = 0;
        isWallJump = false;
        wallJumpPressTimer = 0;
        isWallJumpPress = false;
        GetComponent<CircleCollider2D>().radius = skillRadius;
        y = wallJumpForce;
    }
    

    private void Wall()
    {
        Debug.Log("m_wallJump()");
        rb.velocity = new Vector2(16.0f, 10.0f);
        canJump = false;
    }

    void Set_Scale()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }
    void Update()
    {
        base.DoUpdate();
        /*
         * Cosette的技能释放需要时机，只有身边有子弹的时候才能放技能（即canSkill为true）
         */

        if (bullet != null){
            canSkill = true;
        }else{
            canSkill = false;
        }
        //Debug.Log(transform.localScale.x);
        /*
         * 触墙检测
         */
        isWall = Physics2D.OverlapCircle(left.position, 0.1f, LayerMask.GetMask("ground")) ||  
                    Physics2D.OverlapCircle(right.position, 0.1f, LayerMask.GetMask("ground"));
        if (transform.localScale.x == 1)
        {

            rightwall = false;
            leftwall = true;
        }
            
        if (transform.localScale.x == -1)
        {
            leftwall = false;
            rightwall = true;
        }
        tC = isWall;
        if (isWall && !isGround)
        {

            anim.SetBool("isInWall", true);
        }

        if (!isWall)
        {
            anim.SetBool("isInWall", false);
        }
        //if(isWall && !anim.GetBool("isInWall"))
        //{
        //    anim.SetTrigger("inWallJump");
        //}
        /*
         * 状态转换
         * 新增状态“蹬墙跳”-0
         * 蹬墙跳时 按RUSH键可以冲刺
         *         技能持续标志isWallJump为false则变为“走路”状态
         */
        if (status == 0){
            if(!isWallJump){
                status = 1;
                canJump = false;
            }
            if(Input.GetKeyDown(RUSH) && canRush){
                isWallJump = false;
                status = 3;
                rushTimer = 0;
            }
        }

        //if (Physics2D.OverlapCircle(left.position, 0.01f, LayerMask.GetMask("ground")))
        //{
        //    Debug.Log("In leftWall");
        //    //transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        //}
        //if (Physics2D.OverlapCircle(right.position, 0.01f, LayerMask.GetMask("ground")))
        //{
        //    Debug.Log("In rightWall");
        //    //transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        //}

        /*
         * “走路”和“跳跃”时
         * 如果满足条件 按JUMP键则进入蹬墙跳状态
         */
        if (isGround)
        {
            if (Input.GetKeyDown(JUMP))
            {

                canJump = true;
                Jump();
            }
        }

        if (isWall)
        {
            if (Input.GetKeyDown(JUMP))
            {
                    canJump = true;
                    Jump();
                m_flag = true;
               

                //transform.localScale = new Vector3(-transform.localScale.x,transform.localScale.y,transform.localScale.z);
                
            }
            if (rb.velocity.x == 0 && m_flag)
            {
                m_flag = false;
                Debug.Log("111111111");
                Invoke("Set_Scale",0.02f);
                
             
            }
        }
        
           
        //if(status == 1 || status == 2){
        //    if(Input.GetKeyDown(JUMP)){
        //        if(!isGround && isWall && !isWallJump)
        //        {
        //            if (anim.GetBool("isJump") && anim.GetBool("isInWall"))
        //            {
        //                anim.SetTrigger("inWallJump");
        //                //anim.SetBool("issInWallJump", true);
        //            }
        //            isWallJump = true;
        //            wallJumpPressTimer = 0;
        //            isWallJumpPress = true;
        //            status = 0;
        //            y = wallJumpForce;
        //            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        //        }
        //    }
        //}

        /*
         * 计时器
         * “蹬墙跳”状态下，计时器归0则通过isWallJump标记技能结束
         */
       



        

        /*
         * 接触墙壁时，如果没有正在蹬墙跳则减缓角色下落速度
         */
        if(isWall && !isGround && !isWallJump){
            rb.velocity = new Vector2(rb.velocity.x,-antiSpeed);
        }

        v= rb.velocity;
    }

    private void FixedUpdate() {
        base.DoFixedUpdate();
        
        /*
         * “蹬墙跳”状态下，给角色一个斜向的力
         */
        if (status == 0){
            //Jump();
        }
    }

    /*
     * 重写角色技能-子弹冲
     * 技能持续时间内，给角色一个 斜向的，大小等于冲刺的 速度
     * 持续时间结束后，恢复状态
     */
    protected override void Skill(){
        AudioPoolMgr.AudioPlay("skill", AudioClipName.踩子弹);
        if (skillTimer < skillRushTime){
            rb.velocity = new Vector2(-rushSpeed * transform.localScale.x, rushSpeed);
            skillTimer += Time.deltaTime;
            ShadowPool.instance.GetFormPool();
            bullet.GetComponent<ShotCubeBullet>().SetDir(Vector3.down);
            bullet.GetComponent<Collider2D>().enabled = false;
        }else{
            SkillEnd();
        }
    }

    /*
     * 技能结束
     * 恢复状态
     */
    public override void SkillEnd(){
        skillTimer = 0;
        isSkill = false;
        canSkill = true;
        rb.velocity = new Vector2(0, 0);
    }
    



    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Bullet"){
            bullet = other.gameObject;
            bullet.GetComponent<ShotCubeBullet>().speed /= 5;
            DrawCircle();
            SkillEnd();
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "Bullet"){
            bullet.GetComponent<ShotCubeBullet>().speed *= 5;
            line.enabled = false;
            bullet = null;
        }
    }
}
