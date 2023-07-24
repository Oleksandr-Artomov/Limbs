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
        if (Input.GetKeyDown(KeyCode.RightShift) && buttonDown == false) //changed to old input system
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
        else if (Input.GetKeyDown(KeyCode.RightShift)) //changed to old input system
        {
            buttonDown = false;
        }


        _playerMovement.Move(_limbState);

        _playerJump.Jump();
    }
}
