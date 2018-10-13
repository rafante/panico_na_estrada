using System;
using System.Collections.Generic;
using UnityEngine;

public class Quadro
{
    private string chave = "";
    private string texto = "";
    private string imagem = "";
    private string audio = "";
    private string bgm = "";
    private bool inicio = false;
    private Dictionary<string, string> links = new Dictionary<string, string>();
    private List<string> traducoes = new List<string>();
    private Dictionary<string, string> variaveis = new Dictionary<string, string>();
    private Dictionary<string, string> condicoes = new Dictionary<string, string>();

    public Quadro()
    {
        
    }

    public bool eOInicio()
    {
        return inicio;
    }

    public string obterChave()
    {
        return chave;
    }

    public Dictionary<string, string> obterLinks()
    {
        return links;
    }

    public string obterImagem(){
        return imagem;
    }

    public string obterAudio(){
        return audio;
    }

    public string obterBgm(){
        return bgm;
    }

    public Dictionary<string, string> obterCondicoes()
    {
        return condicoes;
    }

    public string obterTexto()
    {
        return this.texto;
    }

    public void marcarInicio()
    {
        this.inicio = true;
    }

    public void insereChave(string linha)
    {
        chave = linha.Replace(Sinais.chaves["CHAVE"], "");
    }

    public void adicionarTraducao(string linha)
    {
        traducoes.Add(linha.Split(Sinais.chaves["CHAVE_TRADUCAO"].ToCharArray())[1]);
    }

    public void adicionarTexto(string linha)
    {
        texto += "\n" + linha.Replace(Sinais.chaves["TEXTO"], "");
    }

    public void setarImagem(string linha){
        imagem = linha.Replace(Sinais.chaves["IMAGEM"], "");
    }

    public void setarAudio(string linha){
        audio = linha.Replace(Sinais.chaves["AUDIO"], "");
    }

    public void setarBgm(string linha){
        bgm = linha.Replace(Sinais.chaves["BGM"], "");
    }


    

    public void adicionarLink(string linha)
    {
        string linhaEditar = linha.Replace(Sinais.chaves["LINK"], "");
        string[] textoQuadro = linhaEditar.Split(Sinais.chaves["SEPARADOR_LINK"].ToCharArray());
        links.Add(textoQuadro[1], textoQuadro[0]);
    }

    public void criarAlterarVariavel(string linha)
    {
        string[] split1 = linha.Split(Sinais.chaves["VARIAVEL"].ToCharArray());
        string varLinha = split1[1];
        char separador;
        if (varLinha.Contains("="))
        {
            separador = '=';
        }
        else if (varLinha.Contains("+"))
        {
            separador = '+';
        }
        else if (varLinha.Contains("*"))
        {
            separador = '*';
        }
        else
        {
            separador = ' ';
        }
        string[] split2 = varLinha.Split(separador);
        variaveis.Add(split2[0], split2[1]);
    }

    public void adicionarCondicao(string linha)
    {
        string[] split1 = linha.Split(Sinais.chaves["CONDICAO"].ToCharArray());
        string varLinha = split1[1];
        char separador;
        if (varLinha.Contains("="))
        {
            separador = '=';
        }
        else if (varLinha.Contains("+"))
        {
            separador = '+';
        }
        else if (varLinha.Contains("*"))
        {
            separador = '*';
        }
        else
        {
            separador = ' ';
        }
        string[] split2 = varLinha.Split(separador);
        condicoes.Add(split2[0], split2[1]);
    }

    public override string ToString()
    {
        return chave + "\n" +
                texto + "\n" +
                "links:" +
                links.Count +
                "\n" +
                "condicoes:" +
                condicoes.Count;
    }

}