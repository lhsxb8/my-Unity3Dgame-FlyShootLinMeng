using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlodNumber : MonoBehaviour {

    public int Number;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        gameObject.GetComponent<UILabel>().text = "" + Number;
	}
}
