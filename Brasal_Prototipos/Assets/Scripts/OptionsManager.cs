using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsManager : MonoBehaviour
{
    [SerializeField] GameObject transitionScreen, confirmationWindow, deleteButton, volume, soundTest;

    public AudioClip[] music;

    public static AudioClip currentMusic;


    // Start is called before the first frame update
    void Start()
    {
        transitionScreen.SetActive(true);
        transitionScreen.GetComponent<Animator>().SetTrigger("disappear");
        StartCoroutine(Beginning());

        if (MenuManager.firstTimePlaying)
            deleteButton.SetActive(false);

        currentMusic = music[0];
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

    public void UpdateAudioVolume(float volume)
    {
        AudioManager.Volume = volume;
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
        volume.GetComponent<Animator>().SetTrigger("disappear");
        soundTest.GetComponent<Animator>().SetTrigger("disappear");
    }

    public void CloseWindow()
    {
        confirmationWindow.GetComponent<Animator>().SetTrigger("disappear");
        deleteButton.GetComponent<Animator>().SetTrigger("appear");
        volume.GetComponent<Animator>().SetTrigger("appear");
        soundTest.GetComponent<Animator>().SetTrigger("appear");

    }

    public void Erase()
    {
        deleteButton.GetComponent<Animator>().SetTrigger("disappear");
    }

    public void Sound1()
    {
        currentMusic = music[0];
    }


    public void Sound2()
    {
        currentMusic = music[1];

        Debug.Log("huh");
    }

    public void Sound3()
    {
        currentMusic = music[2];
    }

    public void Sound4()
    {
        currentMusic = music[3];
    }

    public void Sound5()
    {
        currentMusic = music[4];
    }

    public void Sound6()
    {
        currentMusic = music[5];
    }

}
