using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public void QuitGame(){
		Application.Quit();
	}

	public void Carregador(string name){
		SceneManager.LoadScene (name);
	}

	
}
