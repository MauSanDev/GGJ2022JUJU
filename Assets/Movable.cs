using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Movable : MonoBehaviour
{
    private Rigidbody2D myRB;

    private void Start()
    {
        myRB = this.GetComponent<Rigidbody2D>();
    }

    public void Push(Vector3 otherPos, Vector3 pushDirection)
    {
        Vector3 thisToOther = otherPos - this.transform.position;
        if(Vector2.Angle(thisToOther,pushDirection) > 180-45) //Player is pushing in my direction
        {
            Vector2 direction = new Vector2(pushDirection.x, pushDirection.y);
            myRB.MovePosition(myRB.position + direction);
        }
    }

}
