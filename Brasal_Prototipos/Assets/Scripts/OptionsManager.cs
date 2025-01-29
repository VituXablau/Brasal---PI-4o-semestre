using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsManager : MonoBehaviour
{
    [SerializeField] GameObject transitionScreen, confirmationWindow, deleteButton;

    // Start is called before the first frame update
    void Start()
    {
        transitionScreen.SetActive(true);
        transitionScreen.GetComponent<Animator>().SetTrigger("disappear");
        StartCoroutine(Beginning());

        if (MenuManager.firstTimePlaying)
        deleteButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Beginning()
    {
        yield return new WaitForSeconds(0.5f);
        transitionScreen.SetActive(false);
        yield return new WaitForSeconds(0.5f);
    }

    public void Menu()
    {
        StartCoroutine(BackToMenu());
    }

    IEnumerator BackToMenu()
    {
        transitionScreen.SetActive(true);
        transitionScreen.GetComponent<Animator>().SetTrigger("appear");
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Menu");
    }

    public void OpenWindow()
    {
        confirmationWindow.GetComponent<Animator>().SetTrigger("appear");
            deleteButton.GetComponent<Animator>().SetTrigger("disappear");
    }

    public void CloseWindow()
    {
        confirmationWindow.GetComponent<Animator>().SetTrigger("disappear");
                deleteButton.GetComponent<Animator>().SetTrigger("appear");
        
    }

    public void Erase()
    {
           deleteButton.GetComponent<Animator>().SetTrigger("disappear");
    }
}
