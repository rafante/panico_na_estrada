using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class Sinais
{
    private static string[] _chaves = new string[]{
        "CHAVE_TRADUCAO",
        "INICIO_QUADRO",
        "FIM_QUADRO"
    };

    public static Dictionary<string, string> chaves = new Dictionary<string, string>();

    public static void carregarSinais(string arquivoSinais)
    {
        TextAsset asset = Resources.Load<TextAsset>(arquivoSinais);
        string arquivo = asset.text;
        string[] linhas = arquivo.Split('\n');
        foreach (var linha in linhas)
        {
            string linhaEditar = linha;
            int ignorarEnter = linha.IndexOf('\r');
            if (ignorarEnter >= 0)
            {
                linhaEditar = linha.Remove(ignorarEnter);
            }
            string[] chaveValor = linhaEditar.Split('=');
            if (!chaves.ContainsKey(chaveValor[0]))
                chaves.Add(chaveValor[0], chaveValor[1]);
            else
                chaves[chaveValor[0]] = chaveValor[1];
            Debug.Log("chave:" + chaveValor[0] + "|valor:" + chaveValor[1]);
        }
        Debug.Log("Fim");
    }


}