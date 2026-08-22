using DG.Tweening;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using System.Numerics;
using UnityEngine;

namespace NodeCanvas.Tasks.Actions {

	public class Spawn : ActionTask {

		public BBParameter<float> animDuration;

		//Use for initialization. This is called only once in the lifetime of the task.
		//Return null if init was successfull. Return an error string otherwise
		protected override string OnInit() {

			//Set the scale to 0
			//Oh God its so annoying having to call unity engine
			agent.transform.localScale = UnityEngine.Vector3.zero;

			agent.transform.DOScale(UnityEngine.Vector3.one, animDuration.value);

			return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {
			EndAction(true);
		}

		//Called when the task is disabled.
		protected override void OnStop() {
			Debug.Log("Spawn complete");

		}
	}
}