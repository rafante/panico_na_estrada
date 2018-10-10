using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Salvar : MonoBehaviour {

	public void Guardar (){
		PlayerPrefs.SetInt("dinheiro", Data.dinheiro);
	}
}
