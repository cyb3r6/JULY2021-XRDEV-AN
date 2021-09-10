namespace ArionDigital
{
    using UnityEngine;
    using UnityEngine.Events;

    public class CrashCrate : MonoBehaviour
    {
        [Header("Whole Create")]
        public MeshRenderer wholeCrate;
        public BoxCollider boxCollider;
        public Rigidbody boxRigidbody;
        [Header("Fractured Create")]
        public GameObject fracturedCrate;
        [Header("Audio")]
        public AudioSource crashAudioClip;
        public UnityEvent OnBreak;

        private void OnCollisionEnter(Collision other)
        {
            if(boxRigidbody.velocity.magnitude > 1.75)
            {
                wholeCrate.enabled = false;
                boxCollider.enabled = false;
                fracturedCrate.SetActive(true);
                crashAudioClip.Play();
                OnBreak?.Invoke();
            }
        }

        [ContextMenu("Test")]
        public void Test()
        {
            wholeCrate.enabled = false;
            boxCollider.enabled = false;
            fracturedCrate.SetActive(true);
        }
    }
}