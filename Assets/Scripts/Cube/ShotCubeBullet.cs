using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotCubeBullet : MonoBehaviour
{
    public float speed;
    public float lifeTime;

    Vector3 dir;
    private void Update() {
        
        transform.Translate(Vector3.right * Time.deltaTime * speed);
        if(lifeTime > 0){
            lifeTime -= Time.deltaTime;
        }else{
            Destroy(gameObject);
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.transform.GetComponent<testCharacter>().Dead();
        }
        Destroy(gameObject);
    }

    public void SetDir(Vector3 d){
        dir = d;

        float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.AngleAxis(angle,Vector3.forward);
    }
}
