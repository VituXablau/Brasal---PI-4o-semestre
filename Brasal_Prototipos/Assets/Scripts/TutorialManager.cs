using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] Sprite amarelo, vermelho, roxo, azul;
    [SerializeField] TextMeshProUGUI characterName, dialogue;
    [SerializeField] GameObject dialogueBox, arrow, continueButton;

    bool skippable;

    int id = 0;


    // Start is called before the first frame update
    void Start()
    {
        skippable = false;
        dialogueBox.GetComponent<Animator>().SetTrigger("hidden");
        StartCoroutine(Beginning());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (skippable)
            {
                StartCoroutine(Tutorial());
                dialogueBox.GetComponent<Animator>().SetTrigger("blink");
            }
        }
    }

    IEnumerator Beginning()
    {
        yield return new WaitForSeconds(0.5f);

        dialogueBox.GetComponent<Animator>().SetTrigger("appear");
        StartCoroutine(Tutorial());
        StartCoroutine(Wait());
    }
    IEnumerator Wait()
    {
        skippable = false;
        yield return new WaitForSeconds(0.5f);
        skippable = true;
    }

    IEnumerator Tutorial()
    {
        switch (id)
        {
            case 1:
                characterName.text = "Amarelo";
                dialogue.text = " Este é seu bioma! É nessa área que você anda, Cinza! Após o tutorial, você poderá clicar com o mouse onde você quer ir!";
                dialogueBox.GetComponent<Image>().sprite = amarelo;
                break;

            case 2:
                characterName.text = "Amarelo";
                dialogue.text = "Você sempre está com sua Bomba Costal Anti-Incêndio equipada! Se aproxime de um foco de incêndio para apagá-lo!";
                dialogueBox.GetComponent<Image>().sprite = amarelo;
                break;

            case 3:
                characterName.text = "Amarelo";
                dialogue.text = "No topo da tela, você vai encontrar os ícones das ferramentas que você pode utilizar! Deixarei Vermelho explicar!";
                dialogueBox.GetComponent<Image>().sprite = amarelo;
                break;

            case 4:
                characterName.text = "Vermelho";
                dialogue.text = "Obrigada, Amarelo!";
                dialogueBox.GetComponent<Image>().sprite = vermelho;

                yield return new WaitForSeconds(0.1f);
                id++;
                id++;
                break;

            case 7:
                characterName.text = "Vermelho";
                dialogue.text = "As ferramentas te darão uma grande ajuda no combate aos incêndios! Cada ferramenta demora 15 segundos para carregar.";
                dialogueBox.GetComponent<Image>().sprite = vermelho;
                break;

            case 8:
                characterName.text = "Vermelho";
                dialogue.text = "Quando uma ferramenta estiver carregada, você pode selecioná-la apertando sua tecla correspondente no teclado!";
                dialogueBox.GetComponent<Image>().sprite = vermelho;
                break;

            case 9:
                characterName.text = "Vermelho";
                dialogue.text = "Após selecionar uma ferramenta, você deve clicar com o botão direito do mouse na área do cenário que deseja utilizá-la.";
                dialogueBox.GetComponent<Image>().sprite = vermelho;
                break;

            case 10:
                characterName.text = "Vermelho";
                dialogue.text = "A primeira ferramenta é o Superdrone! Ao apertar a tela [1], na parte superior do teclado, você pode selecioná-lo.";
                dialogueBox.GetComponent<Image>().sprite = vermelho;
                arrow.GetComponent<Animator>().SetTrigger("1");
                break;

            case 11:
                characterName.text = "Vermelho";
                dialogue.text = "Você também pode colocar ele em áreas sem incêndio. Caso a área onde o Superdrone se encontra comece a pegar fogo, ele agirá contra o incêndio automaticamente!";
                dialogueBox.GetComponent<Image>().sprite = vermelho;
                break;


            case 12:
                characterName.text = "Vermelho";
                dialogue.text = "Você pode ter vários Superdrones no mapa ao mesmo tempo, mas lembre-se que cada Superdrone some após realizar sua função.";
                dialogueBox.GetComponent<Image>().sprite = vermelho;

                break;



            case 13:
                characterName.text = "Vermelho";
                dialogue.text = "Logo depois do Superdrone, você vai ver o ícone para o Satélite de Detecção de Calor!";
                dialogueBox.GetComponent<Image>().sprite = vermelho;
                arrow.GetComponent<Animator>().SetTrigger("2");

                break;

            case 14:
                characterName.text = "Vermelho";
                dialogue.text = "Ao ser acionado, você irá receber informações de onde os próximos focos de incêndio podem surgir! Não é lindo o que a tecnologia é capaz de fazer?";
                dialogueBox.GetComponent<Image>().sprite = vermelho;

                break;

            case 15:
                characterName.text = "Vermelho";
                dialogue.text = " A próxima ferramenta é o Aspersor Inteligente! Você pode posicionar ele num local, e ele agirá contra o fogo ao seu redor imediatamente, sumindo após alguns segundos.";
                dialogueBox.GetComponent<Image>().sprite = vermelho;
                arrow.GetComponent<Animator>().SetTrigger("3");


                break;

            case 16:
                characterName.text = "Vermelho";
                dialogue.text = "A última ferramenta é o Sinalizador a laser!";
                dialogueBox.GetComponent<Image>().sprite = vermelho;
                arrow.GetComponent<Animator>().SetTrigger("4");

                break;


            case 17:
                characterName.text = "Vermelho";
                dialogue.text = "Ao apertar a tecla [4], você pode chamar a ajuda de um hidroavião gigante de combate a incêndio, que vai lhe ajudar a alcançar grandes áreas de incêndio que podem inclusive estar fora de alcance.";
                dialogueBox.GetComponent<Image>().sprite = vermelho;

                break;

            case 18:
                characterName.text = "Roxo";
                dialogue.text = "No topo central da tela, você pode ver o tempo de fase. Ele vai diminuindo enquanto você joga. Seu desempenho será avaliado após chegar a zero. Dê seu melhor nesses dois minutos, Cinza~";
                dialogueBox.GetComponent<Image>().sprite = roxo;
                arrow.GetComponent<Animator>().SetTrigger("5");
                break;

            case 19:
                characterName.text = "Roxo";
                dialogue.text = "Muito cuidado, Cinza. Perder mais de 50% da floresta vai lhe reprovar automaticamente. Mas mais que isso já é suficiente para aprovação.";
                dialogueBox.GetComponent<Image>().sprite = roxo;
                arrow.GetComponent<Animator>().SetTrigger("6");
                break;

               case 20:
                characterName.text = "Roxo";
                dialogue.text = "Caso consiga preservar pelo menos 50% da floresta, você ganhará uma medalha de conclusão.";
                dialogueBox.GetComponent<Image>().sprite = roxo;
                break;   

                 case 21:
                characterName.text = "Roxo";
                dialogue.text = "E se conseguir preservar pelo menos 75%, ganhará uma Medalha de Preservação!";
                dialogueBox.GetComponent<Image>().sprite = roxo;
                break;   

            case 22:
                characterName.text = "Vermelho";
                dialogue.text = "Sempre almeje 100%!! Mesmo que não seja possível! Mentalidade positiva!";
                dialogueBox.GetComponent<Image>().sprite = vermelho;

                break;

            case 23:
                characterName.text = "Azul";
                dialogue.text = "Já falamos da biodiversidade da Mata Atlântica. Os animais aqui são a alma da floresta! Tente salvá-los também.";
                arrow.GetComponent<Animator>().SetTrigger("7");
                dialogueBox.GetComponent<Image>().sprite = azul;

                break;

            case 24:
                characterName.text = "Azul";
                dialogue.text = "Ao parar na frente de um animal, você irá acalmá-lo, e ele contará como salvo!";
                dialogueBox.GetComponent<Image>().sprite = azul;

                break;

            case 25:
                characterName.text = "Azul";
                dialogue.text = "Cada animal que você salvar será marcado com um [x] nesta parte da interface! Tente salvar todos para ganhar uma Medalha de Proteção!";
                dialogueBox.GetComponent<Image>().sprite = azul;

                break;

            case 26:
                characterName.text = "Amarelo";
                dialogue.text = "Não deixe essas árvores pegarem fogo por muito tempo. Dê seu melhor, Cinza!";
                dialogueBox.GetComponent<Image>().sprite = amarelo;

                break;

            case 27:
                characterName.text = "";
                dialogue.text = "";
                dialogueBox.GetComponent<Animator>().SetTrigger("disappear");
                continueButton.GetComponent<Animator>().SetTrigger("appear");
                arrow.GetComponent<Animator>().SetTrigger("8");
                skippable = false;

                yield return new WaitForSeconds(0.3f);
                 dialogueBox.SetActive(false);

                break;














        }


        yield return new WaitForSeconds(0.1f);

        id++;
    }
}
