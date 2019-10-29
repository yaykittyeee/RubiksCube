using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    
    private Cube cube;
    private Vector3 localRotation;
    public float rotateSpeed = 1f;
    private Transform cameraPivot;
    private bool isCubeTouch = false;
    private string touchFace;
    private Vector3 startingCubie;
    private Vector3 nextCubie;
    private int[] swipeDirection;
    private bool isInvalidSwipe;
    private bool isCurrentlyRotating = false;
    private float zoom;

    void Start()
    {
        //somethings not right, rotation is only working for 3x3x3
        transform.parent.transform.position = new Vector3(GameState.CubeSize / 2, GameState.CubeSize / 2, GameState.CubeSize / 2);
        cube = FindObjectOfType<Cube>();
        cameraPivot = transform.parent.transform;
    }
    
    void LateUpdate()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.touches[0];
            if (touch.phase == TouchPhase.Began)
            {
                //check if the user touched the cube
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                if (Physics.Raycast(ray, out RaycastHit hitInfo))
                {
                    if (hitInfo.rigidbody != null)
                    {
                        isCubeTouch = true;
                        startingCubie = hitInfo.collider.transform.parent.transform.position;
                        touchFace = DetermineTouchFace(hitInfo.normal);
                    }
                }
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                //check for swipe to next cubie
                if (isCubeTouch && !isCurrentlyRotating && !isInvalidSwipe)
                {
                    Ray ray = Camera.main.ScreenPointToRay(touch.position);
                    if (Physics.Raycast(ray, out RaycastHit hitInfo))
                    {
                        if (hitInfo.rigidbody != null && startingCubie != hitInfo.collider.transform.parent.transform.position)
                        {
                            if (DetermineTouchFace(hitInfo.normal) == touchFace)
                            {
                                nextCubie = hitInfo.collider.transform.parent.transform.position;
                                DetermineSwipeDirection();
                                DetermineIfInvalidSwipe();
                                if (!isInvalidSwipe)
                                {
                                    isCurrentlyRotating = true;
                                    this.HandleRotation();
                                }
                            }
                            else
                            {
                                isInvalidSwipe = true;
                            }
                        }
                    }
                }
                else if(!isCubeTouch)
                {
                    //rotate the camera around the cube
                    localRotation.x += touch.deltaPosition.x;
                    localRotation.y -= touch.deltaPosition.y;
                    Quaternion destination = Quaternion.Euler(localRotation.y, localRotation.x, 0f);
                    cameraPivot.rotation = Quaternion.Slerp(cameraPivot.rotation, destination, Time.deltaTime * 10f);
                }
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                isCubeTouch = false;
                isCurrentlyRotating = false;
                isInvalidSwipe = false;
            }
        }
        else if (Input.GetAxis("Mouse ScrollWheel") != 0) //Input.touchCount == 2 || 
        {
            //zoom in and out
            //TODO: add touch zoom
            cameraPivot.transform.localScale = cameraPivot.transform.localScale * (1f - Input.GetAxis("Mouse ScrollWheel"));
        }

    }

    private void HandleRotation()
    {
        //rotate cube
        cube.BeginRotation(startingCubie, nextCubie, touchFace, swipeDirection);
    }

    private string DetermineTouchFace(Vector3 normal)
    {
        string face = "";
        switch (normal.ToString())
        {
            case "(0.0, 0.0, -1.0)":
                face = "front";
                break;
            case "(0.0, 0.0, 1.0)":
                face = "back";
                break;
            case "(-1.0, 0.0, 0.0)":
                face = "left";
                break;
            case "(1.0, 0.0, 0.0)":
                face = "right";
                break;
            case "(0.0, 1.0, 0.0)":
                face = "top";
                break;
            case "(0.0, -1.0, 0.0)":
                face = "bottom";
                break;
        }

        return face;
    }

    private void DetermineSwipeDirection()
    {
        //there's probably a better way to do this
        int changeX = (int)nextCubie.x - (int)startingCubie.x;
        int changeY = (int)nextCubie.y - (int)startingCubie.y;
        int changeZ = (int)nextCubie.z - (int)startingCubie.z;
        if (changeX != 0)
        {
            changeX = (changeX > 0) ? 1 : -1;
        }
        if (changeY != 0)
        {
            changeY = (changeY > 0) ? 1 : -1;
        }
        if (changeZ != 0)
        {
            changeZ = (changeZ > 0) ? 1 : -1;
        }
        swipeDirection = new int[]{changeX, changeY, changeZ};
    }

    private void DetermineIfInvalidSwipe()
    {
        // a diagonal swipe is invalid
        // TODO: move other invalid checks to this method?
        isInvalidSwipe = 1 != (Mathf.Abs(swipeDirection[0]) + Mathf.Abs(swipeDirection[1]) + Mathf.Abs(swipeDirection[2]));
    }

}
