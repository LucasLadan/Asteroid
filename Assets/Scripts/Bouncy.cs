using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncy : Meteor
{
    private bool isInside = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "background")
        {
             SetIsActive(true);
        }
        else if (collision.gameObject.CompareTag("Border") && isInside )
        {
            gameObject.transform.Rotate(0, 0, 180);
            GetRigidbody().AddForce(gameObject.transform.up * getSpeed() * 2, ForceMode2D.Impulse);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "background")
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Border"))
        {
            isInside = true;
        }
    }
}
