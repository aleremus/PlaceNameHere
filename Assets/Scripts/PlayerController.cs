using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector3 moveToPoint;
    private Vector3 movementDirection;
    private Rigidbody playerRigidbody;
    private GameObject marker;

    public Camera mainCamera;
    public GameObject directionMarker;
    public bool isMoving;

    private PlayerCharacterstics playerCharacterstics;
    // Start is called before the first frame update
    void OnEnable()
    {
        moveToPoint = transform.position;
        playerRigidbody = gameObject.GetComponent<Rigidbody>();
        isMoving = false;
        playerCharacterstics = GameObject.Find("Player").GetComponent<PlayerCharacterstics>();

        marker = Instantiate(directionMarker);
        marker.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //move to point on the ground
        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        

        if (Input.GetMouseButton(0))
        {
            if (Physics.Raycast(ray, out hit))
            {
                moveToPoint = new Vector3(hit.point.x, moveToPoint.y, hit.point.z);
                transform.LookAt(moveToPoint);
                if (Input.GetMouseButtonDown(0))
                {
                        SetMarker(new Vector3(hit.point.x, 0, hit.point.z));
                }
            }
        }
        
        ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Input.GetMouseButton(1))
        {
            if (Physics.Raycast(ray, out hit))
            {
                transform.LookAt(new Vector3(hit.point.x, moveToPoint.y, hit.point.z));
                if (Input.GetMouseButtonDown(1))
                {
                    SetMarker(new Vector3(hit.point.x, 0, hit.point.z));
                }
            }
            moveToPoint = transform.position;
        }    
    }

    private void FixedUpdate()
    {
        movementDirection = (moveToPoint - transform.position);


        if ((transform.position - moveToPoint).sqrMagnitude < 0.01)
        {
            movementDirection = Vector3.zero;
            isMoving = false;
        }
        else isMoving = true;

        playerRigidbody.velocity = movementDirection.normalized * playerCharacterstics.playerSpeed * Time.deltaTime;
    }


    private void SetMarker(Vector3 position)
    {
        marker.SetActive(false);
        marker.SetActive(true);
        marker.transform.position = position;
    }
}
