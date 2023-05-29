using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class ArenaOutOfBoundsController : MonoBehaviour
{
    private PixelPerfectCamera mainCamera;
    private float halfCameraHeight;
    private float halfCameraWidth;
    private EdgeCollider2D edgeCollider;

    #region UnityMessages
    private void Awake()
    {
        mainCamera = Camera.main.gameObject.GetComponent<PixelPerfectCamera>();
        halfCameraHeight = ((mainCamera.refResolutionY / 2f) / mainCamera.assetsPPU) + 3f;
        halfCameraWidth = ((mainCamera.refResolutionX / 2f) / mainCamera.assetsPPU) + 3f;
        edgeCollider = GetComponent<EdgeCollider2D>();

        Vector2[] points =
        {
            new Vector2 (-halfCameraWidth, halfCameraHeight),
            new Vector2 (halfCameraWidth, halfCameraHeight),
            new Vector2 (halfCameraWidth, -halfCameraHeight),
            new Vector2 (-halfCameraWidth, -halfCameraHeight),
            new Vector2 (-halfCameraWidth, halfCameraHeight)
        };
        edgeCollider.points = points; 
    }
    #endregion
}
