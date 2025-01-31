using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;

public class Airplane : MonoBehaviour
{

    [SerializeField] private NavMeshAgent agent;

    [SerializeField] GameObject waterBomber;

    bool hasDropped;

    float distance;

    float spd = 50f;

    void Start()
    {
        // agent.SetDestination(new Vector3(Signalizer.signalizerLocation.x, 10, Signalizer.signalizerLocation.z));
        GameObject newAirplane = Instantiate(waterBomber, Signalizer.signalizerLocation, quaternion.identity);
        // transform.rotation = Quaternion.Euler(0, -180, 0);
        Destroy(gameObject, 6f);
    }


    void Update()
    {
        transform.Translate(1 * spd * Time.fixedDeltaTime, 0, 0);

        float dist = Signalizer.signalizerLocation.x - this.transform.position.x;
        distance = Mathf.Sqrt(Mathf.Pow(dist, 2));

    }


}
