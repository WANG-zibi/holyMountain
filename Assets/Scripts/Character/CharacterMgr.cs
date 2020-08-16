using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Experimental.Rendering.Universal;

public class CharacterMgr : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("屏蔽按键")]
    public KeyCode JUMP;
    public KeyCode RUSH;
    public KeyCode SKILL;
    public KeyCode CHANGE;

    public bool isFollow = true;

    public GameObject Hall, Cosette;
    public GameObject rainPre;
    public int levelCnt = 1;
    public Transform originPos;
    public string playerName = "Hall";
    public float delayTime = 0.5f;
    static GameObject hall, cosette;
    GameObject curPlayer;
    string nowPlayer;
    float curTime;
    bool isChanging;
    /// <summary>
    /// 布尔判断是否改变
    /// </summary>
    public bool CanChange = true;
    void Start()
    {
        DialogC_GLOBAL.RESETDEADFLAG();

        hall = Instantiate(Hall);
        cosette = Instantiate(Cosette);
        hall.transform.localScale = new Vector2(-hall.transform.localScale.x, hall.transform.localScale.y);
        cosette.transform.localScale = new Vector2(-cosette.transform.localScale.x, cosette.transform.localScale.y);
        if (rainPre != null){
            GameObject rain = Instantiate(rainPre,new Vector3(hall.transform.position.x,hall.transform.position.y+0.3f,hall.transform.position.z),rainPre.transform.rotation);
            rain.transform.parent = hall.transform;
            if(hall.GetComponent<testHall>() != null)
                hall.GetComponent<testHall>().rainPre = rainPre;
            

            GameObject rain1 = Instantiate(rainPre,new Vector3(cosette.transform.position.x,cosette.transform.position.y+0.3f,cosette.transform.position.z),rainPre.transform.rotation);
            rain1.transform.parent = cosette.transform;
        }

        hall.GetComponent<testCharacter>().CMR = this;
        cosette.GetComponent<testCharacter>().CMR = this;
        hall.GetComponent<testCharacter>().levelCnt = levelCnt;
        SetPlayer(playerName,originPos.position);
        hall.GetComponent<testCharacter>().JUMP = JUMP;
        hall.GetComponent<testCharacter>().RUSH = RUSH;
        hall.GetComponent<testCharacter>().SKILL = SKILL;
        hall.GetComponent<testCharacter>().CHANGE = CHANGE;
        cosette.GetComponent<testCharacter>().JUMP = JUMP;
        cosette.GetComponent<testCharacter>().RUSH = RUSH;
        cosette.GetComponent<testCharacter>().SKILL = SKILL;
        cosette.GetComponent<testCharacter>().CHANGE = CHANGE;
    }
    float TimeScaleTimer = 1;
    void Update()
    {
        if(isFollow)
            transform.position = curPlayer.transform.position;

        CalCurTime();

        if(TimeScaleTimer < 1){
            TimeScaleTimer += 0.05f;
            Time.timeScale = 0;
        }else{
            if(Time.timeScale != 1)
                GameObject.Find("Light Effects").GetComponent<Light2D>().color = Color.white;
            Time.timeScale = 1;
        }
    }
    
    public void Revive()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void SetPlayer(string playername,Vector3 pos)
    {
        nowPlayer = playername;
        if(playername =="Hall")
        {
            curPlayer = hall;
            curPlayer.transform.position = pos;
            hall.GetComponent<testCharacter>().canSkill = true;
            hall.transform.localScale = cosette.transform.localScale;
            hall.SetActive(true);
            //cosette.GetComponent<testCharacter>().runS.SetActive(false);
            cosette.SetActive(false);
        }
        if(playername == "Cosette")
        {
            curPlayer = cosette;
            curPlayer.transform.position = pos;
            hall.GetComponent<testCharacter>().SkillEnd();
            
            cosette.transform.localScale = hall.transform.localScale;
            hall.SetActive(false);
           // hall.GetComponent<testCharacter>().runS.SetActive(false);
            cosette.SetActive(true);
            cosette.GetComponent<testCharacter>().canSkill = true;
        }
    }
    public void ChangePlayer()
    {
        if (CanChange && !isChanging)
        {
            isChanging = true;
            if(nowPlayer == "Hall")
            {
                Vector3 pos = hall.transform.position;
                SetPlayer("Cosette", pos);
            }
            else if(nowPlayer =="Cosette")
            {
                Vector3 pos = cosette.transform.position;
                SetPlayer("Hall", pos);
            }

            
            Time.timeScale = 0;
            TimeScaleTimer = 0;
            GameObject.Find("Light Effects").GetComponent<Light2D>().color = Color.green;
        }
        
    }
    void CalCurTime()
    {
        if(isChanging)
        {
            curTime += Time.deltaTime;
        }
        if(curTime >= delayTime)
        {
            isChanging = false;
        }
    }
    // Update is called once per frame
    
}
