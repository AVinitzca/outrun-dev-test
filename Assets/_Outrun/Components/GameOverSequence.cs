using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverSequence : MonoBehaviour
{
    public string PlayTag;
    public string RestartTag;

    public void ShowRestart()
    {
        int found = 0;
        for(int index = 0; index < transform.childCount && found < 2; index++)
        {
            var child = transform.GetChild(index);
            if(child.CompareTag(RestartTag))
            {
                child.gameObject.SetActive(true);
                found++;
            }
            else if(child.CompareTag(PlayTag))
            {
                child.gameObject.SetActive(false);
                found++;
            }
        }
    }
}
