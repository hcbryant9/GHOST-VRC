using UdonSharp;
using UnityEngine;
using UnityEngine.AI;
using VRC.SDKBase;

public class aiFollow : UdonSharpBehaviour
{
    private Vector3 player;
    private NavMeshAgent agent;
    public float maxDistance = 5.0f; // Maximum distance to follow the player
    public GameManager manager;

    void Start()
    {
        player = Networking.LocalPlayer.GetPosition();
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        player = Networking.LocalPlayer.GetPosition();
        player.y = player.y + 1;
        if (player != null)
        {
            if (manager.hasStarted)
            {
                float distanceToPlayer = Vector3.Distance(transform.position, player);

                if (distanceToPlayer > maxDistance)
                {
                    // Set the destination to the player's position
                    agent.SetDestination(player);
                }
                else
                {
                    // Stop the agent when it's within the maxDistance
                    agent.ResetPath();
                }
            }
        }
    }
}
