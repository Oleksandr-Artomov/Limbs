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
    //left leg
    //right leg
    //left arm
    //right arm
    public List<Limb> _limbs;
    [SerializeField] List<Transform> _limbAnchors;


    LimbState _limbState;
    public MovementState _movementState;
    SelectedLimb _selectedLimb;

    //facing left = -1, right = 1
    int direction;


    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _playerJump = GetComponent<PlayerJump>();
        _limbs = new List<Limb>();
        _limbs.Capacity = 4;
        for (int i = 0; i < 4; i++)
        {
            _limbs.Add(null);
        }
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
            //for testing
            if (_limbs[(int)_selectedLimb] != null)
            {
                _limbs[(int)_selectedLimb].GetComponent<SpriteRenderer>().color = Color.green;
            }

            if (_selectedLimb == SelectedLimb.RightArm)
            {
                _selectedLimb = SelectedLimb.LeftLeg;
            }
            else
            {
                _selectedLimb++;
            }

            //for testing
            if (_limbs[(int)_selectedLimb] != null)
            {
                _limbs[(int)_selectedLimb].GetComponent<SpriteRenderer>().color = Color.red;
            }

            _limbuttonDown = true;
        }
        else if (Input.GetKeyUp(KeyCode.Return)) //changed to old input system
        {
            _limbuttonDown = false;
        }

        /*throwing limbs*/
        if (Input.GetMouseButtonDown(0) && _limbs[(int)_selectedLimb] != null && _limbs[(int)_selectedLimb]._limbState == Limb.LimbState.Attached) //left mouse button down
        {
            _limbs[(int)_selectedLimb].ThrowLimb(direction);
        }
        if (Input.GetMouseButtonDown(1) && _limbs[(int)_selectedLimb] != null && _limbs[(int)_selectedLimb]._limbState == Limb.LimbState.Attached) //left mouse button down
        {
            _limbs[(int)_selectedLimb].LimbAttack();
        }

        /*horizontal movement*/
        _playerMovement.Move(_limbState);

        /*vertical movement*/
        _playerJump.Jump();

        //check if player is facing right
        if (_playerMovement.facingRight)
        {
            direction = 1;
        }
        else
        {
            direction = -1;
        }
    }

    public bool CanPickUpLimb(Limb limb)
    {
        //check if limb is alread picked up
        for (int i = 0; i < 4; i++)
        {
            if (_limbs[i] == limb)
            {
                return false;
            }
        }

        //check if there is an empty spot 
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

        _limbs[i]._anchorPoint = _limbAnchors[i].transform;
        _limbs[i]._limbState = Limb.LimbState.Attached;
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), _limbs[i].GetComponent<Collider2D>(), true);
        _limbs[i].GetComponent<Rigidbody2D>().simulated = false;
    }

    public void RemoveLimb(Limb limb)
    {
        for (int i = 0; i < _limbs.Count; i++)
        {
            if (limb == _limbs[i])
            {
                Physics2D.IgnoreCollision(GetComponent<Collider2D>(), _limbs[i].GetComponent<Collider2D>(), false);
                _limbs[i] = null;
            }
        }
    }
}
