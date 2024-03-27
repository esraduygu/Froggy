using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float xPos;
    [SerializeField] private float minY;
    [SerializeField] private float maxY;

    private void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            BehaviorUtilities.Move(transform, Vector3.up);
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            BehaviorUtilities.Move(transform, Vector3.down);
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            BehaviorUtilities.Move(transform, Vector3.left);
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) 
            BehaviorUtilities.Move(transform, Vector3.right);
        
        LimitPosition();
    }

    private void LimitPosition()
    {
        var newPosition = transform.position;
        var clampedX = Mathf.Clamp(newPosition.x, -xPos, xPos);
        var clampedY = Mathf.Clamp(newPosition.y, minY, maxY);
        newPosition = new Vector3(clampedX, clampedY, 0);
        
        transform.position = newPosition;
    }
}
