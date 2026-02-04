using System;
using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Teleportation;

namespace Tengio {
    public class SceneLoader : MonoBehaviour {

        [SerializeField]
        private FadeScreen fadeScreen;


        [SerializeField] private TeleportationProvider teleportationProvider;

        public void LoadScene(TeleportationAnchor anchor) {

            fadeScreen.FadeOut(() => { 
                teleportationProvider.QueueTeleportRequest(
                    new TeleportRequest
                    {
                        destinationPosition = anchor.transform.position,
                        destinationRotation = anchor.transform.rotation
                    }
                 );
                 fadeScreen.FadeIn();
            });
        }
    }
}