using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJump : MonoBehaviour
{
    [SerializeField] GroundCheck _groundCheck;
    [SerializeField] InputActionReference _jumpInput;
    Rigidbody2D _rb;
    Player _player;

    bool _canJump;

    [SerializeField]
    private float _jumpHeight;
    [SerializeField]
    private float _jumpTime;
    [SerializeField]
    private float _fallFasterGravityFactor;
    [SerializeField]
    private float _earlyExitGravityFactor;

    private float _gravityScaleFactor;
    private float _jumpGravity;
    private float _initJumpSpeed;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _player = GetComponent<Player>();

        _jumpGravity = -2 * _jumpHeight / Mathf.Pow(_jumpTime, 2);
        _gravityScaleFactor = _jumpGravity / Physics2D.gravity.y;
        _rb.gravityScale = _gravityScaleFactor;
        _initJumpSpeed = -_jumpGravity * _jumpTime;
    }

    public void Jump()
    {
        if (_jumpInput.action.ReadValue<float>() > 0.5f && _groundCheck.isGrounded && _canJump)
        {
            StartJump();
        }
        else if (_player._movementState == Player.MovementState.Jump)
        {
            JumpUpdate();
        }

        if (_jumpInput.action.ReadValue<float>() == 0.0f)
        {
            _canJump = true;
        }
    } 

    private void StartJump()
    {
        _canJump = false;
        _rb.AddForce(_rb.mass * Vector2.up * _initJumpSpeed, ForceMode2D.Impulse);
    }

    private void JumpUpdate()
    {
        if (_rb.velocity.y < 0)
        {
            _rb.gravityScale = _gravityScaleFactor * _fallFasterGravityFactor;
        }
        else if (_rb.velocity.y > 0 && _jumpInput.action.ReadValue<float>() < 0.5f)
        {
            _rb.gravityScale = _gravityScaleFactor * _earlyExitGravityFactor;
        }
        else
        {
            _rb.gravityScale = _gravityScaleFactor;
        }

        if (_groundCheck.isGrounded)
        {
            _player._movementState = Player.MovementState.Move;
        }
    }

    public bool IsGrounded()
    {
        return _groundCheck.isGrounded;
    }
}
