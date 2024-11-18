using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] treesObj;

    void Start()
    {
        StartCoroutine(SpawnFire(5));
    }

    IEnumerator SpawnFire(float waitTime)
    {
        while (true)
        {
            // Seleciona uma árvore aleatória
            int indexTree = Random.Range(0, treesObj.Length);
            if (treesObj[indexTree] == null)
            {
                yield return null;
                continue;
            }

            var tree = treesObj[indexTree].GetComponent<TreeController>();

            // Verifica se a árvore é válida e não está queimando ou prestes a queimar
            if (tree == null || tree.isBurning || tree.isNextToBurn)
            {
                yield return null;
                continue;
            }

            // Aguarda o tempo definido
            yield return new WaitForSeconds(waitTime);

            // Marca a árvore como próxima a queimar, se ela ainda existir
            if (tree != null)
            {
                tree.isNextToBurn = true;
            }
        }
    }
}
