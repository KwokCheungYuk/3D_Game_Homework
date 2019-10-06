using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiskFactory : MonoBehaviour
{
    public GameObject UFO_instace = null;                 
    private List<DiskData> used = new List<DiskData>();   
    private List<DiskData> free = new List<DiskData>();   

    public GameObject GetDisk(int round)
    {
        int random = 0;
        int range1 = 1, range2 = 4, range3 = 7;           
        float start_y = -10f;                             
        string tag;
        UFO_instace = null;

        //根据回合，随机选择要飞出的飞碟
        if (round == 1)
        {
            random = Random.Range(0, range1);
        }
        else if (round == 2)
        {
            random = Random.Range(0, range2);
        }
        else
        {
            random = Random.Range(0, range3);
        }
        //将要选择的飞碟的tag
        if (random <= range1)
        {
            tag = "UFO1";
        }
        else if (random <= range2 && random > range1)
        {
            tag = "UFO2";
        }
        else
        {
            tag = "UFO3";
        }
        //寻找相同tag的空闲飞碟
        for (int i = 0; i < free.Count; i++)
        {
            if (free[i].tag == tag)
            {
                UFO_instace = free[i].gameObject;
                free.Remove(free[i]);
                break;
            }
        }
        //如果空闲列表中没有，则重新实例化飞碟
        if (UFO_instace == null)
        {
            if (tag == "UFO1")
            {
                UFO_instace = Instantiate(Resources.Load<GameObject>("UFO1"), new Vector3(0, start_y, 0), Quaternion.identity);
            }
            else if (tag == "UFO2")
            {
                UFO_instace = Instantiate(Resources.Load<GameObject>("UFO2"), new Vector3(0, start_y, 0), Quaternion.identity);
            }
            else
            {
                UFO_instace = Instantiate(Resources.Load<GameObject>("UFO3"), new Vector3(0, start_y, 0), Quaternion.identity);
            }
            //给新实例化的飞碟赋予其他属性
            float ran_x = Random.Range(-1f, 1f) < 0 ? -1 : 1;
            UFO_instace.GetComponent<Renderer>().material.color = UFO_instace.GetComponent<DiskData>().color;
            UFO_instace.GetComponent<DiskData>().direction = new Vector3(ran_x, start_y, 0);
            UFO_instace.transform.localScale = UFO_instace.GetComponent<DiskData>().scale;
        }
        //添加到使用列表中
        used.Add(UFO_instace.GetComponent<DiskData>());
        return UFO_instace;
    }

    //回收飞碟
    public void FreeDisk(GameObject disk)
    {
        for (int i = 0; i < used.Count; i++)
        {
            if (disk.GetInstanceID() == used[i].gameObject.GetInstanceID())
            {
                used[i].gameObject.SetActive(false);
                free.Add(used[i]);
                used.Remove(used[i]);
                break;
            }
        }
    }
}
