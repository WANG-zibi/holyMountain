using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackBackController : MonoBehaviour
{
    public Transform GM;
    public Transform bottomPos;
    public Transform dirPos;
    public GameObject dialog;
    public float speed;

    GameObject []tmp;
    bool isEnd;

    private void Start()
    {
        isEnd = false;
       
    }
    private void Update() {
        Debug.Log(isEnd);
        //if(dialog != null)
        //    return;
        tmp = GameObject.FindGameObjectsWithTag("Player");
        if (tmp == null)
            return;
        if (!isEnd)
        {
            Debug.Log("1111111");
            Vector3 pos = GM.position;
            pos.x = bottomPos.position.x;
            //float y = (bottomPos.position.y - pos.y) * 0.1f;
            transform.Translate((pos - bottomPos.position) * speed * Time.deltaTime);
        }

        if (Vector3.Distance(dirPos.position, bottomPos.position) < 1f)
        {
            isEnd = true;
        }
        if (!tmp[0].activeSelf)
        {
            isEnd = false;
        }


        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player"){
            other.GetComponent<testCharacter>().Dead();
        }
    }
}
