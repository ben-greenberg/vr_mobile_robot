using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BezierInterpolation : MonoBehaviour
{
    [SerializeField] private Transform point;
    [SerializeField] private Transform pointEnd;
    [SerializeField] private Transform pointRobot;
    [SerializeField] private float translation_speed;
    [SerializeField] private float rotation_speed;
    [SerializeField] private float signaling_distance;
    [SerializeField] private float curvature;

    // Create a list to populate with waypoints
    List<Transform> waypoints = new List<Transform>();

    float interpolate_amount;
    int waypoint_iterator = 0;
    
    // Start is called before the first frame update  
    void Start()
    {
        // Bezier points
        float vertex1_x = -signaling_distance;
        float vertex2_x = 0.0f;
        float control1_x = vertex1_x - curvature * vertex1_x;
        float control2_x = vertex2_x + curvature * vertex1_x;
        float vertex1_y = 0.0f;
        float vertex2_y = 1.27f;
        float control1_y = 0.0f;
        float control2_y = 1.27f;

        Vector3 previous_position = new Vector3(0f, 0f, 0f);
        int num_bezier_points = 0;
        // Calculate bezier waypoints and add to list

        int iter = 0;
        float bezier_steps = 100;
        for (float t=0; t<=1; t = t + 1/bezier_steps)
        {
            GameObject new_point = new GameObject();
            new_point.name = iter.ToString();

            Vector3 position = new Vector3(0f, 0f, 0f);
            float angle;
            Quaternion rotation = new Quaternion(0f, 0f, 0f, 0f);

            position.x = (float)Math.Pow((1 - t), 3) * vertex1_x + 3 * t * control1_x * (float)Math.Pow((1 - t), 2) + 3 * (float)Math.Pow(t, 2) * (1 - t) * control2_x + (float)Math.Pow(t, 3) * vertex2_x;
            position.y = 0.5f;
            position.z = -((float)Math.Pow((1 - t), 3) * vertex1_y + 3 * t * control1_y * (float)Math.Pow((1 - t), 2) + 3 * (float)Math.Pow(t, 2) * (1 - t) * control2_y + (float)Math.Pow(t, 3) * vertex2_y);

            if (t == 0)
                angle = 0.0f;
            else
            {
                angle = -(float)Math.Atan((position.z - previous_position.z) / (position.x - previous_position.x));
            }
            rotation.SetEulerAngles(0.0f, angle , 0.0f); //TODO: Fix the origin of the robot model
            //print(rotation);
            new_point.transform.SetPositionAndRotation(position, rotation);
            waypoints.Add(new_point.transform);
            previous_position = position;
            num_bezier_points++;
            iter++;
        }

        if (waypoints[waypoints.Count - 1].position.x != 0.0)
        {
            // Add waypoint for passing point
            GameObject new_point = new GameObject();
            new_point.name = iter.ToString();

            Vector3 position = new Vector3(0f, 0.5f, -1.27f);
            float angle = 0.0f;
            Quaternion rotation = new Quaternion(0f, 0f, 0f, 0f);

            rotation.SetEulerAngles(0.0f, angle, 0.0f); //TODO: Fix the origin of the robot model
            new_point.transform.SetPositionAndRotation(position, rotation);
            waypoints.Add(new_point.transform);
        }

        // Add waypoints for the reverse of mirror image of the curve on the path to the exit
        //for (int i = 1; i <= num_bezier_points; i++)
        //{
        //    GameObject new_point = new GameObject();
        //    new_point.name = i.ToString();

        //    Vector3 position = new Vector3(0f, 0f, 0f);
        //    Quaternion rotation = new Quaternion(0f, 0f, 0f, 0f);
        //    position.Set(-waypoints[num_bezier_points - i].position.x, waypoints[num_bezier_points - i].position.y, waypoints[num_bezier_points - i].position.z);
        //    rotation.Set(waypoints[num_bezier_points - i].rotation.x, -waypoints[num_bezier_points - i].rotation.y, waypoints[num_bezier_points - i].rotation.z, waypoints[num_bezier_points - i].rotation.w);
        //    print(rotation);
        //    new_point.transform.SetPositionAndRotation(position, rotation);
        //    waypoints.Add(new_point.transform);
        //}
        //// Add the last waypoint to the list
        //waypoints.Add(pointEnd);
        //for (int i = 0; i < waypoints.Count; i++)
        //{
        //    print(waypoints[i].rotation);
        //}
    }

    // Update is called once per frame  
    void Update()
    {
        //print("waypoint_iterator: " + waypoint_iterator);
        // Check if the final waypoint has been reached
        if (waypoint_iterator == waypoints.Count)
        {
            string sceneChange = "AffectiveSliderScene";
            SceneManager.LoadScene(sceneChange);
            return;
        }

        //Transform start_point = waypoints[waypoint_iterator];
        Transform end_point = waypoints[waypoint_iterator];

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
