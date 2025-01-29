using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Data;
using Mono.Data.Sqlite;


public class MenuManager : MonoBehaviour
{

    [SerializeField] GameObject credits_window, buttons, game_logo, transitionScreen;

    public static bool firstTimePlaying = true, firstTimePA = true, firstTimeAM = true, firstTimeCE = true, firstTimeCA = true;

    string nextScene;


    private readonly string dbfile = "URI=file:Brasal.db.db";
    private IDbConnection conexao;
    string query;

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

    void Start()
    {
        transitionScreen.SetActive(false);
        Conectar();

    }

    public void StartGame()
    {
        buttons.GetComponent<Animator>().SetTrigger("disappear");
        game_logo.GetComponent<Animator>().SetTrigger("disappear");
        transitionScreen.SetActive(true);
        transitionScreen.GetComponent<Animator>().SetTrigger("appear");

        // if (firstTimePlaying)
        // {
        //     nextScene = "Cutscene1";
        //     firstTimePlaying = false;
        // }
        // else
        // {
        //     nextScene = "Hub";
        // }

        if (firstTimePlaying)
        {
            nextScene = "Cutscene1";
        }
        else nextScene = "Hub";

        StartCoroutine(ChangeScene());


    }

    public void Credits()
    {

        credits_window.GetComponent<Animator>().SetTrigger("appear");
        buttons.GetComponent<Animator>().SetTrigger("disappear");
        game_logo.GetComponent<Animator>().SetTrigger("disappear");

          MenuManager.firstTimePlaying = false;
        query = "UPDATE firsttime SET firstTimePlaying = false WHERE id = 1";
        UpdateDatabase();

    }

    public void Options()
    {
        buttons.GetComponent<Animator>().SetTrigger("disappear");
        game_logo.GetComponent<Animator>().SetTrigger("disappear");
        transitionScreen.SetActive(true);
        transitionScreen.GetComponent<Animator>().SetTrigger("appear");
        nextScene = "Options";

        StartCoroutine(ChangeScene());
    }

    public void Exit()
    {
        buttons.GetComponent<Animator>().SetTrigger("disappear");
        Application.Quit();
    }

    public void CloseCredits()
    {
        credits_window.GetComponent<Animator>().SetTrigger("disappear");
        buttons.GetComponent<Animator>().SetTrigger("appear");
        game_logo.GetComponent<Animator>().SetTrigger("appear");
    }

    IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(nextScene);

    }

}
