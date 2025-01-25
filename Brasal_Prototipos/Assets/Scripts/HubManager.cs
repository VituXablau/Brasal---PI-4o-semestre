using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HubManager : MonoBehaviour
{

    [SerializeField] GameObject[] levelButtons;
    [SerializeField] Image[] medals;
    [SerializeField] GameObject info_Window, cam;
    [SerializeField] Sprite winMedal, floraMedal, faunaMedal, blankMedal;

    string selectedLevel;

    Animator anim;

    [SerializeField] TextMeshProUGUI info_title, info_desc;

    void Start()
    {
        GameManager.medals_MataAtlantica[0] = true;

        /////////

        if (!GameManager.medals_MataAtlantica[0])
        {
            for (int i = 1; i < levelButtons.Length; i++)
            {
                levelButtons[i].SetActive(false);
            }
        }

        anim = cam.GetComponent<Animator>();
        info_Window.SetActive(false);
    }

    void Info()
    {
        switch (selectedLevel)
        {
            case "mataAtlantica":
                info_title.text = "Mata Atlântica";

                if (GameManager.medals_MataAtlantica[0])
                {
                    info_desc.text = "" + GameManager.maxPercentage[0] + "% de preservação \n" + GameManager.maxAnimals[0] + " animais salvos";
                    medals[0].GetComponent<Image>().sprite = winMedal;
                }
                else
                {
                    info_desc.text = "Você ainda não venceu essa fase.";
                    medals[0].GetComponent<Image>().sprite = blankMedal;
                }

                if (GameManager.medals_MataAtlantica[1])
                {
                    medals[1].GetComponent<Image>().sprite = floraMedal;
                }
                else
                {
                    medals[1].GetComponent<Image>().sprite = blankMedal;
                }

                if (GameManager.medals_MataAtlantica[2])
                {
                    medals[2].GetComponent<Image>().sprite = faunaMedal;
                }
                else
                {
                    medals[2].GetComponent<Image>().sprite = blankMedal;
                }


                break;

            case "pantanal":
                info_title.text = "Pantanal";

                if (GameManager.medals_Pantanal[0])
                {
                    info_desc.text = "" + GameManager.maxPercentage[1] + "% de preservação \n" + GameManager.maxAnimals[1] + " animais salvos";
                    medals[0].GetComponent<Image>().sprite = winMedal;
                }
                else
                {
                    info_desc.text = "Você ainda não venceu essa fase.";
                    medals[0].GetComponent<Image>().sprite = blankMedal;
                }

                if (GameManager.medals_Pantanal[1])
                {
                    medals[1].GetComponent<Image>().sprite = floraMedal;
                }
                else
                {
                    medals[1].GetComponent<Image>().sprite = blankMedal;
                }

                if (GameManager.medals_Pantanal[2])
                {
                    medals[2].GetComponent<Image>().sprite = faunaMedal;
                }
                else
                {
                    medals[2].GetComponent<Image>().sprite = blankMedal;
                }
                break;

            case "amazonia":
                info_title.text = "Floresta Amazônica";

                if (GameManager.medals_Amazonia[0])
                {
                    info_desc.text = "" + GameManager.maxPercentage[2] + "% de preservação \n" + GameManager.maxAnimals[2] + " animais salvos";
                    medals[0].GetComponent<Image>().sprite = winMedal;
                }
                else
                {
                    info_desc.text = "Você ainda não venceu essa fase.";
                    medals[0].GetComponent<Image>().sprite = blankMedal;
                }

                if (GameManager.medals_Amazonia[1])
                {
                    medals[1].GetComponent<Image>().sprite = floraMedal;
                }
                else
                {
                    medals[1].GetComponent<Image>().sprite = blankMedal;
                }

                if (GameManager.medals_Amazonia[2])
                {
                    medals[2].GetComponent<Image>().sprite = faunaMedal;
                }
                else
                {
                    medals[2].GetComponent<Image>().sprite = blankMedal;
                }
                break;

            case "cerrado":
                info_title.text = "Cerrado";

                if (GameManager.medals_Cerrado[0])
                {
                    info_desc.text = "" + GameManager.maxPercentage[3] + "% de preservação \n" + GameManager.maxAnimals[3] + " animais salvos";
                    medals[0].GetComponent<Image>().sprite = winMedal;
                }
                else
                {
                    info_desc.text = "Você ainda não venceu essa fase.";
                    medals[0].GetComponent<Image>().sprite = blankMedal;
                }

                if (GameManager.medals_Cerrado[1])
                {
                    medals[1].GetComponent<Image>().sprite = floraMedal;
                }
                else
                {
                    medals[1].GetComponent<Image>().sprite = blankMedal;
                }

                if (GameManager.medals_Cerrado[2])
                {
                    medals[2].GetComponent<Image>().sprite = faunaMedal;
                }
                else
                {
                    medals[2].GetComponent<Image>().sprite = blankMedal;
                }
                break;

            case "caatinga":
                info_title.text = "Caatinga";

                if (GameManager.medals_Caatinga[0])
                {
                    info_desc.text = "" + GameManager.maxPercentage[4] + "% de preservação \n" + GameManager.maxAnimals[4] + " animais salvos";
                    medals[0].GetComponent<Image>().sprite = winMedal;
                }
                else
                {
                    info_desc.text = "Você ainda não venceu essa fase.";
                    medals[0].GetComponent<Image>().sprite = blankMedal;
                }

                if (GameManager.medals_Caatinga[1])
                {
                    medals[1].GetComponent<Image>().sprite = floraMedal;
                }
                else
                {
                    medals[1].GetComponent<Image>().sprite = blankMedal;
                }

                if (GameManager.medals_Caatinga[2])
                {
                    medals[2].GetComponent<Image>().sprite = faunaMedal;
                }
                else
                {
                    medals[2].GetComponent<Image>().sprite = blankMedal;
                }
                break;
        }
    }

    public void MataAtlantica()
    {
        anim.SetTrigger("MA");
        selectedLevel = "mataAtlantica";

        StartCoroutine(HideButtons());
        StartCoroutine(ShowInfo());
        info_Window.SetActive(true);
    }

    public void Pantanal()
    {
        anim.SetTrigger("PA");
        selectedLevel = "pantanal";

        StartCoroutine(HideButtons());
        StartCoroutine(ShowInfo());
        info_Window.SetActive(true);

    }

    public void FlorestaAmazonica()
    {
        anim.SetTrigger("AM");
        selectedLevel = "amazonia";

        StartCoroutine(HideButtons());
        StartCoroutine(ShowInfo());
        info_Window.SetActive(true);

    }

    public void Cerrado()
    {
        anim.SetTrigger("CE");
        selectedLevel = "cerrado";

        StartCoroutine(HideButtons());
        StartCoroutine(ShowInfo());
        info_Window.SetActive(true);

    }

    public void Caatinga()
    {
        anim.SetTrigger("CA");
        selectedLevel = "caatinga";

        StartCoroutine(HideButtons());
        StartCoroutine(ShowInfo());
        info_Window.SetActive(true);

    }

    public void EnterLevel()
    {
        info_Window.GetComponent<Animator>().SetInteger("estado", 0);

        switch (selectedLevel)
        {
            case "mataAtlantica":
                SceneManager.LoadScene("MataAtlantica");
                break;

            case "pantanal":
                SceneManager.LoadScene("Pantanal");
                break;

            case "amazonia":
                SceneManager.LoadScene("Amazonia");
                break;

            case "cerrado":
                SceneManager.LoadScene("Cerrado");
                break;

            case "caatinga":
                SceneManager.LoadScene("Caatinga");
                break;
        }
    }

    public void ReturnMap()
    {
        switch (selectedLevel)
        {
            case "mataAtlantica":
                anim.SetTrigger("MA-m");
                break;

            case "pantanal":
                anim.SetTrigger("PA-m");
                break;

            case "amazonia":
                anim.SetTrigger("AM-m");
                break;

            case "cerrado":
                anim.SetTrigger("CE-m");
                break;

            case "caatinga":
                anim.SetTrigger("CA-m");
                break;
        }

        info_Window.GetComponent<Animator>().SetInteger("estado", 0);
        StartCoroutine(ShowButtons());

    }

    IEnumerator ShowInfo()
    {
        yield return new WaitForSeconds(0.5f);

        info_Window.GetComponent<Animator>().SetInteger("estado", 1);
    }

    IEnumerator ShowButtons()
    {
        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < levelButtons.Length; i++)
        {
            levelButtons[i].GetComponent<Animator>().SetInteger("estado", 1);
        }
    }

    IEnumerator HideButtons()
    {
        yield return new WaitForSeconds(0.1f);

        for (int i = 0; i < levelButtons.Length; i++)
        {
            levelButtons[i].GetComponent<Animator>().SetInteger("estado", 0);
        }
    }

    void Update()
    {
        Info();
    }
}
