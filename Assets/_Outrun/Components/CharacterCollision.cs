using Assets._Outrun.Components;
using Assets._Outrun.System;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCollision : MonoBehaviour, IResetable
{
    public string CollectibleTag;
    public string ObstacleTag;
    public float CrashForce;

    private bool firstTime = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(CollectibleTag))
        {
            CollectGold(other.gameObject);
        }
        else if(other.CompareTag(ObstacleTag))
        {
            Crash();
        }
    }

    private void CollectGold(GameObject gameObject)
    {
        Destroy(gameObject);
        GameManager.AddScore();
    }

    public void Reset()
    {
        if (!firstTime)
        {
            var rigidBody = GetComponent<Rigidbody>();
            rigidBody.isKinematic = !rigidBody.isKinematic;
            rigidBody.useGravity = !rigidBody.useGravity;
            rigidBody.constraints = rigidBody.isKinematic ? RigidbodyConstraints.None : RigidbodyConstraints.FreezePositionZ;

            if (!rigidBody.isKinematic)
            {
                rigidBody.WakeUp();
                rigidBody.AddExplosionForce(CrashForce, transform.position + new Vector3(0.5f, -1.0f, 0.5f), 100.0f);               
            }
            else
            {
                Vector3 newPosition = transform.position;
                newPosition.y = -9.5f;
                transform.position = newPosition;
            }
        }
        else
            firstTime = false;
    }

    private void Crash()
    {
        GameManager.End();
    }
}
