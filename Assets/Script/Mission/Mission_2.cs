using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission_2 : MonoBehaviour {


    /// <summary>
    /// 挂载至BackGround
    /// </summary>
    /// 
    public GameObject ShopPrefab;       //shop的背景
    public GameObject Gamer;
    public GameObject ShopUI_plane;     //Shop的NGUI按钮
    public GameObject Mission_Label;    //第二关的Label

    private GameObject Enemy001;
    public GameObject EnemyPrefab_01;      //一号敌人                          
    public GameObject EnemyPrefab_02;      //二号敌人
    public GameObject EnemyPrefab_03;      //三号敌人

    public Transform MoveToUp;          //坐标点声明
    public Transform MoveToDown;
    public Transform MoveToRMiddle;
    public Transform MoveToRUp;
    public Transform MoveToRDown;
    public Transform AppearRMiddle;
    public Transform AppearRDown;
    public Transform AppearRUp;


    private GameObject[] NowEnemy = new GameObject[20];
    private bool[] Judge = new bool[20];

    private float temptime;
    private int sitiuation = 0;         //第几波敌人
    private bool temptemp = true;

    private bool Judge_temp_01 = true; //已用
    private bool Judge_temp_02 = true; //
    private bool Judge_temp_03 = true; //
    private bool Judge_temp_04 = true; //
    private bool Judge_temp_05 = true;
    private bool Judge_temp_06 = true;
    private bool Judge_temp_07 = true;



    private bool JudgeCreatShop = false; //判断商店出现过没



    void Start () {
		
        //将所有的Enemy的Bool赋值为True
        for(int i = 0; i<20;i++)
        {
            Judge[i] = true;
        }

        //先产生第一个ENEMY
        //0号
        CreatEnemy(EnemyPrefab_01, 0 , AppearRUp);
        NowEnemy[0].GetComponent<AI05>().enabled = false;
        NowEnemy[0].transform.localScale = new Vector3(0.15f, 0.15f, 1);

    }
	

	void Update () {
        //更新Judge
		for(int i = 0;i<20;i++)
        {
            if(NowEnemy[i] !=null)
            {
                Judge[i] = true;
            }
            if(NowEnemy[i] == null)
            {
                Judge[i] = false;
            }
        }

        if(sitiuation == 0)
        {
            if(temptemp)
            {
                temptemp = false;
                temptime = Time.time;
            }
            NowEnemy[0].SetActive(true);
            iTween.MoveTo(NowEnemy[0], MoveToRUp.position, 3f);

            if(Time.time>temptime+2f)
            {
                NowEnemy[0].GetComponent<AI05>().enabled = true;
                sitiuation = 1;
                temptemp = true;
            }
        }
        //出现第一只怪后3秒第二只，无须第一只死亡
        if(sitiuation == 1)
        {
            if(temptemp)
            {
                temptemp = false;
                temptime = Time.time;
            }
            if (Time.time > temptime + 5f && Judge_temp_01)
            {
                CreatEnemy(EnemyPrefab_02, 1, AppearRDown);

                NowEnemy[1].GetComponent<AI04>().enabled = false;
                NowEnemy[1].SetActive(true);
                iTween.MoveTo(NowEnemy[1], MoveToRDown.position, 3f);
                Judge_temp_01 = false;
            }

            if(Time.time > temptime + 6f)
            {
                NowEnemy[1].GetComponent<AI04>().enabled = true;
                sitiuation = 2;
                temptemp = true;
            }
        }
        if(sitiuation == 2 && !Judge[0])
        {
            if(temptemp)
            {
                temptemp = false;
                temptime = Time.time;
            }
            if (Judge_temp_02 && Time.time>temptime + 1.5f)
            {
                CreatEnemy(EnemyPrefab_01, 2, AppearRUp);
                NowEnemy[2].transform.localScale = new Vector3(0.15f, 0.15f, 1);
                NowEnemy[2].SetActive(true);
                iTween.MoveTo(NowEnemy[2], MoveToRUp.position, 3f);
                Judge_temp_02 = false;
            }
            if(Time.time>temptime +1.8f)
            {
                NowEnemy[2].GetComponent<AI05>().enabled = true;
                sitiuation = 3;
                temptemp = true;
            }
        }
        if (sitiuation == 3)
        {
            if (temptemp)
            {
                temptemp = false;
                temptime = Time.time;
            }
            if (Judge_temp_03)
            {
                CreatEnemy(EnemyPrefab_01, 3, AppearRDown);
                NowEnemy[3].transform.localScale = new Vector3(0.15f, 0.15f, 1);
                NowEnemy[3].SetActive(true);
                iTween.MoveTo(NowEnemy[3], MoveToRDown.position, 1.5f);
                Judge_temp_03 = false;
            }
            if (Time.time > temptime + 2f)
            {
                NowEnemy[2].GetComponent<AI05>().enabled = true;
                sitiuation = 4;
                temptemp = true;
            }
        }
        if (sitiuation == 4)
        {
            if (temptemp)
            {
                temptemp = false;
                temptime = Time.time;
            }
            if (Judge_temp_04 && Time.time > + 1.5f)
            {
                CreatEnemy(EnemyPrefab_01, 2, AppearRDown);
                NowEnemy[3].transform.localScale = new Vector3(0.15f, 0.15f, 1);
                NowEnemy[3].SetActive(true);
                iTween.MoveTo(NowEnemy[2], MoveToRUp.position, 3f);
                Judge_temp_04 = false;
            }
            if (Time.time > temptime + 1.8f)
            {
                NowEnemy[2].GetComponent<AI05>().enabled = true;
                sitiuation = 5;
                temptemp = true;
            }
        }
        

    }

    //产生ENEMY的函数
    void CreatEnemy(GameObject GOprefab,int i,Transform Appear)
    {
        NowEnemy[i] = Instantiate(GOprefab, Appear.position,Quaternion.identity) as GameObject;
        Judge[i] = true;
        NowEnemy[i].SetActive(false);
        NowEnemy[i].transform.localScale = new Vector3(0.25f, 0.25f, 1);
    }

}
