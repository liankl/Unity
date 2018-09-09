using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class marui : MonoBehaviour {

    #region variable  
    [SerializeField] //保护封装性  
    private float speed = 2f;
    [SerializeField]
    private WayPoint targetPoint, startPoint;
    [SerializeField]
    private Hero mage;

    #endregion

    // Use this for initialization  
    void Start()
    {
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
                print(dist);
                targetPoint = targetPoint.nextWayPoint;
                transform.GetComponent<Animation>().Play("stop");

                yield return new WaitForSeconds(2f);

                dist = 0;

            }

            if (mage != null && Vector3.Distance(transform.position, mage.gameObject.transform.position) <= 6f)
            {

                Debug.Log("侦测到敌人，开始追击！！！");
                yield return StartCoroutine(AIFollowHero());


            }
            Vector3 dir = targetPoint.transform.position - transform.position;
            // Quaternion newRotation = Quaternion.LookRotation(dir);
            //transform.rotation = Quaternion.RotateTowards(transform.rotation, newRotation, Time.deltaTime * speed * 0.8f);
            transform.Translate(dir.normalized * Time.deltaTime * speed);

            //transform.LookAt(targetPoint.transform);
            yield return new WaitForEndOfFrame();


        }
    }
    IEnumerator AIFollowHero()
    {

        while (true)
        {
            //transform.LookAt(targetPoint.transform);
            if (mage != null && Vector3.Distance(transform.position, mage.gameObject.transform.position) > 2.5f)
            {
                //transform.LookAt(mage.transform);
                Vector3 dir = mage.transform.position - transform.position;

                transform.Translate(dir.normalized * Time.deltaTime * speed * 0.8f);
            }
            if (mage != null && Vector3.Distance(transform.position, mage.gameObject.transform.position) <= 2.5f)
            {


                Debug.Log("追击到了，要攻击");

                transform.GetComponent<Animation>().Play("attack");



            }
            if (mage != null && Vector3.Distance(transform.position, mage.gameObject.transform.position) > 6f)
            {
                Debug.Log("敌人已走远，放弃攻击！！！");
                yield break;

            }

            //transform.LookAt(mage.transform);
            yield return new WaitForEndOfFrame();

        }
    }
}
