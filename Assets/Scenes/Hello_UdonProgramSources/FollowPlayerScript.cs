using UdonSharp;
using UnityEngine;
using VRC.SDKBase;

public class FollowPlayerScript : UdonSharpBehaviour
{
    private Vector3 playerPosition;
    public float moveSpeed = 5.0f; // Speed at which the cube follows the player

    private void Start()
    {
        // Get the initial position of the local player's avatar
        playerPosition = Networking.LocalPlayer.GetPosition();
    }

    private void Update()
    {
        //Vector3 distanceToPlayer = playerPosition - transform.position;
        //float distanceToPlayerF = distanceToPlayer.magnitude;
        
       
        playerPosition = Networking.LocalPlayer.GetPosition();
        Vector3 direction = playerPosition - transform.position;
        direction.Normalize();
        transform.position += direction * moveSpeed * Time.deltaTime;
        

        
      
        
    }
}
