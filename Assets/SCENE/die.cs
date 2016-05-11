using UnityEngine;
using System.Collections;

public class die : MonoBehaviour {
	

	public AudioClip death;
	private AudioSource source;
	// Update is called once per frame
	void Start () {

		source = GetComponent<AudioSource>();

	}
	void Update () 
	{
		if(this.transform.position.y<1)
		{
			source.PlayOneShot(death);
		}

	}

}
