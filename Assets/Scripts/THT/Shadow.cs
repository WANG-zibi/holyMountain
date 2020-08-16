using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : MonoBehaviour
{
    private Transform player;

    private SpriteRenderer thisSprite;
    private SpriteRenderer playerSprite;

    private Color color;

    [Header("时间控制参数")]
    public float activeTime;//需要显示的时间
    public float activeStart;//开始显示时间

    [Header("不透明控制")]
    private float alpha;
    public float alphaSet;
    public float alphaNum;

    private void OnEnable()
    {
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p == null)
            return;
        player = p.transform;
        
        thisSprite = GetComponent<SpriteRenderer>();
        playerSprite = player.GetComponent<SpriteRenderer>();
        alpha = alphaSet;

        thisSprite.sprite = playerSprite.sprite;
        transform.position = player.position;
        transform.localScale = player.localScale;
        transform.rotation = player.rotation;

        activeStart = Time.time;
    }
    void Update()
    {
        alpha *= alphaNum;
        color = new Color(0.376f, 1.0f, 1.0f, alpha);
        thisSprite.color = color;

        if(Time.time >= activeStart + activeTime)
        {
            //返回对象池
            ShadowPool.instance.ReturnPool(this.gameObject);
        }
    }
}
