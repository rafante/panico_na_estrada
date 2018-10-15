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
	public GameObject botaoProximo;
	public GameObject grupoBotoes;
	bool somOff = false;

	public void audioOnOff(){
		
		GetComponent<AudioSource>().enabled = false;
		
	}

	public void Pause(){
		Debug.Log("pausou");
		if(estaPausado){
			botaoProximo.SetActive(true);
			grupoBotoes.SetActive(true);
			
			painelPause.SetActive(false);
			estaPausado =  false;
		}else{
			botaoProximo.SetActive(false);
			grupoBotoes.SetActive(false);
			
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
