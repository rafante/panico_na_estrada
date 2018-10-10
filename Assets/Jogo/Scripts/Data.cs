using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : MonoBehaviour {

	public GameObject[] Datas;
	public static int dinheiro;


	void Awake (){

		Datas = GameObject.FindGameObjectsWithTag ("DATA");
		
		if(Datas.Length >= 2){
			Destroy (Datas[0]);
		}

		DontDestroyOnLoad (transform.gameObject);

	}

	void Start (){
		if(PlayerPrefs.HasKey ("dinheiro")){
			dinheiro = PlayerPrefs.GetInt ("dinheiro");
		}else
		{
			PlayerPrefs.SetInt("dinheiro", dinheiro);
		}
	}

	public void Salvar(){
		PlayerPrefs.SetInt("dinheiro", dinheiro);
	}

	
	


}
