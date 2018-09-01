using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour {

    //public GameObject Gamer = GameObject.Find("Gamer");
    //public GameObject Buttom_Blood;
    //public GameObject Buttom_Fire;
    //public GameObject JiGuang;

    public GameObject Shop_UI_Plane; //shop的UI界面
    public GameObject ShopPrefab;    //shop的背景

    private int MissionNumber;  //关卡数
    private int GoldNumber;    //金币数

    public int Cost_Fire;//火力话费
    public int Cost_JiGuang;//激光花费
    public int Cost_Blood;//血量花费


	void Start () {
        //载入关卡数，金币数
        MissionNumber = GameObject.Find("BackGround").GetComponent<MissionSituation>().Missions++;
        GoldNumber = GameObject.Find("GlodText").GetComponent<GlodNumber>().Number;

        //初始化关卡数，金币数
        Cost_Blood = 100;
        Cost_Fire = 120;
        Cost_JiGuang = 180;
	}
	

	void Update () {
		
	}

    public void Click_Blood()
    {
        if (GoldNumber >= Cost_Blood) //加血
        {
            GameObject.Find("BackGround").GetComponent<MissionSituation>().Situation_Gamer_Blood += 2;
            GoldNumber -= 120;
            NewGoldNumber();

            Cost_Blood += 80;
        }
    }
    public void Click_JiGuang()
    {
        if (GoldNumber >= Cost_JiGuang) //激光
        {
            GameObject.Find("BackGround").GetComponent<MissionSituation>().Situation_Gamer_JiGuang++;
            GoldNumber -= 200;
            NewGoldNumber();

            Cost_JiGuang += 100;
        }
    }
    public void Click_Fire()
    {
        if (GoldNumber >= Cost_Fire) //火力
        {
            GameObject.Find("BackGround").GetComponent<MissionSituation>().Situation_Gamer_Fire++;
            GoldNumber -= 150;
            NewGoldNumber();

            Cost_Fire += 100;
        }
    }
    public void Click_NextMission()
    {
        Shop_UI_Plane.SetActive(false);
        ShopPrefab.SetActive(false);
        //Debug.Log("Shoule SetActive false");

        //关闭Mission_1，同时Mission数量增加
        GameObject.Find("BackGround").GetComponent<MissionSituation>().Missions++;
        GameObject.Find("BackGround").GetComponent<Mission_1>().enabled = false;

        int NowMission = GameObject.Find("BackGround").GetComponent<MissionSituation>().Missions;
        if(NowMission == 3)
        {
            GameObject.Find("BackGround").GetComponent<Mission_2>().enabled = true;
        }

    }

    void NewGoldNumber() //更新金币的函数
    {
        GameObject.Find("GlodText").GetComponent<GlodNumber>().Number = GoldNumber;
    }

    
}
