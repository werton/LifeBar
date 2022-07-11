using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceComponent<T> : MonoBehaviour where T : class
{
    [SerializeField] private MonoBehaviour _component;

    public T _implmentation;

    public T Implmentation
    {
        get
        {
            if (_implmentation != null)
            {
                return _implmentation;
            }

            if (_component)
            {
                if (_component is T)
                {
                    _implmentation = (T)(object)_component;
                }
                else
                {
                    Debug.LogError("The component does not implement the interface.");
                }
            }
            else
            {
                _implmentation = GetComponent<T>();
            }

            if (_implmentation != null)
            {
                return _implmentation;
            }
            else
            {
                Debug.LogError("Interface implementation not found on this object.");
            }

            return null;

        }
    }
}
