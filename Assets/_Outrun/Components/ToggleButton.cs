using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleButton : MonoBehaviour
{
    public int DefaultState;
    public List<Sprite> StateIcons;

    private Image innerIcon;
    private int state;

    // Start is called before the first frame update
    void Start()
    {
        state = DefaultState;
        if (transform.childCount > 0)
            innerIcon = transform.GetChild(0).GetComponent<Image>();
        UpdateValues();
    }

    public void Toggle()
    {
        state = ((state + 1) < StateIcons.Count) ? (state + 1) : 0;
        UpdateValues();
    }

    private void UpdateValues()
    {
        var icon = StateIcons[state];
        innerIcon.sprite = icon;
    }
}
