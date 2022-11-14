using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseRay : MonoBehaviour
{

    public static MouseRay Instance { get; private set; }

    private RaycastHit hitInfo;

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            return;
        }
        Destroy(this);
    }


    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out hitInfo);
    }

    public RaycastHit GetHitInfo()
    {
        return hitInfo;
    }
}
