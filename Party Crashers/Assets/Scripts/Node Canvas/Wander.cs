using NodeCanvas.Framework;
using ParadoxNotion.Design;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


namespace NodeCanvas.Tasks.Actions {

	public class Wander : ActionTask {

		[ParadoxNotion.Design.Header("Movement")]
		//movement
        public BBParameter<List<GuestZone>> zoneList;
		public BBParameter<NavMeshAgent> navAgent;

		public BBParameter<float> m_minWaitTime;
        public BBParameter<float> m_maxWaitTime;

        private GuestZone m_currentZone;

		//just some default value to not start as 0
		private float m_closestZoneDistance = Mathf.Infinity;

		private Vector3 m_travelPoint;
		private Coroutine m_currentCoroutine;

        [ParadoxNotion.Design.Header("Drinking")]
        //Drinking
        private bool m_isDrinking = false;
		public bool IsDrinking { get {  return m_isDrinking; } set { m_isDrinking = value; } }
		public BBParameter<float> amountDrunkPerBreak;
		public BBParameter<float> drunkPecentege;
        public BBParameter<float> drinkSpeed;

        //Use for initialization. This is called only once in the lifetime of the task.
        //Return null if init was successfull. Return an error string otherwise
        protected override string OnInit() {
			//get list of all location
			zoneList = TaskManager.Instance.GuestZoneList;

			Debug.Log("Number of lists found: " + zoneList.value.Count);

			GetCurrentZone();

            return null;
		}

		private void GetCurrentZone()
		{
			for (int i = 0; i < zoneList.value.Count; i++)
			{
				//get the transform
				Transform thisZone = zoneList.value[i].GetComponent<Transform>();

				float distance = Vector3.Distance(agent.transform.position, thisZone.position);

				//if the distance is smaller then set the new zone and new distance to eat
				if(distance < m_closestZoneDistance)
				{
                    m_closestZoneDistance = distance;
					m_currentZone = zoneList.value[i];
                }
			}
		}


		private void PickNewZone(float chance)
		{
			float number = 1 / chance;
			float randomNumber = Random.Range(0, 1);

			if (randomNumber > number)
				return;

			int randomZoneNum = Random.Range(0, zoneList.value.Count);
			m_currentZone = zoneList.value[randomZoneNum];

		}

		private void GoToArea()
		{
            //pick new point
            m_travelPoint = m_currentZone.chooseRandomPoint(m_currentZone.ZoneCollider.bounds);
			
			//travel to point
			navAgent.value.SetDestination(m_travelPoint);
		}

		private void Drink()
		{
			if(m_isDrinking)
			{
				float originalDrunkValue = drunkPecentege.value;
				while(drunkPecentege.value <= originalDrunkValue + 15)
				{
					//drink overtime
					drunkPecentege.value += Time.deltaTime * drinkSpeed.value;

				}
				m_isDrinking = false;
            }
		}


		private IEnumerator WanderCoroutine()
		{
			while (true)
			{
				GoToArea();

				//wait until the guest has reached the point
				while (Vector3.Distance(agent.transform.position, m_travelPoint) > .15f)
				{
					yield return null;
				}

				//then trigger the wait time to showcase as if the guest was doing something
				yield return new WaitForSeconds(PickIdleTime());
				PickNewZone(4);
			}
		}


		private float PickIdleTime()
		{
			float idleTime = Random.Range(m_minWaitTime.value, m_maxWaitTime.value);
			return idleTime;
        }


		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {
            //EndAction(true);
            m_currentCoroutine = StartCoroutine(WanderCoroutine());

		}

		//Called once per frame while the action is active.
		protected override void OnUpdate() {
			Drink();

        }

		//Called when the task is disabled.
		protected override void OnStop() {

			if (m_currentCoroutine != null)
			{
				StopCoroutine(m_currentCoroutine);
				m_currentZone = null;
			}

		}

		//Called when the task is paused.
		protected override void OnPause() {
			
		}
	}
}