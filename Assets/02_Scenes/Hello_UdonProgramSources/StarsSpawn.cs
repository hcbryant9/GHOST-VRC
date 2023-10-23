
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class StarsSpawn : UdonSharpBehaviour
{
    public GameObject star;
    
    public void SpawnObject()
    {
        //Spawning A Star
        Debug.Log("spawning a star");
        Instantiate(star, transform.position, transform.rotation);
        Instantiate(star, transform.position, transform.rotation);
        Instantiate(star, transform.position, transform.rotation);
    }
}
