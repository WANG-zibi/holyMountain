using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowPool : MonoBehaviour
{
    public static ShadowPool instance;
    public GameObject shadowPrefab;
    public int shadowCount;
    private Queue<GameObject> qu;

    private void Awake()
    {
        qu = new Queue<GameObject>();
        instance = this;
        InitPool();
    }
    public void InitPool()
    {
        for(int i = 0; i < shadowCount; i++)
        {
            var newShadow = Instantiate(shadowPrefab);
            newShadow.transform.SetParent(transform);//设置子集

            //取消启用，返回对象池
            ReturnPool(newShadow);
        }
    }

    public void ReturnPool(GameObject gameobject)
    {
        gameobject.SetActive(false);
        qu.Enqueue(gameobject);//加入队列
    }

    public GameObject GetFormPool()
    {
        if (qu.Count == 0)
        {
            return null;
        }
        var outShadow = qu.Dequeue();
        outShadow.SetActive(true);

        return outShadow;
    }
}
