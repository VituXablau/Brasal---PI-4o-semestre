using UnityEngine;

public class WaterBomberController : MonoBehaviour
{
    [SerializeField] private LayerMask layerFire;

    private void Start()
    {
        Destroy(gameObject, 4);
    }

    private void Update()
    {
        if (transform.localScale.x > 0)
            ExtinguishFire();
    }

    void ExtinguishFire()
    {
        Collider[] hitTrees = Physics.OverlapSphere(transform.position, transform.localScale.x / 2, layerFire);

        if (hitTrees.Length > 0)
        {
            foreach (Collider tree in hitTrees)
            {
                tree.GetComponent<TreeController>().StartCoroutine(tree.GetComponent<TreeController>().StopBurn(1));
                //tree.GetComponent<TreeController>().StopBurn();
            }
        }
    }
}
