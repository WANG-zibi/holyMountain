using UnityEngine;

public class BgMove : MonoBehaviour
{
    [Header("调整背景移动速度")]
    public float forest1;
    public float forest2;
    public float forest3;
    public float mountain;
    public float sky1;
    public float sky2;
    public float sky3;
    public float sky4;
    public Transform CM;

    private float cm_x_end;
    private float cm_x_start;
    private Rigidbody2D player_rig;
    private GameObject[] player;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {

        timer = 0f;

    }
    void BgControl()
    {
        
        
    }
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= 0.1f)
        {
            cm_x_start = CM.position.x;
            timer = 0f;
        }
        

        //Debug.Log(GameObject.FindGameObjectWithTag("Player").name);
        player = GameObject.FindGameObjectsWithTag("Player");

        
        foreach (GameObject tmp in player)
        {
            player_rig = tmp.GetComponent<Rigidbody2D>();
            if (player_rig.velocity.x != 0)
            {
                cm_x_end = CM.position.x;
                transform.GetChild(0).transform.Translate((cm_x_end - cm_x_start) * (forest1 / 100f), 0, 0);
                transform.GetChild(1).transform.Translate((cm_x_end - cm_x_start) * (forest2 / 100f), 0, 0);
                transform.GetChild(2).transform.Translate((cm_x_end - cm_x_start) * (forest3 / 100f), 0, 0);
                transform.GetChild(3).transform.Translate((cm_x_end - cm_x_start) * (mountain / 100f), 0, 0);
                transform.GetChild(4).transform.Translate((cm_x_end - cm_x_start) * (sky1 / 100f), 0, 0);
                transform.GetChild(5).transform.Translate((cm_x_end - cm_x_start) * (sky2 / 100f), 0, 0);
                transform.GetChild(6).transform.Translate((cm_x_end - cm_x_start) * (sky3 / 100f), 0, 0);
                transform.GetChild(7).transform.Translate((cm_x_end - cm_x_start) * (sky4 / 100f), 0, 0);

            }
        }

    }
}
