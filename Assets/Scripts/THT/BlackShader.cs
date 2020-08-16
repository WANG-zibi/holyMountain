using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class BlackShader : MonoBehaviour
{
    public Material curMaterial;
    private void Start()
    {
        
    }

    private void Update()
    {
   
    }
    public void SetBlackShaderFalse()
    {
        curMaterial.SetFloat("_LuminosityAmount", 1.0f);
    }
    public void SetBlackShaderTrue()
    {
        curMaterial.SetFloat("_LuminosityAmount", 0.0f);
    }

}