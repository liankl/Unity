using UnityEngine;
using System.Collections;

public class WayPoint : MonoBehaviour
{

    #region  
    public WayPoint nextWayPoint;
    #endregion

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, nextWayPoint.gameObject.transform.position);
    }
}