using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupMovement : MonoBehaviour
{
    public float amplitude = 0.2f;
    public float frequency;


    /*
     * For the oscilating movement, I used this tutorial: http://www.donovankeith.com/2016/05/making-objects-float-up-down-in-unity/
     */
    Vector3 posOffset = new Vector3();
    Vector3 tempPos = new Vector3();
    // Start is called before the first frame update
    void Start()
    {
        posOffset = transform.position;
        posOffset.y += 1;
        frequency = (Random.value + 1);

    }

    // Update is called once per frame
    void Update()
    {
        // Rotate the game object that this script is attached to by 15 in the X axis,
        // 30 in the Y axis and 45 in the Z axis, multiplied by deltaTime in order to make it per second
        // rather than per frame.
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
        tempPos = posOffset;
        tempPos.y += Mathf.Sin(Time.fixedTime * frequency) * amplitude;

        transform.position = tempPos;

    }
}
