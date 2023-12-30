// (c) Copyright HutongGames, LLC 2010-2011. All rights reserved.

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.GameObject)]
	[Tooltip("Gets the Parent of a Game Object.")]
	public class GetParentMore : FsmStateAction
	{
		[RequiredField]
		public FsmOwnerDefault gameObject;
		[UIHint(UIHint.Variable)]
		public FsmGameObject storeResult;
		public int repetitions;
		
		public override void Reset()
		{
			gameObject = null;
			storeResult = null;
			repetitions = 0;
		}
		
		public override void OnEnter()
		{
			
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go != null)
			{
				storeResult.Value = go.transform.parent == null ? null : go.transform.parent.gameObject;
				while (repetitions > 0 ) 
				{
					var go2 = storeResult.Value;
					repetitions = repetitions - 1;	
					storeResult.Value = go2.transform.parent == null ? null : go2.transform.parent.gameObject;
				}
				
			}	
			
			else
			{
				storeResult.Value = null;
			}
			
			
			
			Finish();
		}
	}
}