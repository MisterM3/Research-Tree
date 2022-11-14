using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetTree : MonoBehaviour
{

    public void Reset()
    {
        TreeNode[] nodes = GameObject.FindObjectsOfType<TreeNode>();

        foreach(TreeNode node in nodes)
        {
            node.remainingCost = node.Data.cost;
            //Not top node
            if (node.Parents.Count != 0)
            {
                node.SetLocked();
            }
            else node.SetAvailable();
        }
    }
}
