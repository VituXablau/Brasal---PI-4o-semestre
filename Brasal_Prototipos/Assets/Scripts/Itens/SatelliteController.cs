using UnityEngine;

public class SatelliteController : MonoBehaviour
{
    public void ActivateSatellite()
    {
        foreach (GameObject tree in GameManager.Instance.treesObj)
        {
            if (tree.GetComponent<TreeController>().isNextToBurn == true && tree.gameObject != null)
            {
                tree.GetComponent<TreeController>().ShowNextToBurn();
            }
        }
    }
}
