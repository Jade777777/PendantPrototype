using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour, IInteractable
{
    [SerializeField]
    private GameObject _gate;
    public void Interact()
    {
        _gate.SetActive(!_gate.activeSelf);
    }


}
