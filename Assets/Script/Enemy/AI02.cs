using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI02 : MonoBehaviour {

    public GameObject Gamer;
    public float EnemyBlood = 5;
    public float EnemyTotalBlood;
    public Transform BloodLocation;

    public Transform AppearLocation;
    public Transform MoveToLocation;
    public float MoveRate = 3f;
    public float speed = 1.5f;
    private float NextMove = 5f;

    public float FireRate = 5f;
    public float NextFire = 6f;
    private int judgeFire;
    public float FireSpeed = 500f;

    public GameObject FirePrefab;
    public Transform FirePoint;

    private int judge;
    private GameObject BloodPanel;

    private bool StartOntrigger = false;

    //金币
    public GameObject Gold_Prefab;
    private Transform Gold_Location;

    void Start () {
        EnemyTotalBlood = EnemyBlood;
        StartOntrigger = true;
        BloodPanel = Instantiate(Resources.Load("EnemyBlood")) as GameObject;
        transform.Translate(AppearLocation.position);
        BloodPanel.transform.localScale = new Vector3(0.012f, 0.012f, 0.012f);
    }
	

	void Update () {
        ////移动-------------------------------
        if (Time.time > NextMove)
        {
            NextMove = Time.time + MoveRate;
            judge = Random.Range(0, 4);
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

        ////射击---------------------------------------
        if (Time.time > NextFire)
        {
            Invoke("Creat002", 0f);
            Invoke("Creat002", 0.5f);
            Invoke("Creat002", 1f);

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
            Gold_Location = gameObject.transform;
            Destroy(gameObject);
            int RandomGlodNumber = Random.Range(2, 4);
            for (int i = 0; i < RandomGlodNumber; i++)
            {
                GameObject ClonGlod;
                ClonGlod = Instantiate(Gold_Prefab, Gold_Location.position , Quaternion.identity);
                ClonGlod.GetComponent<Rigidbody2D>().AddForce(new Vector3(Random.Range(-5, 5), Random.Range(-50, 50), 0));
            }
        }
       
    }

    void Creat002()
    {
        for (int i = -5; i < 6; i++)
        {
            GameObject temp = Instantiate(FirePrefab, FirePoint.position, Quaternion.identity);
            temp.GetComponent<Rigidbody2D>().AddForce(new Vector2(FireSpeed*-1, 100 * i));
            Destroy(temp, 10f);
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Gamer_Fire" && StartOntrigger)
        {
            EnemyBlood--;
            //Debug.Log("OnTriggerEnter:" + collision.transform.name);
            Destroy(collision.gameObject);
            Debug.Log("EnemyBlood:" + EnemyBlood);
        }
        if (collision.gameObject.tag == "GamerSkill01" && StartOntrigger)
        {
            EnemyBlood -= 2;

        }
    }
}
