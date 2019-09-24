using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using game;

public class Judgment : MonoBehaviour
{
    Stack<GameObject> leftPriests;
    Stack<GameObject> rightPriests;
    Stack<GameObject> leftDevils;
    Stack<GameObject> rightDevils;
    GameObject[] ship;
    SSDirector s1;
    public Judgment(Stack<GameObject>lp, Stack<GameObject>rp, Stack<GameObject>ld, Stack<GameObject>rd, GameObject[] s, SSDirector d)
    {
        this.leftPriests = lp;
        this.rightPriests = rp;
        this.leftDevils = ld;
        this.rightDevils = rd;
        this.ship = s;
        this.s1 = d;
    }

    public State Judge()
    {
        //全都达到右岸
        if (rightPriests.Count == 3 && rightDevils.Count == 3)
        {
            return State.Win;
        }
        //某一边恶魔多余牧师
        int numShipPriest = 0;
        int numLeftPriest = 0;
        int numRightPriest = 0;
        int numShipDevil = 0;
        int numLeftDevil = 0;
        int numRightDevil = 0;
        for (int i = 0; i < 2; i++)
        {
            if (ship[i] != null && ship[i].name == "Priest(Clone)")
            {
                numShipPriest++;
            }
            else if (ship[i] != null && ship[i].name == "Devil(Clone)")
            {
                numShipDevil++;
            }
        }
        if (s1.state == State.Left)
        {
            numLeftPriest = leftPriests.Count + numShipPriest;
            numRightPriest = rightPriests.Count;
            numLeftDevil = leftDevils.Count + numShipDevil;
            numRightDevil = rightDevils.Count;
        }
        else if (s1.state == State.Right)
        {
            numLeftPriest = leftPriests.Count;
            numRightPriest = rightPriests.Count + numShipPriest;
            numLeftDevil = leftDevils.Count;
            numRightDevil = rightDevils.Count + numShipDevil;
        }
        if ((numLeftPriest != 0 && numLeftPriest < numLeftDevil) || (numRightPriest != 0 && numRightPriest < numRightDevil))
        {
            return State.Lose;
        }
        return s1.state;
    }
}
