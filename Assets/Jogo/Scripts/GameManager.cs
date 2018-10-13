using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public GameObject painelPause;
	public bool estaPausado = false;

	public GameObject painelDecisao;
	public GameObject panico;
	public GameObject audios;
	bool somOff = false;

	public void audioOnOff(){
		
		GetComponent<AudioSource>().enabled = false;
		
	}

	public void Pause(){

		if(estaPausado){
			painelPause.SetActive(false);
			estaPausado =  false;
		}else{
			painelPause.SetActive(true);
			estaPausado = true;
		}
	}



	public void Painel(){

		if(estaPausado){
			painelDecisao.SetActive(false);
			estaPausado =  false;

		}else{
			painelDecisao.SetActive(true);
			estaPausado = true;
			panico.SetActive(false);
		}
	}


	public void QuitGame(){
		Application.Quit();
	}

	public void Carregador(string nome){
		SceneManager.LoadScene (nome);
	}

	public void Recomeca(){
		PlayerPrefs.SetString("jogo_salvo", "");
		PlayerPrefs.SetString("bgm", "");
		Carregador("cena_principal");
	}

	
}
