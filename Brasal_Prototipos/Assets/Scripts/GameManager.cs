using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameObject[] treesObj, animalsObj;

    private int init_treesObjLength;
    [HideInInspector] public int cur_treesObjLength;

    private int totalAnimalsObj;
    [HideInInspector] public int savedAnimalsObj;

    [SerializeField] private float spawnFireTime;

    private int levelCurTime_Min = 2, levelCurTime_Sec;
    private Coroutine timer;

    [SerializeField] private TextMeshProUGUI percentage_text, numAnimals_text, timer_text, congratulations_text;
    private float percentage;

    private bool endGame = false;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        StartCoroutine(PrepareNextToBurn(spawnFireTime));
        timer = StartCoroutine(Timer());

        init_treesObjLength = treesObj.Length;
        cur_treesObjLength = init_treesObjLength;

        totalAnimalsObj = animalsObj.Length;

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
            Time.timeScale = 0;
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

            string message;

            if (levelCurTime_Min == 0 && levelCurTime_Sec == 0 && percentage >= 50)
            {
                message = "Você conseguiu a medalha de conclusão";

                if (percentage >= 75)
                    message += " + a de preservação";

                if (savedAnimalsObj == totalAnimalsObj)
                    message += " + a dos animais";

                message += "! Meus parabéns";

                endGame = true;
                congratulations_text.text = message;
                StopCoroutine(timer);

                Time.timeScale = 0;
            }

            if (levelCurTime_Sec < 10)
                timer_text.text = levelCurTime_Min + ":0" + levelCurTime_Sec;
            else
                timer_text.text = levelCurTime_Min + ":" + levelCurTime_Sec;
        }
    }

    public void UpdateAnimals()
    {
        savedAnimalsObj++;

        numAnimals_text.text = "Animais salvos: " + savedAnimalsObj;

        if (savedAnimalsObj == totalAnimalsObj)
        {
            numAnimals_text.text = "Todos os animais foram salvos";
        }
    }

    IEnumerator PrepareNextToBurn(float waitTime)
    {
        while (true)
        {
            // Seleciona uma árvore aleatória
            int indexTree = Random.Range(0, treesObj.Length);

            var tree = treesObj[indexTree].GetComponent<TreeController>();

            // Verifica se a árvore é válida e não está queimando se prestes a queimar ou se já queimou
            if (!tree.isBurning && !tree.isNextToBurn)
            {
                yield return new WaitForSeconds(waitTime);

                // Marca a árvore como próxima a queimar, se ela ainda existir
                if (tree != null)
                {
                    tree.isNextToBurn = true;
                }
            }
        }
    }
}
