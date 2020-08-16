using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DialogC_GLOBAL
{
    private static bool Ins;


    public static void RESETDEADFLAG(){
        if(!Ins){
            Ins = true;
            PlayerPrefs.SetInt("Dead",0);
        }
    }
}
