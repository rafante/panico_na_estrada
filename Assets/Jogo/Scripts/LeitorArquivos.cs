using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
using System.Collections.Generic;

//Esta classe inicializa tudo. Ela lê tanto o arquivo que tem os sinais, como o arquivo que tem os quadros
public class LeitorArquivos : MonoBehaviour
{

    public string arquivoSinais; //nome do arquivo que guarda os sinais
    public string arquivoQuadros; //nome do arquivo que guarda os quadros
    public List<Quadro> quadrosCarregados; // lista com os quadros carregados na memória

    void Start()
    {
        inicializar();
    }

    //Método que inicializa carregando os sinais e os quadros
    public void inicializar()
    {
        Sinais.carregarSinais(arquivoSinais);
        carregarQuadros();
    }

    public void carregarQuadros()
    {
        TextAsset asset = Resources.Load<TextAsset>(arquivoQuadros);
        string quadrosTexto = asset.text;
        string[] linhas = quadrosTexto.Split('\n');

        List<Quadro> quadros = new List<Quadro>();
        Quadro quadro = null;
        for (int i = 0; i < linhas.Length; i++)
        {
            string linha = linhas[i];
            string linhaEditar = linha;
            int ignorarEnter = linha.IndexOf('\r');
            if (ignorarEnter >= 0)
            {
                linhaEditar = linha.Remove(ignorarEnter);
            }
            if (linhaEditar.Contains(Sinais.chaves["INICIO_QUADRO"]))
            {
                quadro = new Quadro();
            }
            else if (linhaEditar.Contains(Sinais.chaves["FIM_QUADRO"]))
            {
                quadros.Add(quadro);
                quadro = null;
            }
            else if (linhaEditar.Contains(Sinais.chaves["CHAVE_TRADUCAO"]))
            {
                quadro.adicionarTraducao(linhaEditar);
            }
            else if (linhaEditar.Contains(Sinais.chaves["TEXTO"]))
            {
                quadro.adicionarTexto(linhaEditar);
            }
            else if (linhaEditar.Contains(Sinais.chaves["LINK"]))
            {
                quadro.adicionarLink(linhaEditar);
            }
            else if (linhaEditar.Contains(Sinais.chaves["VARIAVEL"]))
            {
                quadro.criarAlterarVariavel(linhaEditar);
            }
            else if (linhaEditar.Contains(Sinais.chaves["CONDICAO"]))
            {
                quadro.adicionarCondicao(linhaEditar);
            }
            else if (linhaEditar.Contains(Sinais.chaves["CHAVE"]))
            {
                quadro.insereChave(linhaEditar);
            }
            else if (linhaEditar.Contains(Sinais.chaves["INICIO_HISTORIA"]))
            {
                quadro.marcarInicio();
            }
            else if(linhaEditar.Contains(Sinais.chaves["IMAGEM"])){
                quadro.setarImagem(linhaEditar);
            }
        }
        foreach (var q in quadros)
        {
            Debug.Log(q.ToString());
        }

        quadrosCarregados = quadros;
    }
}