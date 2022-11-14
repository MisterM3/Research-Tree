using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TreeNode : MonoBehaviour
{

    [SerializeField] bool clickable = true;
    [SerializeField] TreeNodeData nodeDate;
    [SerializeField] private NodeState state;
    [SerializeField] TreeNode[] childrenNodes;
    [SerializeField] GameObject visual;

    private List<TreeNode> parentNodes;
    private Renderer rendererVisual;

    public static EventHandler OnAnyNodeActivation;
    public EventHandler onNodeUnlocked;

    public enum NodeState { Locked, Available, Unlocked, Active}
    public NodeState State => state;
    public TreeNodeData Data => nodeDate;
    public TreeNode[] Children => childrenNodes;
    public List<TreeNode> Parents => parentNodes;
    public int remainingCost;



    public void SetData(TreeNodeData data)
    {
        nodeDate = data;
    }

    private void Awake()
    {
        parentNodes = new List<TreeNode>();
    }

    // Start is called before the first frame update
    void Start()
    {

        rendererVisual = visual.GetComponent<Renderer>();

        foreach (TreeNode child in childrenNodes)
        {
            child.AddParent(this);
        }
        remainingCost = Data.cost;
        ResearchPoints.Instance.OnNextTurn += ResearchPoints_OnNextTurn;
        TreeNode.OnAnyNodeActivation += TreeNode_OnAnyNodeActivation;

        switch (state)
        {
            case NodeState.Locked:
                rendererVisual.material.color = Color.gray;
                break;
            case NodeState.Available:
                SetAvailable();
                break;
            case NodeState.Unlocked:
                SetUnlocked();
                break;


        }


    }

    private void TreeNode_OnAnyNodeActivation(object sender, EventArgs e)
    {
        if (state == NodeState.Active) SetAvailable();
    }

    private void ResearchPoints_OnNextTurn(object sender, System.EventArgs e)
    {
        if (state == NodeState.Active) SetActive();
    }

    private void AddParent(TreeNode parent)
    {
        parentNodes.Add(parent);
    }

    // Update is called once per frame
    void Update()
    {
        if (!clickable) return;

        RaycastHit hitInfo = MouseRay.Instance.GetHitInfo();

        bool mouseClickOnNode = (Input.GetMouseButtonDown(0) && state == NodeState.Available && hitInfo.collider == this.gameObject.GetComponent<Collider>());

        if (mouseClickOnNode)
        {
            OnAnyNodeActivation?.Invoke(this, EventArgs.Empty);

            state = NodeState.Active;
            rendererVisual.material.color = Color.red;
        }
    }

    void CheckAvailableNode()
    {
        foreach(TreeNode parent in parentNodes)
        {
            //At least one parent is not unlocked
            if (parent.State != NodeState.Unlocked) return;
            
        }
        SetAvailable();
    }

    public void SetLocked()
    {
        state = NodeState.Locked;
        rendererVisual.material.color = Color.gray;
    }
    public void SetAvailable()
    {
        state = NodeState.Available;
        rendererVisual.material.color = Color.blue;
    }

    void SetActive()
    {
        int researchPoints = ResearchPoints.Instance.GetPoints();

        if (researchPoints < remainingCost)
        {
            remainingCost -= researchPoints;
            ResearchPoints.Instance.RemovePoints(researchPoints);
        } else
        {
            ResearchPoints.Instance.RemovePoints(remainingCost);
            remainingCost = 0;
            SetUnlocked();
        }
    }

    public void SetUnlocked()
    {
        state = NodeState.Unlocked;
        rendererVisual.material.color = Color.green;
        this.transform.GetComponentInChildren<LineVisual>().ActivateLines();

        if (clickable)
        {
            UnlockedTechnologiesVisual.Instance.AddText(nodeDate.code);
        }

        foreach (TreeNode child in childrenNodes)
        {
            child.CheckAvailableNode();
        }
        onNodeUnlocked?.Invoke(this, EventArgs.Empty);
    }





    //Gizmos used for easier seeing connection between nodes
    void OnDrawGizmos()
    {
        foreach(TreeNode child in childrenNodes)
        {
            if (child == null) continue;
            // Draws a blue line from this transform to the target
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, child.transform.position);
        }
    }
}
