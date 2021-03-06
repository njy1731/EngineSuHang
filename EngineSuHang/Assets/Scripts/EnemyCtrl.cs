using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCtrl : MonoBehaviour
{
    public GameObject PopUpText;

    public Slider hpBar = null;

    public static EnemyCtrl EInstance;
    //적 상태
    public enum EnemyState { None, Idle, Move, Wait, GoTarget, Atk, Damage, Die }

    //해골 기본 속성
    [Header("기본 속성")]
    //해골 초기 상태
    public EnemyState enemyState = EnemyState.None;
    //해골 이동 속도
    public float spdMove = 1f;
    //해골이 본 타겟
    public GameObject targetCharactor = null;
    //해골이 본 타겟 위치정보 (매번 안 찾을려고)
    public Transform targetTransform = null;
    //해골이 본 타겟 위치(매번 안 찾을려)
    public Vector3 posTarget = Vector3.zero;

    //해골 애니메이션 컴포넌트 캐싱 
    private Animation EnemyAnimation = null;
    //해골 트랜스폼 컴포넌트 캐싱
    private Transform EnemyTransform = null;

    [Header("애니메이션 클립")]
    public AnimationClip IdleAnimClip = null;
    public AnimationClip MoveAnimClip = null;
    public AnimationClip AtkAnimClip = null;

    private PlayerCtrl player;
    //public AnimationClip DamageAnimClip = null;
    //public AnimationClip DieAnimClip = null;

    [Header("전투속성")]
    //해골 체력
    [SerializeField]
    private int hp = 110;
    private int maxHp = 110;
    //해골 공격 거리
    public float AtkRange = 1.5f;

    public ParticleSystem effect;

    [SerializeField]
    private int enemyCoin = 10;

    private bool isHit = false;


    //private Tweener effectTweener = null;
    //private SkinnedMeshRenderer skinnedMeshRenderer = null;


    void OnAtkAnmationFinished()
    {

    }

    void OnDmgAnmationFinished()
    {

    }

    void OnDieAnmationFinished()
    {
        Debug.Log("Die Animation finished");

        //몬스터 삭제 
        Destroy(gameObject);
    }

    /// <summary>
    /// 애니메이션 이벤트를 추가해주는 함. 
    /// </summary>
    /// <param name="clip">애니메이션 클립 </param>
    /// <param name="funcName">함수명 </param>
    void OnAnimationEvent(AnimationClip clip, string funcName)
    {
        //애니메이션 이벤트를 만들어 준다
        AnimationEvent retEvent = new AnimationEvent();
        //애니메이션 이벤트에 호출 시킬 함수명
        retEvent.functionName = funcName;
        //애니메이션 클립 끝나기 바로 직전에 호출
        retEvent.time = clip.length - 0.1f;
        //위 내용을 이벤트에 추가 하여라
        clip.AddEvent(retEvent);
    }

    void UpdateHP()
    {
        hpBar.maxValue = maxHp;
        hpBar.value = hp;
    }

    void Start()
    {
        hp = maxHp;

        UpdateHP();

        EInstance = this;
        
        //처음 상태 대기상태
        enemyState = EnemyState.Idle;

        //애니메이, 트랜스폼 컴포넌트 캐싱 : 쓸때마다 찾아 만들지 않게
        EnemyAnimation = GetComponent<Animation>();
        EnemyTransform = GetComponent<Transform>();

        //애니메이션 클립 재생 모드 비중
        EnemyAnimation[IdleAnimClip.name].wrapMode = WrapMode.Loop;
        EnemyAnimation[MoveAnimClip.name].wrapMode = WrapMode.Loop;
        EnemyAnimation[AtkAnimClip.name].wrapMode = WrapMode.Once;
        //EnemyAnimation[DamageAnimClip.name].wrapMode = WrapMode.Once;

        //애니메이션 블랜딩 위해 크게 올림
        //EnemyAnimation[DamageAnimClip.name].layer = 10;
        //EnemyAnimation[DieAnimClip.name].wrapMode = WrapMode.Once;
        //EnemyAnimation[DieAnimClip.name].layer = 10;

        //공격 애니메이션 이벤트 추가
        OnAnimationEvent(AtkAnimClip, "OnAtkAnmationFinished");
        //OnAnimationEvent(DamageAnimClip, "OnDmgAnmationFinished");
        //OnAnimationEvent(DieAnimClip, "OnDieAnmationFinished");

        //스킨매쉬 캐싱
        //skinnedMeshRenderer = EnemyTransform.Find("UD_light_infantry").GetComponent<SkinnedMeshRenderer>();
    }

    /// <summary>
    /// 해골 상태에 따라 동작을 제어하는 함수 
    /// </summary>
    void CkState()
    {
        switch (enemyState)
        {
            case EnemyState.Idle:
                //이동에 관련된 RayCast값
                setIdle();
                break;
            case EnemyState.GoTarget:
            case EnemyState.Move:
                setMove();
                break;
            case EnemyState.Atk:
                setAtk();
                break;
            default:
                break;
        }
    }

    void Update()
    {
        CkState();
        AnimationCtrl();
    }

    /// <summary>
    /// 해골 상태가 대기 일 때 동작 
    /// </summary>
    void setIdle()
    {
        if (targetCharactor == null)
        {
            posTarget = new Vector3(EnemyTransform.position.x + Random.Range(-10f, 10f),
                                    EnemyTransform.position.y + 1000f,
                                    EnemyTransform.position.z + Random.Range(-10f, 10f)
                );
            Ray ray = new Ray(posTarget, Vector3.down);
            RaycastHit infoRayCast = new RaycastHit();
            if (Physics.Raycast(ray, out infoRayCast, Mathf.Infinity) == true)
            {
                posTarget.y = infoRayCast.point.y;
            }
            enemyState = EnemyState.Move;
        }
        else
        {
            enemyState = EnemyState.GoTarget;
        }
    }

    /// <summary>
    /// 해골 상태가 이동 일 때 동 
    /// </summary>
    void setMove()
    {
        //출발점 도착점 두 벡터의 차이 
        Vector3 distance = Vector3.zero;
        //어느 방향을 바라보고 가고 있느냐 
        Vector3 posLookAt = Vector3.zero;

        //해골 상태
        switch (enemyState)
        {
            //해골이 돌아다니는 경우
            case EnemyState.Move:
                //만약 랜덤 위치 값이 제로가 아니면
                if (posTarget != Vector3.zero)
                {
                    //목표 위치에서 해골 있는 위치 차를 구하고
                    distance = posTarget - EnemyTransform.position;

                    //만약에 움직이는 동안 해골이 목표로 한 지점 보다 작으 
                    if (distance.magnitude < AtkRange)
                    {
                        //대기 동작 함수를 호출
                        StartCoroutine(setWait());
                        //여기서 끝냄
                        return;
                    }

                    //어느 방향을 바라 볼 것인. 랜덤 지역
                    posLookAt = new Vector3(posTarget.x,
                                            //타겟이 높이 있을 경우가 있으니 y값 체크
                                            EnemyTransform.position.y,
                                            posTarget.z);
                }
                break;
            //캐릭터를 향해서 가는 돌아다니는  경우
            case EnemyState.GoTarget:
                //목표 캐릭터가 있을 땟
                if (targetCharactor != null)
                {
                    //목표 위치에서 해골 있는 위치 차를 구하고
                    distance = targetCharactor.transform.position - EnemyTransform.position;
                    //만약에 움직이는 동안 해골이 목표로 한 지점 보다 작으 
                    if (distance.magnitude < AtkRange)
                    {
                        //공격상태로 변경합니.
                        enemyState = EnemyState.Atk;
                        //여기서 끝냄
                        return;
                    }
                    //어느 방향을 바라 볼 것인. 랜덤 지역
                    posLookAt = new Vector3(targetCharactor.transform.position.x,
                                            //타겟이 높이 있을 경우가 있으니 y값 체크
                                            EnemyTransform.position.y,
                                            targetCharactor.transform.position.z);
                }
                break;
            default:
                break;

        }

        //해골 이동할 방향에 크기를 없애고 방향만 가진(normalized)
        Vector3 direction = distance.normalized;

        //방향은 x,z 사용 y는 땅을 파고 들어갈거라 안함
        direction = new Vector3(direction.x, 0f, direction.z);

        //이동량 방향 구하기
        Vector3 amount = direction * spdMove * Time.deltaTime;

        //캐릭터 컨트롤이 아닌 트랜스폼으로 월드 좌표 이용하여 이동
        EnemyTransform.Translate(amount, Space.World);
        //캐릭터 방향 정하기
        EnemyTransform.LookAt(posLookAt);

    }

    /// <summary>
    /// 대기 상태 동작 함 
    /// </summary>
    /// <returns></returns>
    IEnumerator setWait()
    {
        //해골 상태를 대기 상태로 바꿈
        enemyState = EnemyState.Wait;
        //대기하는 시간이 오래되지 않게 설정
        float timeWait = Random.Range(1f, 3f);
        //대기 시간을 넣어 준.
        yield return new WaitForSeconds(timeWait);
        //대기 후 다시 준비 상태로 변경
        enemyState = EnemyState.Idle;
    }

    /// <summary>
    /// 애니메이션을 재생시켜주는 함 
    /// </summary>
    void AnimationCtrl()
    {
        //해골의 상태에 따라서 애니메이션 적용
        switch (enemyState)
        {
            //대기와 준비할 때 애니메이션 같.
            case EnemyState.Wait:
            case EnemyState.Idle:
                //준비 애니메이션 실행
                EnemyAnimation.CrossFade(IdleAnimClip.name);
                break;
            //랜덤과 목표 이동할 때 애니메이션 같.
            case EnemyState.Move:
            case EnemyState.GoTarget:
                //이동 애니메이션 실행
                EnemyAnimation.CrossFade(MoveAnimClip.name);
                break;
            //공격할 때
            case EnemyState.Atk:
                //공격 애니메이션 실행
                EnemyAnimation.CrossFade(AtkAnimClip.name);
                break;
            //죽었을 때
            case EnemyState.Die:
                //죽을 때도 애니메이션 실행
                //EnemyAnimation.CrossFade(DieAnimClip.name);
                break;
            default:
                break;

        }
    }

    ///<summary>
    ///시야 범위 안에 다른 Trigger 또는 캐릭터가 들어오면 호출 된다.
    ///함수 동작은 목표물이 들어오면 목표물을 설정하고 해골을 타겟 위치로 이동 시킨다 
    ///</summary>
    void OnCkTarget(GameObject target)
    {
        //목표 캐릭터에 파라메터로 검출된 오브젝트를 넣고 
        targetCharactor = target;
        //목표 위치에 목표 캐릭터의 위치 값을 넣습니다. 
        targetTransform = targetCharactor.transform;

        //목표물을 향해 해골이 이동하는 상태로 변경
        enemyState = EnemyState.GoTarget;

    }

    /// <summary>
    /// 해골 상태 공격 모드
    /// </summary>
    void setAtk()
    {
        if (PlayerCtrl.Instance.HP <= 0)
            return;
        //해골과 캐릭터간의 위치 거리 
        float distance = Vector3.Distance(targetTransform.position, EnemyTransform.position); //무겁다

        //공격 거리보다 둘 간의 거리가 멀어 졌다면 
        if (distance > AtkRange + 0.5f)
        {
            //타겟과의 거리가 멀어졌다면 타겟으로 이동 
            enemyState = EnemyState.GoTarget;
        }
    }

    IEnumerator PopUp()
    {
        PopUpText.SetActive(true);
        yield return new WaitForSeconds(1f);
        PopUpText.SetActive(false);
        Destroy(gameObject);
    }

    /// <summary>
    /// 해골 피격 충돌 검출 
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        //만약에 해골이 캐릭터 공격에 맞았다면
        if (other.gameObject.CompareTag("PlayerAtk") == true)
        {
            if (isHit) return;

            isHit = true;
            Instantiate(effect, transform).transform.position = new Vector3(transform.position.x, 1f, transform.position.z);
            PlayerCtrl player = targetCharactor.GetComponent<PlayerCtrl>();

            //해골 체력을 10 빼고
            hp -= 10 + (int)player.STRENGTH/2;
            UpdateHP();
            if (hp >= 0)
            {
                //체력이 0 이상이면 피격 애니메이션을 연출 하고 
                //EnemyAnimation.CrossFade(DamageAnimClip.name);

                //피격 트위닝 이펙트
                //effectDamageTween();
                isHit = false;
            }

            else
            {
                //0 보다 작으면 해골이 죽음 상태로 바꾸어라
                enemyState = EnemyState.Die;
                player.Coin += enemyCoin;
                StartCoroutine(PopUp());
            }   
        }
    }

    public void PlayerDie()
    {
        enemyState = EnemyState.Idle;
    }

    /// <summary>
    /// 피격시 몬스터 몸에서 번쩍번쩍 효과를 준다
    /// </summary>
    //void effectDamageTween()
    //{
    //    //트윈을 돌리다 또 트윈 함수가 진행되면 로직이 엉망이 될 수 있어서 
    //    //트윈 중복 체크로 미리 차단을 해준다
    //    if (effectTweener != null && effectTweener.isComplete == false)
    //    {
    //        return;
    //    }

    //    //번쩍이는 이펙트 색상을 지정해준다
    //    Color colorTo = Color.red;

    //    //트윈의 타겟은 스킨매쉬랜더러, 시간은 0.2초, 파라메터로는 색상 , 반복. 콜백함수
    //    effectTweener = HOTween.To(skinnedMeshRenderer, 0.2f, new TweenParms()
    //                            //색상을 교체
    //                            .Prop("color", colorTo)
    //                            // 반복은 1번만 요요를 안쓰면 빨강색 1회 흰색 1회 해야 한다. 
    //                            .Loops(1, LoopType.Yoyo)
    //                            //피격 이펙트 종료시 이벤트 함수 호출
    //                            .OnStepComplete(OnDamageTweenFinished)
    //        );
    //}

    /// <summary>
    /// 피격이펙트 종료시 이벤트 함수 호출
    /// </summary>
    //void OnDamageTweenFinished()
    //{
    //    //트윈이 끝나면 하얀색으로 확실히 색상을 돌려준다
    //    skinnedMeshRenderer.material.color = Color.white;
    //}
}