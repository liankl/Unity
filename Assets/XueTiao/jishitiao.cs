using UnityEngine;
using System.Collections;

public class jishitiao : MonoBehaviour
{
    public float attackTimer;
    public float attackTime;
    //主摄像机对象
    private Camera camera;
    //NPC名称
    private string name = "剑客";

    //主角对象
    GameObject hero;
    //NPC模型高度
    float npcHeight;
    //红色血条贴图
    public Texture2D blood_red;
    //黑色血条贴图
    public Texture2D blood_black;
    //默认NPC血值
    private float HP = 1000;

    void Start()
    {
        attackTimer = 0;
        attackTime = 0.2f;
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
        npcHeight = (size_y * scal_y) + 2f;

    }

    void Update()
    {
        if (attackTimer > 0)
            attackTimer -= Time.deltaTime;
        if (attackTimer < 0)
            attackTimer = 0;
        if (attackTimer == 0)
        {
            if (HP > 0)
            {
                HP -= 4;
            }
            attackTimer = attackTime;
        }
        //保持NPC一直面朝主角
        transform.LookAt(hero.transform);
    }


    void OnGUI()
    {
        //得到NPC头顶在3D世界中的坐标
        //默认NPC坐标点在脚底下，所以这里加上npcHeight它模型的高度即可
        Vector3 worldPosition = new Vector3(transform.position.x, transform.position.y + npcHeight, transform.position.z);
        //根据NPC头顶的3D坐标换算成它在2D屏幕中的坐标
        Vector2 position = camera.WorldToScreenPoint(worldPosition);
        //得到真实NPC头顶的2D坐标
        position = new Vector2(position.x-200, Screen.height - position.y-40);
        //注解2
        //计算出血条的宽高
        Vector2 bloodSize = GUI.skin.label.CalcSize(new GUIContent(blood_red));
        Vector2 bloodSize1 = GUI.skin.label.CalcSize(new GUIContent(blood_black));

        //通过时间计算香显示区域
        float blood_Height = blood_red.height * HP / 1000;
        //先绘制黑色血条
        GUI.DrawTexture(new Rect(position.x - 600 - (bloodSize1.x / 2), position.y +300- bloodSize1.y, bloodSize1.x, bloodSize1.y), blood_black);
        //在绘制红色血条
        GUI.DrawTexture(new Rect(position.x - 600 - (bloodSize.x / 2), position.y + 430 - bloodSize.y, bloodSize.x, -blood_Height), blood_red);

        //注解3
        //计算NPC名称的宽高
        Vector2 nameSize = GUI.skin.label.CalcSize(new GUIContent(name));
        //设置显示颜色为黄色
        GUI.color = Color.yellow;
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
