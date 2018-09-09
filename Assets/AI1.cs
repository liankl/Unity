using UnityEngine;
using System.Collections;

public class AI1 : MonoBehaviour
{

    Animation anim;
    #region variable  
    [SerializeField] //保护封装性  
    private float speed = 2f;
    [SerializeField]
    private WayPoint targetPoint, startPoint;
    [SerializeField]
    private Hero mage;

    #endregion
    private GameObject qiang;
    private float startTime;
    private float endTime = 0.5F;
    int i;
    // Use this for initialization  
    void Start()
    {
        qiang = GameObject.FindGameObjectWithTag("qiang");
        qiang.GetComponent<MeshCollider>().enabled = false;
        mage.gameObject.GetComponent<jianke>().enabled = false;
        gameObject.GetComponent<Npc1>().enabled = false;
        if (Vector3.Distance(transform.position, startPoint.transform.position) < 1e-2f)
        {
            targetPoint = startPoint.nextWayPoint;
        }
        else
        {

            targetPoint = startPoint;
        }
        StartCoroutine(AINavMesh());
    }

    IEnumerator AINavMesh()
    {


        while (true)
        {
            if (Vector3.Distance(transform.position, targetPoint.transform.position) < 1f)
            {

                var dist = Vector3.Distance(transform.position, targetPoint.transform.position);

                targetPoint = targetPoint.nextWayPoint;
                transform.GetComponent<Animation>().Play("stop");

                yield return new WaitForSeconds(2f);

                dist = 0;

            }

            if (mage != null && Vector3.Distance(transform.position, mage.gameObject.transform.position) <= 6f)
            {

                Debug.Log("侦测到敌人，开始追击！！！");
                //启用血条脚本
                gameObject.GetComponent<Npc1>().enabled = true;
                mage.gameObject.GetComponent<jianke>().enabled = true;
                //（） transform.LookAt(mage.transform);没意义
                yield return StartCoroutine(AIFollowHero());


            }
            Vector3 dir = targetPoint.transform.position - transform.position;
            // Quaternion newRotation = Quaternion.LookRotation(dir);
            //transform.rotation = Quaternion.RotateTowards(transform.rotation, newRotation, Time.deltaTime * speed * 0.8f);
            transform.Translate(dir.normalized * Time.deltaTime * speed, Space.World);
            transform.GetComponent<Animation>().Play("walk");//播放角色行走动画  

            transform.LookAt(targetPoint.transform);
            yield return new WaitForEndOfFrame();


        }
    }
    IEnumerator AIFollowHero()
    {

        while (true)
        {
            //transform.LookAt(targetPoint.transform);
            if (mage != null && Vector3.Distance(transform.position, mage.gameObject.transform.position) > 1.5f)
            {
                transform.LookAt(mage.transform);
                transform.GetComponent<Animation>().Play("run");//播放角色跑动画 
                Vector3 dir = mage.transform.position - transform.position;

                transform.Translate(dir.normalized * Time.deltaTime * speed * 0.8f, Space.World);
            }
            if (mage != null && Vector3.Distance(transform.position, mage.gameObject.transform.position) <= 1.5f)
            {


                Debug.Log("追击到了，要攻击");
                transform.LookAt(mage.transform);

                transform.GetComponent<Animation>().Play("attack");
                qiang.GetComponent<MeshCollider>().enabled = true;
                i = 1;
                yield return new WaitForSeconds(2f);


            }
            if (mage != null && Vector3.Distance(transform.position, mage.gameObject.transform.position) > 6f)
            {
                Debug.Log("敌人已走远，放弃攻击！！！");
                //禁用血条脚本
                gameObject.GetComponent<npc>().enabled = false;
                mage.gameObject.GetComponent<jianke>().enabled = false;
                targetPoint = startPoint.nextWayPoint;//放弃攻击后要回到原点，要不然会满图乱跑

                yield break;

            }

            transform.LookAt(mage.transform);
            yield return new WaitForEndOfFrame();

        }
    }
    private void Update()
    {
        if (i == 1)
        {
            startTime = startTime + Time.deltaTime;


        }
        if (startTime > endTime)
        {

            qiang.GetComponent<MeshCollider>().enabled = false;
            startTime = 0.0F;
            i = 0;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("jian"))
        {
            transform.GetComponent<Animation>().Play("bet");
        }
    }

}

