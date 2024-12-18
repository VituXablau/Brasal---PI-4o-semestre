using System.Collections;
using UnityEngine;

public class TreeController : MonoBehaviour
{
    MeshRenderer meshRenderer;

    [SerializeField] Material treeMaterial, fireMaterial, burnedMaterial, satelliteViewMaterial;

    [SerializeField] LayerMask treeLayer;

    //Bool que retorna se a árvore é a próxima a queimar
    [HideInInspector] public bool isNextToBurn = false;
    //Bool que retorna se a árvore está queimando
    public bool burnImmediately = false, isBurning = false;

    [HideInInspector] public bool burned = false;

    private Coroutine burning;

    [SerializeField] private int minTime, maxTime;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        if (burnImmediately && !isBurning && !burned)
            burning = StartCoroutine(Burn(minTime, maxTime));

        //Fazendo a árvore queimar quando ela for a próxima a queimar, mas ainda não estiver queimando
        if (isNextToBurn && !isBurning && !burned)
            StartCoroutine(StartBurn(5));
    }

    //Coroutine que dá início no fogo da árvore
    IEnumerator StartBurn(float waitTime)
    {
        isBurning = true;

        yield return new WaitForSeconds(waitTime);

        burning = StartCoroutine(Burn(minTime, maxTime));
    }

    //Coroutine que faz a árvore queimar
    IEnumerator Burn(int minTime, int maxTime)
    {
        gameObject.layer = 7;
        meshRenderer.material = fireMaterial;
        isNextToBurn = false;
        burnImmediately = false;

        int randomTime;
        randomTime = Random.Range(minTime, maxTime);

        yield return new WaitForSeconds(randomTime);

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 3, treeLayer);
        foreach (var hitCollider in hitColliders)
        {
            int random;
            random = Random.Range(0, 2);

            if (random != 0)
                hitCollider.gameObject.GetComponent<TreeController>().burnImmediately = true;
        }

        yield return new WaitForSeconds(7.5f);

        Burned();
    }

    //Método que faz a árvore parar de queimar
    public void StopBurn()
    {
        gameObject.layer = 8;
        meshRenderer.material = treeMaterial;
        isBurning = false;

        StopCoroutine(burning);
    }

    //Método que destrói a árvore depois que ela queima
    void Burned()
    {
        gameObject.layer = 8;
        isBurning = false;
        burned = true;
        meshRenderer.material = burnedMaterial;

        GameManager.Instance.cur_treesObjLength--;
        GameManager.Instance.CheckPercentage();
    }

    public void ShowNextToBurn()
    {
        meshRenderer.material = satelliteViewMaterial;
    }
}
