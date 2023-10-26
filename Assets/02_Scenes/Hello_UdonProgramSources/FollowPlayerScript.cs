using UdonSharp;
using UnityEngine;
using VRC.SDKBase;

public class FollowPlayerScript : UdonSharpBehaviour
{
    private Vector3 playerPosition;
    public float moveSpeed = 4.0f; // Speed at which the cube follows the player
    public float maxDistance = 5.0f; // Maximum distance from the player
    private bool follow = false;
    private void Start()
    {
        // Get the initial position of the local player's avatar
        playerPosition = Networking.LocalPlayer.GetPosition();
        playerPosition.y = transform.position.y;
    }
    public void Follow()
    {
        follow = true;
    }
    private void Update()
    {
        if (follow)
        {
            playerPosition = Networking.LocalPlayer.GetPosition();
            playerPosition.y = transform.position.y;
            Vector3 direction = playerPosition - transform.position;


            float distance = direction.magnitude;

            if (distance > maxDistance)
            {
                direction.Normalize();
                transform.position += direction * moveSpeed * Time.deltaTime;

                // Make the model face the direction it's moving
                if (direction != Vector3.zero)
                {
                    transform.LookAt(transform.position + direction);
                }

            }
        }
        
       
        

    }
}
