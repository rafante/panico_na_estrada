using UnityEngine;
using System;

public class LinkQuadro : MonoBehaviour{
    public string textoLink;
    public string chaveLink;

    public void chamarQuadroLink(){
        Historia.instancia.chamarQuadro(chaveLink);
    }
}