using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RobotFreeAnim : MonoBehaviour
{
	public Rigidbody robotRigidbody;
	public GameObject undestroyedRobot;
	public GameObject destroyedRobot;
	Vector3 rot = Vector3.zero;
	float rotSpeed = 40f;
	Animator anim;

	public UnityEvent OnBreak;

	void Awake()
	{
		anim = gameObject.GetComponent<Animator>();
	}

    private void OnEnable()
    {
		if (!anim.GetBool("Open_Anim"))
		{
			anim.SetBool("Open_Anim", true);
		}
	}

	private void OnCollisionEnter(Collision other)
	{
		if (robotRigidbody.velocity.magnitude > 1.75)
		{
			undestroyedRobot.SetActive(false);
			destroyedRobot.SetActive(true);
			OnBreak?.Invoke();
		}
	}

	//   void Update()
	//{
	//	//CheckKey();
	//	//gameObject.transform.eulerAngles = rot;
	//}

	void CheckKey()
	{
		// Walk
		if (Input.GetKey(KeyCode.W))
		{
			anim.SetBool("Walk_Anim", true);
		}
		else if (Input.GetKeyUp(KeyCode.W))
		{
			anim.SetBool("Walk_Anim", false);
		}

		// Rotate Left
		if (Input.GetKey(KeyCode.A))
		{
			rot[1] -= rotSpeed * Time.fixedDeltaTime;
		}

		// Rotate Right
		if (Input.GetKey(KeyCode.D))
		{
			rot[1] += rotSpeed * Time.fixedDeltaTime;
		}

		// Roll
		if (Input.GetKeyDown(KeyCode.Space))
		{
			if (anim.GetBool("Roll_Anim"))
			{
				anim.SetBool("Roll_Anim", false);
			}
			else
			{
				anim.SetBool("Roll_Anim", true);
			}
		}

		// Close
		if (Input.GetKeyDown(KeyCode.LeftControl))
		{
			if (!anim.GetBool("Open_Anim"))
			{
				anim.SetBool("Open_Anim", true);
			}
			else
			{
				anim.SetBool("Open_Anim", false);
			}
		}
	}

}
