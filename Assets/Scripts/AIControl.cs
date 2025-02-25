using UnityEngine;

public class AIControl : MonoBehaviour
{
    public float moveSpeed = 8f;
    private Transform ballTarget;
    private Rigidbody2D aiRigidbody;

    private float minBoundaryX = -4.6f;
    private float maxBoundaryX = 4.7f;
    private float minBoundaryY = 0f;
    private float maxBoundaryY = 6.65f;

    void Start()
    {
        aiRigidbody = GetComponent<Rigidbody2D>();
        if (aiRigidbody == null)
        {
            Debug.LogError("Rigidbody2D não encontrado em AIControl.");
            enabled = false;
            return;
        }

        GameObject ball = GameObject.FindGameObjectWithTag("Ball");
        if (ball != null)
        {
            ballTarget = ball.transform;
        }
        else
        {
            Debug.LogError("Bola não encontrada! Verifique a tag 'Ball'.");
            enabled = false;
            return;
        }
    }

    void FixedUpdate()
    {
        if (ballTarget == null || aiRigidbody == null)
        {
            return;
        }

        Vector2 moveDirection = ballTarget.position - transform.position;
        Vector2 intendedMovement = moveDirection.normalized * moveSpeed * Time.fixedDeltaTime;

        Vector2 newAiPosition = (Vector2)transform.position + intendedMovement;
        newAiPosition.x = Mathf.Clamp(newAiPosition.x, minBoundaryX, maxBoundaryX);
        newAiPosition.y = Mathf.Clamp(newAiPosition.y, minBoundaryY, maxBoundaryY);

        aiRigidbody.MovePosition(newAiPosition);

        if (Vector2.Distance(transform.position, ballTarget.position) < 1.0f)
        {
            Rigidbody2D ballRigidbody = ballTarget.GetComponent<Rigidbody2D>();
            if (ballRigidbody != null)
            {
                ballRigidbody.AddForce(moveDirection.normalized * 2f, ForceMode2D.Impulse);
            }
        }
    }
}
