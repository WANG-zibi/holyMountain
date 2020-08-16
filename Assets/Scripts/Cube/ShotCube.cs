using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotCube : Cube
{
    public GameObject bulletPre;
    public Transform firePosition;

    public float shotInterval;
    public float bulletTime;
    public float bulletSpeed;

    protected float timer = 10f;

    private void Update() {
        if(timer < shotInterval){
            timer += Time.deltaTime;
        }else{
            Shot(null);
            timer = 0;
        }
    }

    protected virtual void Shot(Transform tar){
        AudioPoolMgr.AudioPlay("Trap", AudioClipName.子弹);
        GameObject bullet = Instantiate(bulletPre,firePosition.position,bulletPre.transform.rotation);
        ShotCubeBullet bulletScript = bullet.GetComponent<ShotCubeBullet>();
        bulletScript.SetDir(firePosition.forward);
        bulletScript.lifeTime = bulletTime;
        bulletScript.speed = bulletSpeed;
    }


}
