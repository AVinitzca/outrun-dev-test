using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectToggler : MonoBehaviour
{
    public GameObject TargetObject;

    // This is the same as NavigationTransition but the method name must be different in order to differentiate
    public void Transition()
    {
        TargetObject.SetActive(!TargetObject.activeSelf);
    }
}
