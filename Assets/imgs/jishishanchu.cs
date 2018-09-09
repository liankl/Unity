using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jishishanchu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (this.gameObject.CompareTag("tu1"))
        {
            Destroy(this.gameObject, 1);//达到时间自动消除。
        }
        if (this.gameObject.CompareTag("tu2"))
        {
            Destroy(this.gameObject, 2);//达到时间自动消除。
        }
        if (this.gameObject.CompareTag("tu3"))
        {
            Destroy(this.gameObject, 3);//达到时间自动消除。
        }
        if (this.gameObject.CompareTag("tu4"))
        {
            Destroy(this.gameObject, 4);//达到时间自动消除。
        }
        if (this.gameObject.CompareTag("tu5"))
        {
            Destroy(this.gameObject, 5);//达到时间自动消除。
        }
        if (this.gameObject.CompareTag("tu6"))
        {
            Destroy(this.gameObject, 6);//达到时间自动消除。
        }
    }
   
}
