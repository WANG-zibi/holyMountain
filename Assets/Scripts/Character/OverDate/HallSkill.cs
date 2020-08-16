using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;
/// <summary>
/// 
/// </summary>
public class HallSkill : Character
{
    // Start is called before the first frame update
    [Header("传递对象")]
    public GameObject shadow;
    public BlackShader BS;
    Character nowCharacter;
    GameObject thisShadow;
    [Header("可调参数")]
    public float ShadowRdius;
    public float HallNetWorkRdius;
    public float FutureVisionTime;
    [Header("增强数值参数")]
    public float m_runSpeed;
    public float m_jumpForce;
    public float m_rushSpeed;
    bool isPressF;
    bool isPressFTwice;
    bool isShading;
    bool H_isPressF;
    bool H_isInHallNW;
    bool isActive;
    bool isInAirCanFS;
    float R_runSpeed;
    float R_jumpForce;
    float R_rushSpeed;
    Vector3 orignalPos;
    public bool GetIsActive { get; set; }
    public bool GetFutureVisison
    {
        get;set;
    }
    public bool isSkillChange { get; set; }
    private void Awake()
    {
        thisShadow = Instantiate(shadow);
        thisShadow.SetActive(false);
        thisShadow.GetComponent<ShadowSkill>().hall = gameObject;
        thisShadow.GetComponent<ShadowSkill>().dis = ShadowRdius;
        thisShadow.GetComponent<ShadowSkill>().levelCnt = levelCnt;
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        rb.velocity = Vector2.zero;
        rb.gravityScale = 3f;
    }
    private void OnEnable()
    {
        transform.localScale = new Vector3(-1f, transform.localScale.y, transform.localScale.z);
        rb.velocity = Vector2.zero;
    }
    void Start()
    {
        
    }
    // Update is called once per frame
    /// <summary>
    /// 
    /// </summary>
    void Update()
    {
        
        if (!isSkillChange)
            FutureVisionControl();
        else
            HallNetWalkControl();
        if (!isActive)
        {
            //FutureVisionPlus();
            JumpControl();
            RushTimeContrl();
        }
    }
    private void FixedUpdate()
    {
        if (!isSkillChange)
            FutureVision();
        else HallNetWork();
        if (!isActive)
        {
            GroundMovement();
            
            if(!isCanJump)
            Jump();
            Rush();
        }
    }

    /// <summary>
    /// 未来视输入控制
    /// </summary>
    void FutureVisionControl()
    {
        InAirFSjudge();
        if (Input.GetKeyUp(KeyCode.X) && !isPressF && !isPressFTwice && IsAirJudge())
        {
            
            GetIsActive = true;
            isInAirCanFS = true;
            isPressF = true;
            isPressFTwice = true;
        }
        else if(Input.GetKeyUp(KeyCode.X) && !isPressF && isPressFTwice)
        {
            Debug.Log("hahahahahahahhahah");
            GetIsActive = false;
            isPressFTwice = false;
            isPressF = true;
        }
    }
    bool IsAirJudge()
    {
        if (!isInAirCanFS && !isGround)
        {
            return true;
        }
        
        if (isGround) return true;
        else return false;
    }
    void InAirFSjudge()
    { if (isGround && isInAirCanFS) isInAirCanFS = false; }
    /// <summary>
    /// 未来视逻辑
    /// </summary>
    void FutureVision()
    {
        if (isPressF && isPressFTwice )//放出Shadow
        {
            //BS.SetBlackShaderFalse();
            //Debug.Log("fang");
            isShading = true;
            isPressF = false;
            isActive = true;
            this.GetComponent<Collider2D>().enabled = false;
            rb.gravityScale = 0f;
            rb.velocity = Vector2.zero;
            thisShadow.SetActive(true);
            thisShadow.transform.position = this.transform.position;
        }
        bool isOver = (LimitCircle(this.transform.position, thisShadow.transform.position) >= ShadowRdius);
        if (isShading && ((!isPressFTwice && isPressF))) //收回Shadow
        {
            //BS.SetBlackShaderTrue();
            //Debug.Log("shou");
            isShading = false;
            isPressF = false;
            isPressFTwice = false;
            this.GetComponent<Collider2D>().enabled = true;
            rb.gravityScale = 3f;
            isActive = false;
            isJump = true;
            thisShadow.SetActive(false);
            this.transform.position = thisShadow.transform.position;
            //thisShadow.GetComponent<ShadowSkill>().curTime = 0f;
            //thisShadow.GetComponent<ShadowSkill>().isRushActive = false;
        }
        if(isShading && isOver)
        {
            RetractShadow();
        }
    }


    public void RetractShadow()
    {
            isShading = false;
            isPressF = false;
            isPressFTwice = false;
            Vector3 nowPos = transform.position;
            this.GetComponent<Collider2D>().enabled = true;
            this.GetComponent<Rigidbody2D>().gravityScale = 3f;
            isActive = false;
            isJump = true;
            thisShadow.transform.position = nowPos;
            thisShadow.SetActive(false);
            thisShadow.GetComponent<ShadowSkill>().curTime = 0f;
            thisShadow.GetComponent<ShadowSkill>().isRushActive = false;
    }


    /// <summary>
    /// 距离判断
    /// </summary>
    /// <param name="thisPos"></param>
    /// <param name="nowPos"></param>
    /// <returns></returns>
    float LimitCircle(Vector3 thisPos,Vector3 nowPos)
    {
        var distance = (nowPos - thisPos).magnitude;
        return distance;
    }
    /// <summary>
    /// 霍尔网络控制
    /// </summary>
    void HallNetWalkControl()
    {
        if(Input.GetKeyUp(KeyCode.X) && !H_isInHallNW)
        {
            H_isPressF = true;
            H_isInHallNW = true;
            orignalPos = this.transform.position;
        }
    }
    /// <summary>
    /// 霍尔网络主要逻辑
    /// </summary>
    void HallNetWork()
    {
        if(H_isPressF)
        {
            var nowPos = this.transform.position;
            bool isInRange = (LimitCircle(orignalPos,nowPos) >= HallNetWorkRdius);
            if(isInRange)
            {
                H_isPressF = false;
                ValueRestore();
            }
            else
            {
                ValueEnhance();
            }
        }
    }
    /// <summary>
    /// 数值增强
    /// </summary>
    void ValueEnhance()
    {
        R_runSpeed= nowCharacter.runSpeed;
        R_rushSpeed = nowCharacter.rushSpeed;
        R_jumpForce = nowCharacter.jumpForce;

        nowCharacter.runSpeed = m_runSpeed;
        nowCharacter.rushSpeed = m_rushSpeed;
        nowCharacter.jumpForce = m_jumpForce;
    }

    void ValueRestore()
    {
        nowCharacter.runSpeed = R_runSpeed;
        nowCharacter.rushSpeed = R_rushSpeed;
        nowCharacter.jumpForce = R_jumpForce;
    }

}
