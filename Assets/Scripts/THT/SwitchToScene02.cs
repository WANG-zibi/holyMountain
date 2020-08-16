using UnityEngine.SceneManagement;
using UnityEngine;

public class SwitchToScene02 : MonoBehaviour
{
   
    void Update()
    {
        if(Input.anyKeyDown)
        {
            SceneManager.LoadScene(1);
;        }
    }
}
