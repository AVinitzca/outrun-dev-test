using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationTransition : MonoBehaviour
{
    public GameObject TransitionTarget;

    public void Transition()
    {
        TransitionTarget.SetActive(!TransitionTarget.activeSelf);
    }
}
