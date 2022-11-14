using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ResearchPoints : MonoBehaviour
{

    public static ResearchPoints Instance { get; private set; }

    public event EventHandler OnNextTurn;

    [SerializeField] TextMeshProUGUI turnsText;
    [SerializeField] TextMeshProUGUI pointsText;

    int turns = 0;
    int points = 0;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            return;
        }
        else Destroy(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateText();
    }


    void UpdateText()
    {
        turnsText.text = "Turn: " + turns;
        pointsText.text = "RP: " + points;
    }

    public void NextTurn()
    {
        turns++;
        points++;
        OnNextTurn?.Invoke(this, EventArgs.Empty);
        UpdateText();
    }

    public int GetPoints()
    {
        return points;
    }

    public void RemovePoints(int pointsRemoved)
    {
        points -= pointsRemoved;
    }

    public void Reset()
    {
        turns = 0;
        points = 0;
        UpdateText();
    }
}
