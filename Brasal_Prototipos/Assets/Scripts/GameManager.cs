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

    [SerializeField] private TextMeshProUGUI percentage_text, timer_text, congratulations_text;
    private float percentage;

    private bool endGame = false;

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

    private void Update()
    {
        if (endGame && Input.GetKey(KeyCode.R))
            SceneManager.LoadScene(0);
    }

    public void CheckPercentage()
    {
        percentage = (100 * cur_treesObjLength) / init_treesObjLength;

        if (percentage < 50)
        {
            congratulations_text.text = "Você perdeu!";
            endGame = true;
        }

        percentage_text.text = "Preservação = " + percentage + "%";
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

            if (levelCurTime_Min == 0 && levelCurTime_Sec == 0)
            {
                if (percentage >= 75)
                    congratulations_text.text = "Parabéns, você conseguiu duas medalhas!";
                else
                    congratulations_text.text = "Parabéns, você conseguiu uma medalha!";

                endGame = true;
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
