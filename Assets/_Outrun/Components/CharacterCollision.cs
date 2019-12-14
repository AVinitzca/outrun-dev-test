using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCollision : MonoBehaviour
{
    public string CollectibleTag;
    public string ObstacleTag;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(CollectibleTag))
        {
            Destroy(other.gameObject);
        }
        else if(other.CompareTag(ObstacleTag))
        {
            Debug.Log("EnteredCar");
            Destroy(other.gameObject);
        }
    }
}
