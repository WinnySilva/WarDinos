using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneController : MonoBehaviour {
    Queue<GameObject> dispatchQueue = new Queue<GameObject>();
    public string myPlayerTag;
    private bool someoneIsWalking = false;
    
    void FixedUpdate () {
        if (!someoneIsWalking && dispatchQueue.Count != 0) {
            GameObject go = dispatchQueue.Dequeue();
            if (go != null)
            {
                someoneIsWalking = true;
                GroupController gc = go.GetComponent<GroupController>();
                gc.WaitingForDispatch = false;
                gc.StartWalking();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(myPlayerTag))
            if (!dispatchQueue.Contains(other.gameObject))
                dispatchQueue.Enqueue(other.gameObject);
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag(myPlayerTag)) {
            someoneIsWalking = false;
        }
    }
}
