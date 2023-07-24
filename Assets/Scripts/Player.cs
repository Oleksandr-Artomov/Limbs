using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerMovement))]
public class Player : MonoBehaviour
{
    public enum LimbState
    {
        TwoLeg,
        OneLeg,
        TwoArm,
        OneArm,
        NoLimb
    };

    public enum MovementState
    {
        Move,
        Jump
    };

    PlayerMovement _playerMovement;
    PlayerJump _playerJump;

    [Header("Input")]
    [SerializeField] InputActionReference _changeLimbState; //for testing
    bool buttonDown = false;//for testing


    LimbState _limbState;
    public MovementState _movementState;


    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _playerJump = GetComponent<PlayerJump>();
    }

    void Update()
    {
        if (_changeLimbState.action.ReadValue<float>() > 0.5f && buttonDown == false)
        {
            if (_limbState == LimbState.NoLimb)
            {
                _limbState = LimbState.TwoLeg;
            }
            else
            {
                _limbState++;
            }
            buttonDown = true;
        }
        else if (_changeLimbState.action.ReadValue<float>() == 0.0f)
        {
            buttonDown = false;
        }

        _playerJump.Jump();

        _playerMovement.Move(_limbState);
    }
}
