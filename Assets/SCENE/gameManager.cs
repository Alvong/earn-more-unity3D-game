using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
[RequireComponent (typeof (AudioSource))] 
public class gameManager : MonoBehaviour {
	public AudioClip death;
	private AudioSource source;
	int turn=0;
	int dead=0;
	int money=5000;
	int ro_workers=0;
	int workers=0;
	int goal=5;
	int produced=0;
	int cost=0;
	float timeLeft = 30.0f;
	Rigidbody[] worker_array=new Rigidbody[30];
	//GameObject[]  worker_array=new GameObject[][30];
	//List<Rigidbody> worker_array1 = new List<rigidbody> ();
	//Rigidbody[] dead=new Rigidbody[30];
	public Text turnt; 
	public Text costt;
	public Text timer;
	public Rigidbody people;
	public Text moneyt;
	public Text ro_workerst;
	public Text workerst;
	public Text goalt;
	public Text producedt;

	// Use this for initialization

	void Start()
	{
		source = GetComponent<AudioSource> ();
		InvokeRepeating ("calculation",2,2);
	}
	void display()
	{
		moneyt.text="$: "+money;
		ro_workerst.text="Robot worker: "+ro_workers;
		workerst.text="Workers: "+workers+"/30";
		goalt.text="Goal: "+goal;
		producedt.text="Produced: "+produced;
		costt.text = "Salary: " + cost;
		turnt.text = "Turn: " + turn + " /50";
	}
	public void hiring()
	{
		if (workers < 30) {
			Rigidbody one = (Rigidbody)Instantiate (people, new Vector3 (-272, 3, 20), Quaternion.identity);
			//Rigidbody one = (Rigidbody)Instantiate (people, new Vector3 (-272, 3, 20), Quaternion.identity);
			one.AddForce (transform.forward*150);
			//worker_array1.Add (one);
			worker_array [workers] = one;
			workers++;

		}

	}
	void checkdistance(){
		for(int i=0;i<workers;i++){
			//worker_array [i].AddForce (transform.forward*80);
			if(worker_array[i]!=null){
			if(worker_array[i].transform.position.z>25)
			{
				worker_array [i].GetComponent<MeshRenderer>().enabled = false;
				worker_array [i].transform.position = new Vector3(-270, 3, 46);
				worker_array [i].isKinematic = true;
			}
		}
		}
	}
	public void workhard()
	{
		if(workers>5){
		int i = Random.Range (0, 9);
		if(i==8)
		{
			--workers;
			ro_workers++;
		}
		produced += workers*2;
		suicide ();
		}
	}
	void calculation()
	{
		int num1 = workers * 2;
		int num2 = ro_workers * 50;
		int num3 = num1 + num2;
		produced += num3;
		money = money - 70 * workers;
		cost = workers * 70;
		//produced;


	}
	public void selling(){
		if(goal<=produced)
		{
			money += (produced - goal) * 100;
			goal *= 2;
			produced = 0;
		}
	}
	public void buyrobot(){
		if(money>9999)
		{
			money -= 9999;
			ro_workers++;
		}
	}
	void checkmoney(){
		if(money<0)
		{
			SceneManager.LoadScene (1);
		}

//		if (produced > goal) {
//			money = (produced - goal) * 1000;
//			goal = goal * 2;
//			produced = 0;
//		}
	}
	 void suicide(){
		if(workers>5){
		int i = Random.Range (0, 4);
		while(i>0){
		Rigidbody one = (Rigidbody)Instantiate (people, new Vector3 (-270, 10,28), Quaternion.identity);
		workers--;
		one.transform.position=new Vector3(-Random.Range(250,300 ), 10,Random.Range(25,30 ) );
		one.isKinematic = false;
		one.useGravity = true;
			--i;
				dead = 1;
		}
		
	}
	}
	public void again()
	{
		SceneManager.LoadScene(0);
	}
	public void timeleft(){
		timeLeft -= Time.deltaTime;
		timer.text = "Time Left:" + Mathf.Round(timeLeft);
		if(timeLeft < 1)
		{
			if (produced >= goal&& money>0) {
				goal = goal * 2;
				money += 5000;
				timeLeft = 20.0f;
				produced = 0;
				++turn;

			} else {
				SceneManager.LoadScene (1);
			}
		}
		
	}
	// Update is called once per frame
	void Update () {
		timeleft ();
		display ();
		checkdistance ();
		checkmoney ();
		if (dead == 1) {
			source.PlayOneShot (death);
			dead = 0;
		}
		if(turn>50){SceneManager.LoadScene (2);}
		//checkdeath ();
		//suicide ();
	}
}
