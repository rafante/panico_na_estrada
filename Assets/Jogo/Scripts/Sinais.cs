using UnityEngine;
using System.IO;
using System.Collections.Generic;


//Classe que carrega os sinais e armazena dentro de um array de strings as chaves e de um dicionário de string,string
//os sinais de fato
public class Sinais
{
    private static string[] _chaves = new string[]{
        "CHAVE",
        "CHAVE_TRADUCAO",
        "INICIO_QUADRO",
        "VARIAVEL",
        "LINK",
        "CONDICAO",
        "LINK_TEXTO",
        "FIM_QUADRO",
        "IMAGEM",
        "AUDIOS",
        "BGM"
    };

    public static Dictionary<string, string> chaves = new Dictionary<string, string>(); //dicionário dos sinais

    //método que carrega os sinais
    public static void carregarSinais(string arquivoSinais)
    {
        //Método Resources.Load do Unity que carrega arquivos dentro da pasta Assets/Resources
        //a parte do <TextAsset> serve pra carregar o arquivo como arquivo de texto (e não de imagem, som, etc)
        TextAsset asset = Resources.Load<TextAsset>(arquivoSinais);
        string arquivo = asset.text; //o asset.text é o texto de fato
        string[] linhas = arquivo.Split('\n'); //quebra o texto em linhas usando o \n que é inserido automaticamente quando alguém dá enter
        foreach (var linha in linhas) //loop nas linhas
        {
            string linhaEditar = linha;
            int ignorarEnter = linha.IndexOf('\r'); //remove o caractere \r que é inserido de forma invisível indicando que ao saltar uma
                                                    //linha, é pro cursor voltar lá pro começo na outra linha
            if (ignorarEnter >= 0)
            {
                linhaEditar = linha.Remove(ignorarEnter); //se ele encontrar o \r remove ele da linha
            }
            string[] chaveValor = linhaEditar.Split('='); //uma vez que pegou a linha algumacoisa=outracoisa, dá um split em duas strings
                                                          //divididas pelo sinal = pra pegar o que é igual a o que
            if (!chaves.ContainsKey(chaveValor[0])) // o que ficou na chaveValor[0] é o nome do sinal
                chaves.Add(chaveValor[0], chaveValor[1]); // o que ficou na chaveValor[1] é o valor do sinal (o simbolo de fato)
            else
                chaves[chaveValor[0]] = chaveValor[1];
            Debug.Log("chave:" + chaveValor[0] + "|valor:" + chaveValor[1]); //printa os sinais lá no console pra conferir se deu certo
        }
        Debug.Log("Fim");
    }


}