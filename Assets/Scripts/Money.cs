using UnityEngine;

namespace ilei
{
    public class Money : MonoBehaviour
    {
        private int _point;
        public TextMesh text;

        public int Point { get => _point; set => _point = value; }

        private void Start()
        {
            Point = Random.Range(5, 15);
        }

        private void OnDestroy()
        {
            text.text = $"+{Point}$";
        }
    }
}