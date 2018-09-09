using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class xieping : MonoBehaviour {
    public Text countText;
    private int shuliang;
    private GameObject hero;
    private jianke jiaxie;
    // Use this for initialization
    void Start () {
        hero = GameObject.FindGameObjectWithTag("Player");
        jiaxie = hero.GetComponent<jianke>();
        
    }
    public void addScore(int num)
    {
        shuliang += num;
        updateCountText();
    }
    void updateCountText()
    {
        countText.text = ""+shuliang;
    }

    // Update is called once per frame
    public void buxie()
    {
        
        if (shuliang > 0) { 
        jiaxie.Xie();
            
            shuliang -= 1;
            updateCountText();
        }

    }
}
