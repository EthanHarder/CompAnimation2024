using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class navControl : MonoBehaviour
{

    public GameObject Target;
    public Transform Target_Look;
    private NavMeshAgent agent;
    bool isWalking = true;
    private Animator animator;

    public float speedVal;
    float m_MySliderValue = 0;

    public float heightBar = 0;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isWalking)
        {
            if (Target != null)
                agent.destination = Target.transform.position;
        }
        else
        {
            agent.destination = transform.position;
            rotateTowardTarget();
        }
        
    }

    void rotateTowardTarget()
    {

        Quaternion start = transform.rotation;
        transform.LookAt(Target_Look.position);
        Quaternion end = transform.rotation;

        transform.rotation = Quaternion.Lerp(start, end, 0.05f);
    }

    private void OnTriggerEnter(Collider coll)
    {
        if(coll.name == "Dragon")
        {
            isWalking = false;
            animator.SetTrigger("ATTACK");
        }
    }

    //I have no reason to keep this, it only adds potential problems in our example
    /*
    private void OnTriggerExit(Collider coll)
    {
        if (coll.name == "Dragon")
        {
            isWalking = true;
            animator.SetTrigger("WALK");
        }
    } */

    void OnGUI()
    {
        //Create a Label in Game view for the Slider
        GUI.Label(new Rect(0, 25, 40, 60), "Speed");
        //Create a horizontal Slider to control the speed of the Animator. Drag the slider to 1 for normal speed.
       
        m_MySliderValue = GUI.HorizontalSlider(new Rect(45, 25 * heightBar, 200, 60), m_MySliderValue, 0.0F, 1.0F);
        //Make the speed of the Animator match the Slider value
        animator.speed = m_MySliderValue;
        agent.speed = m_MySliderValue * speedVal;
    }
}
