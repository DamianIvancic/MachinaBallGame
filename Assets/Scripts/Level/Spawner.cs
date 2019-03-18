using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {


    public GameObject ObjectToSpawn;

    public void SpawnObject()
    {
        GameObject ObjectClone = Instantiate(ObjectToSpawn, transform.position, Quaternion.identity);
        ObjectClone.transform.SetParent(transform);
    }
}
