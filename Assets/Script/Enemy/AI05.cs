using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI05 : MonoBehaviour
{


    public GameObject Gamer;
    public float EnemyBlood = 50;
    public float EnemyTotalBlood;
    public Transform BloodLocation;

    public Transform AppearLocation;
    public Transform MoveToLocation;
    public float MoveRate = 5f;
    public float speed = 1f;
    private float NextMove = 5f;

    ///技能1的参数（随机发射子弹）
    ///12发，每0.2秒发一枚
    public float FireRate_01 = 12f;
    public float NextFire_01 = 6f;
    private int judgeFire;
    public float FireSpeed_01 = 50f;
    public GameObject FirePrefab_01; //1号技能实例

    ///技能2的参数（随机发射子弹）
    ///12发，每0.2秒发一枚
    public float FireRate_02 = 12f;
    public float NextFire_02 = 8f;
    public float FireSpeed_02 = 500f;
    public GameObject FirePrefab_02; //2号技能实例

    public Transform FirePoint;

    private int judge;
    private GameObject BloodPanel;

    private bool StartOntrigger = false;

    public GameObject Gold_Prefab;  //金币
    private Transform Gold_location; //金币位置

    public GameObject BloodBag;//血包

    //挂载到ENEMY
    void Start()
    {
        EnemyTotalBlood = EnemyBlood;
        StartOntrigger = true;
        BloodPanel = Instantiate(Resources.Load("EnemyBlood")) as GameObject;
        transform.Translate(AppearLocation.position);
        BloodPanel.transform.localScale = new Vector3(0.025f, 0.025f, 0.012f);

        //iTween.MoveTo(gameObject, MoveToLocation.position, 3f);

    }

    // Update is called once per frame
    void Update()
    {


        ////移动-------------------------------
        if (Time.time > NextMove)
        {
            NextMove = Time.time + MoveRate;
            judge = Random.Range(0, 5);
        }
        if (judge == 1) //向上移动
        {
            transform.Translate(Vector3.up * Time.deltaTime * speed);
        }
        else if (judge == 2) //向下移动
        {
            transform.Translate(Vector3.down * Time.deltaTime * speed);
        }
        else if (judge == 3) //向左移动
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }
        else if (judge == 4 && transform.position.x < 7.5f) //向右移动
        {
            transform.Translate(Vector3.right * Time.deltaTime * speed);
        }
        ////01射击---------------------------------------
        if (Time.time > NextFire_01)
        {
            for (int i = 0; i < 6; i++)
            {
                Invoke("Creat001", 0.2f * i);
            }
            NextFire_01 = Time.time + FireRate_01;
        }
        ////02射击-----------------------------------------
        if (Time.time > NextFire_02)
        {
            for (int i = 0; i < 5; i++)
            {
                Invoke("Creat002", 0.8f * i);
            }
            NextFire_02 = Time.time + FireRate_02;
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
            int RandomGlodNumber = Random.Range(3, 7);
            for (int i = 0; i < RandomGlodNumber; i++)
            {
                GameObject ClonGlod;
                ClonGlod = Instantiate(Gold_Prefab, Gold_location.position, Quaternion.identity);
                ClonGlod.GetComponent<Rigidbody2D>().AddForce(new Vector3(Random.Range(-5, 5), Random.Range(-50, 50), 0));
            }

            GameObject TempBloodBag = Instantiate(BloodBag, Gold_location.position, Quaternion.identity);
            TempBloodBag.GetComponent<Rigidbody2D>().AddForce(new Vector3(Random.Range(-5, 5), Random.Range(-50, 50), 0));
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
        Vector3 FireDirection = new Vector3(Random.Range(-100, -200), Random.Range(-20, 20), 0);
        GameObject temp = Instantiate(FirePrefab_01, FirePoint.position, Quaternion.identity);
        temp.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
        temp.GetComponent<Rigidbody2D>().AddForce(FireDirection);
        Destroy(temp.gameObject, 12f);  //12秒后消失
    }
    void Creat002()
    {
        for (int i = -2; i < 3; i++)
        {
            GameObject temp = Instantiate(FirePrefab_02, FirePoint.position, Quaternion.identity);
            temp.transform.localScale = new Vector3(0.01f, 0.01f, 1f);
            temp.GetComponent<Rigidbody2D>().AddForce(new Vector2(FireSpeed_02 * -1, 100 * i));
            Destroy(temp, 8f);
        }
    }
}
