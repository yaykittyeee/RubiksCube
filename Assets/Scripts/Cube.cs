using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public Cubie cubie;
    private GameManager gameManager;
    private Cubie[,,] cubies;
    
    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        cubies = new Cubie[GameState.CubeSize, GameState.CubeSize, GameState.CubeSize];
    }
    
    void Update()
    {
        
    }

    public void GenerateCube()
    {
        for(int i=0; i<GameState.CubeSize; i++)
        {
            for(int j=0; j<GameState.CubeSize; j++)
            {
                for(int k=0; k<GameState.CubeSize; k++)
                {
                    Cubie newCubie = Instantiate(cubie, transform, false) as Cubie;
                    newCubie.transform.localPosition = new Vector3(i, j, k);
                    //cubie.CubeIndex.Set([i, j, k]); 
                    newCubie.Color = 0;
                    cubies[i,j,k] = newCubie;
                }
            }
        }
    }

    public void ScrambleCube()
    {
        //for Random amount of rotates between 8...16
        //get random axis 0...2 -> xyz
        //random direction -1...1 for each 
        //get random level 0...GameState.CubeSize
        //get list of cubies to rotate
        //call rotate with random values
    }

    public void BeginRotation(Vector3 startingCubie, Vector3 nextCubie, string cubeFace, int[] rotationDirection)
    {
        //determine rotation type
        char rotationAxis = 'x';
        ArrayList cubiesToRotate = new ArrayList();
        startingCubie.x = (int)Mathf.Round(startingCubie.x);
        startingCubie.y = (int)Mathf.Round(startingCubie.y);
        startingCubie.z = (int)Mathf.Round(startingCubie.z);


        if (cubeFace == "front" || cubeFace == "back")
        {
            if(DetermineWhatChanged(rotationDirection) == 'x')
            {
                //x changed
                rotationAxis = 'y';
                for(int i = 0; i < GameState.CubeSize; i++)
                {
                    for (int j = 0; j < GameState.CubeSize; j++)
                    {
                        for (int k = 0; k < GameState.CubeSize; k++)
                        {
                            Debug.Log(startingCubie.y);
                            if (j == startingCubie.y)
                            {
                                cubiesToRotate.Add(cubies[i, j, k]);
                            }
                        }
                    }
                }
            }
            else
            {
                // y changed
                rotationAxis = 'x';
                for (int i = 0; i < GameState.CubeSize; i++)
                {
                    for (int j = 0; j < GameState.CubeSize; j++)
                    {
                        for (int k = 0; k < GameState.CubeSize; k++)
                        {
                            if (i == startingCubie.x)
                            {
                                cubiesToRotate.Add(cubies[i, j, k]);
                            }
                        }
                    }
                }
            }
        }
        else if (cubeFace == "left" || cubeFace == "right")
        {
            if (DetermineWhatChanged(rotationDirection) == 'y')
            {
                //y changed
                rotationAxis = 'z';
                for (int i = 0; i < GameState.CubeSize; i++)
                {
                    for (int j = 0; j < GameState.CubeSize; j++)
                    {
                        for (int k = 0; k < GameState.CubeSize; k++)
                        {
                            if (k == startingCubie.z)
                            {
                                cubiesToRotate.Add(cubies[i, j, k]);
                            }
                        }
                    }
                }
            }
            else
            {
                // z changed
                rotationAxis = 'y';
                for (int i = 0; i < GameState.CubeSize; i++)
                {
                    for (int j = 0; j < GameState.CubeSize; j++)
                    {
                        for (int k = 0; k < GameState.CubeSize; k++)
                        {
                            if (j == startingCubie.y)
                            {
                                cubiesToRotate.Add(cubies[i, j, k]);
                            }
                        }
                    }
                }
            }
        }
        else if (cubeFace == "top" || cubeFace == "bottom")
        {
            if (DetermineWhatChanged(rotationDirection) == 'z')
            {
                //z changed
                rotationAxis = 'x';
                for (int i = 0; i < GameState.CubeSize; i++)
                {
                    for (int j = 0; j < GameState.CubeSize; j++)
                    {
                        for (int k = 0; k < GameState.CubeSize; k++)
                        {
                            if (i == startingCubie.x)
                            {
                                cubiesToRotate.Add(cubies[i, j, k]);
                            }
                        }
                    }
                }
            }
            else
            {
                // x changed
                rotationAxis = 'z';
                for (int i = 0; i < GameState.CubeSize; i++)
                {
                    for (int j = 0; j < GameState.CubeSize; j++)
                    {
                        for (int k = 0; k < GameState.CubeSize; k++)
                        {
                            if (k == startingCubie.z)
                            {
                                cubiesToRotate.Add(cubies[i, j, k]);
                            }
                        }
                    }
                }
            }
        }

        DoRotateCubies(cubiesToRotate, rotationAxis, rotationDirection);

    }

    private char DetermineWhatChanged(int[] rotationDirection)
    {
        if (rotationDirection[0] != 0)
        {
            return 'x';
        }
        else if (rotationDirection[1] != 0)
        {
            return 'y';
        }
        else
        {
            return 'z';
        }
    }

    private void DoRotateCubies(ArrayList cubiesToRotate, char rotationAxis, int[] rotationDirection)
    {
        Vector3 direction = new Vector3(rotationDirection[0], rotationDirection[1], rotationDirection[2]);
        GameObject newRotation = new GameObject();
        newRotation.transform.position = new Vector3(0f, 0f, 0f);

        Debug.Log(cubiesToRotate.Count);
        foreach(Cubie ctr in cubiesToRotate)
        {
            ctr.transform.parent = newRotation.transform;
        }

        //Vector3 rotation;
        //rotation.x = newRotation.transform.localRotation.x;
        //rotation.y = newRotation.transform.localRotation.y;
        //rotation.z = newRotation.transform.localRotation.z;
        //rotation.x = (rotationAxis == 'x') ? rotation.x += (90f * direction.x) : rotation.x;
        //rotation.y = (rotationAxis == 'y') ? rotation.y += (90f * direction.y) : rotation.y;
        //rotation.z = (rotationAxis == 'z') ? rotation.z += (90f * direction.z) : rotation.z;
        //newRotation.transform.Rotate(rotation, Space.World);
        Quaternion quaternion = Quaternion.Euler(direction.x, direction.y, direction.z);
        newRotation.transform.rotation = Quaternion.Lerp(newRotation.transform.rotation, quaternion, Time.deltaTime * 90f);

        foreach (Cubie ctr in cubiesToRotate)
        {
            ctr.transform.parent = transform;
        }

        //needs work
    }
}