using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class UpdateTechTree : MonoBehaviour
{

    static TreeNodeVisual[] visuals;



    //Button to update the visuals in edit mode
    [MenuItem("ResearchTree/UpdateNodeVisuals")]
    static void UpdateVisuals()
    {

        visuals = GameObject.FindObjectsOfType<TreeNodeVisual>();
        foreach (TreeNodeVisual visual in visuals)
        {
            visual.UpdateVis();
        }
    }
    //Button to redraw the connections in edit mode
    [MenuItem("ResearchTree/RedrawTechConnections")]
    public static void UpdateLines()
    {
        //Works in this specific case, If done in a project, use tag or base object to find them to not exidentally remove other linerenderers
        LineRenderer[] lineRenderer = GameObject.FindObjectsOfType<LineRenderer>();
        foreach (LineRenderer line in lineRenderer)
        {
            DestroyImmediate(line.gameObject);
        }

        LineVisual[] lineVisual = GameObject.FindObjectsOfType<LineVisual>();
        foreach (LineVisual lineScript in lineVisual)
        {
            lineScript.MakeLines();
        }
    }

}
