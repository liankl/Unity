using UnityEngine;
using System.Collections;
using System;

public class npc : MonoBehaviour
{
    //小兵
    private GameObject xiaobing;
    //主摄像机对象
    private Camera camera;
    //NPC名称
    private string name = "巡逻小兵";
    
    private int i = 0;

    //主角对象
    private GameObject hero;
    //传主角攻击参数的变量
    private jianke chuancan;

    //NPC模型高度
    float npcHeight;
    //红色血条贴图
    public Texture2D blood_red;
    //黑色血条贴图
    public Texture2D blood_black;
    //默认NPC血值
    private int HP = 15;
    //与血药代码关联
    private xieping xie;
    private GameObject xieyao;
    private string ZHEGE;

    //与力量药剂关联
    private liliang strong;
    private GameObject yaoji;
   

    public int n;


    void Start()
    {
        xieyao = GameObject.FindGameObjectWithTag("xieyao");
        xie = xieyao.GetComponent<xieping>();

        yaoji = GameObject.FindGameObjectWithTag("yaoji");
        strong = yaoji.GetComponent<liliang>();


        ZHEGE = this.transform.tag;
        xiaobing = GameObject.FindGameObjectWithTag(ZHEGE);
        
        //根据Tag得到主角对象
        hero = GameObject.FindGameObjectWithTag("Player");
        //根据GetComponent得到的主角的攻击的值的传参
        chuancan = hero.GetComponent<jianke>();
        
        //得到摄像机对象
        camera = Camera.main;

        //注解1
        //得到模型原始高度
        float size_y = GetComponent<Collider>().bounds.size.y;
        //得到模型缩放比例
        float scal_y = transform.localScale.y;
        //它们的乘积就是高度
        npcHeight = (size_y * scal_y)+0.2f;



        

    }

    void Update()
    {
        //保持NPC一直面朝主角
        //transform.LookAt(hero.transform);
        
    }

    void OnGUI()
    {
        //得到NPC头顶在3D世界中的坐标
        //默认NPC坐标点在脚底下，所以这里加上npcHeight它模型的高度即可
        Vector3 worldPosition = new Vector3(transform.position.x, transform.position.y + npcHeight, transform.position.z);
        //根据NPC头顶的3D坐标换算成它在2D屏幕中的坐标
        Vector2 position = camera.WorldToScreenPoint(worldPosition);
        //得到真实NPC头顶的2D坐标
        position = new Vector2(position.x, Screen.height - position.y);
        //注解2
        //计算出血条的宽高
        Vector2 bloodSize = GUI.skin.label.CalcSize(new GUIContent(blood_red));

        //通过血值计算红色血条显示区域
        int blood_width = blood_red.width * HP / 100;
        //先绘制黑色血条
        GUI.DrawTexture(new Rect(position.x - (bloodSize.x / 2), position.y - bloodSize.y, bloodSize.x, bloodSize.y), blood_black);
        //在绘制红色血条
        GUI.DrawTexture(new Rect(position.x - (bloodSize.x / 2), position.y - bloodSize.y, blood_width, bloodSize.y), blood_red);

        //注解3
        //计算NPC名称的宽高
        Vector2 nameSize = GUI.skin.label.CalcSize(new GUIContent(name));
        //设置显示颜色为黄色
        GUI.color = Color.yellow;
        //绘制NPC名称
        GUI.Label(new Rect(position.x - (nameSize.x / 2), position.y - nameSize.y - bloodSize.y, nameSize.x, nameSize.y), name);

    }

    //下面是经典鼠标点击对象的事件，大家看一下就应该知道是什么意思啦。
    void OnMouseDrag()
    {
        Debug.Log("鼠标拖动该模型区域时");
    }
    /*
    void OnMouseDown()
    {
        Debug.Log("鼠标按下时");

        if (HP > 0)
        {
            HP -= 5;
        }

    }
    */
    // 碰撞开始
    /*void OnCollisionEnter(Collision collision)
    {
        var tag = collision.collider.tag;

        if (HP > 0 && tag=="jian")
        {
            HP -= 5;
        }
    }*/
    void OnTriggerEnter(Collider other)
    {
        
        if (HP > 0 && other.gameObject.CompareTag("jian"))
        {
            HP -=chuancan.gongji;
        }
        if (HP <= 0)
        {
             
            transform.GetComponent<Animation>().Play("die");
            Destroy(xiaobing.GetComponent<AI>());//删除代码
            
            //xiaobing.GetComponent<AI>().enabled = false;
            Destroy(gameObject, 2);
            
            i += 1;    
            jiaxie();
        }
    }
    void jiaxie()
    {

        if (i == 1)
        {
            //随即数判定掉落物品
            System.Random random = new System.Random();
            n = random.Next(1, 3);
            Debug.Log(n);
            if (n == 1)
            {
                xie.addScore(1);
            }
            else
            {
                strong.addScore(1);
            }
            
        }
    }
    
}