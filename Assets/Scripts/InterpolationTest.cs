﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class InterpolationTest : MonoBehaviour
{
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    [SerializeField] private Transform pointC;
    [SerializeField] private Transform pointD;
    [SerializeField] private Transform pointE;
    [SerializeField] private Transform pointF;
    [SerializeField] private Transform pointG;
    [SerializeField] private Transform pointH;
    [SerializeField] private Transform pointI;
    [SerializeField] private Transform pointJ;
    [SerializeField] private Transform pointK;
    [SerializeField] private Transform pointL;
    [SerializeField] private Transform pointK1;
    [SerializeField] private Transform pointJ1;
    [SerializeField] private Transform pointI1;
    [SerializeField] private Transform pointH1;
    [SerializeField] private Transform pointG1;
    [SerializeField] private Transform pointF1;
    [SerializeField] private Transform pointE1;
    [SerializeField] private Transform pointD1;
    [SerializeField] private Transform pointC1;
    [SerializeField] private Transform pointB1;
    [SerializeField] private Transform pointA1;
    [SerializeField] private Transform pointEnd;
    [SerializeField] private Transform pointRobot;

    // Create a list to populate with waypoints
    List<Transform> waypoints = new List<Transform>();

    public float translation_speed = 2.0f;
    public float rotation_speed = 3f;
    float interpolate_amount;
    int waypoint_iterator = 0;
    
    // Start is called before the first frame update  
    void Start()
    {
        // Add the waypoints to the list
        waypoints.Add(pointA);
        waypoints.Add(pointB);
        waypoints.Add(pointC);
        waypoints.Add(pointD);
        waypoints.Add(pointE);
        waypoints.Add(pointF);
        waypoints.Add(pointG);
        waypoints.Add(pointH);
        waypoints.Add(pointI);
        waypoints.Add(pointJ);
        waypoints.Add(pointK);
        waypoints.Add(pointL);
        waypoints.Add(pointK1);
        waypoints.Add(pointJ1);
        waypoints.Add(pointI1);
        waypoints.Add(pointH1);
        waypoints.Add(pointG1);
        waypoints.Add(pointF1);
        waypoints.Add(pointE1);
        waypoints.Add(pointD1);
        waypoints.Add(pointC1);
        waypoints.Add(pointB1);
        waypoints.Add(pointA1);
        waypoints.Add(pointEnd);
    }

    // Update is called once per frame  
    void Update()
    {
        //print("waypoint_iterator: " + waypoint_iterator);
        // Check if the final waypoint has been reached
        if (waypoint_iterator == waypoints.Count)
        {
            return;
        }

        //Transform start_point = waypoints[waypoint_iterator];
        Transform end_point = waypoints[waypoint_iterator];

        // Otherwise, interpolate between current position and next waypoint

        /*
        interpolate_amount = (interpolate_amount + Time.deltaTime) % 1f;

        // Loop through the waypoint list up to the second-to-last item. If subsequent element has been set, move from current element to it
        Transform start_point = waypoints[waypoint_iterator];
        Transform end_point = waypoints[waypoint_iterator + 1];
        print("robot x-pos: " + pointRobot.position.x);
        print("start x-pos: " + start_point.position.x);
        print("end x-pos: " + end_point.position.x);
        pointRobot.position = Vector3.Lerp(start_point.position, end_point.position, interpolate_amount);

        // If the robot has completed the interpolated move, increment the waypoint and reset the interpolation_amount
        if ((1 - interpolate_amount) < 0.03)
        {
            waypoint_iterator++;
            interpolate_amount = 0;
        }
        */

        // Move our position a step closer to the target.
        var translation_step = translation_speed * Time.deltaTime; // calculate distance to move
        var rotation_step = rotation_speed * Time.deltaTime; // calculate distance to move

        pointRobot.position = Vector3.MoveTowards(pointRobot.position, end_point.position, translation_step);

        // Determine which direction to rotate towards
        Vector3 targetDirection = end_point.position - pointRobot.position;

        // Rotate the forward vector towards the target direction by one step
        Vector3 newDirection = Vector3.RotateTowards(pointRobot.forward, end_point.forward, rotation_step, 0.0f);

        pointRobot.rotation = Quaternion.LookRotation(newDirection);
        // print("pointRobot rotation: " + pointRobot.rotation.eulerAngles);

        var position_error = Vector3.Distance(pointRobot.position, end_point.position);
        //print("position error: " + position_error);
        // Check if the position of the cube and sphere are approximately equal.
        if ((Vector3.Distance(pointRobot.position, end_point.position) < 0.001f))
        {
            //print("WAYPOINT REACHED");
            waypoint_iterator++;
        }


    }
}
