using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Signalizer : MonoBehaviour
{
    [SerializeField] GameObject airplane, airplaneSpawnLocation;
    Vector3 airplaneSpawn;

    public static Vector3 signalizerLocation;



    void Start()
    {
        signalizerLocation = this.transform.position;

        GameObject newAirplane = Instantiate(airplane, new Vector3(-30, 15, signalizerLocation.z), quaternion.identity);

        Destroy(gameObject, 5f);        

    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
