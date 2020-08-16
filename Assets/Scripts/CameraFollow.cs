using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    GameObject player;  //主角
    [Header("可调参数")]
    public float speed;  //相机跟随速度
    public float minPosx;  //相机不超过背景边界允许的最小值
    public float maxPosx;  //相机不超过背景边界允许的最大值

    private void Start() {
        player = GameObject.Find("Player(Clone)");
    }
    void Update()
    {
        FixCameraPos();
    }

    void FixCameraPos()
    {
        float pPosX = player.transform.position.x;  //主角 x轴方向 时实坐标值
        float pPosY = player.transform.position.y;  //主角 x轴方向 时实坐标值

        transform.position = new Vector3(pPosX, pPosY, transform.position.z);
    }
}
