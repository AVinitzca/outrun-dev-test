using Assets._Outrun.System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartTransition : MonoBehaviour
{
    public void RestartGame()
    {
        GameManager.Reset();
    }
}
