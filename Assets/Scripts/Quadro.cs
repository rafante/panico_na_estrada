using System;
using System.Collections.Generic;
using UnityEngine;

public class Quadro
{
    private string chave = "";
    private string texto = "";
    private bool inicio = false;
    private List<string> links = new List<string>();
    private List<string> traducoes = new List<string>();
    private Dictionary<string,string> variaveis = new Dictionary<string, string>();
    private Dictionary<string,string> condicoes = new Dictionary<string, string>();

    public Quadro(){

    }

    public bool eOInicio(){
        return inicio;
    }

    public string obterChave(){
        return chave;
    }

    public List<string> obterLinks(){
        return links;
    }

    public Dictionary<string,string> obterCondicoes(){
        return condicoes;
    }

    public void marcarInicio(){
        this.inicio = true;
    }

    public void insereChave(string linha){
        chave = linha.Split(Sinais.chaves["CHAVE"].ToCharArray())[1];
    }

    public void adicionarTraducao(string linha){
        traducoes.Add(linha.Split(Sinais.chaves["CHAVE_TRADUCAO"].ToCharArray())[1]);
    }

    public void adicionarTexto(string linha){
        texto += linha.Split(Sinais.chaves["TEXTO"].ToCharArray())[1];
    }

    public void adicionarLink(string linha){
        links.Add(linha.Split(Sinais.chaves["LINK"].ToCharArray())[1]);
    }

    public void criarAlterarVariavel(string linha){
        string[] split1 = linha.Split(Sinais.chaves["VARIAVEL"].ToCharArray());
        string varLinha = split1[1];
        char separador;
        if(varLinha.Contains("=")){
            separador = '=';
        }else if(varLinha.Contains("+")){
            separador = '+';
        }else if(varLinha.Contains("*")){
            separador = '*';
        }else{
            separador = ' ';
        }
        string[] split2 = varLinha.Split(separador);
        variaveis.Add(split2[0], split2[1]);
    }

    public void adicionarCondicao(string linha){
        string[] split1 = linha.Split(Sinais.chaves["CONDICAO"].ToCharArray());
        string varLinha = split1[1];
        char separador;
        if(varLinha.Contains("=")){
            separador = '=';
        }else if(varLinha.Contains("+")){
            separador = '+';
        }else if(varLinha.Contains("*")){
            separador = '*';
        }else{
            separador = ' ';
        }
        string[] split2 = varLinha.Split(separador);
        condicoes.Add(split2[0], split2[1]);
    }

    public override string ToString(){
        return chave + "\n" +
                texto + "\n" +
                "links:" +
                links.Count + 
                "\n" +
                "condicoes:" +
                condicoes.Count;
    }

}