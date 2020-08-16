using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeManager : MonoBehaviour
{
    const float defaultRebuidTime = 1;
    Dictionary<GameObject,float> rebuildCubes = new Dictionary<GameObject, float>();

    
    public void AddRebuildCube(GameObject cubeObject,float rebuildTime = defaultRebuidTime)
    {
        rebuildCubes.Add(cubeObject,rebuildTime);
    }

    void Update()
    {
        Rebuild();
    }

    void Rebuild()
    {
        List<GameObject> keyList = new List<GameObject>(rebuildCubes.Keys);
        for(int i = keyList.Count - 1; i >= 0; i--)
        {
            GameObject cubeObject = keyList[i];
            if(rebuildCubes[cubeObject] > 0)
            {
                rebuildCubes[cubeObject] -= Time.deltaTime;
            }
            else
            {
                cubeObject.SetActive(true);
                rebuildCubes.Remove(cubeObject);
                cubeObject.GetComponent<FragileCube>().isPlayerOn = false;
            }
        }
    }

}
