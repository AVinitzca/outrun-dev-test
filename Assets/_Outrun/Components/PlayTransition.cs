using Assets._Outrun.System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayTransition : MonoBehaviour
{
    public void PlayGame()
    {
        GameManager.Play(SceneManager.GetActiveScene());
    }
}
