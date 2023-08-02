using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

[CreateAssetMenu(fileName ="Limb", menuName ="ScriptableObjects/Limb", order = 1)]
public class LimbsData : ScriptableObject
{
    [Header("Name")]
    public new string limbName;

    [Header("Stats")]
    [SerializeField] private float _throwSpeed;
    [SerializeField] private float _angularvelocity;
    [SerializeField] private float _knockback;
    [SerializeField] private float _angularVelocity;
}
public class Limb : MonoBehaviour
{ 
    public enum LimbType
    {
        Arm, 
        Leg
    }

    public enum LimbState
    {
        Attached,
        Throwing,
        Returning,
        PickUp
    }

    public Player _attachedPlayer;
    public Transform _anchorPoint;
    Rigidbody2D _rb;

    public LimbType _limbType; //this will help most with animations
    public LimbState _limbState;

    [Header("Customizable")]
    [SerializeField] float _throwSpeed;
    [Tooltip("Put In Angle as a radian")]
    [SerializeField] float _throwAngle;
    [SerializeField] float _angularVelocity;
    private Vector2 _throwVelocity;

    private void Start()
    {
        _limbState = LimbState.PickUp;
        _rb = GetComponent<Rigidbody2D>();
        _throwVelocity.x = _throwSpeed * Mathf.Cos(_throwAngle);
        _throwVelocity.y = _throwSpeed * Mathf.Sin(_throwAngle);
    }

    public void ThrowLimb(int direction)
    {
        _rb.simulated = true;
        _limbState = LimbState.Throwing;
        _throwVelocity.x *= direction;
        _rb.velocity = _throwVelocity;
        _rb.angularVelocity = _angularVelocity;
    }

    public void LimbAttack()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && _limbState == LimbState.PickUp)
        {
            if(collision.gameObject.GetComponent<Player>().CanPickUpLimb(this))
            {
                _attachedPlayer = collision.gameObject.GetComponent<Player>();
            }
        }
    }
}
