using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimbManager : MonoBehaviour
{
    public List<Limb> _limbs;

    void Start()
    {
        _limbs = new List<Limb>();
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Limb");
        for (int i = 0; i < gameObjects.Length; i++)
        {
            _limbs.Add(gameObjects[i].GetComponent<Limb>());
        }
    }

    void Update()
    {
        for (int i = 0; i < _limbs.Count; i++)
        {
            if (_limbs[i]._limbState == Limb.LimbState.Attached)
            {
                _limbs[i].transform.position = _limbs[i]._anchorPoint.position;
            }
            else if (_limbs[i]._limbState == Limb.LimbState.Throwing)
            {
                if (_limbs[i].GetComponent<Rigidbody2D>().velocity.magnitude < 3.0f)
                {
                    _limbs[i]._attachedPlayer.RemoveLimb(_limbs[i]);
                    _limbs[i]._limbState = Limb.LimbState.PickUp;
                }
            }
        }
    }
}
