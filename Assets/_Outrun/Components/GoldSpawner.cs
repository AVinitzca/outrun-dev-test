using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldSpawner : MonoBehaviour
{
    [Range(0.0f, 1.0f)]
    public float ChanceToSpawn;
    public GameObject Gold;

    void Start()
    {
        if (Random.value > ChanceToSpawn)
            Spawn(RandomChildTransform());
    }

    private void Spawn(Transform parent)
    {
        GameObject gold = Instantiate(Gold, parent.position + Vector3.up * 2.5f, Quaternion.identity);
        gold.transform.parent = parent;
    }

    private Transform RandomChildTransform()
    {
        int index = Random.Range(0, transform.childCount);
        return transform.GetChild(index);
    }

}
