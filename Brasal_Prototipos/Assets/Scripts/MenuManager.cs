using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    [SerializeField] GameObject credits_window, buttons, game_logo, transitionScreen;

    public static bool firstTimePlaying = true, firstTimePA = true, firstTimeAM = true, firstTimeCE = true, firstTimeCA = true;

    string nextScene;

    void Start()
    {
        transitionScreen.SetActive(false);
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
