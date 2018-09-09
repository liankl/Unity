using System.Collections;

using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class kaiqi : MonoBehaviour {
	public Image acquire;
	Animation m_anim;
	public float myTime;
	public float showTime=1.5f;
	private bool a;

	void start()
	{
		a = false;
	}
//支持中文;	
// Use this for initialization

	void OnMouseDown()
	{
		  
		m_anim = GetComponent<Animation> ();
		m_anim.Play ("Take 001");
		a = true;

	}

	void Update ()
	{
		
		if (a) {
			myTime += Time.deltaTime;
			if (myTime > showTime) {
				acquire.gameObject.SetActive (true);
			}
		}
	}

	}
