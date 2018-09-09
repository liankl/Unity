/*using UnityEngine;
using System.Collections;
//相机一直拍摄主角的后背
public class CameraFlow : MonoBehaviour
{

    public Transform target;


    public float distanceUp = 15f;
    public float distanceAway = 10f;
    public float smooth = 2f;//位置平滑移动值
    public float camDepthSmooth = 5f;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // 鼠标轴控制相机的远近
        if ((Input.mouseScrollDelta.y < 0 && Camera.main.fieldOfView >= 3) || Input.mouseScrollDelta.y > 0 && Camera.main.fieldOfView <= 80)
        {
            Camera.main.fieldOfView += Input.mouseScrollDelta.y * camDepthSmooth * Time.deltaTime;
        }
    }

    void LateUpdate()
    {
        //相机的位置
        Vector3 disPos = target.position + Vector3.up * distanceUp - target.forward * distanceAway;
        transform.position = Vector3.Lerp(transform.position, disPos, Time.deltaTime * smooth);
        //相机的角度
        transform.LookAt(target.position);
    }
}
*/
using UnityEngine;
using System.Collections;

public class CameraFlow : MonoBehaviour
{
    public Transform target;
    private Vector3 offset;
    // Use this for initialization
    void Start()
    {
        offset = target.position - this.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = target.position - offset;
    }
}

/*
using UnityEngine;
using System.Collections;

public class CameraFlow : MonoBehaviour
{

    public float smooth = 1.5f;
    private Transform player;
    private Vector3 relCameraPos; //相对位置摄像机对人物
    private float relCameraPosMag; //摄像机和人物的距离
    private Vector3 newPos;  //摄像机试着抵达的位置

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        //摄像机相对位置 = 摄像机位置 - 玩家位置
        relCameraPos = transform.position - player.position;
        //实际向量长度-0.5 小一点
        relCameraPosMag = relCameraPos.magnitude - 0.5f;

    }

    void FixedUpdate()
    {

        //摄像机的初始位置 = 玩家位置 + 相对位置
        Vector3 standardPos = player.position + relCameraPos;
        //摄像机的俯视位置 = 玩家位置 + 玩家正上方 * 相对位置向量长度
        Vector3 abovePos = player.position + Vector3.up * relCameraPosMag;

        Vector3[] checkPoints = new Vector3[5];

        checkPoints[0] = standardPos;

        checkPoints[1] = Vector3.Lerp(standardPos, abovePos, 0.25f);
        checkPoints[2] = Vector3.Lerp(standardPos, abovePos, 0.5f);
        checkPoints[3] = Vector3.Lerp(standardPos, abovePos, 0.75f);


        checkPoints[4] = abovePos;

        for (int i = 0; i < checkPoints.Length; i++)
        {
            if (ViewingPosCheck(checkPoints[i]))
            {
                break;
            }
        }

        transform.position = Vector3.Lerp(transform.position, newPos, smooth * Time.deltaTime);

        SmoothLookAt();
    }


    //使用光线投射的方法检测摄像机能否投射碰撞到玩家 
    bool ViewingPosCheck(Vector3 checkPos)
    {

        RaycastHit hit;

        if (Physics.Raycast(checkPos, player.position - checkPos, out hit, relCameraPosMag))
        {
            if (hit.transform != player)
            {
                return false;
            }
        }

        newPos = checkPos;
        return true;
    }
    //使摄像机函数在移动过程中始终面对玩家
    void SmoothLookAt()
    {
        Vector3 relPlayPosition = player.position - transform.position;

        Quaternion lookAtRotation = Quaternion.LookRotation(relPlayPosition, Vector3.up);

        transform.rotation = Quaternion.Lerp(transform.rotation, lookAtRotation, smooth * Time.deltaTime);
    }
}


*/
/*
using UnityEngine;
using System.Collections;

public class CameraFlow : MonoBehaviour
{

    public Vector3 offset;
    private Transform playerBip;
    public float smoothing = 1;

    // Use this for initialization
    void Start()
    {
        playerBip = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //transform.position = playerBip.position + offset;
        Vector3 targetPos = playerBip.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetPos, smoothing * Time.deltaTime);
    }
}*/
/*
using UnityEngine;
using System.Collections;

//第三人称镜头跟随，鼠标控制镜头缩放和环绕

public class CameraFlow : MonoBehaviour
{
    //请在Editor界面选定摄像头的锁定目标，推荐选定模型的头部文件
    public Transform target;
    //防止镜头卡死,在使用前把镜头放在合适位置 
    Vector3 CameraDis;
    // 初始化 
    void Start()
    {
        CameraDis = transform.position - target.position;
    }
    void LateUpdate()
    {

        Scale();
        transform.position = target.position + CameraDis;
        transform.LookAt(target);
        Rotate();
    }
    //缩放
    private void Scale()
    {
        float Scaledis = CameraDis.magnitude;
        Scaledis -= Input.GetAxis("Mouse ScrollWheel") * 5;
        //限制缩放
        if ((Scaledis <= 2))
        {
            Scaledis = 2;
        }
        else if (Scaledis >= 8)
            Scaledis = 8;
        CameraDis = CameraDis.normalized * Scaledis;
    }
    //摄像头环绕
    private void Rotate()
    {
        //速度可以通过修改数字来调整
        transform.RotateAround(target.position, Vector3.up, Input.GetAxis("Mouse X") * 10);
        float t = Input.GetAxis("Mouse Y") * -1 / 5;
        CameraDis = transform.position - target.position;
        CameraDis.y += t;
        //对旋转的角度加以限制
        if (CameraDis.y >= 2)
        {
            CameraDis.y = 2;
        }
        else if (CameraDis.y <= 0)
        {
            CameraDis.y = 0;
        }
    }
}*/