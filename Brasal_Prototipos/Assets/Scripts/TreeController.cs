using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeController : MonoBehaviour
{
    [SerializeField] LayerMask treeLayer;

    //Bool que retorna se a árvore é a próxima a queimar
    [HideInInspector] public bool isNextToBurn = false;
    //Bool que retorna se a árvore está queimando
    public bool burnImmediately = false, isBurning = false;

    private Coroutine burning, spreadFire;

    [SerializeField] private int minTime, maxTime;

    [SerializeField] private GameObject burnedTree_Pref, fireEffect_Pref;
    private GameObject fireEffect_Obj, objChild;

    private MeshRenderer treeRenderer, objChildRenderer;

    [SerializeField] private Material satelliteMaterial;
    private Material treeMaterial, objChildMaterial;

    void Start()
    {
        if (gameObject.transform.childCount > 0)
        {
            objChild = gameObject.transform.GetChild(0).gameObject;
            objChildRenderer = objChild.GetComponent<MeshRenderer>();
            objChildMaterial = objChildRenderer.material;
        }

        treeRenderer = GetComponent<MeshRenderer>();
        treeMaterial = treeRenderer.material;
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
        fireEffect_Obj = Instantiate(fireEffect_Pref, transform.position, Quaternion.identity);

        if (gameObject.transform.childCount > 0)
            objChildRenderer.material = objChildMaterial;
        treeRenderer.material = treeMaterial;

        isNextToBurn = false;
        burnImmediately = false;

        spreadFire = StartCoroutine(SpreadFire());

        yield return new WaitForSeconds(7.5f);

        Burned();
    }

    IEnumerator SpreadFire()
    {
        int randomTime = Random.Range(minTime, maxTime);

        yield return new WaitForSeconds(randomTime);

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 5, treeLayer);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider != null)
            {
                TreeController tree = hitCollider.gameObject.GetComponent<TreeController>();

                int random = Random.Range(0, 3);

                if (random != 0)
                    tree.burnImmediately = true;
            }
        }
    }

    //Método que faz a árvore parar de queimar
    public void StopBurn()
    {
        gameObject.layer = 8;
        Destroy(fireEffect_Obj);

        isBurning = false;

        StopCoroutine(burning);
        StopCoroutine(spreadFire);
    }

    //Método que destrói a árvore depois que ela queima
    void Burned()
    {
        foreach (GameObject tree in GameManager.Instance.treesObj)
        {
            if (tree == gameObject)
            {
                List<GameObject> list = new List<GameObject>(GameManager.Instance.treesObj);

                list.Remove(tree);

                GameManager.Instance.treesObj = list.ToArray();

                Destroy(gameObject);
                Destroy(fireEffect_Obj);
                Destroy(objChild);

                Instantiate(burnedTree_Pref, transform.position, transform.rotation);

                GameManager.Instance.cur_treesObjLength--;
                GameManager.Instance.CheckPercentage();
            }
        }
    }

    public void ShowNextToBurn()
    {
        if (gameObject.transform.childCount > 0)
            objChildRenderer.material = satelliteMaterial;
        treeRenderer.material = satelliteMaterial;
    }
}
