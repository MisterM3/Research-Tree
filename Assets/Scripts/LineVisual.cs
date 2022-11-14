using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineVisual : MonoBehaviour
{

    [SerializeField] LineRenderer linePrefab;

    List<LineRenderer> lines;


    private void Start()
    {
        MakeLines();
    }

    public void MakeLines()
    {
        lines = new List<LineRenderer>();

        TreeNode node = GetComponentInParent<TreeNode>();

        foreach (TreeNode child in node.Children)
        {
            if (child == null)
            {
                Debug.LogError("A Child node is empty in:" + gameObject.name);
                return;
            }

            
            LineRenderer line = Instantiate(linePrefab, transform.position, transform.rotation);
            lines.Add(line);
            line.positionCount = 4;

            Vector3 parentVec3 = this.gameObject.transform.position;
            Vector3 childVec3 = child.gameObject.transform.position;

            Vector3 dPos = childVec3 - parentVec3;
            line.SetPosition(0, Vector3.zero);
            line.SetPosition(1, new Vector3(0, dPos.y / 2f, 0));
            line.SetPosition(2, new Vector3(dPos.x, dPos.y / 2f, 0));
            line.SetPosition(3, dPos);


        }
    }


    public void ActivateLines()
    {
        if (lines.Count == 0) return;
        foreach(LineRenderer line in lines)
        {
            line.startColor = Color.green;
            line.endColor = Color.green;
        }
    }
}
