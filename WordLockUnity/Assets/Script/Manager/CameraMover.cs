using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    public Transform player;

	public float smoothSpeed = 0.125f;
	public Vector3 offset;
	public Transform borderX, borderY;

	void FixedUpdate()
	{
		FollowPlayer();
	}

	void FollowPlayer()
    {
		Vector3 desiredPosition = player.position + offset;
		Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
		if(LevelManager.levelManager.sceneState == GameState.SceneState.Fighting || LevelManager.levelManager.sceneState == GameState.SceneState.SpellWrite)
        {
			smoothedPosition.x = Mathf.Clamp(smoothedPosition.x, -borderX.position.x, borderX.position.x);
			smoothedPosition.y = Mathf.Clamp(smoothedPosition.y, -borderY.position.y, borderY.position.y);
        }
		transform.position = smoothedPosition;
	}

}
