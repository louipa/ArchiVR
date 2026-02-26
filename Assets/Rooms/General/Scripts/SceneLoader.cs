using System;
using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Teleportation;

namespace Tengio {
    public class SceneLoader : MonoBehaviour {

        [SerializeField]
        private FadeScreen fadeScreen;


        [SerializeField] private TeleportationProvider teleportationProvider;

        public void LoadScene(TeleportationAnchor anchor, Action fadeOutCallback = null, Action middleFadeCallback = null, Action fadeInCallback =  null) {

            fadeScreen.FadeOut(() => { 
                if (fadeOutCallback != null)
                    fadeOutCallback();
                teleportationProvider.QueueTeleportRequest(
                    new TeleportRequest
                    {
                        destinationPosition = anchor.transform.position,
                        destinationRotation = anchor.transform.rotation
                    }
                 );
                if  (middleFadeCallback != null)
                    middleFadeCallback();
                
                fadeScreen.FadeIn(fadeInCallback);
            });
        }
    }
}