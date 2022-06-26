using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Holoville.HOTween;

public class BossCtrl : MonoBehaviour
{
    public enum BossState { None, Idle, Move, Wait, GoTarget, Atk, Damage, Die }
    [Header("기본 속성")]
    public BossState bossState = BossState.None;
    public float spdMove = 1f;
    public GameObject targetCharactor = null;
    public Transform targetTransform = null;
    public Vector3 posTarget = Vector3.zero;
    private Animation BossAnimation = null;
    private Transform BossTransform = null;

    [Header("애니메이션 클립")]
    public AnimationClip IdleAnimClip = null;
    public AnimationClip MoveAnimClip = null;
    public AnimationClip AtkAnimClip = null;

    [Header("전투속성")]
    [SerializeField]
    private int hp = 1000;
    public float AtkRange = 3f;

    public ParticleSystem effect;

    [SerializeField]
    private int bossCoin = 150;

    private bool isHit = false;

    void OnAtkAnmationFinished()
    {

    }

    void OnDmgAnmationFinished()
    {

    }

    void OnDieAnmationFinished()
    {
        Debug.Log("Die Animation finished");
        Destroy(gameObject);
    }

    /// <summary>
    /// 애니메이션 이벤트를 추가해주는 함. 
    /// </summary>
    /// <param name="clip">애니메이션 클립 </param>
    /// <param name="funcName">함수명 </param>
    void OnAnimationEvent(AnimationClip clip, string funcName)
    {
        AnimationEvent retEvent = new AnimationEvent();
        retEvent.functionName = funcName;
        retEvent.time = clip.length - 0.1f;
        clip.AddEvent(retEvent);
    }

    void Start()
    {

        bossState = BossState.Idle;
        BossAnimation = GetComponent<Animation>();
        BossTransform = GetComponent<Transform>();
        BossAnimation[IdleAnimClip.name].wrapMode = WrapMode.Loop;
        BossAnimation[MoveAnimClip.name].wrapMode = WrapMode.Loop;
        BossAnimation[AtkAnimClip.name].wrapMode = WrapMode.Once;
        OnAnimationEvent(AtkAnimClip, "OnAtkAnmationFinished");
    }

    /// <summary>
    /// 보스 상태에 따라 동작을 제어하는 함수 
    /// </summary>
    void CkState()
    {
        switch (bossState)
        {
            case BossState.Idle:
                //이동에 관련된 RayCast값
                setIdle();
                break;
            case BossState.GoTarget:
            case BossState.Move:
                setMove();
                break;
            case BossState.Atk:
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
    /// 보스 상태가 대기 일 때 동작 
    /// </summary>
    void setIdle()
    {
        if (targetCharactor == null)
        {
            posTarget = new Vector3(BossTransform.position.x + Random.Range(-10f, 10f),
                                    BossTransform.position.y + 1000f,
                                    BossTransform.position.z + Random.Range(-10f, 10f)
                );
            Ray ray = new Ray(posTarget, Vector3.down);
            RaycastHit infoRayCast = new RaycastHit();
            if (Physics.Raycast(ray, out infoRayCast, Mathf.Infinity) == true)
            {
                posTarget.y = infoRayCast.point.y;
            }
            bossState = BossState.Move;
        }
        else
        {
            bossState = BossState.GoTarget;
        }
    }

    /// <summary>
    /// 보스 상태가 이동 일 때 동 
    /// </summary>
    void setMove()
    {
        Vector3 distance = Vector3.zero;
        Vector3 posLookAt = Vector3.zero;
        switch (bossState)
        {
            case BossState.Move:
                if (posTarget != Vector3.zero)
                {
                    distance = posTarget - BossTransform.position;
                    if (distance.magnitude < AtkRange)
                    {
                        StartCoroutine(setWait());
                        return;
                    }

                    posLookAt = new Vector3(posTarget.x,
                                            BossTransform.position.y,
                                            posTarget.z);
                }
                break;
            case BossState.GoTarget:
                if (targetCharactor != null)
                {
                    distance = targetCharactor.transform.position - BossTransform.position;
                    if (distance.magnitude < AtkRange)
                    {
                        bossState = BossState.Atk;
                        return;
                    }
                    posLookAt = new Vector3(targetCharactor.transform.position.x,
                                            BossTransform.position.y,
                                            targetCharactor.transform.position.z);
                }
                break;
            default:
                break;
        }

        Vector3 direction = distance.normalized;
        direction = new Vector3(direction.x, 0f, direction.z);
        Vector3 amount = direction * spdMove * Time.deltaTime;
        BossTransform.Translate(amount, Space.World);
        BossTransform.LookAt(posLookAt);
    }

    /// <summary>
    /// 대기 상태 동작 함 
    /// </summary>
    /// <returns></returns>
    IEnumerator setWait()
    {
        bossState = BossState.Wait;
        float timeWait = Random.Range(1f, 3f);
        yield return new WaitForSeconds(timeWait);
        bossState = BossState.Idle;
    }

    /// <summary>
    /// 애니메이션을 재생시켜주는 함 
    /// </summary>
    void AnimationCtrl()
    {
        switch (bossState)
        {
            case BossState.Wait:
            case BossState.Idle:
                BossAnimation.CrossFade(IdleAnimClip.name);
                break;
            case BossState.Move:
            case BossState.GoTarget:
                BossAnimation.CrossFade(MoveAnimClip.name);
                break;
            //공격할 때
            case BossState.Atk:
                BossAnimation.CrossFade(AtkAnimClip.name);
                break;
            //죽었을 때
            case BossState.Die:
                break;
            default:
                break;

        }
    }

    ///<summary>
    ///시야 범위 안에 다른 Trigger 또는 캐릭터가 들어오면 호출 된다.
    ///함수 동작은 목표물이 들어오면 목표물을 설정하고 보스를 타겟 위치로 이동 시킨다 
    ///</summary>
    void OnCkTarget(GameObject target)
    {
        targetCharactor = target;
        targetTransform = targetCharactor.transform;
        bossState = BossState.GoTarget;

    }

    /// <summary>
    /// 보스 상태 공격 모드
    /// </summary>
    void setAtk()
    {
        if (PlayerCtrl.Instance.HP <= 0)
            return;
        float distance = Vector3.Distance(targetTransform.position, BossTransform.position); //무겁다
        if (distance > AtkRange + 0.5f)
        {
            bossState = BossState.GoTarget;
        }
    }

    /// <summary>
    /// 보스 피격 충돌 검출 
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerAtk") == true)
        {
            if (isHit) return;

            isHit = true;
            Instantiate(effect, transform).transform.position = new Vector3(transform.position.x, 2.5f, transform.position.z);
            PlayerCtrl player = targetCharactor.GetComponent<PlayerCtrl>();

            hp -= 10 + (int)player.STRENGTH;
            if (hp >= 0)
            {
                isHit = false;
            }
            else
            {
                bossState = BossState.Die;
                player.Coin += bossCoin;
                Destroy(gameObject);
            }
        }
    }

    public void PlayerDie()
    {
        bossState = BossState.Idle;
    }
}