using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneRoomManager : MonoBehaviour
{
    string nextScene;
    [SerializeField] GameObject transitionScreen, F2_button, F2_teaser;
    void Start()
    {
        transitionScreen.SetActive(true);
        transitionScreen.GetComponent<Animator>().SetTrigger("disappear");
        StartCoroutine(Beginning());

        if (HubManager.beatEverything100)
        {
            F2_teaser.SetActive(false);
            F2_button.SetActive(true);
        }
        else
        {
            F2_teaser.SetActive(true);
            F2_button.SetActive(false);
        }


    }

    IEnumerator Beginning()
    {
        yield return new WaitForSeconds(0.5f);
        transitionScreen.SetActive(false);
        yield return new WaitForSeconds(0.5f);


    }

    IEnumerator ChangeScene()
    {
        transitionScreen.SetActive(true);
        transitionScreen.GetComponent<Animator>().SetTrigger("appear");
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(nextScene);

    }
    public void Back()
    {
        nextScene = "Hub";
        StartCoroutine(ChangeScene());
    }
    
    public void Intro()
    {
        nextScene = "Cutscene1";
        StartCoroutine(ChangeScene());
    }
    public void Tutorial()
    {
        nextScene = "MataAtlanticaTutorial";
        StartCoroutine(ChangeScene());
    }
    public void MA1()
    {
        nextScene = "preMA";
        StartCoroutine(ChangeScene());
    }
    public void MA2()
    {
        nextScene = "postMA";
        StartCoroutine(ChangeScene());
    }
    public void PA1()
    {
        nextScene = "prePA";
        StartCoroutine(ChangeScene());
    }
    public void PA2()
    {
        nextScene = "postPA";
        StartCoroutine(ChangeScene());
    }
    public void AM1()
    {
        nextScene = "preAM";
        StartCoroutine(ChangeScene());
    }
    public void AM2()
    {
        nextScene = "postAM";
        StartCoroutine(ChangeScene());
    }
    public void CE1()
    {
        nextScene = "preCE";
        StartCoroutine(ChangeScene());
    }
    public void CE2()
    {
        nextScene = "postCE";
        StartCoroutine(ChangeScene());
    }
    public void CA1()
    {
        nextScene = "preCA";
        StartCoroutine(ChangeScene());
    }
    public void CA2()
    {
        nextScene = "postCA";
        StartCoroutine(ChangeScene());
    }

    public void F1()
    {
        nextScene = "NormalEnding";
        StartCoroutine(ChangeScene());
    }
    public void F2()
    {
        nextScene = "GoodEnding";
        StartCoroutine(ChangeScene());
    }
}
