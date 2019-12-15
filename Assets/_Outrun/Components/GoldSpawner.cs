using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldSpawner : MonoBehaviour
{
    [Range(0.0f, 1.0f)]
    public float ChanceToSpawn;
    public GameObject Gold;

    private GameObject created;

    void Start()
    {
        if (Random.value > ChanceToSpawn)
            Spawn(RandomChildTransform());
    }

    private void Spawn(Transform parent)
    {
        created = Instantiate(Gold, parent.position + Vector3.up * 2.5f, Quaternion.identity);
        created.transform.parent = parent;
    }

    private Transform RandomChildTransform()
    {
        int index = Random.Range(0, transform.childCount);
        return transform.GetChild(index);
    }

    public void Clear()
    {
        if(created != null)
            Destroy(created);
    }

}
