using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Limb : MonoBehaviour
{
    [SerializeField]
    private LimbData _limbData ;
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
    private Vector2 _throwVelocity;

    private void Start()
    {
        _limbState = LimbState.PickUp;
        _rb = GetComponent<Rigidbody2D>();
        _attachedPlayer = GetComponent<Player>();
        


        float angle = _limbData._throwAngle * Mathf.Deg2Rad;

        _throwVelocity.x = _limbData._throwSpeed * Mathf.Cos(angle);
        _throwVelocity.y = _limbData._throwSpeed * Mathf.Sin(angle);
    }

    public void ThrowLimb(int direction)
    {
        _rb.simulated = true;
        _limbState = LimbState.Throwing;
        _throwVelocity.x *= direction;
        _rb.velocity = _throwVelocity;
        _rb.angularVelocity = _limbData._angularVelocity;
    }

    public void LimbAttack()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
    }

    // Limb knockback and pickup
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && _limbState == LimbState.PickUp)
        {
            if (collision.gameObject.GetComponent<Player>().CanPickUpLimb(this))
            {
                _attachedPlayer = collision.gameObject.GetComponent<Player>();
            }
        }

        if (collision.gameObject.tag == "Player" && _limbState == LimbState.Throwing)
        {
            //Knockback

            //Take Damage

            //_attachedPlayer._playerHealth
            
            Debug.Log("Limb hit");
        }
    }
}
