using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMapComponent
{
    public Transform ComponentPosition { get; }

    void OnDestroyComponent();
}
