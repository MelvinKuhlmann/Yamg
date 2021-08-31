using UnityEngine;

namespace YAMG
{
    //duplicate the given transform position, rotation and scale when this object get enable
    public class LocalTransformDuplicator : MonoBehaviour
    {
        public Transform targetTrasnform;

        private void OnEnable()
        {
            transform.localScale = targetTrasnform.localScale;
            transform.localRotation = targetTrasnform.localRotation;
            transform.localPosition = targetTrasnform.localPosition;
        }
    }
}