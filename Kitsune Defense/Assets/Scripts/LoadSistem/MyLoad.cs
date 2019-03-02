using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MyLoad {

	public static void Loading(string Name){

		Persistence.NextLevel = Name;
		SceneManager.LoadScene ("Load");

	}

}
