using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using EZCameraShake;
public class BallController : MonoBehaviour
{
    private Rigidbody rb;
    public float initialSpeed = 7f;
    public float minSpeed = 7f;
    public float maxSpeed = 15f;
    private GameManager gm;
    public GameObject lastPlayerTouch;
    public string powerUpEffect = null;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        LaunchBall();
    }

    private void checkSpeed()
    {
        var speed = rb.velocity;
        if (speed.magnitude > maxSpeed)
        {
            rb.velocity = speed.normalized * maxSpeed;
        }
        else if (speed.magnitude < minSpeed)
        {
            rb.velocity = speed.normalized * minSpeed;
        }
    }

    private void LaunchBall()
    {

        //FIXME CHANCE TO SPAWN INFINITE LOOP HITTING SIDES

        List<Vector3> randomPosList = new List<Vector3>();
        foreach(var player in gm.playerList)
        {
            if (player.isAlive) {
                randomPosList.Add(player.spawnPoint);
            }
        }
        var spawnPoint = randomPosList[Random.Range(0, randomPosList.Count)];
        //spawnPoint = AlterVectorRandom(spawnPoint);

        rb.velocity = new Vector2(spawnPoint.x, spawnPoint.y).normalized * initialSpeed;  
    }

    private Vector3 AlterVectorRandom(Vector3 spawnPoint)
    {
        Vector3 newSpawnPoint = new Vector3();
        var x = spawnPoint.x;
        var y = spawnPoint.y;
        if (y == 0)
        {
            newSpawnPoint = new Vector3(x, Random.Range(-(x-4), x-4), 0);
        }
        else if (x == 0)
        {
            newSpawnPoint = new Vector3(Random.Range(-(y - 4), y - 4), y, 0);
        }
        return newSpawnPoint;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Barrier")
        {
            CameraShaker.Instance.ShakeOnce(0.8f, 1, 0.1f, 0.6f);
        }

        if (collision.transform.tag == "Border")
        {
            //check if powerup enabled
            if (powerUpEffect == "SpikeBall")
            {
                Destroy(gameObject);
                powerUpEffect = null;
                gm.SpawnBall();
                return;
            }

            //else we elim the player

            string name = collision.transform.name;
            int playerId = int.Parse(name[name.Length - 1].ToString()) - 1;
            if (!gm.GetPlayerFromId(playerId).isAlive) { return; } //if player dead we dont wanna colide

            Destroy(this.gameObject); // destroy ball
            gm.BorderCollision(playerId);
        }
        
        if (collision.transform.tag == "Player")
        {
            //fx
            var speed = collision.gameObject.GetComponent<Rigidbody>().velocity.magnitude;
            CameraShaker.Instance.ShakeOnce(0.8f, speed, 0.1f, 0.6f);

            //check if powerup enabled
            if (powerUpEffect == "SpikeBall")
            {
                var player = gm.GetPlayerFromPrefab(collision.gameObject);
                gm.KillPlayer(player.playerIndex);
                ResetVariables();
                return;
            }

            lastPlayerTouch = collision.gameObject; //update lastPlayerTouch for powerUps
            checkSpeed(); //regulate the speed make sure its in bounds
        }
    }
    private void ResetVariables()
    {
        powerUpEffect = null;
        lastPlayerTouch = null;
    }
}
