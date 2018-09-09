using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class changjing : MonoBehaviour
{
	public int a;
	public void OnLoginButtonClick()
	{

		SceneManager.LoadScene(a);//1是场景的索引

	}

}
