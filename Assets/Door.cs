using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Animator rightDoorAnimator;
    public Animator leftDoorAnimator;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        rightDoorAnimator.SetBool("isOpen", true);
        leftDoorAnimator.SetBool("isOpen", true);
    }

    private void OnTriggerExit(Collider other)
    {
        rightDoorAnimator.SetBool("isOpen", false);
        leftDoorAnimator.SetBool("isOpen", false);


    }

}