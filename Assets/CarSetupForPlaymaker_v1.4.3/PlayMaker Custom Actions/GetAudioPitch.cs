// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Audio)]
	[Tooltip("Gets the Pitch of the Audio Clip played by the AudioSource component on a Game Object.")]
	public class GetAudioPitch : ComponentAction<AudioSource>
	{
		[RequiredField]
		[CheckForComponent(typeof(AudioSource))]
		public FsmOwnerDefault gameObject;

		[UIHint(UIHint.Variable)]
		public FsmFloat pitch;
		public bool everyFrame;
		
		public override void Reset()
		{
			gameObject = null;
			pitch = 0;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoGetAudioPitch();

		    if (!everyFrame)
		    {
		        Finish();
		    }
		}
				
		public override void OnUpdate ()
		{
			DoGetAudioPitch();
		}	
		
		void DoGetAudioPitch()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (UpdateCache(go))
			{
			    if (!pitch.IsNone)
			    {
					pitch.Value = audio.pitch;

			    }
			}
		}
	}
}