using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testHall2 : testCharacter
{
/***************************************************************************************************
    以下为    开放给外界的变量
****************************************************************************************************/
    [Header("技能特效")]
    public GameObject effectPre;
    
    [Header("技能半径")]
    public float radius;

    [Header("增强倍数,大于1")]
    public float multiple;

    public BlackShader BS;
    public Material material;

/***************************************************************************************************
    以下为    私有的变量
****************************************************************************************************/
    [Header("保存原数值")]
    float runspeed_R;
    float jumpforce_R;
    float jumpforceadd_R;
    float rushspeed_R;

    [Header("原点")]
    Vector3 ori;
    
    [Header("技能是否开始")]
    bool isSkillStart = false;
    Material thisMaterial;

    GameObject effect;


    private void Awake() {
        init();
    }

    /*
     * 初始化
     */
    public override void init(){
        base.init();
        runspeed_R = runSpeed;
        jumpforce_R = jumpForce;
        //jumpforceadd_R = jumpForceAdd;
        rushspeed_R = rushSpeed;

        thisMaterial = GetComponent<Renderer>().material;
    }

    /*
     * 逐帧检测，角色超出半径会结束技能
     */
    private void Update() {
        base.DoUpdate();

        if(isSkillStart)//技能开始后检测位置
        {
            if(Vector3.Distance(transform.position,ori) >= radius){
                SkillEnd();
            }else{
                canSkill = false;
                ShadowPool.instance.GetFormPool();
            }
        }
    }

    /*
     * 重写角色技能
     * 刚进入技能状态时，改变角色速度
     * 角色超出范围时，结束技能
     */
    protected override void Skill(){
        if(!isSkillStart){
            AudioPoolMgr.AudioPlay("skill", AudioClipName.霍尔网络);
            SkillStart();
        }
    }

    /*
     * 改变角色速度
     */
    void SkillStart(){
        BS.SetBlackShaderFalse();
        if(effect == null)
            effect = Instantiate(effectPre,transform.position,effectPre.transform.rotation);
        GetComponent<Renderer>().material = material;
        isSkillStart = true;
        status = 1;

        runSpeed *= multiple;
        jumpForce *= multiple;
        rushSpeed *= multiple;
        //jumpForceAdd *= multiple;

        ori = transform.position;

    }

    /*
     * 改变恢复速度
     */
    public override void SkillEnd(){
        GetComponent<Renderer>().material = thisMaterial;
        BS.SetBlackShaderTrue();
        
        if(effect != null){
            Destroy(effect);
        }
        isSkillStart = false;
        isSkill = false;

        runSpeed = runspeed_R;
        jumpForce = jumpforce_R;
        //jumpForceAdd = jumpforceadd_R;
        rushSpeed = rushspeed_R;
    }

}
