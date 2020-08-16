
using UnityEngine;
using UnityEngine.UI;

public class SelectLight : MonoBehaviour
{
    int i;
    int L1, L2, L3, L4, L5, L6, L7, L8, L9, L10, L11;//4 4 4 3 4 4 1 1 6 2 1
    public string []h;
    public string []d;
    GameObject[] UIPlayer;
    GameObject level_name;
    bool flag1, flag2, flag3, flag4, flag5, flag6, flag7, flag8, flag9, flag10, flag11, flag_UIplayer;
    
    void Start()
    {
        GameObject.FindGameObjectWithTag("description").GetComponent<Text>().text = d[0];
        GameObject.FindGameObjectWithTag("headline").GetComponent<Text>().text = h[0];
        level_name = GameObject.Find("LevelName");
        UIPlayer = GameObject.FindGameObjectsWithTag("UIPlayer");
        foreach(GameObject tmp in UIPlayer)
        {
            tmp.SetActive(false);
        }
        flag1 = true;
        flag2 = true;
        flag3 = true;
        flag4 = true;
        flag5 = true;
        flag6 = true;
        flag9 = true;
        flag10 = true;
        flag_UIplayer = true;
        i = 1;
        L1 = 0;
        L2 = 0;
        L3 = 0;
        L4 = 0;
        L5 = 0;
        L6 = 0;
        L7 = 0;
        L8 = 0;
        L9 = 0;
        L10 = 0;
        L11 = 0;

        

    }

    void Set_playerflase()
    {
        foreach (GameObject tmp in UIPlayer)
        {
            tmp.SetActive(false);
        }
    }
    void switch_zhangjie(int i)
    {
        switch (i)
        {
            case 1:
                level_name.GetComponent<Text>().text = "1 - 1";
                L1 = 0;
                break;
            case 2:
                level_name.GetComponent<Text>().text = "2 - 1";
                L2 = 0;
                break;
            case 3:
                level_name.GetComponent<Text>().text = "3 - 1";
                L3 = 0;
                break;
            case 4:
                level_name.GetComponent<Text>().text = "4 - 1";
                L4 = 0;
                break;
            case 5:
                level_name.GetComponent<Text>().text = "5 - 1";
                L5 = 0;
                break;
            case 6:
                level_name.GetComponent<Text>().text = "6 - 1";
                L6 = 0;
                break;
            case 7:
                level_name.GetComponent<Text>().text = "7 - 1";
                break;
            case 8:
                level_name.GetComponent<Text>().text = "8 - 1";
                break;
            case 9:
                level_name.GetComponent<Text>().text = "9 - 1";
                L9 = 0;
                break;
            case 10:
                level_name.GetComponent<Text>().text = "10 - 1";
                L10 = 0;
                break;
            case 11:
                level_name.GetComponent<Text>().text = "11 - 1";
                break;
        }

        flag1 = true;
        flag2 = true;
        flag3 = true;
        flag4 = true;
        flag5 = true;
        flag6 = true;
        flag9 = true;
        flag10 = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            AudioPoolMgr.AudioPlay("skill",AudioClipName.背景音乐14);
            flag_UIplayer = true;
            if (i != 11)
            {
                i++;
                GameObject.FindGameObjectWithTag("description").GetComponent<Text>().text = d[i-1];
                GameObject.FindGameObjectWithTag("headline").GetComponent<Text>().text = h[i-1];
                this.transform.GetChild(i - 1).gameObject.SetActive(true);
                this.transform.GetChild(0).position = this.transform.GetChild(i).position;
                this.transform.GetChild(i).gameObject.SetActive(false);
            }
            else
            {
                GameObject.FindGameObjectWithTag("description").GetComponent<Text>().text = d[0];
                GameObject.FindGameObjectWithTag("headline").GetComponent<Text>().text = h[0];
                this.transform.GetChild(0).position = this.transform.GetChild(1).position;
                this.transform.GetChild(i).gameObject.SetActive(true);
                this.transform.GetChild(1).gameObject.SetActive(false);
                i = 1;
            }

            switch_zhangjie(i);


        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            AudioPoolMgr.AudioPlay("skill", AudioClipName.背景音乐14);
            flag_UIplayer = true;
            if (i != 1)
            {
                i--;
                GameObject.FindGameObjectWithTag("description").GetComponent<Text>().text = d[i-1];
                GameObject.FindGameObjectWithTag("headline").GetComponent<Text>().text = h[i-1];
                this.transform.GetChild(i + 1).gameObject.SetActive(true);
                this.transform.GetChild(0).position = this.transform.GetChild(i).position;
                this.transform.GetChild(i).gameObject.SetActive(false);

                if(i == 0)
                {
                    i = 0;
                }
            }

            switch_zhangjie(i);

        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            AudioPoolMgr.AudioPlay("skill", AudioClipName.背景音乐14);

            if (i == 1)//第一章递增
            {
                L1++;

                if (!flag1)
                {
                    Debug.Log("L1:" + L1);
                    L1 = 0;
                    flag1 = true;
                }
                switch (L1)
                {

                    case 0:
                        level_name.GetComponent<Text>().text = "1 - 1";
                        break;
                    case 1:
                        level_name.GetComponent<Text>().text = "1 - 2";
                        break;
                    case 2:
                        level_name.GetComponent<Text>().text = "1 - 3";
                        break;
                    case 3:
                        level_name.GetComponent<Text>().text = "1 - 4";
                        flag1 = false;
                        break;
                }
            }

            if (i == 2)//第二章递增
            {
                
                L2++;
                if (!flag2)
                {
                    L2 = 0;
                    flag2 = true;
                }
                switch (L2)
                {

                    case 0:
                        level_name.GetComponent<Text>().text = "2 - 1";
                        break;
                    case 1:
                        level_name.GetComponent<Text>().text = "2 - 2";
                        break;
                    case 2:
                        level_name.GetComponent<Text>().text = "2 - 3";
                        break;
                    case 3:
                        level_name.GetComponent<Text>().text = "2 - 4";
                        flag2 = false;
                        break;
                }
            }

            if (i == 3)//第三章递增
            {
                
                L3++;
                if (!flag3)
                {

                    L3 = 0;
                    flag3 = true;
                }
                switch (L3)
                {

                    case 0:
                        level_name.GetComponent<Text>().text = "3 - 1";
                        break;
                    case 1:
                        level_name.GetComponent<Text>().text = "3 - 2";
                        break;
                    case 2:
                        level_name.GetComponent<Text>().text = "3 - 3";
                        break;
                    case 3:
                        level_name.GetComponent<Text>().text = "3 - 4";
                        flag3 = false;
                        break;
                }
            }

            if (i == 4)//第四章递增
            {

                L4++;
                if (!flag4)
                {

                    L4 = 0;
                    flag4 = true;
                }
                switch (L4)
                {

                    case 0:
                        level_name.GetComponent<Text>().text = "4 - 1";
                        break;
                    case 1:
                        level_name.GetComponent<Text>().text = "4 - 2";
                        break;
                    case 2:
                        level_name.GetComponent<Text>().text = "4 - 3";
                        flag4 = false;
                        break;
                }
            }

            if (i == 5)//第五章递增
            {

                L5++;
                if (!flag5)
                {

                    L5 = 0;
                    flag5 = true;
                }
                switch (L5)
                {

                    case 0:
                        level_name.GetComponent<Text>().text = "5 - 1";
                        break;
                    case 1:
                        level_name.GetComponent<Text>().text = "5 - 2";
                        break;
                    case 2:
                        level_name.GetComponent<Text>().text = "5 - 3";
                        break;
                    case 3:
                        level_name.GetComponent<Text>().text = "5 - 4";
                        flag5 = false;
                        break;
                }
            }

            if (i == 6)//第六章递增
            {

                L6++;
                if (!flag6)
                {

                    L6 = 0;
                    flag6 = true;
                }
                switch (L6)
                {

                    case 0:
                        level_name.GetComponent<Text>().text = "6 - 1";
                        break;
                    case 1:
                        level_name.GetComponent<Text>().text = "6 - 2";
                        break;
                    case 2:
                        level_name.GetComponent<Text>().text = "6 - 3";
                        break;
                    case 3:
                        level_name.GetComponent<Text>().text = "6 - 4";
                        flag6 = false;
                        break;
                }
            }

            if (i == 9)//第九章递增
            {

                L9++;
                if (!flag9)
                {

                    L9 = 0;
                    flag9 = true;
                }
                switch (L9)
                {

                    case 0:
                        level_name.GetComponent<Text>().text = "9 - 1";
                        break;
                    case 1:
                        level_name.GetComponent<Text>().text = "9 - 2";
                        break;
                    case 2:
                        level_name.GetComponent<Text>().text = "9 - 3";
                        break;
                    case 3:
                        level_name.GetComponent<Text>().text = "9 - 4";
                        break;
                    case 4:
                        level_name.GetComponent<Text>().text = "9 - 5";
                        break;
                    case 5:
                        level_name.GetComponent<Text>().text = "9 - 6";
                        flag9 = false;
                        break;
                }
            }

            if (i == 10)//第十章递增
            {

                L10++;
                if (!flag10)
                {

                    L10 = 0;
                    flag10 = true;
                }
                switch (L10)
                {

                    case 0:
                        level_name.GetComponent<Text>().text = "10 - 1";
                        break;
                    case 1:
                        level_name.GetComponent<Text>().text = "10 - 2";
                        flag10 = false;
                        break;
                }
            }

        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            AudioPoolMgr.AudioPlay("skill", AudioClipName.背景音乐14);

            if (i == 1)//第一章递减
            {
                L1--;
                if(L1 < 0)
                {
                    L1 = 0;
                    flag1 = true;
                }
                switch (L1)
                {

                    case 0:
                        level_name.GetComponent<Text>().text = "1 - 1";
                        break;
                    case 1:
                        level_name.GetComponent<Text>().text = "1 - 2";
                        break;
                    case 2:
                        level_name.GetComponent<Text>().text = "1 - 3";
                        break;
                    case 3:
                        level_name.GetComponent<Text>().text = "1 - 4";
                        break;
                }
            }

            if (i == 2)//第二章递减
            {
                L2--;
                if (L2 < 0)
                {
                    flag2 = true;
                    L2 = 0;
                }
                switch (L2)
                {

                    case 0:
                        level_name.GetComponent<Text>().text = "2 - 1";
                        break;
                    case 1:
                        level_name.GetComponent<Text>().text = "2 - 2";
                        break;
                    case 2:
                        level_name.GetComponent<Text>().text = "2 - 3";
                        break;
                    case 3:
                        level_name.GetComponent<Text>().text = "2 - 4";
                        break;
                }

            }

            if (i == 3)//第三章递减
            {
                L3--;
                if (L3 < 0)
                {
                    flag3 = true;
                    L3 = 0;
                }
                switch (L3)
                {

                    case 0:
                        level_name.GetComponent<Text>().text = "3 - 1";
                        break;
                    case 1:
                        level_name.GetComponent<Text>().text = "3 - 2";
                        break;
                    case 2:
                        level_name.GetComponent<Text>().text = "3 - 3";
                        break;
                    case 3:
                        level_name.GetComponent<Text>().text = "3 - 4";
                        break;
                }

            }
            if (i == 4)//第四章递减
            {
                L4--;
                if (L4 < 0)
                {
                    flag4 = true;
                    L4 = 0;
                }
                switch (L4)
                {

                    case 0:
                        level_name.GetComponent<Text>().text = "4 - 1";
                        break;
                    case 1:
                        level_name.GetComponent<Text>().text = "4 - 2";
                        break;
                    case 2:
                        level_name.GetComponent<Text>().text = "4 - 3";
                        break;
                }

            }

            if (i == 5)//第五章递减
            {
                L5--;
                if (L5 < 0)
                {
                    flag5 = true;
                    L5 = 0;
                }
                switch (L5)
                {

                    case 0:
                        level_name.GetComponent<Text>().text = "5 - 1";
                        break;
                    case 1:
                        level_name.GetComponent<Text>().text = "5 - 2";
                        break;
                    case 2:
                        level_name.GetComponent<Text>().text = "5 - 3";
                        break;
                    case 3:
                        level_name.GetComponent<Text>().text = "5 - 4";
                        break;
                }

            }

            if (i == 6)//第六章递减
            {
                L6--;
                if (L6 < 0)
                {
                    flag6 = true;
                    L6 = 0;
                }
                switch (L6)
                {

                    case 0:
                        level_name.GetComponent<Text>().text = "6 - 1";
                        break;
                    case 1:
                        level_name.GetComponent<Text>().text = "6 - 2";
                        break;
                    case 2:
                        level_name.GetComponent<Text>().text = "6 - 3";
                        break;
                    case 3:
                        level_name.GetComponent<Text>().text = "6 - 4";
                        break;
                }

            }

            if (i == 9)//第九章递减
            {
                L9--;
                if (L9 < 0)
                {
                    flag9 = true;
                    L9 = 0;
                }
                switch (L9)
                {

                    case 0:
                        level_name.GetComponent<Text>().text = "9 - 1";
                        break;
                    case 1:
                        level_name.GetComponent<Text>().text = "9 - 2";
                        break;
                    case 2:
                        level_name.GetComponent<Text>().text = "9 - 3";
                        break;
                    case 3:
                        level_name.GetComponent<Text>().text = "9 - 4";
                        break;
                    case 4:
                        level_name.GetComponent<Text>().text = "9 - 5";
                        break;
                    case 5:
                        level_name.GetComponent<Text>().text = "9 - 6";
                        break;
                }

            }

            if (i == 10)//第十章递减
            {
                L10--;
                if (L10 < 0)
                {
                    flag10 = true;
                    L10 = 0;
                }
                switch (L10)
                {

                    case 0:
                        level_name.GetComponent<Text>().text = "10 - 1";
                        break;
                    case 1:
                        level_name.GetComponent<Text>().text = "10 - 2";
                        break;
                }

            }
        }


        if((i == 1 || i == 2) && flag_UIplayer)
        {
            Set_playerflase();
            UIPlayer[2].transform.position = new Vector3(-3.0f * 3.0f, 1.2f * 2.4f, 0f);
            UIPlayer[2].SetActive(true);
            flag_UIplayer = false;
        }

        if ((i == 3 || i == 4) && flag_UIplayer)
        {
            Set_playerflase();
            UIPlayer[0].transform.position = new Vector3(-3.0f * 3.0f, 1.3f * 2.4f, 0f);
            UIPlayer[0].SetActive(true);
            flag_UIplayer = false;
        }

        if ((i == 5 || i == 6 || i == 7) && flag_UIplayer)
        {
            Set_playerflase();
            UIPlayer[0].transform.position = new Vector3(-10.85f, 1.3f * 2.4f, 0f);
            UIPlayer[3].transform.position = new Vector3(-7f, 1.2f * 2.4f, 0f);
            UIPlayer[0].SetActive(true);
            UIPlayer[3].SetActive(true);
            flag_UIplayer = false;
        }

        if(i == 8)
        {
            Set_playerflase();
            UIPlayer[3].transform.position = new Vector3(-3f * 3.0f, 1.2f * 2.4f, 0f);
            UIPlayer[3].SetActive(true);
            flag_UIplayer = false;
        }

        if (i == 10)
        {
            Set_playerflase();
            UIPlayer[4].transform.position = new Vector3(-3f * 3.0f, 1.2f * 2.4f, 0f);
            UIPlayer[4].SetActive(true);
            flag_UIplayer = false;
        }

        if ((i == 11 || i == 9) && flag_UIplayer)
        {
            Set_playerflase();
            UIPlayer[1].transform.position = new Vector3(-11f, 1.4f * 2.4f, 0f);
            UIPlayer[4].transform.position = new Vector3(-7.5f, 1.2f * 2.4f, 0f);
            UIPlayer[1].SetActive(true);
            UIPlayer[4].SetActive(true);
            flag_UIplayer = false;
        }
    }
}
