/*==============================================================================
Copyright (c) 2010-2014 Qualcomm Connected Experiences, Inc.
All Rights Reserved.
Confidential and Proprietary - Protected under copyright and other laws.
==============================================================================*/

using UnityEngine;
using System.Collections.Generic;


namespace Vuforia
{
    /// <summary>
    /// A custom handler that implements the ITrackableEventHandler interface.
    /// </summary>
    public class DefaultTrackableEventHandler : MonoBehaviour,
                                                ITrackableEventHandler
    {

		#region BySpring
		//springDefine

		//定义一个开关
		public bool isDown=false;

		//canves
		public GameObject canvesObj;

		private GameController gameController;

		List<string> nameList=new List<string>();

		//springDefine
		#endregion

        #region PRIVATE_MEMBER_VARIABLES
 
        private TrackableBehaviour mTrackableBehaviour;
    
        #endregion // PRIVATE_MEMBER_VARIABLES



        #region UNTIY_MONOBEHAVIOUR_METHODS

		void Awake()
		{
			#region BySpring

			///
			////这里有一个坑，往后可以研究，如果放在start函数里面调用  在TrackerLost调用的时候会报一个空引用异常
			//

			//springDefine
			gameController=GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
			//springDefine

			#endregion
		}
    
        void Start()
        {
            mTrackableBehaviour = GetComponent<TrackableBehaviour>();
            if (mTrackableBehaviour)
            {
                mTrackableBehaviour.RegisterTrackableEventHandler(this);
            }

//			#region BySpring
//
//			//springDefine
//			gameController=GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
//			//springDefine
//
//			#endregion
        }

        #endregion // UNTIY_MONOBEHAVIOUR_METHODS



        #region PUBLIC_METHODS

        /// <summary>
        /// Implementation of the ITrackableEventHandler function called when the
        /// tracking state changes.
        /// </summary>
        public void OnTrackableStateChanged(
                                        TrackableBehaviour.Status previousStatus,
                                        TrackableBehaviour.Status newStatus)
        {
            if (newStatus == TrackableBehaviour.Status.DETECTED ||
                newStatus == TrackableBehaviour.Status.TRACKED ||
                newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
            {
                OnTrackingFound();

				#region BySpring
 				//UserDefine

				//
				canvesObj.SetActive(true);

				//控制是否下载
				if(isDown)
				{
					if (!nameList.Contains (mTrackableBehaviour.TrackableName))
					{
						nameList.Add (mTrackableBehaviour.TrackableName);
						StartCoroutine (gameController.LoadModelOnServer (mTrackableBehaviour.TrackableName));
					}
				}

				gameController.scan.SetActive(false);
				//将name传到控制类中
				gameController.imageTargetTag=mTrackableBehaviour.TrackableName;

				if(GetComponentInChildren<CollierController>()!=null)
				{
					GetComponentInChildren<CollierController>().enabled=true;
				}

				//加载要显示的文本
				gameController.LoadLocalText(mTrackableBehaviour.TrackableName);


				//UserDefine
				#endregion
            }
            else
            {
                OnTrackingLost();
				canvesObj.SetActive (false);

				gameController.scan.SetActive (true);
				//丢失将ImageTargetTag设为空
				gameController.imageTargetTag = "";


            }
        }

        #endregion // PUBLIC_METHODS



        #region PRIVATE_METHODS


        private void OnTrackingFound()
        {
            Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
            Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);

            // Enable rendering:
            foreach (Renderer component in rendererComponents)
            {
                component.enabled = true;
            }

            // Enable colliders:
            foreach (Collider component in colliderComponents)
            {
                component.enabled = true;
            }
            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " found");
        }


        private void OnTrackingLost()
        {
            Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
            Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);

            // Disable rendering:
            foreach (Renderer component in rendererComponents)
            {
                component.enabled = false;
            }

            // Disable colliders:
            foreach (Collider component in colliderComponents)
            {
                component.enabled = false;
            }

            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");
        }

        #endregion // PRIVATE_METHODS
    }
}
