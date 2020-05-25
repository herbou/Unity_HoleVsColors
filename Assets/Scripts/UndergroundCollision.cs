using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class UndergroundCollision : MonoBehaviour
{

	void OnTriggerEnter (Collider other)
	{
		//Object or Obstacle is at the bottom of the Hole

		if (!Game.isGameover) {
			string tag = other.tag;
			//------------------------ O B J E C T --------------------------
			if (tag.Equals ("Object")) { 
				Level.Instance.objectsInScene--;
				UIManager.Instance.UpdateLevelProgress ();

				//Make sure to remove this object from Magnetic field
				Magnet.Instance.RemoveFromMagnetField (other.attachedRigidbody);

				Destroy (other.gameObject);

				//check if win
				if (Level.Instance.objectsInScene == 0) {
					//no more objects to collect (WIN)
					UIManager.Instance.ShowLevelCompletedUI ();
					Level.Instance.PlayWinFx ();

					//Load Next level after 2 seconds
					Invoke ("NextLevel", 2f);
				}
			}
			//---------------------- O B S T A C L E -----------------------
			if (tag.Equals ("Obstacle")) {
				Game.isGameover = true;
				Destroy (other.gameObject);

				//Shake the camera for 1 second
				Camera.main.transform
					.DOShakePosition (1f, .2f, 20, 90f)
					.OnComplete (() => {
					//restart level after shaking complet
					Level.Instance.RestartLevel ();
				});
			}
		}
	}

	void NextLevel ()
	{
		Level.Instance.LoadNextLevel ();
	}
		
}
