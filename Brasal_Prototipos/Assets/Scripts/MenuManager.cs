using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{

    [SerializeField] GameObject credits_window, buttons, game_logo, transitionScreen;

    public static bool firstTimePlaying = true, firstTimePA = true, firstTimeAM = true, firstTimeCE = true, firstTimeCA = true;

    string nextScene;

    [SerializeField] GameObject sa2_hud;
    [SerializeField] TextMeshProUGUI[] sa2_text;

    [SerializeField] Image[] sa2_images;

    [SerializeField] Sprite[] sa2_sprites;

    [SerializeField] GameObject[] sa2_objects;


    public static bool sa2 = false;

    void Start()
    {
        transitionScreen.SetActive(true);
        transitionScreen.GetComponent<Animator>().SetTrigger("disappear");
        StartCoroutine(Beginning());


        if (sa2)
        {
            sa2_hud.SetActive(true);
            for (int i = 0; i < sa2_text.Length; i++)
            {
                sa2_text[i].color = new Color(1, 1, 1, 1);
            }

            for (int i = 0; i < 5; i++)
            {
                sa2_images[i].GetComponent<Image>().sprite = sa2_sprites[0];
            }
            sa2_images[5].GetComponent<Image>().sprite = sa2_sprites[1];

            sa2_objects[0].GetComponent<Image>().sprite = sa2_sprites[4];
            sa2_objects[1].SetActive(false);


        }
        else
        {
            sa2_hud.SetActive(false);
        }

    }

    IEnumerator Beginning()
    {
        yield return new WaitForSeconds(0.3f);

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
