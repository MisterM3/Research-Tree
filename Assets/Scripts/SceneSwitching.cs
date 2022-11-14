using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitching : MonoBehaviour
{
    

    public void SwitchStandard()
    {
        SceneManager.LoadScene(0);
    }

    public void SwitchRandom()
    {
        SceneManager.LoadScene(1);
    }
}
