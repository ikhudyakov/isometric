using UnityEngine;
using UnityEngine.AI;

namespace ilei
{
    public class GuestController : Controller
    {
        private float _timer;
        private GameObject[] Points;
        private Transform point;
        public bool Sitting;
        [Range(1, 4)]
        public int SittingType;

        public float Timer { get => _timer; set => _timer = value; }


        private void Awake()
        {
            Points = GameObject.FindGameObjectsWithTag("Way Point");
            Agent = gameObject.GetComponent<NavMeshAgent>();
            Animator = GetComponent<Animator>();
            Timer = 0;
        }

        private void Update()
        {
            if (Sitting)
            {
                Sit();
            }
            else
            {
                Move();
            }
        }

        public override void Move()
        {
            base.Move();
            Agent.enabled = true;
            Timer -= Time.deltaTime;
            if (Timer <= 0)
            {
                Timer = Random.Range(7, 12);
                point = Points[Random.Range(0, Points.Length)].transform;
            }
            Animator.SetBool("sitting", Sitting);
            Agent.SetDestination(point.position);
            Vector3 direction = (point.transform.position - transform.position).normalized;
            Quaternion lookRotationGO = Quaternion.LookRotation(direction);
            lookRotationGO.x = 0;
            lookRotationGO.z = 0;

            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotationGO, Time.deltaTime * 10f);
            if (Agent.remainingDistance > Agent.stoppingDistance)
            {
                Animator.SetFloat("speed", 1f);
            }
            else
            {
                Animator.SetFloat("speed", 0);
            }
        }

        private void Sit()
        {
            Agent.enabled = false;
            Animator.SetBool("sitting", Sitting);
            Animator.SetFloat("sitting type", SittingType);
        }
    }
}
