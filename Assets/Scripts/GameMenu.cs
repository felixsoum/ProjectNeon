using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
	public enum Animation
	{
		MenuFade,
		MenuEnd,
	}
	public Animator menuAnimator = null;

	public Text menuText = null;
	public Button menuButton = null;

	// Put all enemies under a parent and link it here, to detect when enemies are all dead
	public GameObject enemyContainer = null;



	void Start()
	{
		if( menuButton != null )
		{
			menuButton.gameObject.SetActive(false);
		}
	}

	void Update()
	{
		if( enemyContainer != null && enemyContainer.transform.childCount == 0 )
		{
			if( menuAnimator != null )
			{
				menuAnimator.Play( Animation.MenuEnd.ToString() );
				if( menuText != null && menuButton != null)
				{
					menuText.text = "SYSTEM CLEAN";
					menuButton.gameObject.SetActive(true);
				}
			}
		}
	}

	// The function only shows in the editor when it's public?
	public void OnButton_Restart()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

}
