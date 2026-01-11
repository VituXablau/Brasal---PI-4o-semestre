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

    [SerializeField] GameObject waterParticles;

    // Controle visual das esferas
    public float searchSphereRadius = 5f, workSphereRadius = 1f; // Raio da esfera em SearchingFire e Raio da esfera em Working

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
            transform.LookAt(new Vector3(targetPos.x, transform.position.y, targetPos.z));
        }
        else
            modeName = "SearchingFire";
    }

    void SearchingFire()
    {
        Collider[] hitTrees = Physics.OverlapSphere(transform.position, searchSphereRadius, layerFire);

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
            transform.LookAt(new Vector3(targetPos.x, transform.position.y, targetPos.z));
        }
        else
        {
            Collider[] hitTrees = Physics.OverlapSphere(transform.position, workSphereRadius, layerFire);

            if (hitTrees.Length > 0)
            {
                StartCoroutine(PutOutFire(1.5f, hitTrees[0].gameObject));
                Debug.Log(hitTrees[0].gameObject.name);
            }
        }
    }

    private IEnumerator PutOutFire(float waitSeconds, GameObject objectBurning)
    {
        waterParticles.SetActive(true);

        yield return new WaitForSeconds(waitSeconds);

        if (objectBurning != null)
            objectBurning.GetComponent<TreeController>().StartCoroutine(objectBurning.GetComponent<TreeController>().StopBurn(1));
        //objectBurning.GetComponent<TreeController>().StopBurn();

        waterParticles.SetActive(false);

        yield return new WaitForSeconds(1);

        Destroy(gameObject);
    }

    // Adicione este método para visualizar as esferas de overlap
    private void OnDrawGizmos()
    {
        // Esfera de busca (SearchingFire) - Cor Amarela
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, searchSphereRadius);

        // Esfera de trabalho (Working) - Cor Verde
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, workSphereRadius);

        // Linha para mostrar a posição alvo (opcional, mas útil)
        if (modeName == "Working" || modeName == "MovingToTarget")
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, targetPos);
            Gizmos.DrawWireSphere(targetPos, 0.5f);
        }
    }
}
