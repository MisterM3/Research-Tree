using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;


public class TreeNodeVisual : MonoBehaviour
{
    private TreeNodeData data;
    [SerializeField] Image image;
    [SerializeField] TextMeshProUGUI nameResearch;
    [SerializeField] TextMeshProUGUI costResearch;

    TreeNode parent;

    private void Start()
    {
        ResearchPoints.Instance.OnNextTurn += ResearchPoints_OnNextTurn;

        parent = GetComponentInParent<TreeNode>();
        UpdateVis();
    }

    private void ResearchPoints_OnNextTurn(object sender, System.EventArgs e)
    {
        costResearch.text = $"Cost: " + parent.remainingCost;
    }

    public void UpdateVis()
    {
        data = GetComponentInParent<TreeNode>().Data;
        image.sprite = data.picture;
        nameResearch.text = data.displayName;
        costResearch.text = $"Cost: " + data.cost;

    }

}
