
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 音效的名字
/// </summary>
public enum AudioClipName
{
    起跳落地,
    //死亡
    死亡,
    //走路
    走路草,
    走路雪,
    走路雨,
    //跳跃男
    跳跃男1,
    跳跃男2,
    跳跃男3,
    //跳跃女
    跳跃女1,
    跳跃女2,
    跳跃女3,
        //冲刺
    冲刺1,
    冲刺2,
    冲刺3,
    //陷阱
    子弹,
    挤压方块,
    弹簧,
    砸,
    易碎方块,
    //环境音
    鸟鸣,
    大下雨,
    中下雨,
    小下雨,
    打雷1,
    打雷2,
    洞穴,
    心跳,
    风声1,
    风声2,
    风声3,
    //技能
    未来视瞬移,
    未来视分身,
    霍尔网络,
    踩子弹,
    暗影步,
    //UI
    背景音乐1,
    背景音乐2,
    背景音乐3,
    背景音乐4,
    背景音乐5,
    背景音乐6,
    背景音乐7,
    背景音乐8,
    背景音乐9,
    背景音乐10,
    背景音乐11,
    背景音乐12,
    背景音乐13,
    背景音乐14,
    背景音乐15,

    点击音,
    关卡切换音
}

/// <summary>
/// 音效池管理类
/// </summary>
public class AudioPoolMgr : MonoBehaviour
{
    public  static AudioPoolMgr thisInstance;
    [Header("音效")]
    //public AudioClip[] audioList = new AudioClip[3];
    public AudioSource Player;
    public AudioSource Rush;
    public AudioSource Jump;
    public AudioSource skill;
    public AudioSource srrounding;
    public AudioSource background;
    public AudioSource Trap;
    [System.Serializable]
    public struct AudioItem
    {
        public AudioClipName name;
        public AudioClip audioClip;
    }

    /// <summary>
    /// 在Inspector面板上显示接收音频和其名字
    /// </summary>
    public AudioItem[] audioItems;

    /// <summary>
    /// 存储音效的字典，在Awake中进行初始化
    /// </summary>
   static public   Dictionary<AudioClipName, AudioClip> audioClips = new Dictionary<AudioClipName, AudioClip>();

    
    
    private void Awake()
    {

        
        for (int i = 0; i < audioItems.Length; i++)
        {
            if (!audioClips.ContainsKey(audioItems[i].name))
            {
                audioClips.Add(audioItems[i].name, audioItems[i].audioClip);
            }
        }
        //backGround = gameObject.AddComponent<AudioSource>();
        //surrounding = gameObject.AddComponent<AudioSource>();
        //run = gameObject.AddComponent<AudioSource>();
        //将audioItems数组中的音效初始化给audioClips字典，更方便使用
        for (int i = 0; i < audioItems.Length; i++)
        {
            if (!audioClips.ContainsKey(audioItems[i].name))
            {
                audioClips.Add(audioItems[i].name, audioItems[i].audioClip);
            }
        }
        //初始化对象池
        //Player = Instantiate(_instance);
        Player = this.gameObject.AddComponent<AudioSource>();
        background = this.gameObject.AddComponent<AudioSource>();
        Trap = this.gameObject.AddComponent<AudioSource>();
        Rush = this.gameObject.AddComponent<AudioSource>();
        Jump = this.gameObject.AddComponent<AudioSource>();
        srrounding = this.gameObject.AddComponent<AudioSource>();
        skill = this.gameObject.AddComponent<AudioSource>();
        thisInstance = this;
    }
    private void Start()
    {
        srrounding.loop = true;
        background.loop = true;

    }


    static public void AudioPlay(string name, AudioClipName idx)
    {
        if (name == "srrounding")
        {
            thisInstance.srrounding.playOnAwake = true;
            thisInstance.srrounding.clip = audioClips[idx];
            thisInstance.srrounding.loop = true;
            thisInstance.srrounding.Play();

        }
        if (name == "player")
        {
            if (!thisInstance.Player.isPlaying)
            {
                thisInstance.Player.clip = audioClips[idx];
                thisInstance.Player.Play();
            }
        }
        if (name == "skill")
        {
            thisInstance.skill.clip = audioClips[idx];
            thisInstance.skill.Play();

        }
        if (name == "rush")
        {

            thisInstance.Rush.clip = audioClips[idx];
            thisInstance.Rush.Play();

        }
        if (name == "jump")
        {
            if (!thisInstance.Jump.isPlaying)
            {
                thisInstance.Jump.clip = audioClips[idx];
                thisInstance.Jump.Play();
            }

        }
        if (name == "background")
        {
            thisInstance.background.clip = audioClips[idx];
            thisInstance.background.loop = true;
            thisInstance.background.Play();
        }
        if (name == "Trap")
        {
            if (!thisInstance.Trap.isPlaying)
            {
                thisInstance.Trap.volume = 0.5f;
                thisInstance.Trap.clip = audioClips[idx];
                thisInstance.Trap.Play();
            }
        }
        else
        {
            //Debug.LogError("name select ERROR!");
        }
    }

}
