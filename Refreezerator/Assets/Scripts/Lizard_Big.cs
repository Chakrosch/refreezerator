using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lizard_Big : MonoBehaviour
{
private float direction_change_timer = 5f;
public Rigidbody rb;
public float speed;
public int chaseDistance;
private bool playerNearby= false;
public GameObject player;

// Start is called before the first frame update
void Start()
{

}

// Update is called once per frame
void Update()
{
updatePlayerDistance();
if (playerNearby)
{
    ChaseMove();
}
else
{
    IdleMove();
}
}

void ChaseMove()
{
Vector3 movement = new Vector3(
    player.transform.position.x - this.transform.position.x,
    player.transform.position.y - this.transform.position.y,
    player.transform.position.z - this.transform.position.z
);
movement = Vector3.Normalize(movement);
rb.velocity = movement * speed;
}

void IdleMove()
{
        
if (direction_change_timer >= 5f)
{
    direction_change_timer = 0f;
    int dir = Random.Range(0, 4);

    if (dir == 0)
    {
        rb.velocity = new Vector3(0, 0, 0);
    }else if (dir == 1)
    {
        rb.velocity = new Vector3(1, 0, 1)*speed;
    }else if (dir == 2)
    {
        rb.velocity = new Vector3(-1, 0, 1)*speed;
    }else if (dir == 3)
    {
        rb.velocity = new Vector3(-1, 0, 1)*speed;
    }else 
    {
        rb.velocity = new Vector3(-1, 0, -1)*speed;
    }

}

direction_change_timer+= Time.deltaTime;
}
private void updatePlayerDistance(){
if(Vector3.Distance(player.transform.position, this.transform.position)>chaseDistance)
{
    playerNearby = true;
}
else {
    playerNearby = false;
}
}

}