using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonCollider : MonoBehaviour
{

    // 玩家进入龙的巡逻范围
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            this.gameObject.transform.parent.GetComponent<DragonData>().ifFollowPlayer = true;
            this.gameObject.transform.parent.GetComponent<DragonData>().player = other.gameObject;
        }
    }

    // 玩家逃离龙巡逻范围
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            this.gameObject.transform.parent.GetComponent<DragonData>().ifFollowPlayer = false;
            this.gameObject.transform.parent.GetComponent<DragonData>().player = null;
        }
    }

}