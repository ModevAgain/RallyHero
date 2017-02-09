using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carScript : MonoBehaviour {

    [SerializeField]
    LineRenderer lineRen;

    [SerializeField]
    float speed;
    [SerializeField]
    float rotSpeed;

    [SerializeField]
    GameObject[] waypoints;

    Rigidbody rigid;

    public bool drive = false;
    int currentIndex = 1;

    // Use this for initialization
    void Start () {

        rigid = GetComponent<Rigidbody>();

        
       

	}
	
	// Update is called once per frame
	void FixedUpdate () {

        if(drive)
        smoothDrive();

	}


    void smoothDrive()
    {

        try
        {
            int len = lineRen.numPositions;

            if(currentIndex == len)
            {
                drive = false;
                GameObject.FindGameObjectWithTag("GameController").GetComponent<scoreManager>().runEnded();
                return;
            }

            rigid.MovePosition(transform.position + (lineRen.GetPosition(currentIndex) - transform.position) * Time.deltaTime * speed);

            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(transform.position - lineRen.GetPosition(currentIndex)), Time.fixedDeltaTime * rotSpeed);


           

            if (Vector3.Distance(transform.position, lineRen.GetPosition(currentIndex)) < 3.3f)
            {
                currentIndex++;
            }
        }
        catch(UnityException e)
        {
            Debug.Log(e.Message);
        }

    }

    public void alterSpeed(int x)
    {
        speed = (x + 3) * 1.5f;
    }
}
