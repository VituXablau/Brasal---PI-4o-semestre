using UnityEngine;

public class SatelliteController : MonoBehaviour
{
    [SerializeField] private GameObject[] treesObj;

    public void ActivateSatellite()
    {
        foreach (GameObject tree in treesObj)
        {
            if (tree.GetComponent<TreeController>().isNextToBurn == true)
            {
                tree.GetComponent<TreeController>().ShowNextToBurn();
            }
        }
    }
}
