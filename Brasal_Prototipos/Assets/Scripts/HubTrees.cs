using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HubTrees : MonoBehaviour
{

    [SerializeField] private Camera _mainCamera;

    private void LateUpdate()
    {

        Vector3 cameraPosition = _mainCamera.transform.position;

        cameraPosition.y = transform.position.y;

        transform.LookAt(cameraPosition);

        transform.Rotate(0f, 180f, 0f);
    }
}
