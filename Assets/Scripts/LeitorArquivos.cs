using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

public class LeitorArquivos : MonoBehaviour{

    public string arquivoSinais;

    void Start(){
        Sinais.carregarSinais(arquivoSinais);
    }
}