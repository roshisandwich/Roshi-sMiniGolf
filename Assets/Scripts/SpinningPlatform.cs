using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningPlatform : MonoBehaviour
{
    [SerializeField] private float speed;
    private enum Direction {Clockwise, Counter_Clockwise};
    [SerializeField] private Direction direction;
    private float time;

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (direction == Direction.Clockwise)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, time * -speed);
        }
        else if (direction == Direction.Counter_Clockwise)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, time * speed);

        }
    }
}
