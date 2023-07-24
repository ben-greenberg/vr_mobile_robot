using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingDoors : MonoBehaviour
{
    [SerializeField] private Transform robot;
    [SerializeField] private Transform FrontDoor1;
    [SerializeField] private Transform FrontDoor2;
    [SerializeField] private Transform BackDoor1;
    [SerializeField] private Transform BackDoor2;
    [SerializeField] private Transform FrontDoor1_closed;
    [SerializeField] private Transform FrontDoor1_open;
    [SerializeField] private Transform FrontDoor2_closed;
    [SerializeField] private Transform FrontDoor2_open;
    [SerializeField] private Transform BackDoor1_closed;
    [SerializeField] private Transform BackDoor1_open;
    [SerializeField] private Transform BackDoor2_closed;
    [SerializeField] private Transform BackDoor2_open;

    public float translation_speed = 4.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var sliding_door_threshold = 5.0;
        var translation_step = translation_speed * Time.deltaTime; // calculate distance to move

        print("Distance: " + Vector3.Distance(robot.position, FrontDoor1_closed.position));

        if (Vector3.Distance(robot.position, FrontDoor1_closed.position) < sliding_door_threshold)
        {
            FrontDoor1.position = Vector3.MoveTowards(FrontDoor1.position, FrontDoor1_open.position, translation_step);
            FrontDoor2.position = Vector3.MoveTowards(FrontDoor2.position, FrontDoor2_open.position, translation_step);
        }
        else
        {
            FrontDoor1.position = Vector3.MoveTowards(FrontDoor1.position, FrontDoor1_closed.position, translation_step);
            FrontDoor2.position = Vector3.MoveTowards(FrontDoor2.position, FrontDoor2_closed.position, translation_step);
        }

        if (Vector3.Distance(robot.position, BackDoor1_closed.position) < sliding_door_threshold)
        {
            BackDoor1.position = Vector3.MoveTowards(BackDoor1.position, BackDoor1_open.position, translation_step);
            BackDoor2.position = Vector3.MoveTowards(BackDoor2.position, BackDoor2_open.position, translation_step);
        }
        else
        {
            BackDoor1.position = Vector3.MoveTowards(BackDoor1.position, BackDoor1_closed.position, translation_step);
            BackDoor2.position = Vector3.MoveTowards(BackDoor2.position, BackDoor2_closed.position, translation_step);
        }
    }
}
