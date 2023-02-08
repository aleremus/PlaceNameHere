using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Indicator : MonoBehaviour
{
    public GameObject redCyllinder;
    public GameObject greenCyllinder;

    public bool isOn;
    private bool lastState;

    // Start is called before the first frame update
    void Start()
    {
        redCyllinder.SetActive(true);
        greenCyllinder.SetActive(false);
        isOn = false;
        lastState = isOn;
    }

    

    public void Switch()
    {
        redCyllinder.active = !redCyllinder.active;
        greenCyllinder.active = !greenCyllinder.active;
        isOn = !isOn;
    }

    private void OnEnable()
    {
        redCyllinder.SetActive(true);
        greenCyllinder.SetActive(false);
        isOn = false;
        lastState = isOn;
    }
}
