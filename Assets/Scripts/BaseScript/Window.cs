using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Window : MonoBehaviour
{
    const int TABheight = 50;

    public string functionName;

    [SerializeField]
    Button minimizeBtn, normalizeBtn, maximizeBtn, closeBtn;
    Text nameText;

    WindowSorter sorter;
    Rect winRect;
    WinState winState;
    enum WinState
    {
        normal = 0, minimum, maximum
    }
    bool onMaxim;

    Vector3 clickPos;

    // Use this for initialization
    void Start()
    {
        nameText.text = functionName;
        sorter = transform.parent.GetComponent<WindowSorter>();
    }

    // Update is called once per frame
    void Update()
    {
        /*switch (winState)
        {
            case WinState.minimum:
                break;
            case WinState.normal:
                break;
            case WinState.maximum:
                break;
        }*/

        if (Input.GetMouseButtonDown(0))
        {
            clickPos = Input.mousePosition - transform.position;
        }
        if (Input.GetMouseButton(0))
        {
            Drag();
        }
    }



    #region Button Methods
    public void Minimize()
    {
        winState = WinState.minimum;
    }

    public void Maximize()
    {
        winState = WinState.maximum;
        sorter.ToFront(transform);
    }

    public void Close()
    {
        Destroy(gameObject);
    }

    public void Drag()
    {
        transform.position = Input.mousePosition - clickPos;

    }
    #endregion
}