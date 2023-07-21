using BepInEx;
using UnityEngine;
using UnityEngine.XR;
using Utilla;


namespace AdvancedAirJump
{
    [ModdedGamemode]
    [BepInDependency("org.legoandmars.gorillatag.utilla", "1.5.0")]
    [BepInPlugin("com.kongo.gorillatag.advancedairjump", "AdvancedAirjump", "1.0.0")]
    public class Plugin : BaseUnityPlugin
    {
        public GameObject sphere;
        public GameObject sphere2;
        public Vector3 size;
        bool inRoom;
        bool leftplat;
        bool rightplat;
        bool enabled1;

        void OnEnable()
        {
            if (!inRoom)
                return;
            {
                enabled1 = true;
            }
        }

        void OnDisable()
        {
            enabled1 = false;
        }
        void Start()
        {
            /* A lot of Gorilla Tag systems will not be set up when start is called /*
			/* Put code in OnGameInitialized to avoid null references */

            Utilla.Events.GameInitialized += OnGameInitialized;
        }


        void OnGameInitialized(object sender, EventArgs e)
        {
            sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere2 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            Update();
        }

        void Update()
        {
           
                    {
                leftplat = false;
                rightplat = false;
                InputDevices.GetDeviceAtXRNode(XRNode.LeftHand).TryGetFeatureValue(CommonUsages.gripButton, out leftplat);
                InputDevices.GetDeviceAtXRNode(XRNode.RightHand).TryGetFeatureValue(CommonUsages.gripButton, out rightplat);
                sphere.GetComponent<SphereCollider>().enabled = false;
                sphere.transform.SetParent(GorillaLocomotion.Player.Instance.leftHandFollower.transform, false);
                sphere2.GetComponent<SphereCollider>().enabled = false;
                sphere2.transform.SetParent(GorillaLocomotion.Player.Instance.rightHandFollower, false);
                sphere.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                sphere2.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            }
            if (inRoom == true)
             {
                if (enabled1 == true)
                {
                    
                    if (leftplat == true)
                    {
                        sphere.GetComponent<SphereCollider>().enabled = true;
                        sphere.GetComponent<MeshRenderer>().material.color = Color.black;
                        
                    }
                    if (leftplat == false)
                    {
                        sphere.GetComponent<SphereCollider>().enabled = false;
                        sphere.GetComponent<MeshRenderer>().material.color = Color.white;
                    }
                    
                    if (rightplat == true)
                    {
                        sphere2.GetComponent<SphereCollider>().enabled = true;
                        sphere2.GetComponent<MeshRenderer>().material.color = Color.black;
                        sphere2.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                    }
                    if (rightplat == false)
                    {
                        sphere2.GetComponent<SphereCollider>().enabled = false;
                        sphere2.GetComponent<MeshRenderer>().material.color = Color.white;
                        sphere2.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                    }
                }
             }
        }
        [ModdedGamemodeJoin]
        public void OnJoin(string gamemode)

        {
            inRoom = true;
        }

        [ModdedGamemodeLeave]
        public void OnLeave(string gamemode)
        {
            inRoom = false;
        }


    }
}