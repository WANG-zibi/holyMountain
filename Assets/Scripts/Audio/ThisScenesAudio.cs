using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class ThisScenesAudio : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioClipName BGM;
    public AudioClipName SE;
    private void Awake()
    {
        
    }
    void Start()
    {
        GameObject[] thisCount = GameObject.FindGameObjectsWithTag("audio");
        if (thisCount.Length > 1)
        {
            if (thisCount[0].GetComponent<AudioPoolMgr>().background.clip != AudioPoolMgr.audioClips[BGM])
            {
                for(int i = 0;i<= thisCount.Length - 2;i++)
                    Destroy(thisCount[i]);
            }
            else
            {
                return;
            }
        }
        if (AudioPoolMgr.thisInstance.background.clip != AudioPoolMgr.audioClips[BGM] )
        {
            AudioPoolMgr.thisInstance.background.clip = AudioPoolMgr.audioClips[BGM];
            AudioPoolMgr.thisInstance.background.loop = true;
            AudioPoolMgr.thisInstance.background.Play();
        }
        if (AudioPoolMgr.thisInstance.srrounding.clip != AudioPoolMgr.audioClips[SE])
        {
            AudioPoolMgr.thisInstance.srrounding.clip = AudioPoolMgr.audioClips[SE];
            AudioPoolMgr.thisInstance.background.loop = true;
            AudioPoolMgr.thisInstance.srrounding.Play();
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
