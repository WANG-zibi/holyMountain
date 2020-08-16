using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class ShadowText : MonoBehaviour
{
    // Start is called before the first frame update
    Text ID;
    public Transform player;
    float k = -1f;
    Vector3 now;
    private void Start()
    {
        ID = GetComponent<Text>();
        string dig = Random.Range(0, 8127).ToString();
        ID.text = dig;
        transform.localScale = new Vector3(k * transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }
    private void Update()
    {

        if (now.x != player.localScale.x)
        {
            transform.localScale = new Vector3(k * transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
        now = player.localScale;
    }

}
