using System.Collections;
using UnityEngine;

public class DroneController : MonoBehaviour
{
    [HideInInspector]
    public Vector3 targetPos;

    [SerializeField]
    private float spd;

    private string modeName = "MovingToTarget";

    [SerializeField] private LayerMask layerFire;

    void Update()
    {
        switch (modeName)
        {
            case "MovingToTarget":
                MoveToTarget();
                break;
            case "SearchingFire":
                SearchingFire();
                break;
            case "Working":
                Working();
                break;
        }
    }

    void MoveToTarget()
    {
        if (transform.position.x != targetPos.x && transform.position.z != targetPos.z)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(targetPos.x, transform.position.y, targetPos.z), spd * Time.deltaTime);
        }
        else
            modeName = "SearchingFire";
    }

    void SearchingFire()
    {
        Collider[] hitTrees = Physics.OverlapSphere(transform.position, 2.5f, layerFire);

        if (hitTrees.Length > 0)
        {
            targetPos = hitTrees[0].transform.position;
            modeName = "Working";
        }
    }

    private void Working()
    {
        if (transform.position.x != targetPos.x && transform.position.z != targetPos.z)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(targetPos.x, transform.position.y, targetPos.z), spd * Time.deltaTime);
        }
        else
        {
            Collider[] hitTrees = Physics.OverlapSphere(transform.position, 1f, layerFire);

            if (hitTrees.Length > 0)
            {
                StartCoroutine(PutOutFire(1.5f, hitTrees[0].gameObject));
                Debug.Log(hitTrees[0].gameObject.name);
            }
        }
    }

    private IEnumerator PutOutFire(float waitSeconds, GameObject objectBurning)
    {
        yield return new WaitForSeconds(waitSeconds);

        objectBurning.GetComponent<TreeController>().StopBurn();

        yield return new WaitForSeconds(1);

        Destroy(gameObject);
    }
}
