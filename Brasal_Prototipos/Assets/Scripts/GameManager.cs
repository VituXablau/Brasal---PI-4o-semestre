using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameObject[] treesObj;

    private int init_treesObjLength;
    [HideInInspector] public int cur_treesObjLength;

    [SerializeField] private float spawnFireTime;

    private int levelCurTime_Min = 2, levelCurTime_Sec;
    private Coroutine timer;

    [SerializeField] private TextMeshProUGUI percentage_text, timer_text;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        StartCoroutine(SpawnFire(spawnFireTime));
        timer = StartCoroutine(Timer());

        init_treesObjLength = treesObj.Length;
        cur_treesObjLength = init_treesObjLength;

        CheckPercentage();
    }

    public void CheckPercentage()
    {
        float x;
        x = (100 * cur_treesObjLength) / init_treesObjLength;

        if (x < 50)
            SceneManager.LoadScene(0);

        percentage_text.text = "Preservação = " + x + "%";
    }

    IEnumerator Timer()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);

            levelCurTime_Sec--;

            if (levelCurTime_Sec < 0)
            {
                levelCurTime_Min--;
                levelCurTime_Sec = 59;
            }

            if (levelCurTime_Min == 0)
            {
                Debug.Log("Venceu");
                StopCoroutine(timer);
            }

            if (levelCurTime_Sec < 10)
                timer_text.text = levelCurTime_Min + ":0" + levelCurTime_Sec;
            else
                timer_text.text = levelCurTime_Min + ":" + levelCurTime_Sec;
        }
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

            // Verifica se a árvore é válida e não está queimando se prestes a queimar ou se já queimou
            if (tree == null || tree.isBurning || tree.isNextToBurn || tree.burned)
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
