using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CosetteSkill : Character
{

    public Transform left;
    public Transform right;


    [Header("可调参数")]
    //public Transform GroundCheck;
    public float BulletTime;
    public float WallJumpTime;
    public float AntiForce;
    public float bulletRdius;
    //public new Transform groundCheck;

    bool m_isGround; //判断是否在地上
    bool m_isJumpSpeed;
    bool m_isJumpButtonDown;
    bool isLeft, isRight;
    bool isWall;
    float m_curTime;
    bool isWallJump;
    float nowTime;
    float m_jumpTime;
    bool isBullet;
    bool isBulleting;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        transform.localScale = new Vector3(-1f, transform.localScale.y, transform.localScale.z);
        rb.velocity = Vector2.zero;
        rb.gravityScale = 3f;
    }
    
    void Update()
    {
        //Debug.Log(isWallJump);
        WallJumpTimeCtrl();
        Vector2 pos = transform.position;
        isBulleting = Physics2D.OverlapCircle(pos, bulletRdius, LayerMask.GetMask("bullet"));
        isLeft = Physics2D.OverlapCircle(left.position, 0.1f, ground);
        isRight = Physics2D.OverlapCircle(right.position, 0.1f, ground);
        isWall = (isLeft || isRight);
        m_isGround = Physics2D.OverlapCircle(groundCheck.position, 0.1f, ground);
        BulletRushControl();
        JumpControl();
        RushTimeContrl();
    }
    void FixedUpdate()
    {
        GroundMovement();
        SpeedDecrease();
        
        if(!isCanJump)
            Jump();
        Rush();
    }

    /// <summary>
    /// 
    /// </summary>

    void WallJumpTimeCtrl()
    {
        if (isWallJump)
        {
            nowTime += Time.deltaTime;
        }
        if (nowTime > WallJumpTime)
        {
            nowTime = 0f;
            isWallJump = false;
        }
    }   
    /// <summary>
    /// 移动逻辑实现
    /// </summary>
    new void GroundMovement()
    {
        float hor = Input.GetAxisRaw("Horizontal");
        if (hor != 0f)
        {
            transform.localScale = new Vector3(-hor, 1, 1);
            rb.velocity = new Vector2(hor * runSpeed, rb.velocity.y);
        }

        if (hor == 0f && !isWallJump)
        {
                rb.velocity = new Vector2(hor * runSpeed, rb.velocity.y);
        }
    }
    /// <summary>
    /// 子弹冲逻辑
    /// </summary>
    void BulletRushControl()
    {
        if (Input.GetKeyDown(KeyCode.X) && isBulleting)
        {
            isBullet = true;
            isWallJump = true;
        }
        if (m_curTime < rushTime && isBulleting && isBullet)
        {
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(-rushSpeed * transform.localScale.x, rushSpeed);
            m_curTime += Time.deltaTime;
        }
        if (m_curTime >= BulletTime)
        {
            rb.gravityScale = 3f;
            rb.velocity = new Vector2(0f, rb.velocity.y);
            m_curTime = 0f;
            isBullet = false;
            isBulleting = false;
        }
    }


    new void JumpControl()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !m_isJumpButtonDown && (m_isGround || isWall))
        {
            m_isJumpButtonDown = true;
            m_isJumpSpeed = true;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (rb.velocity.y > 0f)
                rb.velocity = new Vector2(rb.velocity.x, 0.5f);
            m_isJumpButtonDown = false;
            m_isJumpSpeed = false;
            m_jumpTime = 0f;
        }
        if (m_isJumpButtonDown && m_jumpTime < JumpDownButtonTime)
        {
            m_jumpTime += Time.deltaTime;
        }
        if (m_isJumpButtonDown && m_jumpTime >= JumpDownButtonTime)
        {
            m_isJumpButtonDown = false;
            m_isJumpSpeed = false;
            m_jumpTime = 0f;
        }
    }
    /// <summary>
    /// 普通跳跃速度实现
    /// </summary>
    new void Jump()
    {
        if (m_isJumpButtonDown && m_isJumpSpeed && !isWall)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        if (m_isJumpButtonDown && m_isJumpSpeed && isWall)
        {
            isWallJump = true;
            float dirVal = isLeft == false ? 1f:-1f;
            dirVal *= -transform.localScale.x;
            rb.velocity = new Vector2(-dirVal*jumpForce, jumpForce);
        }
    }
    void SpeedDecrease()
    {
        if(isWall)
        {
            rb.velocity = new Vector2(rb.velocity.x,-AntiForce);
        }
    }
    
}

