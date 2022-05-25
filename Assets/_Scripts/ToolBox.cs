using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum lookDirection
{
    Left,
    Right,
    Up,
    Down
}

public enum tankClass
{
    none,
    Tank1,
    Tank2,
    Tank3,
    Mine
}

public class ToolBox : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    // Receives a Transform, Vector3 and rotationspeed(optional), returns a Quaternion with new look direction
    public static Quaternion LookAt(Transform objectToMove, Vector3 targetPosition, float rotationSpeed = 5f)
    {
        Vector2 dir = targetPosition - objectToMove.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        Quaternion returnMe = Quaternion.Slerp(objectToMove.rotation, rotation, rotationSpeed * Time.deltaTime);
        return returnMe;
    }

    // Receives a Transform, Vector3 and speed(optional), gives back the new Vector3 to move in target direction
    public static Vector3 MoveTo(Transform objectToMove, Vector3 targetPosition, float speed = 1f)
    {
        Vector3 direction = (targetPosition - objectToMove.position).normalized;
        Vector3 movementVector = objectToMove.position + (direction * speed * Time.deltaTime);
        return movementVector;
    }

    // Return a Vector3 in relation to the lookDirection Enum for the standard
    public static Vector3 StandardLookDirection(lookDirection myLookDirection, Transform objectTransfrom)
    {
        Vector3 setDirection;

        if (myLookDirection == lookDirection.Left)
            setDirection = Vector3.left;
        else if (myLookDirection == lookDirection.Right)
            setDirection = Vector3.right;
        else if (myLookDirection == lookDirection.Up)
            setDirection = Vector3.up;
        else
            setDirection = Vector3.down;

        setDirection += objectTransfrom.position;

        return setDirection;
    }

    // Receives a Transform and moves the object to mouse position
    public static void FollowMouse(GameObject objectToMove)
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        objectToMove.transform.position = mousePos;
    }  
}
