using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //Sistema de movimentação e interacao com o fogo
    private Camera cam;
    private NavMeshAgent agent;

    private Animator animator;

    [SerializeField] private LayerMask layerFire;

    private Coroutine putOutFire, interactWithAnimal;

    private bool isExtinguishing = false;

    private bool isInteractingWithSomething = false;

    private bool onWater = false;

    //Sistema de itens
    private enum itens { none, drone, satellite, sprinkler, waterBomber }
    private string itemName = "none";

    [SerializeField]
    private GameObject drone_Pref, satellite_Obj, sprinkler_Pref, waterBomber_Pref;

    Vector3 ScreenPosition, WorldPosition;

    private bool isSelecting;

    [SerializeField] GameObject itemPreview;

    [SerializeField] Sprite item1, item2, item3, item4;

    private void Start()
    {
        cam = Camera.main;
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        itemPreview.SetActive(false);
    }

    void Update()
    {
        InteractOrMove();
        ItemCursor();

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (GameManager.droneCooldown >= 15)
            {
                SetItens(itens.drone);
                isSelecting = true;
                itemPreview.SetActive(true);
                itemPreview.GetComponent<SpriteRenderer>().sprite = item1;
            }


        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (GameManager.satelliteCooldown >= 15)
            {
                SetItens(itens.satellite);
                isSelecting = true;
                itemPreview.SetActive(true);
                itemPreview.GetComponent<SpriteRenderer>().sprite = item2;
            }

        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (GameManager.sprinklerCooldown >= 15)
            {
                SetItens(itens.sprinkler);
                isSelecting = true;
                itemPreview.SetActive(true);
                itemPreview.GetComponent<SpriteRenderer>().sprite = item3;
            }

        }


        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (GameManager.planeCooldown >= 15)
            {
                SetItens(itens.waterBomber);
                isSelecting = true;
                itemPreview.SetActive(true);
                itemPreview.GetComponent<SpriteRenderer>().sprite = item4;
            }

        }


        if (Input.GetMouseButton(1))
        {
            SpawnItens();
        }

        if (HasAgentStopped())
            animator.SetBool("Walking", false);



        if (onWater)
        {
            agent.speed = 2.5f;
        }
        else
        {
            agent.speed = 3.5f;
        }


    }

    void ItemCursor()
    {
        ScreenPosition = Input.mousePosition;
        //ScreenPosition.z = Camera.main.nearClipPlane + 1;
        ScreenPosition.z = -18;
        WorldPosition = Camera.main.ScreenToWorldPoint(ScreenPosition);

        itemPreview.transform.position = WorldPosition;
    }

    #region Sistema de movimentacao e interacao com o fogo
    //Método que recebe o input de movimentação/interação do personagem
    void InteractOrMove()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitMouse;

            if (Physics.Raycast(ray, out hitMouse))
            {
                Vector3 dir = transform.forward;
                RaycastHit hit;

                //Verificando se a layer que o raio colidiu
                if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), dir, out hit, 2.5f))
                {
                    //Layer do fogo
                    if (hit.collider.gameObject.layer == 7)
                    {
                        Move();

                        ExtinguishFire(hit.collider.gameObject);
                    }
                    //Layer do animal assustado
                    else if (hit.collider.gameObject.layer == 10)
                    {
                        Move();

                        interactWithAnimal = StartCoroutine(InteractWithAnimal(hit.collider.gameObject));

                        ExtinguishFire(null);
                    }
                    //Qualquer outra layer
                    else
                    {
                        Move();

                        ExtinguishFire(null);
                    }
                }
                //Não colidiu com nada
                else
                {
                    Move();

                    ExtinguishFire(null);
                }
            }
        }

        if (Input.GetMouseButtonUp(0) && putOutFire != null)
            StopCoroutine(putOutFire);
    }

    //Método que movimenta o personagem
    void Move()
    {
        if (!isInteractingWithSomething)
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                agent.SetDestination(hit.point);
            }

            animator.SetBool("Walking", true);
        }
        else
        {
            agent.SetDestination(this.transform.position);
            animator.SetBool("Walking", false);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.layer == 4)
        {
            onWater = true;
        }


    }

    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.layer == 4)
        {
            onWater = true;
        }

    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.layer == 4)
        {
            onWater = false;
        }
    }

    bool HasAgentStopped()
    {
        return !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance;
    }

    //Método que apaga o fogo que o personagem está vendo
    void ExtinguishFire(GameObject objectBurning)
    {
        //Iniciando a coroutine que apaga o fogo
        if (objectBurning != null && !isExtinguishing)
        {
            putOutFire = StartCoroutine(PutOutFire(2.5f, objectBurning));
        }
        //Parando a coroutine antes dela ser finalizada, caso o objeto não esteja mais ao alcance do jogador
        else if (objectBurning == null && isExtinguishing)
        {
            StopCoroutine(putOutFire);
            isExtinguishing = false;
            isInteractingWithSomething = false;
            animator.SetBool("Extinguish", false);
        }
    }

    IEnumerator PutOutFire(float waitSeconds, GameObject objectBurning)
    {
        isExtinguishing = true;
        isInteractingWithSomething = true;
        animator.SetBool("Extinguish", true);

        yield return new WaitForSeconds(waitSeconds);

        objectBurning.GetComponent<TreeController>().StopBurn();
        isExtinguishing = false;
        isInteractingWithSomething = false;
        animator.SetBool("Extinguish", false);
    }

    IEnumerator InteractWithAnimal(GameObject animal)
    {
        isInteractingWithSomething = true;
        animal.GetComponent<AnimalsController>().Interacting();

        yield return new WaitForSeconds(1);

        isInteractingWithSomething = false;
        interactWithAnimal = null;
        animal.GetComponent<AnimalsController>().RunAway();
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

            Debug.Log(itemPos);

            switch (itemName)
            {
                case "drone":
                    GameObject drone_Obj = Instantiate(drone_Pref, new Vector3(transform.position.x, drone_Pref.transform.position.y, transform.position.z), quaternion.identity);
                    drone_Obj.GetComponent<DroneController>().targetPos = itemPos;
                    itemName = itens.none.ToString();
                    GameManager.droneCooldown = 0;
                    itemPreview.SetActive(false);

                    break;
                case "satellite":
                    satellite_Obj.GetComponent<SatelliteController>().ActivateSatellite();
                    itemName = itens.none.ToString();
                    GameManager.satelliteCooldown = 0;
                    itemPreview.SetActive(false);
                    break;

                case "sprinkler":
                    Instantiate(sprinkler_Pref, new Vector3(itemPos.x, itemPos.y, itemPos.z), quaternion.identity);
                    itemName = itens.none.ToString();
                    GameManager.sprinklerCooldown = 0;
                    itemPreview.SetActive(false);
                    break;

                case "waterBomber":
                    Instantiate(waterBomber_Pref, new Vector3(itemPos.x, 3.15f, itemPos.z), quaternion.identity);
                    itemName = itens.none.ToString();
                    GameManager.planeCooldown = 0;
                    itemPreview.SetActive(false);
                    break;
            }
        }
    }
    #endregion
}