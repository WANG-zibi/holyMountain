using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class JumpSmoke : MonoBehaviour
{

    public float Factor;
    private void Start()
    {

    }
    private void Update()
    {
        Invoke("Remove", Factor);
    }
    void Remove()
    {
        Destroy(this.gameObject);
    }
}
