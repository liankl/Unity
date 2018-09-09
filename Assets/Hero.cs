using System;
using UnityEngine;

public class Hero : MonoBehaviour {
    private int b;
    
    private GameObject jian;
    int i = 0;
    int j = 1;
   

    private float myTime;
    private float nextTime=1F;
    private float startTime;//开始附加碰撞体
    private float endTime=1F;//结束去除碰撞体
    // Use this for initialization
    void Start () {
        
        jian = GameObject.FindGameObjectWithTag("jian");
        jian.GetComponent<BoxCollider>().enabled = false;
        


    }
    /*
    //储存第一次的时间
    public void SetDateTime()
    {
        //使用了PlayerPrefs    获取的时候一样

        PlayerPrefs.SetString("SetTime", DateTime.Now.ToShortTimeString());

    }
    // 判断第一次和现在的时间间隔
    public void GetDateTime()
    {

        DateTime nowTime = DateTime.Now;
        DateTime oldTime = DateTime.Parse(PlayerPrefs.GetString("SetTime"));

        TimeSpan timeSpan = nowTime - oldTime;

        //判断上次储存的时间
        if (timeSpan.Days < 1)
        {
            if (timeSpan.Hours <1)
            {
                if (timeSpan.Minutes < 60)
                {
                    Debug.Log("Minutes: " + timeSpan.Minutes);
                }
            }

        }
        
        


    }
    */


    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown("j"))
        {

            if (i == 0)
            {

                i = 1;

                jian.GetComponent<BoxCollider>().enabled = true;

                transform.GetComponent<Animation>().Play("attack");


            }
            if (j == 0)
            {

                j = 1;

                jian.GetComponent<BoxCollider>().enabled = true;

                transform.GetComponent<Animation>().Play("attack2");


            }

        }

        if (i == 1)
        {

            startTime = startTime + Time.deltaTime;

        }
        if (startTime > endTime)
        {

            jian.GetComponent<BoxCollider>().enabled = false;
            startTime = 0.0F;
            j = 0;
        }

        if (j == 1)
        {

            myTime = myTime + Time.deltaTime;

        }
        if (myTime > nextTime)
        {

            jian.GetComponent<BoxCollider>().enabled = false;
            myTime = 0.0F;

            i = 0;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        for (b = 0; b < 18; b++) { 
            if (other.gameObject.CompareTag("qiang"+b))
        {
            transform.GetComponent<Animation>().Play("beiji");
            i = 0;
            jian.GetComponent<BoxCollider>().enabled = false;
        }
        }
    }
}
