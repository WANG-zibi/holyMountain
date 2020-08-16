using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class InputLimit : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player"){
            keybd_event(88,0,0,0);
        
            

            Destroy(gameObject);
        }
    }

    [DllImport("user32.dll", EntryPoint = "keybd_event")]
    public static extern void keybd_event(
 
            byte bVk,    //虚拟键值 对应按键的ascll码十进制值
 
            byte bScan,// 0
 
            int dwFlags,  //0 为按下，1按住，2为释放
 
            int dwExtraInfo  // 0
 
    );
}
