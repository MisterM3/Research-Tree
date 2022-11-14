using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TreeNodeData", menuName = "ScriptableObjects/TreeNodeData", order = 1)]
public class TreeNodeData : ScriptableObject
{
    [SerializeField] public string code;
    [SerializeField] public string displayName;
    [SerializeField] public Sprite picture;
    [SerializeField] public int cost;
    [SerializeField] public int weight = 100;


}