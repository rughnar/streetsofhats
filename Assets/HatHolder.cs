using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using UnityEngine;

public class HatHolder : MonoBehaviour
{
    Stack<HatController> hats;


    void Awake()
    {
        Array hatControllers = GetComponentsInChildren<HatController>();
        hats = new Stack<HatController>();
        foreach (HatController hc in hatControllers)
        {
            hats.Push(hc);
        }
    }

    public GameObject RemoveHat()
    {
        if (hats.Count > 0)
        {
            return hats.Pop().gameObject;
        }
        return null;
    }

    public void AddHat(GameObject hat)
    {
        hat.transform.SetParent(this.gameObject.transform);
        hat.transform.localPosition = new Vector3(0f, hats.Count() * 0.3f);
        hats.Push(hat.GetComponent<HatController>());
    }
}
