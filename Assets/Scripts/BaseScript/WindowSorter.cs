using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowSorter : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ToFront(Transform winTransform)
    {
        winTransform.SetSiblingIndex(transform.childCount - 1);
    }
}
