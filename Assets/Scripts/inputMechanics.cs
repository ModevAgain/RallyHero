using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inputMechanics : MonoBehaviour {

    
    private bool throttle;
    private bool breaks;
    private bool clutch;

    [SerializeField]
    GameObject actionPlane;
    [SerializeField]
    GameObject particleObject;

    [Space]
    [Header("Raycast origins")]
    [SerializeField]
    GameObject throttleCaster;
    [SerializeField]
    GameObject breakCaster;
    [SerializeField]
    GameObject clutchCaster;

    [SerializeField]
    float inputTime;

    float inputTimer = 1;


    Color standard;

    public bool Throttle
    {
        get
        {
            return throttle;
        }

        set
        {
            throttle = value;
        }
    }

    public bool Breaks
    {
        get
        {
            return breaks;
        }

        set
        {
            breaks = value;
        }
    }

    public bool Clutch
    {
        get
        {
            return clutch;
        }

        set
        {
            clutch = value;
        }
    }

    // Use this for initialization
    void Start () {
        standard = actionPlane.GetComponent<Renderer>().material.color;
    }
	
	// Update is called once per frame
	void Update () {

        //Throttle = Input.GetKey(KeyCode.D);
        //Breaks = Input.GetKey(KeyCode.S);
        //Clutch = Input.GetKey(KeyCode.A);

        if(inputTimer > inputTime)
        {
            inputTimer = 0;

            if (Input.GetKey(KeyCode.D))
            {
               // Debug.Log("grün");
                actionPlane.GetComponent<Renderer>().material.color = Color.green;
                raycastAction(cubeMechanics.actionType.THROTTLE);
            }

            if (Input.GetKey(KeyCode.S))
            {
                //Debug.Log("rot");
                actionPlane.GetComponent<Renderer>().material.color = Color.red;
                raycastAction(cubeMechanics.actionType.BREAKS);
            }

            if (Input.GetKey(KeyCode.A))
            {
               // Debug.Log("gelb");
                actionPlane.GetComponent<Renderer>().material.color = Color.yellow;
                raycastAction(cubeMechanics.actionType.CLUTCH);
            }

        }
        else
        {
            inputTimer += 1 * Time.deltaTime;
        }

        

        if(!clutch && !breaks && !throttle)
        {
            actionPlane.GetComponent<Renderer>().material.color = standard;
        }

    }

    void raycastAction(cubeMechanics.actionType type)
    {
        RaycastHit hit;

        switch (type)
        {
            case cubeMechanics.actionType.THROTTLE:
                
                if(Physics.Raycast(throttleCaster.transform.position, throttleCaster.transform.forward,out hit))
                {

                    if(hit.transform.GetComponent<cubeMechanics>().getType() == type)
                    {
                        GameObject tempParticleObj = Instantiate<GameObject>(particleObject, hit.point, new Quaternion());

                        var ma = tempParticleObj.GetComponentInChildren<ParticleSystem>().main;
                        ma.startColor = Color.green;

                        bool chain = hit.transform.GetComponent<cubeMechanics>().follower != null;

                        StartCoroutine(waitDestroy(tempParticleObj, 0.3f, false));
                        StartCoroutine(waitDestroy(hit.transform.gameObject, 0.1f, chain));
                        GetComponent<scoreManager>().alterScore(20);
                    }
                }
                else
                {
                   // Debug.Log("grüner raycast trifft nicht ");
                    GetComponent<scoreManager>().resetMultiplier();
                    GetComponent<scoreManager>().alterScore(-5);
                }

                break;
            case cubeMechanics.actionType.BREAKS:
                if (Physics.Raycast(breakCaster.transform.position, breakCaster.transform.forward, out hit))
                {
                    if (hit.transform.GetComponent<cubeMechanics>().getType() == type)
                    {
                        GameObject tempParticleObj = Instantiate<GameObject>(particleObject, hit.point, new Quaternion());

                        var ma = tempParticleObj.GetComponentInChildren<ParticleSystem>().main;
                        ma.startColor = Color.red;

                        bool chain = hit.transform.GetComponent<cubeMechanics>().follower != null;

                        StartCoroutine(waitDestroy(tempParticleObj, 0.3f, false));
                        StartCoroutine(waitDestroy(hit.transform.gameObject, 0.1f, chain));
                        GetComponent<scoreManager>().alterScore(20);
                    }
                }
                else
                {
                    //Debug.Log("roter raycast trifft nicht ");
                    GetComponent<scoreManager>().resetMultiplier();
                    GetComponent<scoreManager>().alterScore(-5);
                }
                break;
            case cubeMechanics.actionType.CLUTCH:
                if (Physics.Raycast(clutchCaster.transform.position, clutchCaster.transform.forward, out hit))
                {
                    if (hit.transform.GetComponent<cubeMechanics>().getType() == type)
                    {
                        GameObject tempParticleObj = Instantiate<GameObject>(particleObject, hit.point, new Quaternion());

                        var ma = tempParticleObj.GetComponentInChildren<ParticleSystem>().main;
                        ma.startColor = Color.yellow;

                        bool chain = hit.transform.GetComponent<cubeMechanics>().follower != null;

                        StartCoroutine(waitDestroy(tempParticleObj, 0.3f,false));
                        StartCoroutine(waitDestroy(hit.transform.gameObject, 0.1f, chain));
                        GetComponent<scoreManager>().alterScore(20);
                    }
                }
                else
                {
                    //Debug.Log("gelber raycast trifft nicht ");
                    GetComponent<scoreManager>().resetMultiplier();
                    GetComponent<scoreManager>().alterScore(-5);
                }
                break;
        }


    }

    IEnumerator waitDestroy(GameObject obj, float waitTime, bool chain)
    {
        if (!chain)
        {
            yield return new WaitForSeconds(waitTime);
            Destroy(obj);
        }
        else
        {
            obj.GetComponent<Renderer>().enabled = false;
            obj.GetComponent<cubeMechanics>().wasTriggered = true;
            waitDestroy(obj, 0.7f, false);
        }
        
    }
    }
