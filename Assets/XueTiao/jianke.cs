using UnityEngine;
using System.Collections;

public class jianke : MonoBehaviour
{

    //主摄像机对象
    private Camera camera;

    private int j;
    //增加力量计时器
    private float startTime;
    private float endTime = 10.0F;

    //主角对象
    GameObject hero;
    //NPC模型高度
    float npcHeight;
    //红色血条贴图
    public Texture2D blood_red;
    //黑色血条贴图
    public Texture2D blood_black;
    //默认NPC血值
    public int HP;
    //默认主角攻击力
    public int gongji;

    private int i;
    void Start()
    {
        HP = 100;
        gongji = 5;
        //根据Tag得到主角对象
        hero = GameObject.FindGameObjectWithTag("Player");
        //得到摄像机对象
        camera = Camera.main;

        //注解1
        //得到模型原始高度
        float size_y = GetComponent<Collider>().bounds.size.y;
        //得到模型缩放比例
        float scal_y = transform.localScale.y;
        //它们的乘积就是高度
        npcHeight = (size_y * scal_y)+2f;

    }

   void Update()
    {
        //保持NPC一直面朝主角
        transform.LookAt(hero.transform);
        if (j == 1) { 
        startTime = startTime + Time.deltaTime;
            if (startTime > endTime)
            {
                gongji = 5;
                j = 0;
                startTime = 0.0F;
            }
        }
    }
    //加血代码
   public void Xie()
    {
        if (HP < 100)
        {
            HP += 10;
        }
    }
    //加攻击代码
    public void Gong()
    {
        gongji = 15;
        j = 1;
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
        Vector2 bloodSize1 = GUI.skin.label.CalcSize(new GUIContent(blood_black));

        //通过血值计算红色血条显示区域
        int blood_width = blood_red.width * HP / 100;
        //先绘制黑色血条
        GUI.DrawTexture(new Rect(position.x-650 - (bloodSize1.x/2 ), position.y -40- bloodSize1.y, bloodSize1.x, bloodSize1.y), blood_black);
        //在绘制红色血条
        GUI.DrawTexture(new Rect(position.x -600- (bloodSize.x / 2), position.y -140- bloodSize.y, blood_width, bloodSize.y), blood_red);

        





    }

    //下面是经典鼠标点击对象的事件，大家看一下就应该知道是什么意思啦。
    void OnMouseDrag()
    {
        Debug.Log("鼠标拖动该模型区域时");
    }
    /*按鼠标减血
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

        if (HP > 0 && tag == "qiang")
        {
            HP -= 5;
        }
    }*/
    void OnTriggerEnter(Collider other)
    {
        for (i = 0; i < 18; i++)
        {
            if (HP > 0 && other.gameObject.CompareTag("qiang"+i))
            {
                HP -= 5;
            }
        }
        
    }

    void OnMouseUp()
    {
        Debug.Log("鼠标抬起时");
    }

    void OnMouseEnter()
    {
        Debug.Log("鼠标进入该对象区域时");
    }
    void OnMouseExit()
    {
        Debug.Log("鼠标离开该模型区域时");
    }
    void OnMouseOver()
    {
        Debug.Log("鼠标停留在该对象区域时");
    }

}