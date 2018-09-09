using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class liliang : MonoBehaviour {
    public Text countText;
    private int shuliang;
    private GameObject hero;
    private jianke zengqiang;
    // Use this for initialization
    void Start () {
        hero = GameObject.FindGameObjectWithTag("Player");
        zengqiang = hero.GetComponent<jianke>();
    }
    public void addScore(int num)
    {
        shuliang += num;
        updateCountText();
    }
    void updateCountText()
    {
        countText.text = "" + shuliang;
    }
    // Update is called once per frame
    public void jiaqiang()
    {
        if (shuliang > 0)
        {
            zengqiang.Gong();
            shuliang -= 1;
            updateCountText();
        }	
	}
}
