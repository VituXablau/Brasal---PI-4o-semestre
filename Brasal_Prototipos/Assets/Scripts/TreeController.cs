using System.Collections;
using UnityEngine;

public class TreeController : MonoBehaviour
{
    MeshRenderer meshRenderer;

    [SerializeField] Material treeMaterial, fireMaterial;

    [SerializeField] LayerMask treeLayer;

    //Bool que retorna se a árvore é a próxima a queimar
    [HideInInspector] public bool isNextToBurn = false;
    //Bool que retorna se a árvore está queimando
    public bool burnImmediately = false, isBurning = false;

    private Coroutine burning;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        if (burnImmediately && !isBurning)
            burning = StartCoroutine(Burn());

        //Fazendo a árvore queimar quando ela for a próxima a queimar, mas ainda não estiver queimando
        if (isNextToBurn && !isBurning)
            StartCoroutine(StartBurn(5));
    }

    //Coroutine que dá início no fogo da árvore
    IEnumerator StartBurn(float waitTime)
    {
        isBurning = true;

        yield return new WaitForSeconds(waitTime);

        burning = StartCoroutine(Burn());
    }

    //Coroutine que faz a árvore queimar
    IEnumerator Burn()
    {
        gameObject.layer = 7;
        meshRenderer.material = fireMaterial;
        isNextToBurn = false;
        burnImmediately = false;

        int randomTime;
        randomTime = Random.Range(3, 6);

        yield return new WaitForSeconds(randomTime);

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 3, treeLayer);
        foreach (var hitCollider in hitColliders)
        {
            int random;
            random = Random.Range(0, 3);

            Debug.Log(random);

            if (random != 0)
                hitCollider.gameObject.GetComponent<TreeController>().burnImmediately = true;
        }

        yield return new WaitForSeconds(2.5f);
        Destroy(gameObject);
    }

    //Método que faz a árvore parar de queimar
    public void StopBurn()
    {
        gameObject.layer = 0;
        meshRenderer.material = treeMaterial;
        isBurning = false;

        StopCoroutine(burning);
    }

    //Método que destrói a árvore depois que ela queima
    void Burned()
    {

    }
}
