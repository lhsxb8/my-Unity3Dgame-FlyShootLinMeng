using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mission_1 : MonoBehaviour {

    /// <summary>
    /// 挂载至BackGround
    /// </summary>
    /// 
    public GameObject ShopPrefab;       //shop的背景
    public GameObject Gamer;
    public GameObject ShopUI_plane;     //Shop的NGUI按钮

    private GameObject Enemy001;
    public GameObject EnemyPrefab;      //一号敌人                          
    public GameObject EnemyTwoPrefab;   //二号敌人
    public GameObject EnemyPrefab_03; //三号敌人
    
    public Transform MoveToUp;          //坐标点声明
    public Transform MoveToDown;
    public Transform MoveToMiddle;
    public Transform Appear;


    private GameObject[] NowEnemy = new GameObject[10];      
    private bool []Judge = new bool[10];

    private float temptime;
    private int sitiuation = 0;         //第几波敌人
    private bool temptemp = true;

    private bool JudgeCreat = true;    //BOSS的僚机产生
    private bool JudgeCreatShop = false; //判断商店出现过没
    void Start () {
        
        //
        //初始化金币
        GameObject.Find("GlodText").GetComponent<GlodNumber>().Number = 0;

        for (int i = 0; i < 6; i++)
        {
            NowEnemy[i] = Instantiate(EnemyPrefab, Appear)as GameObject;
            Judge[i] = true;
            NowEnemy[i].SetActive(false);
            NowEnemy[i].transform.localScale = new Vector3(0.2f, 0.1f, 1f);
            NowEnemy[i].GetComponent<AI01>().enabled = false;
        }
        //生成2号敌人
        CreatEnemy(EnemyTwoPrefab, 6);
        NowEnemy[6].GetComponent<AI02>().enabled = false;

        //生成3号敌人
        CreatEnemy(EnemyPrefab_03, 7);
        NowEnemy[7].GetComponent<AI03>().enabled = false;
    }

    void Update()
    {
        for(int i = 0;i<10;i++)
        {
            if (NowEnemy[i] == null)
                Judge[i] = false;
        }

        if (sitiuation == 0)
        {
            if(temptemp)
            {
                temptime = Time.time;
                temptemp = false;
            }
            
            NowEnemy[0].SetActive(true);
            iTween.MoveTo(NowEnemy[0], MoveToMiddle.position, 3f);

            if (Time.time > temptime + 1f)
            {
                NowEnemy[0].GetComponent<AI01>().enabled = true;
                sitiuation = 1;
                temptemp = true;
            }
        }
        if (sitiuation == 1 && !Judge[0])
        {
            if (temptemp)
            {
                temptime = Time.time;
                temptemp = false;
            }

            NowEnemy[1].SetActive(true);
            NowEnemy[2].SetActive(true);

            iTween.MoveTo(NowEnemy[1], MoveToDown.position, 3f);
            iTween.MoveTo(NowEnemy[2], MoveToUp.position, 3.5f);
            if (Time.time > temptime + 1f)
            {
                NowEnemy[1].GetComponent<AI01>().enabled = true;
                NowEnemy[2].GetComponent<AI01>().enabled = true;
                temptemp = true;
                sitiuation = 2;
            }

        }
        if (sitiuation == 2 && !Judge[1] && !Judge[2])
        {
            if (temptemp)
            {
                temptime = Time.time;
                temptemp = false;
            }

            NowEnemy[3].SetActive(true);
            NowEnemy[4].SetActive(true);
            NowEnemy[5].SetActive(true);

            iTween.MoveTo(NowEnemy[3], MoveToDown.position, 3f);
            iTween.MoveTo(NowEnemy[4], MoveToUp.position, 3.5f);
            iTween.MoveTo(NowEnemy[5], MoveToMiddle.position, 3.8f);

            if (Time.time > temptime + 1f)
            {
                NowEnemy[3].GetComponent<AI01>().enabled = true;
                NowEnemy[4].GetComponent<AI01>().enabled = true;
                NowEnemy[5].GetComponent<AI01>().enabled = true;
                sitiuation = 3;
                temptemp = true;
            }
        }
        if (sitiuation == 3 && !Judge[3] && !Judge[4] && !Judge[5])
        {

            if (temptemp)
            {
                temptime = Time.time;
                temptemp = false;
            }

            NowEnemy[6].SetActive(true);

            iTween.MoveTo(NowEnemy[6], MoveToMiddle.position, 3.5f);

            if (Time.time > temptime + 1f)
            {
                NowEnemy[6].GetComponent<AI02>().enabled = true;
                sitiuation = 4;
                temptemp = true;
            }
        }
        if (sitiuation == 4 && !Judge[6])
        {
            if (temptemp)
            {
                temptime = Time.time;
                temptemp = false;
            }

            if (Time.time > temptime + 1f) NowEnemy[7].SetActive(true);
            iTween.MoveTo(NowEnemy[7], MoveToMiddle.position, 2f);

            if (Time.time > temptime + 3f)
            {
                NowEnemy[7].GetComponent<AI03>().enabled = true;
                sitiuation = 5;
                temptemp = true;
            }
        }
        
        if(sitiuation == 5 && JudgeCreat)
        {
            
            if(Time.time>temptime+7f)
            {
                CreatEnemy(EnemyPrefab, 8);
                NowEnemy[8].transform.localScale = new Vector3(0.2f, 0.1f, 1f);
                NowEnemy[8].SetActive(true);
                iTween.MoveTo(NowEnemy[8], MoveToUp.position, 2f);
                NowEnemy[8].GetComponent<AI01>().enabled = true;

                JudgeCreat = false;
            }

        }

        if(!Judge[7] && !JudgeCreatShop)
        {
            Invoke("ChangeToShop", 5f);
            JudgeCreatShop = true;
            
        }
    }
    void CreatEnemy(GameObject GOprefab,int i)
    {
        NowEnemy[i] = Instantiate(GOprefab, Appear) as GameObject;
        Judge[i] = true;
        NowEnemy[i].SetActive(false);
        NowEnemy[i].transform.localScale = new Vector3(0.2f, 0.2f, 1f);
        
    }

    
    void ChangeToShop()
    {

        ShopPrefab.SetActive(true);                         //Shop背景出现
        ShopPrefab.GetComponent<TweenAlpha>().enabled = true;

        ShopUI_plane.SetActive(true);
        ShopUI_plane.GetComponent<TweenAlpha>().enabled = true;
    }
    void Disappear_Gamer()
    {
        Gamer.GetComponent<TweenAlpha>().enabled = true;
    }
}
