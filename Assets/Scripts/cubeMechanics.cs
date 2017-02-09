using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubeMechanics : MonoBehaviour {

    [SerializeField]
    actionType type;

    [SerializeField]
    float speed;

    public GameObject follower;
    public bool wasTriggered = false;

    GameObject actionPlane;

    GameObject GameManager;
    Rigidbody rigid;

	// Use this for initialization
	void Start () {
        rigid = GetComponent<Rigidbody>();
        GameManager = GameObject.FindGameObjectWithTag("GameController");
        actionPlane = GameObject.FindGameObjectWithTag("actionPlane");
        
	}
	
	// Update is called once per frame
	void Update () {

        if(!wasTriggered && transform.position.y < actionPlane.transform.position.y - 0.9)
        {
            Debug.Log("destroyed because not trigger");
            GameManager.GetComponent<scoreManager>().resetMultiplier();
            GameManager.GetComponent<scoreManager>().alterScore(-5);
            Destroy(gameObject);
        }

	}

    
    void FixedUpdate()
    {
        rigid.MovePosition(transform.position - transform.up * Time.deltaTime * speed);
    }

    public void setType(actionType t)
    {
        type = t;
        switch (t)
        {
            case actionType.THROTTLE: GetComponent<Renderer>().material.color = Color.green;
                break;
            case actionType.BREAKS:
                GetComponent<Renderer>().material.color = Color.red;
                break;
            case actionType.CLUTCH:
                GetComponent<Renderer>().material.color = Color.yellow;
                break;
        }
    }

    public actionType getType()
    {
        return type;
    }

    public enum actionType
    {
        THROTTLE,
        BREAKS,
        CLUTCH
    }
}
