using UnityEngine;
using UnityEngine.UIElements;

public class Monster : CardEffect
{
    public MonsterData monsterData;
    private TextHp textHp;
    [SerializeField] private ParticleSystem attackEffect;

    private void Awake()
    {
        textHp = GetComponentInChildren<TextHp>();
    }
    private void Start()
    {
        Init();
        monsterData.currentHp = monsterData.maxHp;
        textHp.SetHP(monsterData);
    }
    private void OnEnable()
    {
        Init();
    }
    public override void ApplyEffect(Hero hero)
    {
        var damage = new DamageData();
        damage.damage = monsterData.currentHp;
        hero.TakeDamage(damage, out bool dead);
        if (dead == false)
        {
            hero.AddEXP(monsterData.rewardExp);
            Debug.Log($"add exp {monsterData.rewardExp}");
        }
        var effect = SimpleObjectPool.Instance.GetObjectFromPool(attackEffect, transform.position);
        var card = GetComponent<Card>();
        CardManager.Instance.MoveCardsAfterUse(card);
    }

    public void Init()
    {
        var heroData = CardManager.Instance.hero.heroData;
        monsterData.maxHp = (int)((heroData.level + 1) * monsterData.baseHp * monsterData.multiple);
        monsterData.currentHp = monsterData.maxHp;
    }

    public void UpdateHpWhenPlayerLevelUp()
    {
        int hpLost = monsterData.maxHp - monsterData.currentHp;
        Init();
        monsterData.currentHp = monsterData.maxHp - hpLost;
    }

    private void OnDisable()
    {
        Destroy(this);
    }

    public void TakeDamage(DamageData data, out bool dead)
    {
        dead = false;
        monsterData.currentHp -= data.damage;
        if (monsterData.currentHp <= 0)
        {
            monsterData.currentHp = 0;
            dead = true;
        }
        textHp.SetHP(monsterData);

        if (dead == true)
        {
            CardManager.Instance.hero.AddEXP(monsterData.rewardExp);
            Debug.Log($"add exp {monsterData.rewardExp}");

            var monsterCard = GetComponent<Card>();
            monsterCard.Disappear();
            CardId nextCardId = CardManager.Instance.GetNextCard();
            var newCard = CardManager.Instance.SpawnCard(monsterCard.Pos, nextCardId);
            newCard.ShowSpawnAnimation();
        }
    }
}