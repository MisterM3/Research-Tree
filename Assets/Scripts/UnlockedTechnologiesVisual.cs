using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UnlockedTechnologiesVisual : MonoBehaviour
{

    public static UnlockedTechnologiesVisual Instance { get; private set; }

    [SerializeField] TextMeshProUGUI text;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            return;
        }

        Destroy(this);
    }

    public void Start()
    {
        text.text = "Unlocked Technologies:";
    }

    public void AddText(string pText)
    { 
        text.text += ("<br>" + pText);
    }
}
