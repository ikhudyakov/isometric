using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace ilei
{
    public class OfficiantController : Controller, IGameController
    {
        private int _score;
        private int _level;
        private Camera _camera;
        private RaycastHit _hit;
        private Text _textScore;
        private Text _textLevel;
        private Image _progressBar;

        public LayerMask layerMask;

        public Camera Camera => _camera;
        public int Score { get => _score; set => _score = value; }
        public int Level { get => _level; set => _level = value; }

        private void Awake()
        {
            Agent = gameObject.GetComponent<NavMeshAgent>();
            Animator = GetComponent<Animator>();
            _camera = FindObjectOfType<Camera>();
            Score = 0;
            Level = 1;
            _textScore = GameObject.Find("Score").GetComponent<Text>();
            _textLevel = GameObject.Find("Level").GetComponent<Text>();
            _progressBar = GameObject.Find("ProgressBar").GetComponent<Image>();
        }

        private void Update()
        {
            Move();
        }

        public override void Move()
        {
            
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out _hit, 100, layerMask))
                {
                    Agent.SetDestination(_hit.point);
                    if (_hit.collider.GetComponent<Money>())
                    {
                        Score += _hit.collider.GetComponent<Money>().Point;
                        Debug.Log($" Всего монет: {Score}");
                        Count();
                        _textScore.text = $"{Score}$";
                        Destroy(_hit.collider.transform.parent.gameObject, 1f);
                        Destroy(_hit.collider.gameObject);
                    }
                }
            }
            Vector3 direction = (_hit.point - transform.position).normalized;
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

        public void Count()
        {
            if (Score >= 100)
            {
                Score -= 100;
                Level++;
                _textLevel.text = Level.ToString();
            }
            _progressBar.fillAmount = Score / 100f;
        }
    }
}
