using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapComponentBase : MonoBehaviour, IMapComponent
{
    protected Transform _position;

    public Transform ComponentPosition 
    {
        get => _position;       
    }

    public virtual void OnDestroyComponent()
    {
        Destroy(gameObject);
    }
}
