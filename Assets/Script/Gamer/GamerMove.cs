using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamerMove : MonoBehaviour {

    public GameObject Situation;//状态量从这里调入
    private int Situation_JiGuang; //激光状态量
    private int Situation_Fire;    //火力状态量

    public float speed;
    public Transform FirePoint;

    public GameObject Fire01;
    public float fireRate01 = 0.3f;
    private float nextFier01 = 0.3f;

    public GameObject SkillPrefab;
    public float SkillRate01 = 10f;
    private float nextSkill = 5f;


    public float GamerBlood;
    public float GamerTotalBlood;

    public GameObject HeroSlider;
	// 挂载至Gamer
	void Start () {
        GamerTotalBlood = Situation.GetComponent<MissionSituation>().Situation_Gamer_Blood;
        GamerBlood = GamerTotalBlood;
	}
	
	
	void Update () {

        //更新状态量
        Situation_Fire = GameObject.Find("BackGround").GetComponent<MissionSituation>().Situation_Gamer_Fire;
        Situation_JiGuang = GameObject.Find("BackGround").GetComponent<MissionSituation>().Situation_Gamer_JiGuang;
        GamerTotalBlood = GameObject.Find("BackGround").GetComponent<MissionSituation>().Situation_Gamer_Blood;

        //玩家移动----------------------------
        if(Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.up * Time.deltaTime * speed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.down * Time.deltaTime * speed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * Time.deltaTime * speed);
        }

        //发射子弹01
        if(Input.GetKey(KeyCode.J) && Time.time > nextFier01)
        {
            //Debug.Log("Shoule Instantiate");

            Vector2 Force01 = new Vector2(500f, 0f);
            nextFier01 = fireRate01 + Time.time;

            GameObject temp01 = (GameObject)Instantiate(Fire01, FirePoint.position, Quaternion.identity);//初始子弹
            temp01.GetComponent<Rigidbody2D>().AddForce(Force01);
            Destroy(temp01, 5f);

            //追加子弹的生成
            for (int i = 1;i<=(Situation_Fire-1)*2;i++)    //上侧子弹
            {
                if(i%2==0) //i是偶数，就在上面实例化子弹
                {
                    GameObject temp02 = (GameObject)Instantiate(Fire01, FirePoint.position, Quaternion.identity);
                    Vector2 TempForce = new Vector2(500f, 1 * (i-1) * 80);
                    temp02.GetComponent<Rigidbody2D>().AddForce(TempForce);
                }

                if(i%2==1) //i是奇数,就在下面实例化子弹
                {
                    GameObject temp03 = (GameObject)Instantiate(Fire01, FirePoint.position, Quaternion.identity);
                    Vector2 TempForce = new Vector2(500f, -1 * i * 80);
                    temp03.GetComponent<Rigidbody2D>().AddForce(TempForce);
                }
            }

            //实例化激光

        }

        //K技能
        if(Input.GetKeyDown(KeyCode.K) && Time.time > nextSkill)
        {
            Vector2 Force01 = new Vector2(300f, 0f);
            nextSkill = SkillRate01 + Time.time;

            GameObject temp01 = (GameObject)Instantiate(SkillPrefab, FirePoint.position, Quaternion.identity);
            temp01.GetComponent<Rigidbody2D>().AddForce(Force01);
            Destroy(temp01, 10f);
        }

        //血量UI显示------------------------------------

        HeroSlider.GetComponent<UISlider>().value = GamerBlood / GamerTotalBlood;

        //Debug.Log("GamerBlood" + GamerBlood + "\n" + "GamerTotalBlood" + GamerTotalBlood);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        //碰到敌人子弹
        if (collision.gameObject.tag == "Enemy_Fire")
        {
            GamerBlood--;
            //Debug.Log("OnTriggerEnter:" + collision.transform.name);
            Destroy(collision.gameObject);
        }

        //碰到金币
        if (collision.gameObject.tag == "Glod")
        {

            GameObject.Find("GlodText").GetComponent<GlodNumber>().Number += 10 * GameObject.Find("BackGround").GetComponent<MissionSituation>().Missions;
            //加的金币等于关数乘10
            Destroy(collision.gameObject);
        }

        //碰到血包
        if(collision.gameObject.tag == "AddBlood")
        {
            GamerTotalBlood++;
            GamerBlood = GamerTotalBlood;
            Destroy(collision.gameObject);
        }
    }

    
}
