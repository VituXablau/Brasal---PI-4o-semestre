using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Data;
using Mono.Data.Sqlite;

public class Cutscenes : MonoBehaviour
{
    private readonly string dbfile = "URI=file:Brasal.db.db";
    private IDbConnection conexao;
    string query;
    [SerializeField] SkinnedMeshRenderer cinza_face, amarelo_face, vermelho_face, roxo_face, azul_face;
    [SerializeField] Material neutral, happy, sad, angry;
    [SerializeField] Sprite cinza, amarelo, vermelho, roxo, azul, blank;
    [SerializeField]
    GameObject transitionScreen, quote, almir, dialogueBox, gota, cam, qg,
     personagemCinza, personagemAmarelo, personagemVermelho, personagemRoxo, personagemAzul;
    [SerializeField] TextMeshProUGUI characterName, dialogue;

    string currentScene;

    bool skippable = true, introQuote = false;

    int id = 1;

    // Start is called before the first frame update
    private void Awake()
    {
        currentScene = SceneManager.GetActiveScene().name;

    }
    void Start()
    {
         Conectar();

        transitionScreen.SetActive(true);
        dialogueBox.GetComponent<Animator>().SetTrigger("stayblack");

        if (currentScene == "Cutscene1")
        {
            introQuote = true;
            dialogueBox.GetComponent<Animator>().SetTrigger("hidden");
            StartCoroutine(Cutscene1QuoteAppear());
            skippable = false;

            personagemVermelho.SetActive(false);
            personagemRoxo.SetActive(false);
            personagemAzul.SetActive(false);
        }
        else
        {
            skippable = false;
            dialogueBox.GetComponent<Animator>().SetTrigger("hidden");
            StartCoroutine(Beginning());
        }

        switch (currentScene)
        {

            case "postMA":
                MenuManager.firstTimePlaying = false;
                query = "UPDATE firsttime SET firstTimePlaying = false WHERE id = 1";
                UpdateDatabase();
                break;

            case "postPA":
                MenuManager.firstTimePA = false;
                query = "UPDATE firsttime SET firstTimePA = false WHERE id = 1";
                UpdateDatabase();
                break;

            case "postAM":
                MenuManager.firstTimeAM = false;
                query = "UPDATE firsttime SET firstTimeAM = false WHERE id = 1";
                UpdateDatabase();

                break;

            case "postCE":
                MenuManager.firstTimeCE = false;
                query = "UPDATE firsttime SET firstTimeCE = false WHERE id = 1";
                UpdateDatabase();
                break;

            case "postCA":
                MenuManager.firstTimeCA = false;
                query = "UPDATE firsttime SET firstTimeCA = false WHERE id = 1";
                UpdateDatabase();
                break;



        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if ((currentScene == "Cutscene1") && (introQuote))
            {
                if (skippable)
                {
                    id = 0;
                    StartCoroutine(Cutscene1QuoteDisappear());
                    skippable = false;
                }

            }
            else
            {

                if (skippable)
                {
                    switch (currentScene)
                    {
                        case "Cutscene1":
                            StartCoroutine(Cutscene1());
                            break;
                        case "preMA":
                            StartCoroutine(preMA());
                            break;
                        case "postMA":
                            StartCoroutine(postMA());
                            break;
                        case "prePA":
                            StartCoroutine(prePA());
                            break;
                        case "postPA":
                            StartCoroutine(postPA());
                            break;
                        case "preAM":
                            StartCoroutine(preAM());
                            break;
                        case "postAM":
                            StartCoroutine(postAM());
                            break;
                        case "preCE":
                            StartCoroutine(preCE());
                            break;
                        case "postCE":
                            StartCoroutine(postCE());
                            break;
                        case "preCA":
                            StartCoroutine(preCA());
                            break;
                        case "postCA":
                            StartCoroutine(postCA());
                            break;
                        case "NormalEnding":
                            StartCoroutine(NormalEnding());
                            break;
                        case "GoodEnding":
                            StartCoroutine(GoodEnding());
                            break;

                    }

                    skippable = false;
                    dialogueBox.GetComponent<Animator>().SetTrigger("blink");
                    StartCoroutine(Wait());
                }

            }
        }

    }

    IEnumerator Beginning()
    {
        yield return new WaitForSeconds(1f);
        switch (currentScene)
        {
            case "preMA":
                StartCoroutine(preMA());
                break;
            case "postMA":
                StartCoroutine(postMA());
                break;
            case "prePA":
                StartCoroutine(prePA());
                break;
            case "postPA":
                StartCoroutine(postPA());
                break;
            case "preAM":
                StartCoroutine(preAM());
                break;
            case "postAM":
                StartCoroutine(postAM());
                break;
            case "preCE":
                StartCoroutine(preCE());
                break;
            case "postCE":
                StartCoroutine(postCE());
                break;
            case "preCA":
                StartCoroutine(preCA());
                break;
            case "postCA":
                StartCoroutine(postCA());
                break;
            case "NormalEnding":
                StartCoroutine(NormalEnding());
                break;
            case "GoodEnding":
                StartCoroutine(GoodEnding());
                break;
        }

        transitionScreen.GetComponent<Animator>().SetTrigger("disappear");
        yield return new WaitForSeconds(1f);
        dialogueBox.GetComponent<Animator>().SetTrigger("appear");
        yield return new WaitForSeconds(1f);
        skippable = true;
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.3f);
        skippable = true;
    }
    IEnumerator BlackFade()
    {
        transitionScreen.GetComponent<Animator>().SetTrigger("appear");
        skippable = false;
        yield return new WaitForSeconds(1f);

        personagemCinza.transform.position = new UnityEngine.Vector3(1.4f, -0.838f, -6.41f);
        personagemAmarelo.transform.position = new UnityEngine.Vector3(3.41f, -0.838f, -7.74f);
        cam.transform.position = new UnityEngine.Vector3(-0.27f, 1.43f, -0.16f);
        gota.transform.position = new UnityEngine.Vector3(1.021f, 0.402f, -6.091f);
        cam.transform.rotation = Quaternion.Euler(11.96f, -196, 0);
        personagemCinza.transform.rotation = Quaternion.Euler(0, -173, 0);
        personagemAmarelo.transform.rotation = Quaternion.Euler(0, -90.1f, 0);
        qg.transform.position = new UnityEngine.Vector3(4.1f, -0.53f, -5.04f);
        qg.transform.rotation = Quaternion.Euler(0, 24.9f, 0);
        personagemVermelho.SetActive(true);
        personagemRoxo.SetActive(true);
        personagemAzul.SetActive(true);

        dialogueBox.GetComponent<Animator>().SetTrigger("hidden");

        transitionScreen.GetComponent<Animator>().SetTrigger("disappear");
        yield return new WaitForSeconds(0.5f);
        dialogueBox.GetComponent<Animator>().SetTrigger("appear");

        StartCoroutine(Cutscene1());

        yield return new WaitForSeconds(5);
        skippable = true;

    }

    IEnumerator Cutscene1()
    {

        switch (id)
        {
            case 0:
                characterName.text = "Amarelo";
                dialogue.text = "Ah, olá novato! Primeira vez nesse posto?";
                dialogueBox.GetComponent<Image>().sprite = amarelo;
                break;

            case 1:
                characterName.text = "Cinza";
                dialogue.text = "Sim…";
                dialogueBox.GetComponent<Image>().sprite = cinza;

                break;

            case 2:
                characterName.text = "Amarelo";
                dialogue.text = "Bem vindo a Brigada Brasal! É um prazer conhecê-lo ao vivo! Ouvimos muito sobre você nos treinamentos!";
                dialogueBox.GetComponent<Image>().sprite = amarelo;
                break;

            case 3:
                characterName.text = "Amarelo";
                dialogue.text = "Eu sou o líder da brigada, pode só me chamar de Amarelo! Aqui a gente se chama pelo cor do capacete! Dessa forma, te chamaremos de Cinza!";
                dialogueBox.GetComponent<Image>().sprite = amarelo;
                break;

            case 4:
                characterName.text = "Amarelo";
                dialogue.text = "É tipo quando te chamam por um apelido quando você é calouro na faculdade!";
                dialogueBox.GetComponent<Image>().sprite = amarelo;
                amarelo_face.material = happy;
                break;


            case 5:
                characterName.text = "Amarelo";
                dialogue.text = "Infelizmente eu não passei por essa experiência…";
                dialogueBox.GetComponent<Image>().sprite = amarelo;
                amarelo_face.material = sad;

                gota.GetComponent<Animator>().SetTrigger("appear");


                break;

            case 6:
                characterName.text = "Amarelo";
                dialogue.text = "De qualquer forma!! Bem vindo a equipe! Nossa função é garantir o bem estar das pessoas e instituições!";
                dialogueBox.GetComponent<Image>().sprite = amarelo;
                amarelo_face.material = angry;

                gota.GetComponent<Animator>().SetTrigger("disappear");

                break;

            case 7:
                characterName.text = "Amarelo";
                dialogue.text = "Bem, no caso dessa brigada em específico, nós cuidamos especificamente da prevenção e combate de incêndios florestais! Criminosos ou naturais!";
                dialogueBox.GetComponent<Image>().sprite = amarelo;
                amarelo_face.material = neutral;
                break;

            case 8:
                characterName.text = "Amarelo";
                dialogue.text = "Vamos dar uma volta pelas regiões do Brasil nessas próximas missões!";
                dialogueBox.GetComponent<Image>().sprite = amarelo;
                amarelo_face.material = happy;
                break;

            case 9:
                characterName.text = "Amarelo";
                dialogue.text = "Por enquanto, conheça os outros brigadistas que você vai interagir!!";
                dialogueBox.GetComponent<Image>().sprite = amarelo;
                amarelo_face.material = happy;
                break;

            case 10:
                characterName.text = "";
                dialogue.text = "";
                dialogueBox.GetComponent<Image>().sprite = amarelo;

                transitionScreen.SetActive(true);
                skippable = false;
                StartCoroutine(BlackFade());

                break;

            case 11:
                characterName.text = "Vermelho";
                dialogue.text = "Oii! Eu sou a Vermelho!";
                dialogueBox.GetComponent<Image>().sprite = vermelho;
                vermelho_face.material = happy;
                amarelo_face.material = neutral;
                break;

            case 12:
                characterName.text = "Vermelho";
                dialogue.text = "Fui responsável pela criação das ferramentas que a gente vai utilizar mais para a frente!";
                dialogueBox.GetComponent<Image>().sprite = vermelho;
                vermelho_face.material = neutral;
                amarelo_face.material = neutral;
                break;

            case 13:
                characterName.text = "Vermelho";
                dialogue.text = "Quando a gente chegar lá eu te explico!";
                dialogueBox.GetComponent<Image>().sprite = vermelho;
                vermelho_face.material = neutral;
                break;

            case 14:
                characterName.text = "Vermelho";
                dialogue.text = "Desculpa me adiantar, eu só me empolgo muito falando do avanço tecnológico que tivemos até agora para lidar com incêndios…";
                dialogueBox.GetComponent<Image>().sprite = vermelho;
                vermelho_face.material = sad;
                break;

            case 15:
                characterName.text = "Amarelo";
                dialogue.text = "Não tem o que se preocupar, Vermelho! Como pode ver, Cinza, todo mundo aqui se dedica muito ao trabalho.";
                dialogueBox.GetComponent<Image>().sprite = amarelo;
                amarelo_face.material = happy;
                vermelho_face.material = happy;
                break;

            case 16:
                characterName.text = "Roxo";
                dialogue.text = "Pode apostar que sim. Prazer, novato, eu sou Roxo.";
                dialogueBox.GetComponent<Image>().sprite = roxo;
                vermelho_face.material = neutral;
                amarelo_face.material = neutral;
                break;

            case 17:
                characterName.text = "Roxo";
                dialogue.text = "Eu posso não ser tão empolgada quanto o Vermelho, mas pode apostar que eu dou duro no meu trabalho.";
                dialogueBox.GetComponent<Image>().sprite = roxo;
                vermelho_face.material = neutral;
                amarelo_face.material = neutral;
                break;

            case 18:
                characterName.text = "Azul";
                dialogue.text = "Sim, sim, eu também. Eu sou Azul, aliás. Bem vindo a equipe, Cinza.";
                dialogueBox.GetComponent<Image>().sprite = azul;
                break;

            case 19:
                characterName.text = "Amarelo";
                dialogue.text = "Perfeito, perfeito! Agora que todo mundo já se introduziu a você, vamos para a missão!";
                dialogueBox.GetComponent<Image>().sprite = amarelo;
                amarelo_face.material = happy;
                gota.GetComponent<Animator>().SetTrigger("appear");
                break;

            case 20:
                characterName.text = "Roxo";
                dialogue.text = "Já vai colocá-lo em uma missão?";
                dialogueBox.GetComponent<Image>().sprite = roxo;
                break;

            case 21:
                characterName.text = "Amarelo";
                dialogue.text = "É claro! A melhor forma de aprender é pondo em prática!";
                dialogueBox.GetComponent<Image>().sprite = amarelo;
                break;

            case 22:
                characterName.text = "Amarelo";
                dialogue.text = "Sabe geografia, Cinza?";
                dialogueBox.GetComponent<Image>().sprite = amarelo;
                break;

            case 23:
                characterName.text = "Cinza";
                dialogue.text = "Uh, sim?";
                dialogueBox.GetComponent<Image>().sprite = cinza;

                gota.GetComponent<Animator>().SetTrigger("disappear");
                break;

            case 24:
                characterName.text = "Amarelo";
                dialogue.text = "Teste rápido! Onde se localiza a Mata Atlântica? ";
                dialogueBox.GetComponent<Image>().sprite = amarelo;
                break;

            case 25:
                characterName.text = "Cinza";
                dialogue.text = "Se me lembro bem, tem na costa leste, nordeste, sudeste e sul do Brasil.";
                dialogueBox.GetComponent<Image>().sprite = cinza;
                break;

            case 26:
                characterName.text = "Amarelo";
                dialogue.text = " Bingo! Esse terreno tropical será sua próxima parada. Vamos te enviar para a costa!";
                dialogueBox.GetComponent<Image>().sprite = amarelo;
                break;

            case 27:
                characterName.text = "Cinza";
                dialogue.text = "Ok… Quando?";
                dialogueBox.GetComponent<Image>().sprite = cinza;
                break;

            case 28:
                characterName.text = "Amarelo";
                dialogue.text = "Agora!";
                dialogueBox.GetComponent<Image>().sprite = amarelo;

                break;

            case 29:
                characterName.text = "Cinza";
                dialogue.text = "Agora!?";
                dialogueBox.GetComponent<Image>().sprite = cinza;

                gota.GetComponent<Animator>().SetTrigger("appear");


                break;

            case 30:
                characterName.text = "Amarelo";
                dialogue.text = "Eu disse que o melhor aprendizado é na prática! Mas não se preocupe! Ao estar lá, você irá receber melhores instruções!";
                dialogueBox.GetComponent<Image>().sprite = amarelo;
                break;

            case 31:
                characterName.text = "Azul";
                dialogue.text = "A gente vai manter contato pelo rádio.";
                dialogueBox.GetComponent<Image>().sprite = azul;
                gota.GetComponent<Animator>().SetTrigger("disappear");

                break;

            case 32:
                characterName.text = "Vermelho";
                dialogue.text = "Pode ter certeza que você vai sair sabendo como que cada coisinha funciona!";
                dialogueBox.GetComponent<Image>().sprite = vermelho;
                vermelho_face.material = happy;
                break;


            case 33:
                characterName.text = "Roxo";
                dialogue.text = "Boa sorte, Cinza.";
                dialogueBox.GetComponent<Image>().sprite = roxo;
                break;

            case 34:
                characterName.text = "Cinza";
                dialogue.text = "Certo!";
                dialogueBox.GetComponent<Image>().sprite = cinza;
                break;

            case 35:
                characterName.text = "";
                dialogue.text = "";
                dialogueBox.GetComponent<Image>().sprite = blank;
                dialogueBox.GetComponent<Animator>().SetTrigger("disappear");
                transitionScreen.GetComponent<Animator>().SetTrigger("appear");

                yield return new WaitForSeconds(0.5f);

                if (!HubManager.beatEverything && !HubManager.beatEverything)
                {
                    SceneManager.LoadScene("preMA");
                }
                else
                {
                    SceneManager.LoadScene("CutscenesRoom");
                }
                break;
        }

        yield return new WaitForSeconds(0.1f);

        id++;

    }

    IEnumerator preMA()
    {
        switch (id)
        {
            case 1:
                characterName.text = "Amarelo";
                dialogue.text = "Ah, a Mata Atlântica… Eu adoro aqui! A floresta tropical, a biodiversidade…";
                dialogueBox.GetComponent<Image>().sprite = amarelo;
                break;

            case 2:
                characterName.text = "Amarelo";
                dialogue.text = "É uma pena que os incêndios estão sendo cada vez mais frequentes por aqui.";
                dialogueBox.GetComponent<Image>().sprite = amarelo;
                break;

            case 3:
                characterName.text = "Amarelo";
                dialogue.text = "Esse bioma sofre muito de incêndios por ação humana, mais de 90% dos focos.";
                dialogueBox.GetComponent<Image>().sprite = amarelo;
                break;

            case 4:
                characterName.text = "Amarelo";
                dialogue.text = "É realmente uma pena. É muito mais fácil perder a biodiversidade do que recuperá-la.";
                dialogueBox.GetComponent<Image>().sprite = amarelo;
                break;

            case 5:
                characterName.text = "Cinza";
                dialogue.text = "Também é um risco para as pessoas. A fumaça e o aumento de dióxido de carbono e metano no ar provocam a mudança climática.";
                dialogueBox.GetComponent<Image>().sprite = cinza;
                break;

            case 6:
                characterName.text = "Amarelo";
                dialogue.text = "E é perigoso para a saúde pública! Por isso que a gente precisa ficar de olho!";
                dialogueBox.GetComponent<Image>().sprite = amarelo;
                break;

            case 7:
                characterName.text = "Vermelho";
                dialogue.text = "Vambora Cinza! É hora de controlar o incêndio!";
                dialogueBox.GetComponent<Image>().sprite = amarelo;
                break;

            case 8:
                characterName.text = "";
                dialogue.text = "";
                dialogueBox.GetComponent<Image>().sprite = blank;
                dialogueBox.GetComponent<Animator>().SetTrigger("disappear");
                transitionScreen.GetComponent<Animator>().SetTrigger("appear");

                yield return new WaitForSeconds(0.5f);

                if (!HubManager.beatEverything && !HubManager.beatEverything)
                {
                    SceneManager.LoadScene("MataAtlanticaTutorial");
                }
                else
                {
                    SceneManager.LoadScene("CutscenesRoom");
                }
                break;


        }


        yield return new WaitForSeconds(0.1f);

        id++;
    }

    IEnumerator postMA()
    {
        switch (id)
        {
            case 1:
                characterName.text = "Amarelo";
                dialogue.text = "Wohoo!! Boa Cinza! Você mostrou como faz! Isso merece uma comemoração!";
                dialogueBox.GetComponent<Image>().sprite = amarelo;
                amarelo_face.material = happy;
                break;

            case 2:
                characterName.text = "Vermelho";
                dialogue.text = "Foi uma quantidade alarmante de fogo, líder. Deveríamos ficar atentos.";
                dialogueBox.GetComponent<Image>().sprite = vermelho;
                vermelho_face.material = sad;

                break;

            case 3:
                characterName.text = "Amarelo";
                dialogue.text = "Estamos de olho! Recentemente veio a alteração a Lei 14.944/2024, Lei do Manejo do Fogo. Nosso objetivo é seguir essas diretrizes quando administrando florestas!";
                dialogueBox.GetComponent<Image>().sprite = amarelo;
                amarelo_face.material = angry;
                break;

            case 4:
                characterName.text = "Roxo";
                dialogue.text = "Nossa função é manter as ações preventivas, estabelecer prioridade de áreas de conservação e recuperação, trazer o diagnóstico da vegetação restante…";
                dialogueBox.GetComponent<Image>().sprite = roxo;
                amarelo_face.material = neutral;
                break;

            case 5:
                characterName.text = "Amarelo";
                dialogue.text = "Parabéns Cinza! Agora vai cuidar da papelada!";
                dialogueBox.GetComponent<Image>().sprite = amarelo;
                amarelo_face.material = happy;
                break;

            case 6:
                characterName.text = "Cinza";
                dialogue.text = "...";
                dialogueBox.GetComponent<Image>().sprite = cinza;
                gota.GetComponent<Animator>().SetTrigger("appear");
                break;

            case 7:
                characterName.text = "Amarelo";
                dialogue.text = "Um Plano de Conservação e Recuperação da Mata Atlântica já é uma ação incentivada pela ONG SOS Mata Atlântica. Vamos fazer o possível para dar menos trabalho para eles!";
                dialogueBox.GetComponent<Image>().sprite = amarelo;
                gota.GetComponent<Animator>().SetTrigger("disappear");
                amarelo_face.material = angry;
                break;

            case 8:
                characterName.text = "Amarelo";
                dialogue.text = "Ficaremos atentos a Mata Atlântica, mas agora a gente tem que dar uma olhadinha nos outros biomas. Infelizmente, todo o Brasil está em risco.";
                dialogueBox.GetComponent<Image>().sprite = amarelo;
                break;


            case 9:
                characterName.text = "";
                dialogue.text = "";
                dialogueBox.GetComponent<Image>().sprite = blank;
                dialogueBox.GetComponent<Animator>().SetTrigger("disappear");
                transitionScreen.GetComponent<Animator>().SetTrigger("appear");
                yield return new WaitForSeconds(0.5f);

                if (!HubManager.beatEverything && !HubManager.beatEverything)
                {
                    SceneManager.LoadScene("Hub");
                }
                else
                {
                    SceneManager.LoadScene("CutscenesRoom");
                }
                break;


        }


        yield return new WaitForSeconds(0.1f);

        id++;
    }

    IEnumerator prePA()
    {
        switch (id)
        {
            case 1:
                characterName.text = "Roxo";
                dialogue.text = "Incêndios podem ocorrer naturalmente, mas também são causados por atividades humanas, muitas vezes ampliados pela seca.";
                dialogueBox.GetComponent<Image>().sprite = roxo;
                break;

            case 2:
                characterName.text = "Amarelo";
                dialogue.text = "Pantanal! Um bioma alagado em sua maior parte! Como ocorrem os incêndios? Cinza!";
                dialogueBox.GetComponent<Image>().sprite = amarelo;
                amarelo_face.material = angry;

                break;

            case 3:
                characterName.text = "Cinza";
                dialogue.text = "Uh, bem, o pantanal tem algumas características notáveis. Seu clima é tropical e bem definido, então, o verão com chuvas e o inverno seco.";
                dialogueBox.GetComponent<Image>().sprite = cinza;
                amarelo_face.material = neutral;
                break;

            case 4:
                characterName.text = "Cinza";
                dialogue.text = "Por basicamente chover em um único período, possui poucas chuvas. Entre maio e setembro, onde é seco, o local não é inundado.";
                dialogueBox.GetComponent<Image>().sprite = cinza;
                break;

            case 5:
                characterName.text = "Cinza";
                dialogue.text = "É nesse período que as queimadas agravam.";
                dialogueBox.GetComponent<Image>().sprite = cinza;
                break;

            case 6:
                characterName.text = "Amarelo";
                dialogue.text = "Bravo! Geralmente, as áreas alagadas ajudam a controlar o fogo, atrasando a proliferação das chamas.";
                dialogueBox.GetComponent<Image>().sprite = amarelo;
                amarelo_face.material = happy;
                break;

            case 7:
                characterName.text = "Amarelo";
                dialogue.text = "Mas as mudanças climáticas mexem com o ciclo de chuvas e secas do bioma.";
                dialogueBox.GetComponent<Image>().sprite = amarelo;
                amarelo_face.material = sad;
                break;

            case 8:
                characterName.text = "Cinza";
                dialogue.text = "Por isso que estamos aqui, certo?";
                dialogueBox.GetComponent<Image>().sprite = cinza;
                break;



            case 9:
                characterName.text = "Amarelo";
                dialogue.text = "Precisaremos de coordenação e eficiência aqui! Nossa movimentação será dificultada! Todos atrás de mim!.";
                dialogueBox.GetComponent<Image>().sprite = amarelo;
                amarelo_face.material = angry;
                break;


            case 10:
                characterName.text = "";
                dialogue.text = "";
                dialogueBox.GetComponent<Image>().sprite = blank;
                dialogueBox.GetComponent<Animator>().SetTrigger("disappear");
                transitionScreen.GetComponent<Animator>().SetTrigger("appear");
                yield return new WaitForSeconds(0.5f);


                if (!HubManager.beatEverything && !HubManager.beatEverything)
                {
                    SceneManager.LoadScene("Pantanal");
                }
                else
                {
                    SceneManager.LoadScene("CutscenesRoom");
                }
                break;

        }


        yield return new WaitForSeconds(0.1f);

        id++;
    }

    IEnumerator postPA()
    {
        switch (id)
        {
            case 1:
                characterName.text = "Cinza";
                dialogue.text = "Bom trabalho, Amarelo. Mas, temos conhecimento sobre a causa desses incêndios?";
                dialogueBox.GetComponent<Image>().sprite = cinza;
                break;

            case 2:
                characterName.text = "Cinza";
                dialogue.text = "Foi natural? Criminoso? Roxo e Azul estavam comentando sobre como ações humanas agravam incêndios.";
                dialogueBox.GetComponent<Image>().sprite = cinza;

                break;

            case 3:
                characterName.text = "Amarelo";
                dialogue.text = "Querido Cinza, eles estão corretíssimos!";
                dialogueBox.GetComponent<Image>().sprite = amarelo;

                break;

            case 4:
                characterName.text = "Amarelo";
                dialogue.text = "Resumidamente, queimam para abrir novas áreas para o plantio e para criação de animais. O Pantanal é encontrado no Centro-Oeste, grande setor agropecuário!";
                dialogueBox.GetComponent<Image>().sprite = amarelo;
                break;

            case 5:
                characterName.text = "Amarelo";
                dialogue.text = "Além disso, tem o desmatamento. O fogo é usado para a limpeza após remoção da vegetação.";
                dialogueBox.GetComponent<Image>().sprite = amarelo;
                break;

            case 6:
                characterName.text = "Cinza";
                dialogue.text = "Limpeza huh… Talvez a curto prazo.";
                dialogueBox.GetComponent<Image>().sprite = cinza;

                break;

            case 7:
                characterName.text = "Amarelo";
                dialogue.text = "O pantanal sofreu demais com as queimadas nos últimos 4 anos, desde 2020.";
                dialogueBox.GetComponent<Image>().sprite = amarelo;
                amarelo_face.material = sad;
                break;

            case 8:
                characterName.text = "Amarelo";
                dialogue.text = "Neste momento, é uma área de grande risco! Mais de 60% da vegetação foi perdida para o fogo.";
                dialogueBox.GetComponent<Image>().sprite = amarelo;
                amarelo_face.material = sad;
                break;

            case 9:
                characterName.text = "Amarelo";
                dialogue.text = "A solução que nós, humanos, queremos alcançar é inovar nas técnicas de plantio, fiscalizar áreas de fogo e focar em recuperar as áreas devastadas!";
                dialogueBox.GetComponent<Image>().sprite = amarelo;
                break;



            case 10:
                characterName.text = "Cinza";
                dialogue.text = "Fiscalização, né? Estamos sendo chamados para alguns desses.";
                dialogueBox.GetComponent<Image>().sprite = cinza;

                break;


            case 11:
                characterName.text = "Amarelo";
                dialogue.text = "Sim! Incêndios controlados, aceitos pelo governo e com ajuda de brigadas para não criar uma grande área de perigo! ⚠⚠⚠";
                dialogueBox.GetComponent<Image>().sprite = amarelo;
                amarelo_face.material = angry;
                break;


            case 12:
                characterName.text = "Amarelo";
                dialogue.text = "Também é nossa função conscientizar moradores rurais de como se prevenir de incêndios!";
                dialogueBox.GetComponent<Image>().sprite = amarelo;
                break;

            case 13:
                characterName.text = "Cinza";
                dialogue.text = "Hm. Falando nisso, o que acabamos de controlar foi um incêndio natural, causado por alguma faísca, ou um ato humano?";
                dialogueBox.GetComponent<Image>().sprite = cinza;

                break;

            case 14:
                characterName.text = "Amarelo";
                dialogue.text = "Eu honestamente não sei. Deixaremos o caso para investigação. Por enquanto, deveríamos nos concentrar em nossas missões! Ha, ha!";
                dialogueBox.GetComponent<Image>().sprite = amarelo;
                amarelo_face.material = happy;
                break;


            case 15:
                characterName.text = "";
                dialogue.text = "";
                dialogueBox.GetComponent<Image>().sprite = blank;
                dialogueBox.GetComponent<Animator>().SetTrigger("disappear");
                transitionScreen.GetComponent<Animator>().SetTrigger("appear");
                yield return new WaitForSeconds(0.5f);

                if (!HubManager.beatEverything && !HubManager.beatEverything)
                {
                    SceneManager.LoadScene("Hub");
                }
                else
                {
                    SceneManager.LoadScene("CutscenesRoom");
                }
                break;



        }


        yield return new WaitForSeconds(0.1f);

        id++;
    }

    IEnumerator preAM()
    {
        switch (id)
        {
            case 1:
                characterName.text = "Amarelo";
                dialogue.text = "Amazônia! Folhas grandes, floresta úmida e tropical!";
                dialogueBox.GetComponent<Image>().sprite = amarelo;

                break;

            case 2:
                characterName.text = "Roxo";
                dialogue.text = "E árvores caídas por onde passamos… Isso não é natural.";
                dialogueBox.GetComponent<Image>().sprite = roxo;
                break;

            case 3:
                characterName.text = "Roxo";
                dialogue.text = "Expansão da criação de gado, cultivo de soja, atividades de garimpo, exploração ilegal de madeira e a apropriação indevida de terras públicas…";
                dialogueBox.GetComponent<Image>().sprite = roxo;
                break;


            case 4:
                characterName.text = "Roxo";
                dialogue.text = "Cada vez mais a Amazônia se encontra num ponto de não-retorno.";
                dialogueBox.GetComponent<Image>().sprite = roxo;
                break;

            case 5:
                characterName.text = "Amarelo";
                dialogue.text = "Verdade, mas a gente ainda pode tentar reverter isso! Ou pelo menos proteger o que restou! Vamos lá Roxo! É hora de proteger o coração do país!";
                dialogueBox.GetComponent<Image>().sprite = amarelo;
                break;

            case 6:
                characterName.text = "Roxo";
                dialogue.text = "Hmph… Certo.";
                dialogueBox.GetComponent<Image>().sprite = roxo;

                break;

            case 7:
                characterName.text = "";
                dialogue.text = "";
                dialogueBox.GetComponent<Image>().sprite = blank;
                dialogueBox.GetComponent<Animator>().SetTrigger("disappear");
                transitionScreen.GetComponent<Animator>().SetTrigger("appear");
                yield return new WaitForSeconds(0.5f);

                if (!HubManager.beatEverything && !HubManager.beatEverything)
                {
                    SceneManager.LoadScene("Amazonia");
                }
                else
                {
                    SceneManager.LoadScene("CutscenesRoom");
                }
                break;


        }


        yield return new WaitForSeconds(0.1f);

        id++;
    }

    IEnumerator postAM()
    {
        switch (id)
        {
            case 1:
                characterName.text = "Amarelo";
                dialogue.text = "Bom trabalho, Roxo!";
                dialogueBox.GetComponent<Image>().sprite = amarelo;

                break;

            case 2:
                characterName.text = "Roxo";
                dialogue.text = "Essas áreas de desmatamento… Parece que seja quem fez isso, estava tentando esconder o quanto de destruição causou.";
                dialogueBox.GetComponent<Image>().sprite = roxo;
                break;

            case 3:
                characterName.text = "Azul";
                dialogue.text = "Só fez trocar um crime pelo outro.";
                dialogueBox.GetComponent<Image>().sprite = azul;
                break;


            case 4:
                characterName.text = "Cinza";
                dialogue.text = "Pior, no final ficou com os dois.";
                dialogueBox.GetComponent<Image>().sprite = cinza;
                break;

            case 5:
                characterName.text = "Roxo";
                dialogue.text = "Bem, não somos detetives, essa não é nossa função. vamos relatar às autoridades e continuar com nosso trabalho.";
                dialogueBox.GetComponent<Image>().sprite = roxo;
                break;


            case 6:
                characterName.text = "";
                dialogue.text = "";
                dialogueBox.GetComponent<Image>().sprite = blank;
                dialogueBox.GetComponent<Animator>().SetTrigger("disappear");
                transitionScreen.GetComponent<Animator>().SetTrigger("appear");
                yield return new WaitForSeconds(0.5f);


                if (!HubManager.beatEverything && !HubManager.beatEverything)
                {
                    SceneManager.LoadScene("Hub");
                }
                else
                {
                    SceneManager.LoadScene("CutscenesRoom");
                }
                break;




        }


        yield return new WaitForSeconds(0.1f);

        id++;
    }

    IEnumerator preCE()
    {
        switch (id)
        {
            case 1:
                characterName.text = "Amarelo";
                dialogue.text = "Aqui estamos nós! Cerrado! As famosas savanas brasileiras! A vegeração composta por capim é a primeira coisa que me vem à mente, ha, ha!";
                dialogueBox.GetComponent<Image>().sprite = amarelo;

                break;

            case 2:
                characterName.text = "Azul";
                dialogue.text = "Os incêndios deste lugar estão diretamente ligados com o desmatamento. O solo fica mais propício ao alastramento do fogo, natural ou ilegal.";
                dialogueBox.GetComponent<Image>().sprite = azul;
                break;

            case 3:
                characterName.text = "Vermelho";
                dialogue.text = "Mais um cenário perdendo sua biodiversidade por ações humanas…";
                dialogueBox.GetComponent<Image>().sprite = vermelho;
                break;


            case 4:
                characterName.text = "Amarelo";
                dialogue.text = "Enfim, vejamos o que temos aqui…";
                dialogueBox.GetComponent<Image>().sprite = amarelo;
                break;

            case 5:
                characterName.text = "Azul";
                dialogue.text = " É exatamente isso, com o aumento de queimadas para agropecuária, o solo ficou suscetível para que o fogo se alastrasse, destruindo tudo que vê pela frente.";
                dialogueBox.GetComponent<Image>().sprite = azul;
                break;

            case 6:
                characterName.text = "Vermelho";
                dialogue.text = "Enquanto isso é verdade, esse incêndio em específico foi causado por causas naturais. Biomassa, baixa umidade, alta temperatura…";
                dialogueBox.GetComponent<Image>().sprite = vermelho;
                break;

            case 7:
                characterName.text = "Azul";
                dialogue.text = "Ocasionados pela ação humana.";
                dialogueBox.GetComponent<Image>().sprite = azul;
                break;

            case 8:
                characterName.text = "Cinza";
                dialogue.text = "É mais correto dizer que foi agravado pela ação humana.";
                dialogueBox.GetComponent<Image>().sprite = cinza;
                break;

            case 9:
                characterName.text = "Cinza";
                dialogue.text = " Baixa umidade e alta temperatura são coisas naturais, mas as ações humanas, como a emissão de carbono no ar através de queimadas,";
                dialogueBox.GetComponent<Image>().sprite = cinza;
                break;

            case 10:
                characterName.text = "Cinza";
                dialogue.text = "aceleraram drasticamente o processo de aumento das queimadas naturais.";
                dialogueBox.GetComponent<Image>().sprite = cinza;
                break;

            case 11:
                characterName.text = "Vermelho";
                dialogue.text = "Ah! Meus satélites detectaram sinais de fogo recentes em locais que não estavam previstos acontecer.";
                dialogueBox.GetComponent<Image>().sprite = vermelho;
                break;

            case 12:
                characterName.text = "Amarelo";
                dialogue.text = "Estão iniciando um incêndio criminal!?";
                dialogueBox.GetComponent<Image>().sprite = amarelo;
                break;

            case 13:
                characterName.text = "Vermelho";
                dialogue.text = "É o mais provável. Vamos lá! É nossa função preservar esta área!";
                dialogueBox.GetComponent<Image>().sprite = vermelho;
                break;


            case 14:
                characterName.text = "";
                dialogue.text = "";
                dialogueBox.GetComponent<Image>().sprite = blank;
                dialogueBox.GetComponent<Animator>().SetTrigger("disappear");
                transitionScreen.GetComponent<Animator>().SetTrigger("appear");
                yield return new WaitForSeconds(0.5f);

                if (!HubManager.beatEverything && !HubManager.beatEverything)
                {
                    SceneManager.LoadScene("Cerrado");
                }
                else
                {
                    SceneManager.LoadScene("CutscenesRoom");
                }
                break;


        }


        yield return new WaitForSeconds(0.1f);

        id++;
    }

    IEnumerator postCE()
    {
        switch (id)
        {
            case 1:
                characterName.text = "Amarelo";
                dialogue.text = "Bom trabalho, Vermelho! Conseguiu descobrir a causa do incêndio que presenciamos?";
                dialogueBox.GetComponent<Image>().sprite = amarelo;

                break;

            case 2:
                characterName.text = "Vermelho";
                dialogue.text = " É o que comentamos. O fogo foi iniciado pela indústria de agropecuária para abrir espaço para pasto e agricultura.";
                dialogueBox.GetComponent<Image>().sprite = vermelho;
                break;

            case 3:
                characterName.text = "Amarelo";
                dialogue.text = "É claro que a agropecuária é um setor importante, mas certamente é necessário uma alternativa para tamanha destruição.";
                dialogueBox.GetComponent<Image>().sprite = amarelo;

                break;


            case 4:
                characterName.text = "Roxo";
                dialogue.text = "Queimadas só são a primeira alternativa por ser fácil e barato, apesar do preço a pagar a longo prazo.";
                dialogueBox.GetComponent<Image>().sprite = roxo;
                break;

            case 5:
                characterName.text = "Azul";
                dialogue.text = "É sempre bom ressaltar a emissão de gases que contribuem para o aquecimento global.";
                dialogueBox.GetComponent<Image>().sprite = azul;
                break;

            case 6:
                characterName.text = "Cinza";
                dialogue.text = "A Embrapa vem buscando formas sustentáveis para realizar a limpeza sem precisar de queimadas. Plantio direto, sistemas agroflorestais…";
                dialogueBox.GetComponent<Image>().sprite = cinza;
                break;

            case 7:
                characterName.text = "Amarelo";
                dialogue.text = "Por isso o controle de incêndio é importante! Claro, não é ideal, mas evita que o fogo se alastre por mais que o necessário.";
                dialogueBox.GetComponent<Image>().sprite = amarelo;
                break;

            case 8:
                characterName.text = "Cinza";
                dialogue.text = "Seria um incêndio com autorização, certo?";
                dialogueBox.GetComponent<Image>().sprite = cinza;
                break;

            case 9:
                characterName.text = "Amarelo";
                dialogue.text = "Autorização do governo para planejar um incêndio, com requerimento e motivo claro. Brigadas como a nossa auxiliam na estruturação.";
                dialogueBox.GetComponent<Image>().sprite = amarelo;
                break;

            case 10:
                characterName.text = "Amarelo";
                dialogue.text = "O próprio site do governo tem informações para quem tiver interesse!";
                dialogueBox.GetComponent<Image>().sprite = amarelo;
                break;

            case 11:
                characterName.text = "Vermelho";
                dialogue.text = "Incêndios não regulados são configurados como crime.";
                dialogueBox.GetComponent<Image>().sprite = vermelho;
                break;

            case 12:
                characterName.text = "Amarelo";
                dialogue.text = "Enfim… Bom trabalho para nós! Que venha a próxima missão!";
                dialogueBox.GetComponent<Image>().sprite = amarelo;
                break;


            case 13:
                characterName.text = "";
                dialogue.text = "";
                dialogueBox.GetComponent<Image>().sprite = blank;
                dialogueBox.GetComponent<Animator>().SetTrigger("disappear");
                transitionScreen.GetComponent<Animator>().SetTrigger("appear");
                yield return new WaitForSeconds(0.5f);

                if (!HubManager.beatEverything && !HubManager.beatEverything)
                {
                    SceneManager.LoadScene("Hub");
                }
                else
                {
                    SceneManager.LoadScene("CutscenesRoom");
                }
                break;

        }

        yield return new WaitForSeconds(0.1f);

        id++;
    }

    IEnumerator preCA()
    {
        switch (id)
        {
            case 1:
                characterName.text = "Azul";
                dialogue.text = "Caatinga, um bioma completamente brasileiro, localizado majoritariamente no Nordeste.";
                dialogueBox.GetComponent<Image>().sprite = azul;

                break;

            case 2:
                characterName.text = "Azul";
                dialogue.text = "Ela é adaptada a períodos de seca, por passar por secas extremas e estiagem.";
                dialogueBox.GetComponent<Image>().sprite = azul;

                break;

            case 3:
                characterName.text = "Azul";
                dialogue.text = "Seu solo é pedregoso, dificultando o armazenamento de água. Sua exploração agrícola e de recursos naturais.";
                dialogueBox.GetComponent<Image>().sprite = azul;

                break;

            case 4:
                characterName.text = "Amarelo";
                dialogue.text = "…";
                dialogueBox.GetComponent<Image>().sprite = amarelo;

                break;


            case 5:
                characterName.text = "Amarelo";
                dialogue.text = "Huh. Geralmente sou eu quem faço os resumos. Mas você mandou muito bem, Azul! Eu não teria dito melhor!";
                dialogueBox.GetComponent<Image>().sprite = amarelo;
                break;

            case 6:
                characterName.text = "Azul";
                dialogue.text = "Já passei um bom tempo na Caatinga, conheço um pouco sobre. O terreno árido, as chamas que surgem naturalmente…";
                dialogueBox.GetComponent<Image>().sprite = azul;
                break;


            case 7:
                characterName.text = "Vermelho";
                dialogue.text = "Estiagem é o atraso de chuvas, né?";
                dialogueBox.GetComponent<Image>().sprite = vermelho;
                break;

            case 8:
                characterName.text = "Cinza";
                dialogue.text = "Resumidamente, sim.";
                dialogueBox.GetComponent<Image>().sprite = cinza;
                break;

            case 9:
                characterName.text = "Roxo";
                dialogue.text = "Eu sei que a Caatinga é quente, mas tá quente demais…";
                dialogueBox.GetComponent<Image>().sprite = roxo;
                break;

            case 10:
                characterName.text = "Azul";
                dialogue.text = "O clima seco e as queimadas naturais sempre foram características da região.";
                dialogueBox.GetComponent<Image>().sprite = azul;
                break;

            case 11:
                characterName.text = "Azul";
                dialogue.text = "Mas, graças ao aquecimento global, estamos experienciando um aumento anual mais elevado da temperatura.";
                dialogueBox.GetComponent<Image>().sprite = azul;
                break;

            case 12:
                characterName.text = "Amarelo";
                dialogue.text = "Bem, vamos manter o foco na missão! Não sabemos se o fogo que vamos enfrentar é criminoso ou natural, mas sabemos que nosso objetivo é apagá-lo!";
                dialogueBox.GetComponent<Image>().sprite = amarelo;

                break;



            case 13:
                characterName.text = "";
                dialogue.text = "";
                dialogueBox.GetComponent<Image>().sprite = blank;
                dialogueBox.GetComponent<Animator>().SetTrigger("disappear");
                transitionScreen.GetComponent<Animator>().SetTrigger("appear");
                yield return new WaitForSeconds(0.5f);


                if (!HubManager.beatEverything && !HubManager.beatEverything)
                {
                    SceneManager.LoadScene("Caatinga");
                }
                else
                {
                    SceneManager.LoadScene("CutscenesRoom");
                }
                break;


        }


        yield return new WaitForSeconds(0.1f);

        id++;
    }

    IEnumerator postCA()
    {
        switch (id)
        {
            case 1:
                characterName.text = "Amarelo";
                dialogue.text = "Bom trabalho, Azul! Sobrevivemos em meio a um calor de 35°C, isso sem contar o fogo! Ha, ha!";
                dialogueBox.GetComponent<Image>().sprite = amarelo;

                break;

            case 2:
                characterName.text = "Azul";
                dialogue.text = "Bom trabalho, pessoal. É nosso dever proteger e preservar os biomas, além de mitigar os efeitos das mudanças climáticas.";
                dialogueBox.GetComponent<Image>().sprite = azul;
                break;

            case 3:
                characterName.text = "Azul";
                dialogue.text = "Vamos dar nosso melhor.";
                dialogueBox.GetComponent<Image>().sprite = azul;
                break;

            case 4:
                characterName.text = "";
                dialogue.text = "";
                dialogueBox.GetComponent<Image>().sprite = blank;
                dialogueBox.GetComponent<Animator>().SetTrigger("disappear");
                transitionScreen.GetComponent<Animator>().SetTrigger("appear");
                yield return new WaitForSeconds(0.5f);

                if (!HubManager.beatEverything && !HubManager.beatEverything)
                {
                    SceneManager.LoadScene("Hub");
                }
                else
                {
                    SceneManager.LoadScene("CutscenesRoom");
                }

                break;


        }


        yield return new WaitForSeconds(0.1f);

        id++;
    }

    IEnumerator NormalEnding()
    {
        switch (id)
        {
            case 1:
                characterName.text = "Amarelo";
                dialogue.text = "Bom trabalho, todos vocês!";
                dialogueBox.GetComponent<Image>().sprite = amarelo;
                amarelo_face.material = happy;

                break;

            case 2:
                characterName.text = "Cinza";
                dialogue.text = "Não teríamos conseguido sem o senhor!";
                dialogueBox.GetComponent<Image>().sprite = cinza;
                cinza_face.material = angry;
                break;

            case 3:
                characterName.text = "Vermelho";
                dialogue.text = " Sim, sim!";
                dialogueBox.GetComponent<Image>().sprite = vermelho;
                vermelho_face.material = happy;
                break;

            case 4:
                characterName.text = "Roxo";
                dialogue.text = "Com uma liderança dessas, é claro que conseguir.";
                dialogueBox.GetComponent<Image>().sprite = roxo;
                roxo_face.material = angry;
                break;

            case 5:
                characterName.text = "Azul";
                dialogue.text = "Eu acho que todos nós merecemos o crédito.";
                dialogueBox.GetComponent<Image>().sprite = azul;
                azul_face.material = angry;
                break;

            case 6:
                characterName.text = "Amarelo";
                dialogue.text = "Aww, vocês vão me fazer corar!";
                dialogueBox.GetComponent<Image>().sprite = amarelo;
                amarelo_face.material = happy;

                break;

            case 7:
                characterName.text = "Amarelo";
                dialogue.text = "Eu acredito que se continuarmos dando duro, conseguiremos atingir limites ainda maiores!";
                dialogueBox.GetComponent<Image>().sprite = amarelo;
                amarelo_face.material = happy;

                break;



            case 8:
                characterName.text = "";
                dialogue.text = "";
                dialogueBox.GetComponent<Image>().sprite = blank;
                dialogueBox.GetComponent<Animator>().SetTrigger("disappear");
                transitionScreen.GetComponent<Animator>().SetTrigger("appear");
                yield return new WaitForSeconds(0.5f);
                SceneManager.LoadScene("Menu");
                break;


        }


        yield return new WaitForSeconds(0.1f);

        id++;
    }

    IEnumerator GoodEnding()
    {
        switch (id)
        {
            case 1:
                characterName.text = "";
                dialogue.text = "Após sua excelente performance defendendo o seu país de incêndios, a Brigada Brasal foi convidada ao Planalto Central para ser premiada com medalhas de honra.";
                dialogueBox.GetComponent<Image>().sprite = blank;


                break;

            case 2:
                characterName.text = "Amarelo";
                dialogue.text = "Ótimo trabalho, todos vocês!!!";
                dialogueBox.GetComponent<Image>().sprite = amarelo;
                amarelo_face.material = happy;
                break;

            case 3:
                characterName.text = "Cinza";
                dialogue.text = "Não teríamos conseguido sem o senhor!";
                dialogueBox.GetComponent<Image>().sprite = cinza;
                cinza_face.material = happy;
                break;

            case 4:
                characterName.text = "Vermelho";
                dialogue.text = "De forma alguma!!!";
                dialogueBox.GetComponent<Image>().sprite = vermelho;
                vermelho_face.material = happy;
                break;

            case 5:
                characterName.text = "Roxo";
                dialogue.text = "Com uma liderança dessas, é claro que íamos ser excepcionais.";
                dialogueBox.GetComponent<Image>().sprite = roxo;
                roxo_face.material = happy;
                break;

            case 6:
                characterName.text = "Azul";
                dialogue.text = "Eu acho que todos nós fomos incríveis!";
                dialogueBox.GetComponent<Image>().sprite = azul;
                azul_face.material = angry;
                break;

            case 7:
                characterName.text = "Amarelo";
                dialogue.text = "*Sniff sniff*... Acho que me emocionei!";
                dialogueBox.GetComponent<Image>().sprite = amarelo;
                amarelo_face.material = sad;
                break;

            case 8:
                characterName.text = "";
                dialogue.text = "Reconhecemos a bravura de sua brigada.";
                dialogueBox.GetComponent<Image>().sprite = blank;
                break;

            case 9:
                characterName.text = "";
                dialogue.text = "Apaixonados, alma e coração, ao arriscar suas vidas todos os dias para ajudar na vida de milhões de brasileiros.";
                dialogueBox.GetComponent<Image>().sprite = blank;
                break;

            case 10:
                characterName.text = "";
                dialogue.text = "É com felicidade que entregamos esta medalha de serviço.";
                dialogueBox.GetComponent<Image>().sprite = blank;
                break;



            case 11:
                characterName.text = "";
                dialogue.text = "";
                dialogueBox.GetComponent<Image>().sprite = blank;
                dialogueBox.GetComponent<Animator>().SetTrigger("disappear");
                transitionScreen.GetComponent<Animator>().SetTrigger("appear");
                yield return new WaitForSeconds(0.5f);
                SceneManager.LoadScene("Menu");
                break;


        }


        yield return new WaitForSeconds(0.1f);

        id++;
    }

    IEnumerator Cutscene1QuoteAppear()
    {
        transitionScreen.GetComponent<Animator>().SetTrigger("stayblack");

        yield return new WaitForSeconds(0.5f);

        quote.GetComponent<Animator>().SetTrigger("appear");
        almir.GetComponent<Animator>().SetTrigger("appear");

        yield return new WaitForSeconds(1f);

        skippable = true;

    }

    IEnumerator Cutscene1QuoteDisappear()
    {
        quote.GetComponent<Animator>().SetTrigger("disappear");
        almir.GetComponent<Animator>().SetTrigger("disappear");

        yield return new WaitForSeconds(1);

        transitionScreen.GetComponent<Animator>().SetTrigger("disappear");

        yield return new WaitForSeconds(0.5f);

        dialogueBox.GetComponent<Animator>().SetTrigger("appear");

        transitionScreen.SetActive(false);
        introQuote = false;

        yield return new WaitForSeconds(0.5f);

        skippable = true;

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
}
