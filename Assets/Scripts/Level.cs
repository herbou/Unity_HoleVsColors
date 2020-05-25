using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
	#region Singleton class: Level

	public static Level Instance;

	void Awake ()
	{
		if (Instance == null) {
			Instance = this;
		}
	}

	#endregion

	[SerializeField] ParticleSystem winFx;

	[Space]
	//remaining objects
	[HideInInspector] public int objectsInScene;
	//total objects at the beginning
	[HideInInspector] public int totalObjects;

	//the Objects parent
	[SerializeField] Transform objectsParent;

	[Space]
	[Header ("Materials & Sprites")]
	[SerializeField] Material groundMaterial;
	[SerializeField] Material objectMaterial;
	[SerializeField] Material obstacleMaterial;
	[SerializeField] SpriteRenderer groundBorderSprite;
	[SerializeField] SpriteRenderer groundSideSprite;
	[SerializeField] Image progressFillImage;

	[SerializeField] SpriteRenderer bgFadeSprite;

	[Space]
	[Header ("Level Colors-------")]
	[Header ("Ground")]
	[SerializeField] Color groundColor;
	[SerializeField] Color bordersColor;
	[SerializeField] Color sideColor;

	[Header ("Objects & Obstacles")]
	[SerializeField] Color objectColor;
	[SerializeField] Color obstacleColor;

	[Header ("UI (progress)")]
	[SerializeField] Color progressFillColor;

	[Header ("Background")]
	[SerializeField] Color cameraColor;
	[SerializeField] Color fadeColor;


	void Start ()
	{
		CountObjects ();
		UpdateLevelColors ();
	}

	void CountObjects ()
	{
		//Count collectable white objects
		totalObjects = objectsParent.childCount;
		objectsInScene = totalObjects;
	}

	public void PlayWinFx ()
	{
		winFx.Play ();
	}

	public void LoadNextLevel ()
	{
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
	}

	public void RestartLevel ()
	{
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
	}

	void UpdateLevelColors ()
	{
		groundMaterial.color = groundColor;
		groundSideSprite.color = sideColor;
		groundBorderSprite.color = bordersColor;

		obstacleMaterial.color = obstacleColor;
		objectMaterial.color = objectColor;

		progressFillImage.color = progressFillColor;

		Camera.main.backgroundColor = cameraColor;
		bgFadeSprite.color = fadeColor;
	}

	void OnValidate ()
	{
		//This method will exeute whenever you change something of this script in the inspector
		//this method won't be included in the final Build (Editor only)

		UpdateLevelColors ();
	}
}
