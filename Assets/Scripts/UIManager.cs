using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
	#region Singleton class: UIManager

	public static UIManager Instance;

	void Awake ()
	{
		if (Instance == null) {
			Instance = this;
		}
	}

	#endregion

	[Header ("Level Progress UI")]
	//sceneOffset: because you may add other scenes like (Main menu, ...)
	[SerializeField] int sceneOffset;
	[SerializeField] TMP_Text nextLevelText;
	[SerializeField] TMP_Text currentLevelText;
	[SerializeField] Image progressFillImage;

	[Space]
	[SerializeField] TMP_Text levelCompletedText;

	[Space]
	//white fading panel at the start
	[SerializeField] Image fadePanel;

	void Start ()
	{
		FadeAtStart ();

		//reset progress value
		progressFillImage.fillAmount = 0f;

		SetLevelProgressText ();
	}

	void SetLevelProgressText ()
	{
		int level = SceneManager.GetActiveScene ().buildIndex + sceneOffset;
		currentLevelText.text = level.ToString ();
		nextLevelText.text = (level + 1).ToString ();
	}

	public void UpdateLevelProgress ()
	{
		float val = 1f - ((float)Level.Instance.objectsInScene / Level.Instance.totalObjects);
		//transition fill (0.4 seconds)
		progressFillImage.DOFillAmount (val, .4f);
	}

	//--------------------------------------
	public void ShowLevelCompletedUI ()
	{
		//fade in the text (from 0 to 1) with 0.6 seconds
		levelCompletedText.DOFade (1f, .6f).From (0f);
	}

	public void FadeAtStart ()
	{
		//fade out the panel (from 1 to 0) with 1.3 seconds
		fadePanel.DOFade (0f, 1.3f).From (1f);
	}
}
