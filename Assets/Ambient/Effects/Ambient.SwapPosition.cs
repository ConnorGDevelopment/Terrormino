using UnityEngine;

namespace Ambient
{
    public class SwapPosition : Effect
    {
        public GameObject PrimaryObject;
        public GameObject SecondaryObject;

        public override void OnEnable()
        {
            base.OnEnable();
            Helpers.Debug.CheckIfSetInInspector(PrimaryObject, "PrimaryObject");
            Helpers.Debug.CheckIfSetInInspector(SecondaryObject, "SecondaryObject");
        }

        public override void OnTriggerEffect()
        {
            var primaryOldPosition = new Vector3(
                PrimaryObject.transform.position.x,
                PrimaryObject.transform.position.y,
                PrimaryObject.transform.position.z
            );


            var secondaryOldPosition = new Vector3(
                SecondaryObject.transform.position.x,
                SecondaryObject.transform.position.y,
                SecondaryObject.transform.position.z
            );



            PrimaryObject.transform.position = secondaryOldPosition;
            SecondaryObject.transform.position = primaryOldPosition;

            Debug.Log("Test");
        }
    }
}