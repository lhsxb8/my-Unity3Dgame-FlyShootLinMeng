using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamerSkill : MonoBehaviour {

    /// <summary>
    /// 挂载至玩家技能
    /// </summary>
	void Start () {
		
	}
	

	void Update () {
		
	}
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy_Fire")
            Destroy(collision.gameObject);
    }
}
