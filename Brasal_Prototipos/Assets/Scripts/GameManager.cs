using System.Collections;
using System.Data;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;
using Mono.Data.Sqlite;

public class GameManager : MonoBehaviour
{
    private readonly string dbfile = "URI=file:Brasal.db.db";
    private IDbConnection conexao;
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
    private bool winMedal, floraMedal, faunaMedal, pause, tutorial;

    public static bool endGame = false, gameOver = false;

    [SerializeField] private Image droneIcon, satelliteIcon, sprinklerIcon, planeIcon, medal1, medal2, medal3;

    public static float droneCooldown, satelliteCooldown, sprinklerCooldown, planeCooldown;

    [SerializeField] GameObject endScreen, pauseScreen, Medal_Console, HUDobj, notPanel, pausenotPanel, player, continueButton, fakePlayer, esc, transitionScreen;
    [SerializeField] GameObject[] earnedMedals, fakeAnimals;

    string currentScene, nextScene, query;

    bool pausable;
    int num;

    [SerializeField] Sprite sp_medal1, sp_medal2, sp_medal3;


    public static bool[]
   medals_MataAtlantica = new bool[3],
   medals_Pantanal = new bool[3],
   medals_Amazonia = new bool[3],
   medals_Cerrado = new bool[3],
   medals_Caatinga = new bool[3];

    public static float[] maxPercentage = { 0, 0, 0, 0, 0 };
    public static int[] maxAnimals = { 0, 0, 0, 0, 0 };

    private void Awake()
    {
        Conectar();

        Instance = this;

        currentScene = SceneManager.GetActiveScene().name;

    }

    void Start()
    {   
        endGame = false;
        gameOver = false;
        pausable = false;
        transitionScreen.SetActive(true);
        StartCoroutine(Beginning());

        if (currentScene == "MataAtlanticaTutorial")
        {
            tutorial = true;

            for (int i = 0; i < animalsObj.Length; i++)
            {
                animalsObj[i].SetActive(false);
            }

            player.SetActive(false);
            Medal_Console.SetActive(false);
            endScreen.SetActive(false);
            pauseScreen.SetActive(false);

        }
        else
        {
            StartCoroutine(PrepareNextToBurn(spawnFireTime));
            timer = StartCoroutine(Timer());


        }

        Time.timeScale = 1;
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



        for (int i = 0; i < earnedMedals.Length; i++)
        {
            earnedMedals[i].SetActive(false);
        }
        Medal_Console.SetActive(false);
        endScreen.SetActive(false);
        pauseScreen.SetActive(false);
    }

    private void Update()
    {
        DisplayItems();
        Pause();
    }

    IEnumerator Beginning()
    {
        yield return new WaitForSeconds(1f);
        transitionScreen.GetComponent<Animator>().SetTrigger("disappear");
        yield return new WaitForSeconds(0.3f);
        transitionScreen.SetActive(false);
        pausable = true;
    }

    public void RestartLevel()
    {
        if (pausable)
        {
            Time.timeScale = 1f;

            if (pause)
            {
                //pausenotPanel.GetComponent<Animator>().SetInteger("estado", 0);
            }
            else
            {
                notPanel.GetComponent<Animator>().SetInteger("estado", 0);
                Medal_Console.GetComponent<Animator>().SetInteger("estado", 0);

                for (int i = 0; i < earnedMedals.Length; i++)
                {
                    earnedMedals[i].SetActive(false);
                }
            }

            transitionScreen.SetActive(true);
            transitionScreen.GetComponent<Animator>().SetTrigger("appear");

            StartCoroutine(GoToRestart());
        }


    }

    IEnumerator GoToRestart()
    {

        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(currentScene);

    }

    IEnumerator GoToMenu()
    {
        transitionScreen.SetActive(true);
        transitionScreen.GetComponent<Animator>().SetTrigger("appear");
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Menu");
    }

    void Pause()
    {
        if ((!pause) & (!endGame))
        {
            if (!tutorial)
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    if (pausable)
                    {
                        pauseScreen.SetActive(true);
                        pausenotPanel.GetComponent<Animator>().SetInteger("estado", 1);
                        pause = true;
                        pausable = false;
                        StartCoroutine(PauseGame());
                    }

                }
            }

        }

    }
    IEnumerator PauseGame()
    {
        yield return new WaitForSeconds(0.3f);
        pausable = true;
        Time.timeScale = 0;
    }

    public void Unpause()
    {
        if (pause && pausable)
        {
            pausable = false;
            Time.timeScale = 1;
            pausenotPanel.GetComponent<Animator>().SetInteger("estado", 0);
            StartCoroutine(DeactivatePause());
        }

    }

    public void EndTutorial()
    {
        StartCoroutine(PrepareNextToBurn(spawnFireTime));
        timer = StartCoroutine(Timer());
        for (int i = 0; i < animalsObj.Length; i++)
        {
            animalsObj[i].SetActive(true);
        }

        player.SetActive(true);

        for (int i = 0; i < fakeAnimals.Length; i++)
        {
            fakeAnimals[i].SetActive(false);
        }

        fakePlayer.SetActive(false);

        continueButton.GetComponent<Animator>().SetTrigger("disappear");

        esc.GetComponent<Animator>().SetTrigger("appear");

        tutorial = false;
    }

    IEnumerator DeactivatePause()
    {
        yield return new WaitForSeconds(0.2f);
        pauseScreen.SetActive(false);
        pause = false;
        pausable = true;

    }

    public void Menu()
    {
        if (pausable)
        {
            Time.timeScale = 1f;
            if (pause)
            {
                pausenotPanel.GetComponent<Animator>().SetInteger("estado", 0);
            }
            else
            {
                notPanel.GetComponent<Animator>().SetInteger("estado", 0);
                Medal_Console.GetComponent<Animator>().SetInteger("estado", 0);

                for (int i = 0; i < earnedMedals.Length; i++)
                {
                    earnedMedals[i].SetActive(false);
                }
            }

            StartCoroutine(GoToMenu());
        }


    }

    public void Proceed()
    {
        if (gameOver)
        {
            StartCoroutine(GoToMenu());
        }
        else
        {
            switch (currentScene)
            {
                case "MataAtlanticaTutorial":
                    if (MenuManager.firstTimePlaying)
                    {
                        nextScene = "postMA";
                        StartCoroutine(Proceeding());
                    }
                    else if (!MenuManager.firstTimePlaying)
                    {
                        nextScene = "CutscenesRoom";
                        StartCoroutine(Proceeding());
                    }
                    break;

                case "MataAtlantica":

                    nextScene = "Hub";
                    StartCoroutine(Proceeding());

                    break;

                case "Pantanal":
                    if (MenuManager.firstTimePA)
                    {
                        nextScene = "postPA";
                        StartCoroutine(Proceeding());
                    }
                    else
                    {
                        nextScene = "Hub";
                        StartCoroutine(Proceeding());
                    }
                    break;


                case "Amazonia":
                    if (MenuManager.firstTimeAM)
                    {
                        nextScene = "postAM";
                        StartCoroutine(Proceeding());
                    }
                    else
                    {
                        nextScene = "Hub";
                        StartCoroutine(Proceeding());
                    }
                    break;

                case "Cerrado":
                    if (MenuManager.firstTimeCE)
                    {
                        nextScene = "postCE";
                        StartCoroutine(Proceeding());
                    }
                    else
                    {
                        nextScene = "Hub";
                        StartCoroutine(Proceeding());
                    }
                    break;

                case "Caatinga":
                    if (MenuManager.firstTimeCA)
                    {
                        nextScene = "postCA";
                        StartCoroutine(Proceeding());
                    }
                    else
                    {
                        nextScene = "Hub";
                        StartCoroutine(Proceeding());
                    }
                    break;
            }
        }
    }

    IEnumerator Proceeding()
    {
        transitionScreen.SetActive(true);
        transitionScreen.GetComponent<Animator>().SetTrigger("appear");
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(nextScene);
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
            StopCoroutine(PrepareNextToBurn(spawnFireTime));
            StopCoroutine(Timer());
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

                StopCoroutine(PrepareNextToBurn(spawnFireTime));
                StopCoroutine(Timer());


                if (!faunaMedal)
                    stats_text.text = "" + percentage + "% de preservação\n" + savedAnimalsObj + " animais foram salvos!";
                else
                    stats_text.text = "" + percentage + "% de preservação\n Todos animais foram salvos!";

                if (percentage >= 75)
                    floraMedal = true;

                if (savedAnimalsObj == totalAnimalsObj)
                    faunaMedal = true;

                if ((!faunaMedal) && (!floraMedal))
                    medals_text.text = "Você conseguiu: Medalha de Conclusão!";

                if ((faunaMedal) && (!floraMedal))
                    medals_text.text = "Você conseguiu: Medalha de Conclusão! Medalha de Proteção!";

                if ((!faunaMedal) && (floraMedal))
                    medals_text.text = "Você conseguiu: Medalha de Conclusão! Medalha de Preservação!";

                if ((faunaMedal) && (floraMedal))
                    medals_text.text = "Você conseguiu: Medalha de Conclusão! Medalha de Preservação! Medalha de Proteção!";

                Medal_Console.SetActive(true);
                endScreen.SetActive(true);
                HUDobj.SetActive(false);
                StartCoroutine(MedalAnimations());
                StopCoroutine(timer);

                earnedMedals[0].SetActive(true);
                if (floraMedal)
                    earnedMedals[1].SetActive(true);
                if (faunaMedal)
                    earnedMedals[2].SetActive(true);

                switch (currentScene)
                {
                    case "MataAtlantica":

                        medals_MataAtlantica[0] = true;
                        query = "UPDATE medalhas SET medalhaconclusao = true WHERE fase = 'mataatlantica'";
                        UpdateDatabase();

                        if (floraMedal)
                        {
                            medals_MataAtlantica[1] = true;
                            query = "UPDATE medalhas SET medalhapreservacao = true WHERE fase = 'mataatlantica'";
                            UpdateDatabase();
                        }
                        if (faunaMedal)
                        {
                            medals_MataAtlantica[2] = true;
                            query = "UPDATE medalhas SET medalhaprotecao = true WHERE fase = 'mataatlantica'";
                            UpdateDatabase();
                        }

                        if (percentage > maxPercentage[0])
                        {
                            maxPercentage[0] = percentage;
                            query = "UPDATE medalhas SET maxpercentage = @num WHERE fase = 'mataatlantica'";
                            num = 0;
                            UpdateDatabasePorcentagem();
                        }

                        if (savedAnimalsObj > maxAnimals[0])
                        {
                            maxAnimals[0] = savedAnimalsObj;
                            query = "UPDATE medalhas SET maxAnimals = @num WHERE fase = 'mataatlantica'";
                            num = 0;
                            UpdateDatabaseAnimal();
                        }

                        break;
                    case "MataAtlanticaTutorial":

                        medals_MataAtlantica[0] = true;
                        query = "UPDATE medalhas SET medalhaconclusao = true WHERE fase = 'mataatlantica'";
                        UpdateDatabase();

                        if (floraMedal)
                        {
                            medals_MataAtlantica[1] = true;
                            query = "UPDATE medalhas SET medalhapreservacao = true WHERE fase = 'mataatlantica'";
                            UpdateDatabase();
                        }
                        if (faunaMedal)
                        {
                            medals_MataAtlantica[2] = true;
                            query = "UPDATE medalhas SET medalhaprotecao = true WHERE fase = 'mataatlantica'";
                            UpdateDatabase();
                        }

                        if (percentage > maxPercentage[0])
                        {
                            maxPercentage[0] = percentage;
                            query = "UPDATE medalhas SET maxpercentage = @num WHERE fase = 'mataatlantica'";
                            num = 0;
                            UpdateDatabasePorcentagem();
                        }

                        if (savedAnimalsObj > maxAnimals[0])
                        {
                            maxAnimals[0] = savedAnimalsObj;
                            query = "UPDATE medalhas SET maxAnimals = @num WHERE fase = 'mataatlantica'";
                            num = 0;
                            UpdateDatabaseAnimal();
                        }

                        break;
                    case "Pantanal":

                        medals_Pantanal[0] = true;
                        query = "UPDATE medalhas SET medalhaconclusao = true WHERE fase = 'pantanal'";
                        UpdateDatabase();

                        if (floraMedal)
                        {
                            medals_Pantanal[1] = true;
                            query = "UPDATE medalhas SET medalhapreservacao = true WHERE fase = 'pantanal'";
                            UpdateDatabase();
                        }
                        if (faunaMedal)
                        {
                            medals_Pantanal[2] = true;
                            query = "UPDATE medalhas SET medalhaprotecao = true WHERE fase = 'pantanal'";
                            UpdateDatabase();
                        }

                        if (percentage > maxPercentage[1])
                        {
                            maxPercentage[1] = percentage;
                            query = "UPDATE medalhas SET maxpercentage = @num WHERE fase = 'pantanal'";
                            num = 1;
                            UpdateDatabasePorcentagem();
                        }

                        if (savedAnimalsObj > maxAnimals[1])
                        {
                            maxAnimals[1] = savedAnimalsObj;
                            query = "UPDATE medalhas SET maxAnimals = @num WHERE fase = 'pantanal'";
                            num = 1;
                            UpdateDatabaseAnimal();
                        }

                        break;

                    case "Amazonia":

                        medals_Amazonia[0] = true;
                        query = "UPDATE medalhas SET medalhaconclusao = true WHERE fase = 'amazonia'";
                        UpdateDatabase();

                        if (floraMedal)
                        {
                            medals_Amazonia[1] = true;
                            query = "UPDATE medalhas SET medalhapreservacao = true WHERE fase = 'amazonia'";
                            UpdateDatabase();
                        }
                        if (faunaMedal)
                        {
                            medals_Amazonia[2] = true;
                            query = "UPDATE medalhas SET medalhaprotecao = true WHERE fase = 'amazonia'";
                            UpdateDatabase();
                        }

                        if (percentage > maxPercentage[2])
                        {
                            maxPercentage[2] = percentage;
                            query = "UPDATE medalhas SET maxpercentage = @num WHERE fase = 'amazonia'";
                            num = 2;
                            UpdateDatabasePorcentagem();
                        }

                        if (savedAnimalsObj > maxAnimals[2])
                        {
                            maxAnimals[2] = savedAnimalsObj;
                            query = "UPDATE medalhas SET maxAnimals = @num WHERE fase = 'amazonia'";
                            num = 2;
                            UpdateDatabaseAnimal();
                        }

                        break;

                    case "Cerrado":

                        medals_Cerrado[0] = true;
                        query = "UPDATE medalhas SET medalhaconclusao = true WHERE fase = 'cerrado'";
                        UpdateDatabase();

                        if (floraMedal)
                        {
                            medals_Cerrado[1] = true;
                            query = "UPDATE medalhas SET medalhapreservacao = true WHERE fase = 'cerrado'";
                            UpdateDatabase();
                        }
                        if (faunaMedal)
                        {
                            medals_Cerrado[2] = true;
                            query = "UPDATE medalhas SET medalhaprotecao = true WHERE fase = 'cerrado'";
                            UpdateDatabase();
                        }

                        if (percentage > maxPercentage[3])
                        {
                            maxPercentage[3] = percentage;
                            query = "UPDATE medalhas SET maxpercentage = @num WHERE fase = 'cerrado'";
                            num = 3;
                            UpdateDatabasePorcentagem();
                        }

                        if (savedAnimalsObj > maxAnimals[3])
                        {
                            maxAnimals[3] = savedAnimalsObj;
                            query = "UPDATE medalhas SET maxAnimals = @num WHERE fase = 'cerrado'";
                            num = 3;
                            UpdateDatabaseAnimal();
                        }

                        break;

                    case "Caatinga":


                        medals_Caatinga[0] = true;
                        query = "UPDATE medalhas SET medalhaconclusao = true WHERE fase = 'caatinga'";
                        UpdateDatabase();

                        if (floraMedal)
                        {
                            medals_Caatinga[1] = true;
                            query = "UPDATE medalhas SET medalhapreservacao = true WHERE fase = 'caatinga'";
                            UpdateDatabase();
                        }
                        if (faunaMedal)
                        {
                            medals_Caatinga[2] = true;
                            query = "UPDATE medalhas SET medalhaprotecao = true WHERE fase = 'caatinga'";
                            UpdateDatabase();
                        }

                        if (percentage > maxPercentage[4])
                        {
                            maxPercentage[4] = percentage;
                            query = "UPDATE medalhas SET maxpercentage = @num WHERE fase = 'caatinga'";
                            num = 4;
                            UpdateDatabasePorcentagem();
                        }

                        if (savedAnimalsObj > maxAnimals[4])
                        {
                            maxAnimals[4] = savedAnimalsObj;
                            query = "UPDATE medalhas SET maxAnimals = @num WHERE fase = 'caatinga'";
                            num = 4;
                            UpdateDatabaseAnimal();
                        }

                        break;
                }

            }

            if (levelCurTime_Sec < 10)
                timer_text.text = levelCurTime_Min + ":0" + levelCurTime_Sec;
            else
                timer_text.text = levelCurTime_Min + ":" + levelCurTime_Sec;
        }
    }

    void SkipLevel()
    {
        if (Input.GetKeyDown(KeyCode.F8))
            Proceed();
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
    private void Conectar()
    {
        conexao = new SqliteConnection(dbfile);
        try
        {
            conexao.Open();
            Debug.Log("conexao ok");
        }
        catch { Debug.Log("erro"); }
    }

    void UpdateDatabase()
    {
        using (var comando = conexao.CreateCommand())
        {
            comando.CommandText = query;
            comando.ExecuteNonQuery();
        }
    }

    void UpdateDatabasePorcentagem()
    {
        using (var comando = conexao.CreateCommand())
        {
            comando.CommandText = query;
            comando.Parameters.Add(new SqliteParameter("@num", maxPercentage[num]));
            comando.ExecuteNonQuery();
        }
    }

    void UpdateDatabaseAnimal()
    {
        using (var comando = conexao.CreateCommand())
        {
            comando.CommandText = query;
            comando.Parameters.Add(new SqliteParameter("@num", maxAnimals[num]));
            comando.ExecuteNonQuery();
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

    IEnumerator MedalAnimations()
    {

        yield return new WaitForSeconds(2);
        earnedMedals[0].SetActive(true);


        // if (floraMedal)
        // {
        //     yield return new WaitForSeconds(2);
        //     earnedMedals[1].SetActive(true);

        //     if (faunaMedal)
        //     {
        //         yield return new WaitForSeconds(2);
        //         earnedMedals[2].SetActive(true);
        //     }
        // }
        // else
        // {
        //     if (faunaMedal)
        //     {
        //         yield return new WaitForSeconds(2);
        //         earnedMedals[2].SetActive(true);
        //     }
        // }



    }
}
