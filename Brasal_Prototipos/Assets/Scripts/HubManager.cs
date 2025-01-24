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
        DataManager.medals_MataAtlantica[0] = true;

        /////////

        if (!DataManager.medals_MataAtlantica[0])
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

                if (DataManager.medals_MataAtlantica[0])
                {
                    info_desc.text = "" + DataManager.maxPercentage[0] + "% de preservação \n" + DataManager.maxAnimals[0] + "animais salvos";
                    medals[0].GetComponent<Image>().sprite = winMedal;
                }
                else
                {
                    info_desc.text = "Você ainda não venceu essa fase.";
                    medals[0].GetComponent<Image>().sprite = blankMedal;
                }

                if (DataManager.medals_MataAtlantica[1])
                {
                    medals[1].GetComponent<Image>().sprite = floraMedal;
                }
                else
                {
                    medals[1].GetComponent<Image>().sprite = blankMedal;
                }

                if (DataManager.medals_MataAtlantica[2])
                {
                    medals[2].GetComponent<Image>().sprite = faunaMedal;
                }
                else
                {
                    medals[2].GetComponent<Image>().sprite = blankMedal;
                }


                break;

            case "pantanal":
                info_title.text = "Pantanal ";

                if (DataManager.medals_Pantanal[0])
                {
                    info_desc.text = "" + DataManager.maxPercentage[0] + "% de preservação \n" + DataManager.maxAnimals[0] + "animais salvos";
                    medals[0].GetComponent<Image>().sprite = winMedal;
                }
                else
                {
                    info_desc.text = "Você ainda não venceu essa fase.";
                    medals[0].GetComponent<Image>().sprite = blankMedal;
                }

                if (DataManager.medals_Pantanal[1])
                {
                    medals[1].GetComponent<Image>().sprite = floraMedal;
                }
                else
                {
                    medals[1].GetComponent<Image>().sprite = blankMedal;
                }

                if (DataManager.medals_Pantanal[2])
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

                if (DataManager.medals_Amazonia[0])
                {
                    info_desc.text = "" + DataManager.maxPercentage[0] + "% de preservação \n" + DataManager.maxAnimals[0] + "animais salvos";
                    medals[0].GetComponent<Image>().sprite = winMedal;
                }
                else
                {
                    info_desc.text = "Você ainda não venceu essa fase.";
                    medals[0].GetComponent<Image>().sprite = blankMedal;
                }

                if (DataManager.medals_Amazonia[1])
                {
                    medals[1].GetComponent<Image>().sprite = floraMedal;
                }
                else
                {
                    medals[1].GetComponent<Image>().sprite = blankMedal;
                }

                if (DataManager.medals_Amazonia[2])
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

                if (DataManager.medals_Cerrado[0])
                {
                    info_desc.text = "" + DataManager.maxPercentage[0] + "% de preservação \n" + DataManager.maxAnimals[0] + "animais salvos";
                    medals[0].GetComponent<Image>().sprite = winMedal;
                }
                else
                {
                    info_desc.text = "Você ainda não venceu essa fase.";
                    medals[0].GetComponent<Image>().sprite = blankMedal;
                }

                if (DataManager.medals_Cerrado[1])
                {
                    medals[1].GetComponent<Image>().sprite = floraMedal;
                }
                else
                {
                    medals[1].GetComponent<Image>().sprite = blankMedal;
                }

                if (DataManager.medals_Cerrado[2])
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

                if (DataManager.medals_Caatinga[0])
                {
                    info_desc.text = "" + DataManager.maxPercentage[0] + "% de preservação \n" + DataManager.maxAnimals[0] + "animais salvos";
                    medals[0].GetComponent<Image>().sprite = winMedal;
                }
                else
                {
                    info_desc.text = "Você ainda não venceu essa fase.";
                    medals[0].GetComponent<Image>().sprite = blankMedal;
                }

                if (DataManager.medals_Caatinga[1])
                {
                    medals[1].GetComponent<Image>().sprite = floraMedal;
                }
                else
                {
                    medals[1].GetComponent<Image>().sprite = blankMedal;
                }

                if (DataManager.medals_Caatinga[2])
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
        anim.SetInteger("estado", 1);
        selectedLevel = "mataAtlantica";
        //Info();
        StartCoroutine(ShowInfo());
        info_Window.SetActive(true);

        for (int i = 0; i < levelButtons.Length; i++)
        {
            levelButtons[i].GetComponent<Animator>().SetInteger("estado", 0);
        }
    }

    public void Pantanal()
    {
        anim.SetInteger("estado", 2);
        selectedLevel = "pantanal";
        //Info();
        StartCoroutine(ShowInfo());
        info_Window.SetActive(true);

        for (int i = 0; i < levelButtons.Length; i++)
        {
            levelButtons[i].GetComponent<Animator>().SetInteger("estado", 0);
        }
    }

    public void FlorestaAmazonica()
    {
        anim.SetInteger("estado", 3);
        selectedLevel = "amazonia";
        //Info();
        StartCoroutine(ShowInfo());
        info_Window.SetActive(true);

        for (int i = 0; i < levelButtons.Length; i++)
        {
            levelButtons[i].GetComponent<Animator>().SetInteger("estado", 0);
        }
    }

    public void Cerrado()
    {
        anim.SetInteger("estado", 4);
        selectedLevel = "cerrado";
        //Info();
        StartCoroutine(ShowInfo());
        info_Window.SetActive(true);

        for (int i = 0; i < levelButtons.Length; i++)
        {
            levelButtons[i].GetComponent<Animator>().SetInteger("estado", 0);
        }
    }

    public void Caatinga()
    {
        anim.SetInteger("estado", 5);
        selectedLevel = "caatinga";
        //Info();
        StartCoroutine(ShowInfo());
        info_Window.SetActive(true);

        for (int i = 0; i < levelButtons.Length; i++)
        {
            levelButtons[i].GetComponent<Animator>().SetInteger("estado", 0);
        }
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
        anim.SetInteger("estado", 0);
        info_Window.GetComponent<Animator>().SetInteger("estado", 0);
        StartCoroutine(ShowButtons());

    }

    IEnumerator ShowInfo()
    {
        yield return new WaitForSeconds(0.5f);
        Info();
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

    void Update()
    {
        Info();
    }
}
