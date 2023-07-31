using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D _rb;
    Player _player;
    PlayerJump _playerJump;

    [Header("Input")]
    [SerializeField] InputActionReference inputMove;
    public float input;

    [Header("Customizable")]
    [SerializeField] float _2LegMoveSpeed;
    [SerializeField] float _1LegMoveSpeed;
    [SerializeField] float _2ArmMoveSpeed;
    [SerializeField] float _1ArmMoveSpeed;
    [SerializeField] float _rollMoveSpeed;
    [SerializeField] float _airSpeed;

    [SerializeField] float _startMovePoint = 0.5f;
    [SerializeField] float _smoothMoveSpeed = 0.06f; //the higher the number the less responsive it gets

    Vector3 zeroVector = Vector3.zero;

    public bool facingRight;


    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _player = GetComponent<Player>();
        _playerJump = GetComponent<PlayerJump>();
    }

    public void Move(Player.LimbState state)
    {
        float moveSpeed = 0f;
        input = Input.GetAxisRaw("Horizontal"); //changed to old input system
        if (input <= -_startMovePoint)
        {
            moveSpeed = -1f;
            facingRight = false;
        }
        else if (input >= _startMovePoint)
        {
            moveSpeed = 1f;
            facingRight = true;
        }

        switch (state)
        {
            case Player.LimbState.TwoLeg:
                moveSpeed *= _2LegMoveSpeed;
                break;
            case Player.LimbState.OneLeg:
                moveSpeed *= _1LegMoveSpeed;
                break;
            case Player.LimbState.TwoArm:
                moveSpeed *= _2ArmMoveSpeed;
                break;
            case Player.LimbState.OneArm:
                moveSpeed *= _1ArmMoveSpeed;
                break;
            case Player.LimbState.NoLimb:
                moveSpeed *= _rollMoveSpeed;
                break;
            default: break;
        }

        Vector3 targetVelocity = new Vector2(moveSpeed, _rb.velocity.y);
        _rb.velocity = Vector3.SmoothDamp(_rb.velocity, targetVelocity, ref zeroVector, _smoothMoveSpeed);
    }
}
