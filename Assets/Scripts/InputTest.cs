using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    float f;
    // Update is called once per frame
    void Update()
    {
        f = Input.GetAxis("Horizontal");
    }

    private void OnGUI() {
        GUI.TextArea(new Rect(0, 0, 250, 40), "Current Button : " + f);
    }
}
