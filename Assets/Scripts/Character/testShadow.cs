using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testShadow : testCharacter
{
/***************************************************************************************************
    以下为    开放但是在界面隐藏的变量
****************************************************************************************************/
    [HideInInspector]
    public GameObject hall;

    [HideInInspector]
    public float dis;

/***************************************************************************************************
    以下为    私有的变量
****************************************************************************************************/
    [Header("分身材质")]
    Material material;


    void Start(){
        init();
        
    }

    /*
     * 初始化
     */
    public override void init(){
        base.init();
        canRush = true;
        material = GetComponent<Renderer>().material;
        if(levelCnt == 6){
            material.SetFloat("_DragInterval",10);
        }else if(levelCnt == 7){
            material.SetFloat("_DragInterval",1);
        }
    }
    
    /*
     * 逐帧检测，根据离角色的距离改变透明度
     */
    void Update(){
        base.DoUpdate();
        material.color = new Color(material.color.r,material.color.g,material.color.b,(dis-(Vector3.Distance(transform.position,hall.transform.position)))/dis+0.2f);
        
    }

    /*
     * 残影的技能即结束未来视
     */
    protected override void Skill(){
        isSkill = false;
        hall.GetComponent<testHall>().SkillEnd();
    }

    void Set_testHall()
    {
        hall.GetComponent<testHall>().enabled = true;
    }
    /*
     * 残影的技能就调用Hall的“残影死亡”方法
     */
    public override void Dead(){
        isSkill = false;

        hall.GetComponent<testHall>().SkillDead();
        hall.GetComponent<testHall>().enabled = false;
        Invoke("Set_testHall", 0.5f);
    }
}
