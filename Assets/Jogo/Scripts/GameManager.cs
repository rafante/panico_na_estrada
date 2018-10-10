using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public GameObject painelPause;
	public bool estaPausado = false;

	public void Pause(){

		if(estaPausado){
			painelPause.SetActive(false);
			estaPausado =  false;
		}else{
			painelPause.SetActive(true);
			estaPausado = true;
		}
	}



	public void QuitGame(){
		Application.Quit();
	}

	public void Carregador(string name){
		SceneManager.LoadScene (name);
	}

	
}
