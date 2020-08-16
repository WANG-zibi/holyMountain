using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class testCharacter : MonoBehaviour
{
    /***************************************************************************************************
        以下为    开放给外界的变量
    ****************************************************************************************************/
    [Header("死亡特效")]
    public GameObject deadEffectPre;
    [Header("状态特效")]
    public GameObject jumpSmoke;
    public GameObject RunSmoke;
    public GameObject rushSmoke;
    public GameObject DropSmoke;
    [HideInInspector]
    public GameObject runS;
    public int levelCnt;
    
    [Header("地面检测点")] 
    public Transform groundCheck; //地面检测点
    
    [Header("按键")] 
    public KeyCode JUMP;
    public KeyCode RUSH;
    public KeyCode SKILL;
    public KeyCode CHANGE;
    [Header("屏蔽按键")]
    public bool CANINPUT;
    [Header("跑跳冲速度")] 
    public float runSpeed;
    public float jumpForce;
    public float rushSpeed;

    [Header("最小跳跃时间")] 
    public float minJumpTime;

    [Header("跳冲时间")] 
    public float rushTime;
    public float jumpTime;
    [Header("加速减速时间，建议50，20")] 
    public float runTime;
    public float stopTime;

/***************************************************************************************************
    以下为    私有和继承的变量
****************************************************************************************************/
    [Header("上一次移动的方向")]
    float dirLast;

    [Header("角色状态：跑-1，跳-2，冲-3,技能-4,新增 蹬墙跳-0")] 
    public int status;

    [Header("实际跑跳力度")] 
    float runSpeedReal;
    float jumpForceReal;
    [Header("跳跃累加因子")]
    float x = 0;
    [Header("状态转换条件bool值")] 
    protected bool isGround; //判断是否在地上
    public bool canJump;//判断能否跳跃
    public bool canRush;//判断能否冲刺
    [HideInInspector]
    public bool canSkill;//判断能否放技能
    protected bool isSkill;//判断是否正在放技能
    bool jumpKeyUp = false;//判断跳跃键是否松开
    [Header("计时器")] 
    protected float jumpTimer;
    protected float rushTimer;

    protected float groundTimer;

    [Header("组件")] 
    protected Rigidbody2D rb;
    protected Collider2D[] colls;
    protected Animator anim;

    [Header("特效")] 
    GameObject walkEffect;

    bool A_isJumping;
/***************************************************************************************************
    以下为    开放但是在界面隐藏的变量
****************************************************************************************************/

    [HideInInspector] 
    public CharacterMgr CMR;

    ///[HideInInspector] 
    public bool isTrap;
    [HideInInspector] 
    public bool isCanJump;
    

    public static bool tC;
    protected static bool leftwall;
    protected static bool rightwall;
    bool flag, flag2, flag3, flag4;
    GameObject[] player;
    private void Start()
    {
        init();
        flag = true;
        flag2 = true;
        flag3 = true;
        flag4 = true;
        
    }
    /*
     * 初始化
     */
    public virtual void init()
    {
        status = 1;
        rb = GetComponent<Rigidbody2D>();
        colls = GetComponents<Collider2D>();
        anim = GetComponent<Animator>();
        jumpTimer = jumpTime;
        rushTimer = rushTime;
        groundTimer = 0.1f;
        isGround = false;
        canJump = false;
        canRush = false;
        canSkill = true;
        isSkill = false;
        jumpForceReal = jumpForce;        
        CANINPUT = true;
        runS = Instantiate(RunSmoke);
        runS.SetActive(false);
        //groundCheck.position + new Vector3(0f, 1f, 0f);    
    }
    private void Update()
    {
        DoUpdate();
    }
    private void FixedUpdate()
    {
        DoFixedUpdate();
    }
    private void OnDisable()
    {
        if(runS !=null)
        runS.SetActive(false);    
    }
    protected void DoUpdate()
    {
        /*
         * 状态转换
         */
        if(rb.velocity.y <0f)
        {
            anim.SetBool("isDrop", true);
        }
        else
        anim.SetBool("isDrop", false);
        if (Input.GetKeyDown(CHANGE))//在任意状态按下变身键时，执行切换角色
        {
            AudioPoolMgr.AudioPlay("skill", AudioClipName.暗影步);
            CMR.ChangePlayer();
        }
        StateAnyToSkill();//在任意状态按下技能键时，切换到“技能”状态
        
        switch (status)
        {
            case 1://“走路”状态，可以转换为“跳跃”和“冲刺”
                StateWalkToJump();
                StateAnyToRush();
                break;
            case 2://“跳跃”状态，可以转换为“走路”和“冲刺”
                StateJumpToWalk();
                StateAnyToRush();
                break;
            case 3://“冲刺”状态，可以转换为“走路”
                StateRushToWalk();
                break;
            case 4://“技能”状态，可以转换为“走路”
                StateSkillToWalk();
                break;
        }
        
        /*
         * 跳跃速度会随按键时间增长，当按键抬起时，停止这个增长
         */
        if (Input.GetKeyUp(JUMP))
        {
            anim.SetTrigger("jumpingDown");
            if(jumpTimer > minJumpTime){
                
                jumpTimer = jumpTime;
            }
            else{
                jumpKeyUp = true;
            }
        }

        if(jumpTimer > minJumpTime && jumpKeyUp){
            
            jumpTimer = jumpTime;
        }
        /*
         * 地面检测，落地时恢复 技能、跳跃、冲刺
         */
        isGround = Physics2D.OverlapCircle(groundCheck.position, 0.1f, LayerMask.GetMask("ground"));
        

        if (isGround)
        {
            groundTimer = 0.1f;
            canJump = true;
            canRush = true;
            canSkill = true;
        }
        else if (tC)
        {
            groundTimer = 0.1f;
            canJump = true;
        }
        else
        {
            
                
            if (groundTimer > 0)
            {
                groundTimer -= Time.deltaTime;
            }
            else
            {

                canJump = false;
                groundTimer = 0.1f;
            }
        }
        if(isGround && status == 1)
        {
            if(!A_isJumping)
            {
                AudioPoolMgr.AudioPlay("jump", AudioClipName.起跳落地);
                A_isJumping = true;
                var dropS = Instantiate(DropSmoke, groundCheck.position + new Vector3(0f, 0.9f, 0f), Quaternion.identity); // 新特效
                dropS.transform.localScale = new Vector3(-this.transform.localScale.x, this.transform.localScale.y, this.transform.localScale.z);
            }
            anim.SetBool("isJump", false);
        }
        
        /*
         * 计时器
         */
        JumpTimerCal();
        RushTimerCal();
    }
    protected void DoFixedUpdate()
    {
        /*
         * 状态功能
         */
        switch (status)
        {
            case 1://走路状态只能走（横向移动）
                Move();
                break;
            case 2://跳跃状态能走（横向移动）能跳（纵向移动）
                Move();
                Jump();
                break;
            case 3://冲刺状态只能冲
                Rush();
                break;
            case 4://技能状态只能进行技能
                Skill();
                break;
        }
    }

    
    /*
     * 死亡
     */
    public virtual void Dead()
    {
        //控制对话框播放，最好别改
        PlayerPrefs.SetInt("Dead",1);
        AudioPoolMgr.AudioPlay("player", AudioClipName.死亡);
        runS.SetActive(false);        
        Instantiate(deadEffectPre,transform.position,deadEffectPre.transform.rotation);
        Invoke("Revive", 1.25f);
        gameObject.SetActive(false);
    }

    /*
     * 复活
     */
    void Revive()
    {
        DontDestroyOnLoad(AudioPoolMgr.thisInstance);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //this.init();
        //CMR.Revive();
    }




/***************************************************************************************************
    以下为    计时器    
****************************************************************************************************/




    /*
     * 跳跃计时器
     * 当跳跃键保持按下时，跳跃速度线性增加
     * 当跳跃速度停止增长后，跳跃速度曲线减小，公式为y=-4/9*x*x+1
     */
    void JumpTimerCal()
    {
        if (status == 2)
        {
            if(jumpTimer < minJumpTime){
                jumpTimer += Time.deltaTime;
                
                x = (1.5f * minJumpTime);
                if(x>1.5f) x = 1.5f;
                jumpForceReal = ((-4.0f/9) * x * x + 1) * jumpForce;
            }else if(jumpTimer < jumpTime){
                jumpTimer += Time.deltaTime;

                x += (1.5f * jumpTime) * Time.deltaTime;
                if(x>1.5f) x = 1.5f;
                jumpForceReal = ((-4.0f/9) * x * x + 1) * jumpForce;
            }

        }
    }
    
    /*
     * 冲刺计时器
     */
    void RushTimerCal(){
        if (rushTimer < rushTime)
        {
            rushTimer += Time.deltaTime;
        }
    }
    



/***************************************************************************************************
    以下为    功能函数    
****************************************************************************************************/

    /*
     * 走路（横向移动）
     * 横向速度曲线改变
     */
    void Move()
    {
        float limit = 4f / 3f;
        float k = 9f / 16f;
        float runFactor = limit / runTime;
        float stopFactor = limit / stopTime;
        float hor = 0;
        
		if (CANINPUT)
            hor = Input.GetAxisRaw("Horizontal");
        if (hor > 0) hor = 1;        
        else if(hor < 0) hor = -1;
        if (hor == 0)
        {
            if (rb.velocity.x == 0f)
            {
                runS.SetActive(false);
            }

            float RS = runSpeed * k * (Mathf.Max(runSpeedReal - stopFactor, 0f) * Mathf.Max(runSpeedReal - stopFactor, 0f));
            runSpeedReal = Mathf.Max(runSpeedReal -= stopFactor, 0f);
            anim.SetBool("isRun", false);
            rb.velocity = new Vector2(-transform.localScale.x * RS, rb.velocity.y);
        }
        else
        {

            if (isGround)
            {
                AudioPoolMgr.AudioPlay("player", AudioClipName.走路草);
                runS.SetActive(true);
            }
            runS.transform.position =  groundCheck.position + new Vector3(0f, 1f, 0f);
            runS.transform.localScale = new Vector3(-this.transform.localScale.x, this.transform.localScale.y, this.transform.localScale.z);
            anim.SetBool("isRun", true);
            transform.localScale = new Vector3(-hor, 1, 1);
            float RS = runSpeed * k * (Mathf.Min(runSpeedReal + runFactor, limit) * Mathf.Min(runSpeedReal + runFactor, limit));
            runSpeedReal = Mathf.Min(runSpeedReal += runFactor, limit);
            //如果移动方向改变，速度重置
            if (hor!=dirLast){
                dirLast = hor;
                runSpeedReal = 0;
                RS = 0;
            }else{
                runSpeedReal = Mathf.Min(runSpeedReal, limit);
            }
            rb.velocity = new Vector2(hor * RS, rb.velocity.y);
        }
    }
    
    

    /*
     * 跳跃（纵向移动）
     * 纵向速度为jumpForceReal（此变量根据按键时间和跳跃时间改变）
     */
    protected virtual void Jump()
    {
        player = GameObject.FindGameObjectsWithTag("Player");


        if (player[0].name == "testCosette(Clone)")//GameObject.Find("testCosette(Clone)").activeSelf)
        {

            if (tC || flag4)
            {
                flag4 = true;
                Debug.Log("isWall");
                if (leftwall || flag2)
                {
                    flag3 = false;
                    flag = false;
                    flag2 = true;
                    rb.velocity = new Vector2(12.0f, jumpForceReal);
                }
                if (rightwall || flag3)
                {
                    flag = false;
                    flag2 = false; ;
                    flag3 = true;
                    rb.velocity = new Vector2(-12.0f, jumpForceReal);
                }

            }
            if (isGround || flag)
            {
                isGround = false;
                flag2 = false;
                flag3 = false;
                flag4 = false;
                flag = true;
                Debug.Log("In Jump");
                rb.velocity = new Vector2(rb.velocity.x, jumpForceReal);
                canJump = false;
            }

        }
        if (player[0].name == "testHall(Clone)" || player[0].name == "testHallPlus(Clone)")
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForceReal);
            canJump = false;
        }
        //Debug.Log("In Jump");

    }

   

    /*
     * 冲刺
     */
    void Rush()
    {
        rb.velocity = new Vector2(-rushSpeed * transform.localScale.x, 0f);
        canRush = false;
        ShadowPool.instance.GetFormPool();
    }
    
    /*
     * 执行技能，需要子类重写
     */
    protected virtual void Skill()
    {
        Debug.LogWarning("No Skill Implament");
    }
    public virtual void SkillEnd()
    {
        Debug.LogWarning("No SkillEnd Implament");
    }




/***************************************************************************************************
    以下为    状态转换
****************************************************************************************************/

    /*
     * 任意状态转变为技能状态
     */
    void StateAnyToSkill(){
        if (Input.GetKeyDown(SKILL) && canSkill)
        {
            canSkill = false;//每次落地之前只能放一次技能
            isSkill = true;//标记技能正在执行的变量
            status = 4;
        }
    }
    
    /*
     * 走路状态转变为跳跃状态（跳跃状态也只能由走路状态转换）
     */
    void StateWalkToJump(){
        if (Input.GetKeyDown(JUMP) && canJump)
        {
            if (this.gameObject.name == "testHall(Clone)")
            {
                //int idx = Random.Range((int)AudioClipName.跳跃男1, (int)AudioClipName.跳跃男3);
                AudioPoolMgr.AudioPlay("jump", AudioClipName.跳跃男1);            
            }
            if (this.gameObject.name == "testCosette(Clone)")
            {
				//AudioMgr.AudioPlay("jump", 46, 49);   
                int idx = Random.Range((int)AudioClipName.跳跃女1, (int)AudioClipName.跳跃女3);                AudioPoolMgr.AudioPlay("jump", (AudioClipName)idx);            
            }
            A_isJumping = false;
            anim.SetBool("isJump", true);            
            jumpTimer = 0;//跳跃时间计时器清零
            jumpForceReal = jumpForce;//实际跳跃力度为初始力度
            var jumpS = Instantiate(jumpSmoke, groundCheck.position + new Vector3(0f, 0.9f ,0f), Quaternion.identity); // 新特效
            jumpS.transform.localScale = new Vector3(-this.transform.localScale.x, this.transform.localScale.y, this.transform.localScale.z);
            //Instantiate(jumpEffectPre, groundCheck.position, jumpEffectPre.transform.rotation);
            status = 2;
            jumpKeyUp = false;
        }
    }
    
    /*
     * 任意状态转变为冲刺状态
     */
    void StateAnyToRush(){
        if (Input.GetKeyDown(RUSH) && canRush)
        {
            int idx = Random.Range((int)AudioClipName.冲刺1, (int)AudioClipName.冲刺3);
            AudioPoolMgr.AudioPlay("rush", (AudioClipName)idx);
            var RushS = Instantiate(rushSmoke, groundCheck.position + new Vector3(0f, 0.9f, 0f), Quaternion.identity); // 冲刺特效
            RushS.transform.localScale = new Vector3(-this.transform.localScale.x, this.transform.localScale.y, this.transform.localScale.z); rushTimer = 0;//冲刺计时器清零
            status = 3;
        }
       }
    
    
    /*
     * 跳跃状态转变为走路状态
     * 当跳跃时间结束时，使得角色的状态从“跳跃”变为“走路”
     */
    void StateJumpToWalk(){
        if (jumpTimer >= jumpTime){
            x = 0;//x作为跳跃速度计算因子，跳跃结束时重置
            status = 1;
        }
    }
    
    /*
     * 冲刺状态转变为走路状态
     * 当冲刺时间结束时，使得角色的状态从“冲刺”变为“走路”
     */
    void StateRushToWalk(){
        if (rushTimer >= rushTime){
            status = 1;
        }
    }
    
    /*
     * 技能状态转变为走路状态
     * 当技能结束时（技能标志isSkill为false），
        使得角色的状态从“技能”变为“走路”，重置跳跃
     */
    void StateSkillToWalk(){
        if (!isSkill){
            canJump = true;
            status = 1;
        }
    }

}
