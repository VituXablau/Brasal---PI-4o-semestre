using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    //Sistema de movimentação e interacao com o fogo
    [SerializeField] private Camera cam;
    [SerializeField] private NavMeshAgent agent;

    [SerializeField] private LayerMask layerFire;

    private Coroutine putOutFire;
    private bool isExtinguishing = false;

    //Sistema de itens
    private enum itens { none, drone, satellite, sprinkler, waterBomber }
    private string itemName = "none";

    [SerializeField]
    private GameObject drone_Pref, satellite_Obj, sprinkler_Pref, waterBomber_Pref;

    void Update()
    {
        InteractOrMove();

        if (Input.GetKeyDown(KeyCode.Alpha1))
            SetItens(itens.drone);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            SetItens(itens.satellite);
        if (Input.GetKeyDown(KeyCode.Alpha3))
            SetItens(itens.sprinkler);
        if (Input.GetKeyDown(KeyCode.Alpha4))
            SetItens(itens.waterBomber);

        if (Input.GetMouseButton(1))
        {
            SpawnItens();
        }
    }

    #region Sistema de movimentacao e interacao com o fogo
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
    #endregion

    #region Sistema dos itens
    void SetItens(itens item)
    {
        itemName = item.ToString();
    }

    void SpawnItens()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 itemPos;
            itemPos = hit.point;

            switch (itemName)
            {
                case "drone":
                    GameObject drone_Obj = Instantiate(drone_Pref, new Vector3(transform.position.x, drone_Pref.transform.position.y, transform.position.z), quaternion.identity);
                    drone_Obj.GetComponent<DroneController>().targetPos = itemPos;
                    itemName = itens.none.ToString();
                    break;
                case "satellite":
                    satellite_Obj.GetComponent<SatelliteController>().ActivateSatellite();
                    itemName = itens.none.ToString();
                    break;
                case "sprinkler":
                    Instantiate(sprinkler_Pref, new Vector3(itemPos.x, sprinkler_Pref.transform.position.y, itemPos.z), quaternion.identity);
                    itemName = itens.none.ToString();
                    break;
                case "waterBomber":
                    Instantiate(waterBomber_Pref, new Vector3(itemPos.x, waterBomber_Pref.transform.position.y, itemPos.z), quaternion.identity);
                    itemName = itens.none.ToString();
                    break;
            }
        }
    }
    #endregion
}