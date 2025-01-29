using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuTrees : MonoBehaviour
{
   [SerializeField] private GameObject _mainCamera;

    private void LateUpdate()
    {

        Vector3 cameraPosition = _mainCamera.transform.position;

        cameraPosition.y = transform.position.y;

        transform.LookAt(cameraPosition);

        transform.Rotate(0f, 180f, 0f);
    }
}
