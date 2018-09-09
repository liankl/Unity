using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gongji : MonoBehaviour
{
        private GameObject jian;
        private GameObject jianke;
      int i = 0;
      int j = 1;


       private float myTime;
       private float nextTime = 1F;
       private float startTime;//开始附加碰撞体
       private float endTime = 1F;//结束去除碰撞体*/
    private void Start()
    {
          jian = GameObject.FindGameObjectWithTag("jian");
          jian.GetComponent<BoxCollider>().enabled = false;
        jianke = GameObject.FindGameObjectWithTag("Player");

    }
        private void Update()
      {
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
    public void onTest()
    {
        if (i == 0)
        {
            i = 1;
            jian.GetComponent<BoxCollider>().enabled = true;
            jianke.transform.GetComponent<Animation>().Play("attack");
        }
        if (j == 0)
        {

            j = 1;

            jian.GetComponent<BoxCollider>().enabled = true;

            jianke.transform.GetComponent<Animation>().Play("attack2");


        }

    }
}
