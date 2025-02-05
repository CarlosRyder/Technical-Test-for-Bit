using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private Transform camTransform;
    private Transform playerTransform;
    private BoxCollider2D levelLimit;
    private float cameraSizeVertical;
    private float cameraSizeHorizontal;

    void Awake()
    {
        camTransform = transform;
        playerMovement = FindFirstObjectByType<PlayerMovement>();
        playerTransform = playerMovement.transform;
        levelLimit = GameObject.Find("LevelLimit").GetComponent<BoxCollider2D>();
        cameraSizeVertical = Camera.main.orthographicSize;
        cameraSizeHorizontal = Camera.main.orthographicSize * Camera.main.aspect;
    }

    void Update()
    {
        if (playerTransform != null)
        {
            camTransform.position = new Vector3(Mathf.Clamp(playerTransform.position.x, levelLimit.bounds.min.x + cameraSizeHorizontal, levelLimit.bounds.max.x - cameraSizeHorizontal),
                                                (Mathf.Clamp(playerTransform.position.y, levelLimit.bounds.min.y + cameraSizeVertical, levelLimit.bounds.max.y - cameraSizeVertical)),
                                                camTransform.position.z);
        }
    }
}
