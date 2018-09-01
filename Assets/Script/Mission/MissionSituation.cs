using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionSituation : MonoBehaviour {


    public int Missions = 1;

    public int Situation_Gamer_JiGuang = 0; 
    public int Situation_Gamer_Blood = 6;
    public int Situation_Gamer_Fire = 1;

    public GameObject Gamer;

	void Start () {
        Invoke("StartMission1", 8f);
        
	}
	

	void Update () {
		
	}

    public void StartMission1()
    {
        gameObject.GetComponent<Mission_1>().enabled = true;
    }
}
