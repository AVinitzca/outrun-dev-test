using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistanceListener : MonoBehaviour
{
    public CarEngine CarEngine;

    private Text text;

    void Start()
    {
        this.text = GetComponent<Text>();
    }

    // No point on doing an actual observer, this updates every time
    void Update()
    {
        this.text.text = CarEngine.DistanceTraveled + " km";
    }
}
