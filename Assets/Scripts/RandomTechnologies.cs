using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTechnologies : MonoBehaviour
{

    [SerializeField] TreeNode researchOne;
    [SerializeField] TreeNode researchTwo;

    [SerializeField] TreeNode NullNode;
        

    TreeNode[] allTechNodes;
    [SerializeField] List<TreeNode> availableTech;
    [SerializeField] List<TreeNode> pickedNodes;

    public void Start()
    {
        availableTech = new List<TreeNode>();
        allTechNodes = GameObject.FindObjectsOfType<TreeNode>();

        researchOne.onNodeUnlocked += NewHand;
        researchTwo.onNodeUnlocked += NewHand;
        
        //Starts the technologies
        Reset();

    }

    public void Reset()
    {
        foreach (TreeNode node in allTechNodes)
        {
            if (node.State == TreeNode.NodeState.Available)
            {
                if (node != researchOne && node != researchTwo)
                {
                    Debug.Log(node);
                    availableTech.Add(node);
                }
            }
        }

        ResearchPick();
    }

    private void NewHand(object sender, System.EventArgs e)
    {
        foreach (TreeNode node in pickedNodes)
        {
            if (((TreeNode)sender).Data == node.Data)
            {
                node.SetUnlocked();
                foreach(TreeNode child in node.Children)
                {
                    if (child.State == TreeNode.NodeState.Available)
                    {
                        if (!availableTech.Equals(child)) availableTech.Add(child);
                    }
                }
            }
            else availableTech.Add(node);
        }


        pickedNodes.Clear();
        researchOne.SetAvailable();
        researchTwo.SetAvailable();
        ResearchPick();
    }

    public TreeNode PickResearch()
    {
        int totalWeights = 0;

        //Add all weights toghether
        foreach(TreeNode node in availableTech)
        {
            totalWeights += node.Data.weight;
        }

        int random = Random.Range(0, totalWeights + 1);

        int weight = 0;

        
        foreach (TreeNode node in availableTech)
        {
            weight += node.Data.weight;
            //Checks if random is between the weights 
            if (random <= weight)
            {
                pickedNodes.Add(node);
                return node;
            }
        }

        Debug.LogWarning("Something went wrong picking a node");
        return NullNode;
    }

    public void ResearchPick()
    {

        TreeNode One = PickResearch();
        researchOne.SetData(One.Data);
        researchOne.remainingCost = researchOne.Data.cost;
        availableTech.Remove(One);

        TreeNode Two = PickResearch();
        researchTwo.SetData(Two.Data);
        researchTwo.remainingCost = researchTwo.Data.cost;
        availableTech.Remove(Two);

        researchOne.GetComponentInChildren<TreeNodeVisual>().UpdateVis();
        researchTwo.GetComponentInChildren<TreeNodeVisual>().UpdateVis();
    }
}
