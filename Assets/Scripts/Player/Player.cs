using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

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

    public enum SelectedLimb
    {
        LeftLeg,
        RightLeg,
        LeftArm,
        RightArm
    };

    PlayerMovement _playerMovement;
    PlayerJump _playerJump;

    [Header("Input")]
    [SerializeField] InputActionReference _changeLimbState; //for testing
    bool buttonDown = false;//for testing
    bool _limbuttonDown = false;//for testing

    //the location of the limb in the list dictates what limb it is
    List<Limb> _limbs; 


    LimbState _limbState;
    public MovementState _movementState;
    SelectedLimb _selectedLimb;


    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _playerJump = GetComponent<PlayerJump>();
        _limbs = new List<Limb>();
        _limbs.Capacity = 4;
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

        /*swapping limbs*/
        if (Input.GetKeyDown(KeyCode.Return) && _limbuttonDown == false) //changed to old input system
        {
            if (_selectedLimb == SelectedLimb.LeftLeg)
            {
                _selectedLimb = SelectedLimb.RightArm;
            }
            else
            {
                _selectedLimb++;
            }
            _limbuttonDown = true;
        }
        else if (Input.GetKeyDown(KeyCode.Return)) //changed to old input system
        {
            _limbuttonDown = false;
        }

        /*throwing limbs*/
        if (Input.GetMouseButtonDown(0) && _limbs[(int)_selectedLimb] != null) //left mouse button down
        {
            _limbs[(int)_selectedLimb].ThrowLimb();
        }
        if (Input.GetMouseButtonDown(1) && _limbs[(int)_selectedLimb] != null) //left mouse button down
        {
            _limbs[(int)_selectedLimb].LimbAttack();
        }

        /*horizontal movement*/
        _playerMovement.Move(_limbState);

        /*vertical movement*/
        _playerJump.Jump();
    }

    public bool CanPickUpLimb(Limb limb)
    {
        for (int i = 0; i < 4; i++)
        {
            if (_limbs[i] == null)
            {
                _limbs[i] = limb;
                AttachLimb(i);
                return true;
            }
        }
        return false;
    }

    private void AttachLimb(int i)
    {
        if (i == 0 || i == 1)
        {
            _limbs[i]._limbType = Limb.LimbType.Leg;
        }
        else
        {
            _limbs[i]._limbType = Limb.LimbType.Arm;
        }
        //set the limb into the proper position
    }
}
