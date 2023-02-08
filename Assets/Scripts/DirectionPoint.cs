using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionPoint : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 90;
    [SerializeField] float scalingRate = 0.1f;

    // Update is called once per frame
    void Update()
    {
        transform.localScale -= Vector3.one * scalingRate * Time.deltaTime;
        transform.Rotate(rotationSpeed * Time.deltaTime * Vector3.up);

        if (transform.localScale.sqrMagnitude <= 0.01)
            gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        transform.localScale = Vector3.one;
    }
}
