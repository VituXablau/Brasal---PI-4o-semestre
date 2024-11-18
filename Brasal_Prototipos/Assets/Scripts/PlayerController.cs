using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private NavMeshAgent agent;

    [SerializeField] private LayerMask layerFire;

    private Coroutine putOutFire;
    private bool isExtinguishing = false;

    void FixedUpdate()
    {
        InteractOrMove();
    }

    //Método que recebe o input de movimentação/interação do personagem
    void InteractOrMove()
    {
        if (Input.GetMouseButton(0))
        {
            DetectingFire(2.5f, layerFire);
        }
    }

    //Método que verifica se existe fogo na frente do personagem
    void DetectingFire(float lengthOfRay, LayerMask fireLayer)
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, lengthOfRay, fireLayer))
        {
            Move(false, hit);
            ExtinguishFire(hit.collider.gameObject);
        }
        else
        {
            Move(true, hit);
            ExtinguishFire(null);
        }
    }

    //Método que movimenta o personagem
    void Move(bool move, RaycastHit hitDetectingFire)
    {
        if (move)
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                agent.SetDestination(hit.point);
            }
        }
        else
        {
            agent.SetDestination(this.transform.position);
        }
    }

    //Método que apaga o fogo que o personagem está vendo
    void ExtinguishFire(GameObject objectBurning)
    {
        if (objectBurning != null && !isExtinguishing)
        {
            putOutFire = StartCoroutine(PutOutFire(2.5f, objectBurning));
        }
        else if (objectBurning == null && isExtinguishing)
        {
            StopCoroutine(putOutFire);
            isExtinguishing = false;
        }
    }

    IEnumerator PutOutFire(float waitSeconds, GameObject objectBurning)
    {
        isExtinguishing = true;

        yield return new WaitForSeconds(waitSeconds);

        objectBurning.GetComponent<TreeController>().StopBurn();
        isExtinguishing = false;
    }
}
