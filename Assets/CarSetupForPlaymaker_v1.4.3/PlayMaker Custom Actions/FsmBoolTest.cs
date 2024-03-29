// (c) Copyright HutongGames, LLC 2010-2012. All rights reserved.
// Modified by Jean Fabre : contact@fabrejean.net 
// this is a combination of BoolTest and GetFsmBool since I don't want the extras steps required otherwise.
// I also don't feel like saving a variable for every single check from other fsm, I want to avoid redundance sometimes.
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Logic)]
	[Tooltip("Sends Events based on the value of a Bool Variable from another FSM.")]
	public class FsmBoolTest : FsmStateAction
	{
		[RequiredField]
		public FsmOwnerDefault gameObject;
		[UIHint(UIHint.FsmName)]
		[Tooltip("Optional name of FSM on Game Object")]
		public FsmString fsmName;
		[RequiredField]
		[UIHint(UIHint.FsmBool)]
		public FsmString variableName;

		public FsmEvent isTrue;
		public FsmEvent isFalse;
		
		public bool everyFrame;
		
		private bool storedValue;
		
		GameObject goLastFrame;
		PlayMakerFSM fsm;
		
		
		public override void Reset()
		{
			gameObject = null;
			fsmName = "";
			
			
			storedValue = false;
			isTrue = null;
			isFalse = null;
		}

		public override void OnEnter()
		{
			DoGetFsmBool();

			Fsm.Event(storedValue ? isTrue : isFalse);

			if (!everyFrame)
				Finish();
		}

		public override void OnUpdate()
		{
			DoGetFsmBool();
			
			Fsm.Event(storedValue ? isTrue : isFalse);
		}

		void DoGetFsmBool()
		{
			GameObject go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go == null) return;
			
			// only get the fsm component if go has changed

			if (go != goLastFrame)
			{
				goLastFrame = go;
				fsm = ActionHelpers.GetGameObjectFsm(go, fsmName.Value);
			}			
			
			if (fsm == null) return;
			
			FsmBool fsmBool = fsm.FsmVariables.GetFsmBool(variableName.Value);
			
			if (fsmBool == null) return;
			
			storedValue = fsmBool.Value;
		}
		
		public override string ErrorCheck()
		{
			if (isTrue ==null && isFalse==null)
			{
				return "You need to at least have isTrue or IsFalse event defined.";
			}
			return "";
		}

	}
}