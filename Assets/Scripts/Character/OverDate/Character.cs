using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [HideInInspector]
    public CharacterMgr CMR;

    [HideInInspector]
    public int levelCnt;
    public Transform groundCheck;//地面检测点
    public LayerMask ground;//地面层
    [Header("冲刺CD")]
    public float rushCD;

    [Header("跳起特效")]
    public GameObject jumpEffectPre;
    [Header("走路特效")]
    public GameObject walkEffectPre;
    [Header("可调参数")]
    
    public float runSpeed;
    public float jumpForce;
    public float rushSpeed;
    public float rushTime;
    public float JumpDownButtonTime;
    [Header("陷阱接口")]
    public bool isTrap;
    public Rigidbody2D rb;
    protected Collider2D coll;
    protected Animator anim;
    public bool isRushActive;
    protected bool isGround; //判断是否在地上
    bool isJumpSpeed;
    bool isJumpButtonDown;
    bool isRushCD;
    [HideInInspector]
    public float curTime;
    float jumpTime;
    float rushCDtime;
    public bool isJump { get; set; }
    public bool isCanJump { get; set; }
    GameObject walkEffect;
    public virtual void Dead()
    {
        Invoke("Revive", 1f);
        gameObject.SetActive(false);
    }

    void Revive()
    {
        CMR.Revive();
    }

    /// <summary>
    /// 移动逻辑实现
    /// </summary>
    virtual protected void GroundMovement() 
    {
        float hor = Input.GetAxisRaw("Horizontal");
        if (hor != 0f)
        {
            if(walkEffect == null && isGround)
                walkEffect = Instantiate(walkEffectPre,groundCheck.position,jumpEffectPre.transform.rotation);
            transform.localScale = new Vector3(-hor, 1, 1);
            anim.SetBool("isRun",true);
            rb.velocity = new Vector2(hor * runSpeed, rb.velocity.y);
        }
        if (hor == 0f)
        {
            anim.SetBool("isRun", false);

                rb.velocity = new Vector2(hor * runSpeed, rb.velocity.y);
            
        }
    }
    /// <summary>
    /// 冲刺输入/条件判断
    /// </summary>
    virtual protected void RushTimeContrl()
    {
        RushCDtime();
        if (Input.GetKeyDown(KeyCode.Z) && !isRushActive && !isRushCD)
        {
            rb.gravityScale = 0f;
            isRushActive = true;
        }
        if(curTime<rushTime && isRushActive)
        {
            curTime += Time.deltaTime;
        }
        if(curTime>= rushTime)
        {
            isRushCD = true;
            curTime = 0f;
            rb.gravityScale = 3f;
            isRushActive = false;
        }
    }
    void RushCDtime()
    {
        if(isRushCD)
        {
            rushCDtime += Time.deltaTime;
        }
        if(rushCDtime >= rushCD)
        {
            rushCDtime = 0f;
            isRushCD = false;
        }
    }
    /// <summary>
    /// 冲刺逻辑实现
    /// </summary>
    virtual protected void Rush()
    {
        if (isRushActive)
        {
            rb.velocity = new Vector2(-rushSpeed * transform.localScale.x,0f);
            ShadowPool.instance.GetFormPool();
        }
    }
    /// <summary>
    /// 跳跃输入/条件判断
    /// </summary>
    virtual protected void JumpControl()
    {
        isGround = Physics2D.OverlapCircle(groundCheck.position, 0.1f, ground);

        if (isGround && anim.GetBool("isJump"))
        {
            Instantiate(jumpEffectPre,groundCheck.position,jumpEffectPre.transform.rotation);
            anim.SetBool("isJump", false);
        }
        if(!isGround && anim.GetBool("isJump") && rb.velocity.y <= 0f)
        {
            anim.SetTrigger("jumpingDown");
         }
        if (Input.GetKeyDown(KeyCode.Space) && !isJumpButtonDown && (isGround || isJump))
        {
            
            Instantiate(jumpEffectPre,groundCheck.position,jumpEffectPre.transform.rotation);
            anim.SetBool("isJump",true);
            isJump = false;
            isJumpButtonDown = true;
            isJumpSpeed = true;

        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if(rb.velocity.y >0f)
            rb.velocity = new Vector2(rb.velocity.x,0.5f);
            anim.SetBool("isJump", true);
            isJumpButtonDown = false;
            isJumpSpeed = false;
            jumpTime = 0f;
        }
        if(isJumpButtonDown && jumpTime<JumpDownButtonTime)
        {
            jumpTime += Time.deltaTime;
        }
        if(isJumpButtonDown && jumpTime >= JumpDownButtonTime)
        {
            isJumpButtonDown = false;
            isJumpSpeed = false;
            jumpTime = 0f;
        }
    }

    /// <summary>
    /// 跳跃速度实现
    /// </summary>
    virtual protected void Jump() 
    {
        if(isJumpButtonDown && isJumpSpeed)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
     }

}
