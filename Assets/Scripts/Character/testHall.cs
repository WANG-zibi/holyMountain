using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.Experimental.Rendering.Universal;

public class testHall : testCharacter
{
/***************************************************************************************************
    以下为    开放给外界的变量
****************************************************************************************************/
    [Header("拖就完事了")]
    public GameObject shadow;
    public BlackShader BS;
    [Header("场景材质")]
    public Material lightshader;
    public Material blackshader;

    [Header("未来视距离")]
    public float shadowRdius;
    [Header("未来视死亡特效")]
    public GameObject shadowDeadEffectPre;

/***************************************************************************************************
    以下为    私有的变量
****************************************************************************************************/
    GameObject thisShadow;
    GameObject shadowDeadEffect;
    float shadowEffectTimer;
    
/***************************************************************************************************
    以下为    隐藏的变量
****************************************************************************************************/
    [HideInInspector]
    [Header("阴影头上的雨水")]
    public GameObject rainPre;

    private void Start()
    {
        init();
    }
    /*
     * 初始化
     */
    public override void init(){
        base.init();
        thisShadow = Instantiate(shadow);
        if(rainPre != null){
            GameObject rain = Instantiate(rainPre,new Vector3(thisShadow.transform.position.x,thisShadow.transform.position.y+0.3f,thisShadow.transform.position.z),rainPre.transform.rotation);
            rain.transform.parent = thisShadow.transform;
        }
        thisShadow.transform.localScale = transform.localScale;
        thisShadow.SetActive(false);

        thisShadow.GetComponent<testShadow>().hall = gameObject;
        thisShadow.GetComponent<testShadow>().dis = shadowRdius;

        
        shadowEffectTimer = 1;
    }

    /*
     * 重写角色技能
     * 刚进入技能状态时，放出残影
     * 残影超出范围时，残影死亡
     */
    bool isSkillStart = true;//只在此处应用，保证只执行一次
    protected override void Skill(){
        if(isSkillStart){
            AudioPoolMgr.AudioPlay("skill",AudioClipName.未来视分身);
            SkillStart();
        }

        if(Vector3.Distance(transform.position,thisShadow.transform.position) >= shadowRdius){
            
            SkillDead();
        }
    }

    /*
     * 放出残影
     * 画面变为黑白
     * 取消角色的碰撞体和重力，将角色速度设为0
     * 将残影的active状态设为true，初始化残影，将残影的位置设为角色的位置
     */
    void SkillStart(){

        GameObject tmp = GameObject.Find("BackGround");
        SpriteRenderer[] sprite = tmp.GetComponentsInChildren<SpriteRenderer>();
        foreach(SpriteRenderer s in sprite)
        {
            s.sharedMaterial = blackshader;
        }
        for (int i = 0; i < GameObject.Find("Grid").transform.childCount; ++i)
            GameObject.Find("Grid").transform.GetChild(i).gameObject.GetComponent<TilemapRenderer>().sharedMaterial = blackshader;
        isSkillStart = false;
        BS.SetBlackShaderFalse();

        for(int i = 0; i < colls.Length; i++){
            Collider2D c = colls[i];
            c.enabled = false;
        }
        rb.velocity = new Vector2(0,0);
        rb.gravityScale = 0;

        thisShadow.transform.localScale = transform.localScale;
        thisShadow.SetActive(true);
        thisShadow.GetComponent<testShadow>().init();
        thisShadow.transform.position = this.transform.position;
    }

    /*
     * 残影死亡
     * 恢复画面
     * 将残影的active状态设为false，将残影的位置设为角色的位置
     * 恢复角色的碰撞体和重力，将角色的技能状态isSkill标记为false表示技能结束，将角色的技能canSkill标记false表示不能重复放技能
     * 将isSkillStart重置为true，以便下次放技能时使用
     */
    float TimeScaleTimer = 1;
    public void SkillDead(){
       
        GameObject tmp = GameObject.Find("BackGround");
        SpriteRenderer[] sprite = tmp.GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer s in sprite)
        {
            s.sharedMaterial = lightshader;
        }
        for (int i = 0; i < GameObject.Find("Grid").transform.childCount; ++i)
            GameObject.Find("Grid").transform.GetChild(i).gameObject.GetComponent<TilemapRenderer>().sharedMaterial = lightshader;
        

        isSkillStart = true;
        isSkill = false;
        BS.SetBlackShaderTrue();
        if(shadowDeadEffect == null){
            shadowEffectTimer = 0;
            shadowDeadEffect = Instantiate(shadowDeadEffectPre,thisShadow.transform.position,deadEffectPre.transform.rotation);
        }

        thisShadow.transform.position = this.transform.position;
        thisShadow.GetComponent<testCharacter>().runS.SetActive(false);
        thisShadow.SetActive(false);

        for(int i = 0; i < colls.Length; i++){
            Collider2D c = colls[i];
            c.enabled = true;
        }
        rb.gravityScale = 3;
        canSkill = false;

        // Time.timeScale = 0;
        // TimeScaleTimer = 0;

        
    }

    /*
     * 技能结束（提供给残影调用）
     * 恢复画面
     * 将残影的active状态设为false，将角色的位置设为残影的位置
     * 恢复角色的碰撞体和重力，将角色的技能状态isSkill标记为false表示技能结束，将角色的技能canSkill标记false表示不能重复放技能
     * 将isSkillStart重置为true，以便下次放技能时使用
     */
    public override void SkillEnd(){
        
        AudioPoolMgr.AudioPlay("skill", AudioClipName.未来视瞬移);

        GameObject tmp = GameObject.Find("BackGround");
        SpriteRenderer[] sprite = tmp.GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer s in sprite)
        {
            s.sharedMaterial = lightshader;
        }
        for (int i = 0; i < GameObject.Find("Grid").transform.childCount; ++i)
            GameObject.Find("Grid").transform.GetChild(i).gameObject.GetComponent<TilemapRenderer>().sharedMaterial = lightshader;
        
        
        isSkillStart = true;
        isSkill = false;
        BS.SetBlackShaderTrue();
		if(thisShadow != null){
	        this.transform.position = thisShadow.transform.position;

            
            transform.localScale = thisShadow.transform.localScale;
	        if(thisShadow.GetComponent<testCharacter>().runS != null)
	            thisShadow.GetComponent<testCharacter>().runS.SetActive(false);
	        thisShadow.SetActive(false);        
        }
        
        if(colls != null){
            for(int i = 0; i < colls.Length; i++){
                Collider2D c = colls[i];
                c.enabled = true;
            }
        }
        if(rb != null)
            rb.gravityScale = 3;
        canSkill = false;

        groundTimer = 10;

        
    }

    private void Update() {
        base.DoUpdate();
        // if(TimeScaleTimer < 1){
        //     TimeScaleTimer += 0.01f;
        //     Time.timeScale = 0;
        // }else{
        //     if(Time.timeScale != 1)
        //         GameObject.Find("Light Effects").GetComponent<Light2D>().color = Color.white;
        //     Time.timeScale = 1;
        // }


        if(shadowDeadEffect != null){
            if(shadowEffectTimer < 0.4f){
                shadowEffectTimer += Time.deltaTime;
            }else{
                shadowEffectTimer = 0.4f;
                Destroy(shadowDeadEffect);
            }
        }
    }
}
