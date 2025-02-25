using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private Rigidbody2D playerRigidbody;
    public float movementSpeed = 10f;
    private Camera gameCamera;
    public float upperLimitY = 0f;

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        gameCamera = Camera.main;

        if (playerRigidbody == null)
        {
            Debug.LogError("Rigidbody2D não encontrado em PlayerControl.");
            enabled = false;
        }
        if (gameCamera == null)
        {
            Debug.LogError("Câmera principal não encontrada.");
            enabled = false;
        }
    }

    void FixedUpdate()
    {
        Vector3 cursorScreenPosition = Input.mousePosition;
        Vector3 cursorWorldPosition = gameCamera.ScreenToWorldPoint(cursorScreenPosition);
        cursorWorldPosition.z = transform.position.z;

        Vector2 adjustedPosition = new Vector2(
            Mathf.Clamp(cursorWorldPosition.x, gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x, gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x),
            Mathf.Clamp(cursorWorldPosition.y, gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y, upperLimitY)
        );

        playerRigidbody.MovePosition(adjustedPosition);
    }
}
