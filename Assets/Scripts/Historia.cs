using UnityEngine;
using System;
using System.Collections.Generic;

public class Historia
{

    public List<Quadro> quadros;
    public LeitorArquivos leitor;
    public Dictionary<string, string> variaveis;

    public Quadro atual;

    void Start()
    {
        inicializar();
    }

    public void inicializar()
    {
        variaveis = new Dictionary<string, string>();
        leitor.inicializar();
        quadros = leitor.quadrosCarregados;
        foreach (var quadro in quadros)
        {
            if (quadro.eOInicio())
            {
                atual = quadro;
                break;
            }
        }
    }

    public List<Quadro> proximos()
    {
        if (atual != null)
        {
            List<string> links = atual.obterLinks();
            List<Quadro> linksPossiveis = new List<Quadro>();
            List<Quadro> quadrosLinks = new List<Quadro>();
            foreach (var link in links)
            {
                Quadro quadro = null;
                foreach (Quadro q in quadros)
                {
                    if (q.obterChave() == link)
                    {
                        quadro = q;
                        break;
                    }
                }
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