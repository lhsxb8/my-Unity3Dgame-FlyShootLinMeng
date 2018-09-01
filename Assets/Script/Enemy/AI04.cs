using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI04 : MonoBehaviour
{


    public GameObject Gamer;
    public float EnemyBlood = 3;
    public float EnemyTotalBlood;
    public Transform BloodLocation;

    public Transform AppearLocation;
    public Transform MoveToLocation;
    public float MoveRate = 5f;
    public float speed = 1f;
    private float NextMove = 5f;

    public float FireRate = 3f;
    public float NextFire = 6f;
    private int judgeFire;
    public float FireSpeed = 20f;

    public GameObject FirePrefab;
    public Transform FirePoint;

    private int judge;
    private GameObject BloodPanel;

    private bool StartOntrigger = false;

    public GameObject Gold_Prefab;  //金币
    private Transform Gold_location; //金币位置
    public GameObject Blood_Prefab; //血包prefab
    private Transform Blood_location;//血包位置

    //挂载到ENEMY
    void Start()
    {
        EnemyTotalBlood = EnemyBlood;
        StartOntrigger = true;
        BloodPanel = Instantiate(Resources.Load("EnemyBlood")) as GameObject;
        transform.Translate(AppearLocation.position);
        BloodPanel.transform.localScale = new Vector3(0.015f, 0.015f, 0.015f);

        //iTween.MoveTo(gameObject, MoveToLocation.position, 3f);

    }


    void Update()
    {


        ////移动-------------------------------
        if(Time.time>NextMove)
        {
            gameObject.transform.Translate(new Vector3(-2f * speed * Time.deltaTime, 0, 0));
        }
        ////射击---------------------------------------
        if (Time.time > NextFire)
        {
            Invoke("Creat001", 0f);
            Invoke("Creat001", 0.5f);

            NextFire = Time.time + FireRate;
        }

        ////血条跟随
        BloodPanel.transform.position = BloodLocation.position;
        ////血条更新
        BloodPanel.GetComponent<UISlider>().value = EnemyBlood / EnemyTotalBlood;
        if (EnemyBlood <= 0) Destroy(BloodPanel);
        ////判断死亡------------------------------------
        if (EnemyBlood <= 0)
        {
            Gold_location = gameObject.transform;
            Destroy(gameObject);
            GameObject ClonGlod;
            ClonGlod = Instantiate(Gold_Prefab, Gold_location.position, Quaternion.identity);

            //判断是否掉落血包
            int Judge_BloodCreat = Random.Range(0,10);
            if(Judge_BloodCreat == 5)
            {
                GameObject Blood = Instantiate(Blood_Prefab, Blood_location.position, Quaternion.identity);
            }

        }


    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Gamer_Fire" && StartOntrigger)
        {
            EnemyBlood--;
            //Debug.Log("OnTriggerEnter:" + collision.transform.name);
            Destroy(collision.gameObject);
            //Debug.Log("EnemyBlood:" + EnemyBlood);
        }
        if (collision.gameObject.tag == "GamerSkill01" && StartOntrigger)
        {
            EnemyBlood -= 2;

        }
    }

    void Creat001()
    {
        Vector3 FireDirection = -gameObject.transform.position + Gamer.transform.position;
        float Val = Mathf.Sqrt(FireDirection.x * FireDirection.x + FireDirection.y * FireDirection.y + FireDirection.z * FireDirection.z);
        FireDirection = FireDirection * 10 / Val;
        GameObject temp = Instantiate(FirePrefab, FirePoint.position, Quaternion.identity);
        temp.GetComponent<Rigidbody2D>().AddForce(FireDirection * FireSpeed);
        Destroy(temp, 10f);
    }
}
