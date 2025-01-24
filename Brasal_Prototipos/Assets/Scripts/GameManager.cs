using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    [SerializeField] private TextMeshProUGUI percentage_text, numAnimals_text, timer_text, congratulations_text, stats_text, proceed_text, medals_text;
    private float percentage;
    private bool endGame = false, gameOver = false, winMedal, floraMedal, faunaMedal;

    [SerializeField] private Image droneIcon, satelliteIcon, sprinklerIcon, planeIcon, medal1, medal2, medal3;

    public static float droneCooldown, satelliteCooldown, sprinklerCooldown, planeCooldown;

    [SerializeField] GameObject endScreen, Medal_Console, HUDobj;

    string currentScene;

    [SerializeField] Sprite sp_medal1, sp_medal2, sp_medal3;



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

        droneCooldown = 0;
        satelliteCooldown = 0;
        sprinklerCooldown = 0;
        planeCooldown = 0;

        droneIcon.fillAmount = 0;

        satelliteIcon.fillAmount = 0;

        sprinklerIcon.fillAmount = 0;

        planeIcon.fillAmount = 0;

        currentScene = SceneManager.GetActiveScene().name;

        Medal_Console.SetActive(false);
        endScreen.SetActive(false);

        Time.timeScale = 1;
    }

    private void Update()
    {
        if (endGame && Input.GetKey(KeyCode.R))
            SceneManager.LoadScene(0);

        DisplayItems();
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(currentScene);
    }

    public void Menu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Proceed()
    {
        if (gameOver)
        {
            SceneManager.LoadScene("MainMenu");
        }
        else
        {
            Debug.Log("comiig sun");
        }
    }

    public void CheckPercentage()
    {
        percentage = (100 * cur_treesObjLength) / init_treesObjLength;

        if (percentage < 50)
        {
            congratulations_text.text = "Você perdeu!";
            endGame = true;
            gameOver = true;
            proceed_text.text = "Menu";
            Time.timeScale = 0;
            HUDobj.SetActive(false);
            endScreen.SetActive(true);
        }

        percentage_text.text = "Preservação: " + percentage + "%";
    }

    void DisplayItems()
    {
        droneIcon.fillAmount = droneCooldown / 15;

        satelliteIcon.fillAmount = satelliteCooldown / 15;

        sprinklerIcon.fillAmount = sprinklerCooldown / 15;

        planeIcon.fillAmount = planeCooldown / 15;
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

            if (droneCooldown == 0)
                StartCoroutine(DroneCounter());

            if (satelliteCooldown == 0)
                StartCoroutine(SatelliteCounter());

            if (sprinklerCooldown == 0)
                StartCoroutine(SprinklerCounter());

            if (planeCooldown == 0)
                StartCoroutine(PlaneCounter());


            //string message;

            if (levelCurTime_Min == 0 && levelCurTime_Sec == 0 && percentage >= 50)
            {
                // message = "Você conseguiu a medalha de conclusão";

                // if (percentage >= 75)
                //     message += " + a de preservação";

                // if (savedAnimalsObj == totalAnimalsObj)
                //     message += " + a dos animais";

                // message += "! Meus parabéns";

                winMedal = true;
                endGame = true;
                congratulations_text.text = "Você Ganhou!";
                proceed_text.text = "Prosseguir";


                if (!faunaMedal)
                    stats_text.text = "" + percentage + "% de preservação\n" + savedAnimalsObj + " animais foram salvos!";
                else
                    stats_text.text = "" + percentage + "% de preservação\n Todos animais foram salvos!";

                if (percentage >= 75)
                    floraMedal = true;

                if (savedAnimalsObj == totalAnimalsObj)
                    faunaMedal = true;

                if ((!faunaMedal) && (!floraMedal))
                    medals_text.text = "Você conseguiu: Medalha de conclusão!";

                if ((faunaMedal) && (!floraMedal))
                    medals_text.text = "Você conseguiu: Medalha de conclusão! Medalha de Fauna!";

                if ((!faunaMedal) && (floraMedal))
                    medals_text.text = "Você conseguiu: Medalha de conclusão! Medalha de Preservação!";

                if ((faunaMedal) && (floraMedal))
                    medals_text.text = "Você conseguiu: Medalha de conclusão! Medalha de Preservação! Medalha de Fauna!";


                medal1.GetComponent<Image>().sprite = sp_medal1;

                if (floraMedal)
                    medal2.GetComponent<Image>().sprite = sp_medal2;
                if (faunaMedal)
                    medal3.GetComponent<Image>().sprite = sp_medal3;


                Medal_Console.SetActive(true);
                endScreen.SetActive(true);
                   HUDobj.SetActive(false);

                StopCoroutine(timer);

                Time.timeScale = 0;
            }

            if (levelCurTime_Sec < 10)
                timer_text.text = levelCurTime_Min + ":0" + levelCurTime_Sec;
            else
                timer_text.text = levelCurTime_Min + ":" + levelCurTime_Sec;
        }
    }

    void EndgameWindow()
    {

    }

    public void UpdateAnimals()
    {
        savedAnimalsObj++;

        switch (savedAnimalsObj)
        {
            case 0:
                numAnimals_text.text = "Animais: [ ] [ ] [ ]";
                break;
            case 1:
                numAnimals_text.text = "Animais: [x] [ ] [ ]";
                break;
            case 2:
                numAnimals_text.text = "Animais: [x] [x] [ ]";
                break;
            case 3:
                numAnimals_text.text = "Animais: [x] [x] [x]";
                break;
        }



        // if (savedAnimalsObj == totalAnimalsObj)
        // {
        //     numAnimals_text.text = "Todos os animais foram salvos";
        // }
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

    IEnumerator DroneCounter()
    {
        while (droneCooldown < 15)
        {
            yield return new WaitForSeconds(1);
            droneCooldown++;
        }

    }

    IEnumerator SatelliteCounter()
    {
        while (satelliteCooldown < 15)
        {
            yield return new WaitForSeconds(1);
            satelliteCooldown++;

        }
    }

    IEnumerator SprinklerCounter()
    {
        while (sprinklerCooldown < 15)
        {
            yield return new WaitForSeconds(1);
            sprinklerCooldown++;

        }
    }

    IEnumerator PlaneCounter()
    {
        while (planeCooldown < 15)
        {
            yield return new WaitForSeconds(1);
            planeCooldown++;

        }
    }
}
