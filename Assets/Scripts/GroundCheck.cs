using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private LayerMask groundLayer;
    public bool isGrounded;

    private float checkDistance = 0.2f;

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkDistance, groundLayer);
    }
}
