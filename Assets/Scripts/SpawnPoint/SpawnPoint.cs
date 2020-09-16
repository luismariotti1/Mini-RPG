using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public GameObject prefabToSpawn;
    public float reaptInterval;

    void Start()
    {
        if(reaptInterval > 0)
        {
            InvokeRepeating("SpawnObject", 0.0f, reaptInterval);
        }
    }

    void Update()
    {
        
    }

    public GameObject SpawnObject()
    {
        if(prefabToSpawn != null)
        {
            return (Instantiate(prefabToSpawn, transform.position, Quaternion.identity));
        }
        return null;
    }
}
