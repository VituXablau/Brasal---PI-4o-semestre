using UnityEngine;

public class SprinklerController : MonoBehaviour
{
    [SerializeField] private float radiusCol;

    [SerializeField] private LayerMask layerFire;

    [SerializeField] GameObject waterParticles;

    void Update()
    {
        SearchingFire();
    }

    void SearchingFire()
    {
        Collider[] hitTrees = Physics.OverlapSphere(transform.position, radiusCol, layerFire);

        if (hitTrees.Length > 0)
            ExtinguishFire(hitTrees);
    }

    void ExtinguishFire(Collider[] trees)
    {
        foreach (Collider tree in trees)
        {
            waterParticles.SetActive(true);
            tree.GetComponent<TreeController>().StartCoroutine(tree.GetComponent<TreeController>().StopBurn(0));
            //tree.gameObject.GetComponent<TreeController>().StopBurn();
        }

        Destroy(gameObject, 5);
    }
}
