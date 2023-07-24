using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
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
        PickUp
    }

    public Player _attachedPlayer;
    Rigidbody2D _rb;

    public LimbType _limbType; //this will help most with animations
    public LimbState _limbState;

    [Header("Customizable")]
    [SerializeField] float _throwSpeed;
    [Tooltip("Put In Angle as a radian")]
    [SerializeField] float _throwAngle;
    [SerializeField] float _rotationSpeed;
    private Vector2 _throwVelocity;

    private void Start()
    {
        _throwVelocity.x = _throwSpeed * Mathf.Cos(_throwAngle);
        _throwVelocity.y = _throwSpeed * Mathf.Sin(_throwAngle);
    }

    public void ThrowLimb()
    {
        _limbState = LimbState.Throwing;
        _rb.velocity = _throwVelocity;
    }

    public void LimbAttack()
    {

    }

    private void OnCollisionEnter2D(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if(collision.gameObject.GetComponent<Player>().CanPickUpLimb(this))
            {
                _attachedPlayer = collision.gameObject.GetComponent<Player>();
            }
        }
    }
}
