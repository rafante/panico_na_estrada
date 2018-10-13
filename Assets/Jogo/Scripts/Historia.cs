using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(LeitorArquivos))]
public class Historia : MonoBehaviour
{
    public Text texto;
    public GameObject botaoLinkPrefab;
    public Transform botoesContainer;
    public List<Quadro> quadros;
    private LeitorArquivos leitor;
    public Dictionary<string, string> variaveis;
    public Image fundo;

    public Quadro atual;
    public static Historia instancia;
    public AudioSource som;
    public AudioSource BGM;


    void Awake()
    {
        if (instancia == null)
            instancia = this;
    }

    void Start()
    {
        inicializar();
    }

    public void Salvar()
    {
        PlayerPrefs.SetString("jogo_salvo", atual.obterChave());
    }

    public void inicializar()
    {
        leitor = GetComponent<LeitorArquivos>();
        variaveis = new Dictionary<string, string>();
        leitor.inicializar();
        //busca o primeiro quadro e coloca ele na propriedade "atual"
        quadros = leitor.quadrosCarregados;
        string jogoSalvo = PlayerPrefs.GetString("jogo_salvo");
        string bgmSalvo = PlayerPrefs.GetString("bgm");

        if (jogoSalvo != "") // Se encontrou um jogo salvo, o atual passa a ser o jogo salvo
        {
            foreach (var quadro in quadros)
            {
                if (quadro.obterChave() == jogoSalvo)
                {
                    atual = quadro;
                    break;
                }
            }
        }
        else  // Se não encontrou um jogo salvo, o atual passa a ser aquele que tem o sinal de inicio
        {
            foreach (var quadro in quadros)
            {
                if (quadro.eOInicio())
                {
                    atual = quadro;
                    break;
                }
            }
        }

        if (bgmSalvo != "") // Se encontrou um jogo salvo, o atual passa a ser o jogo salvo
        {
            foreach (var quadro in quadros)
            {
                if (quadro.obterChave() == bgmSalvo)
                {
                    atual = quadro;
                    break;
                }
            }
        }
        else  // Se não encontrou um jogo salvo, o atual passa a ser aquele que tem o sinal de inicio
        {
            foreach (var quadro in quadros)
            {
                if (quadro.eOInicio())
                {
                    atual = quadro;
                    break;
                }
            }
        }

        /* if(bgmSalvo != ""){
            tocaBgm(bgmSalvo);
        }*/

        mostraQuadroAtual();
    }

    public void chamarQuadro(string quadroAChamar)
    {
        atual = null;
        foreach (Quadro quadro in quadros)
        {
            if (quadro.obterChave() == quadroAChamar)
            {
                atual = quadro;
                break;
            }
        }
        if (atual == null)
            throw new Exception("Tentativa de chamar quadro inexistente: " + quadroAChamar);
        mostraQuadroAtual();
    }

    public void mostraQuadroAtual()
    {
        texto.text = atual.obterTexto();
        //obter os links possíveis
        List<Quadro> links = proximos();
        foreach (var botao in botoesContainer.GetComponentsInChildren<Button>())
        {
            DestroyImmediate(botao.gameObject);
        }
        if (atual.obterImagem() != "")
        {
            mostrarImagem();
        }


        if (atual.obterAudio() != "")
        {
            tocaSom();
        }

        if (atual.obterBgm() != "")
        {
            tocaBgm(atual.obterBgm());
        }

        foreach (var link in atual.obterLinks())
        {
            GameObject botao = Instantiate(botaoLinkPrefab);
            botao.GetComponentInChildren<Text>().text = link.Value;
            if (botao.GetComponentInChildren<Text>().text == "")
                botao.GetComponentInChildren<Text>().text = "Continuar";
            botao.GetComponent<LinkQuadro>().textoLink = link.Value;
            botao.GetComponent<LinkQuadro>().chaveLink = link.Key;
            botao.transform.SetParent(botoesContainer);
            botao.transform.localScale = Vector3.one;
            botao.transform.position = Vector3.zero;
        }
    }

    public void tocaSom()
    {
        string audio = atual.obterAudio();
        AudioClip clip = Resources.Load<AudioClip>("Sons/" + audio);
        if (clip != null)
        {
            som.clip = clip;
            som.Play();
        }
    }

    public void tocaBgm(string somBgm)
    {
        AudioClip bgm = Resources.Load<AudioClip>("Sons/BGM/" + somBgm);
        if (bgm != null)
        {
            BGM.clip = bgm;
            BGM.Play();

        }
    }

    public void mostrarImagem()
    {
        string imagem = atual.obterImagem();
        Sprite img = Resources.Load<Sprite>("Imagens/" + imagem);
        if (img != null)
            fundo.sprite = img;
    }


    public List<Quadro> proximos()
    {
        if (atual != null)
        {
            Dictionary<string, string> links = atual.obterLinks();
            List<Quadro> linksPossiveis = new List<Quadro>();
            List<Quadro> quadrosLinks = new List<Quadro>();
            foreach (var link in links)
            {
                Quadro quadro = null;
                foreach (Quadro q in quadros)
                {
                    Debug.Log("Comparando " + q.obterChave() + " com " + link.Key);
                    if (q.obterChave() == link.Key)
                    {
                        quadro = q;
                        break;
                    }
                }
                if (quadro == null)
                    throw new Exception("Não encontrado quadro com link: " + link.Key);
                quadrosLinks.Add(quadro);
            }

            foreach (Quadro qLink in quadrosLinks)
            {
                Dictionary<string, string> condicoes = qLink.obterCondicoes();
                bool adicionar = true;

                foreach (var condicao in condicoes)
                {
                    if (!variaveis.ContainsKey(condicao.Key) || variaveis[condicao.Key] != condicao.Value)
                    {
                        adicionar = false;
                        break;
                    }
                }
                if (adicionar)
                    linksPossiveis.Add(qLink);
            }
            return linksPossiveis;
        }
        return null;
    }
}