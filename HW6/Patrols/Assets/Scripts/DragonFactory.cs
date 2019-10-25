using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonFactory : MonoBehaviour
{
    private List<GameObject> usedDragons = new List<GameObject>();
    private Vector3[] vec = new Vector3[9]; //保存每条龙的初始位置
    public List<GameObject> getDragons()
    {
        int[] pos_x = { -26, 0, 19 };
        int[] pos_z = {-25,3, 29 };
        int index = 0;
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                vec[index] = new Vector3(pos_x[i], 0, pos_z[j]);
                index++;
            }
        }
        for (int i = 0; i < 9; i++)
        {
            GameObject dragon = Instantiate(Resources.Load<GameObject>("Dragon"));
            dragon.transform.position = vec[i];
            dragon.GetComponent<DragonData>().sign = i + 1;
            dragon.GetComponent<DragonData>().start_position = vec[i];
            usedDragons.Add(dragon);
        }
        return usedDragons;
    }

    public void stopDragon()
    {
        for (int i = 0; i < usedDragons.Count; i++)
        {
            usedDragons[i].gameObject.GetComponent<Animator>().SetBool("run", false);
        }
    }
}
