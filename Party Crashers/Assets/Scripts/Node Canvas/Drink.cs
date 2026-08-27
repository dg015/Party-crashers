using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace NodeCanvas.Tasks.Actions {

	public class Drink : ActionTask {

        public BBParameter<float> drinkTime;
        public BBParameter<float> drunkPecentege;
        public float drinkSpeed;


        //Use for initialization. This is called only once in the lifetime of the task.
        //Return null if init was successfull. Return an error string otherwise
        protected override string OnInit() {
			return null;
		}

        private void StartDrinking()
        {
            float originalDrunkValue = drunkPecentege.value;
            while (drunkPecentege.value <= originalDrunkValue + 15)
            {
                //drink overtime
                drunkPecentege.value += Time.deltaTime * drinkSpeed;
            }
            EndAction(true);
        }



        //This is called once each time the task is enabled.
        //Call EndAction() to mark the action as finished, either in success or failure.
        //EndAction can be called from anywhere.
        protected override void OnExecute() {
            StartDrinking();

        }

		//Called once per frame while the action is active.
		protected override void OnUpdate() {
			
		}

		//Called when the task is disabled.
		protected override void OnStop() {
			
		}

		//Called when the task is paused.
		protected override void OnPause() {
			
		}
	}
}