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
            var primaryOldPosition = PrimaryObject.transform.position;
            var secondaryOldPosition = SecondaryObject.transform.position;

            PrimaryObject.transform.position = secondaryOldPosition;
            SecondaryObject.transform.position = primaryOldPosition;
        }
    }
}