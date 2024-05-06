using UnityEngine;

public class ItemShield : Item
{
    public int shieldAmount;
    public ParticleSystem effect;
    public override void ApplyEffect(Hero hero)
    {
        Debug.Log("use item heal");
        var data = new DamageData();
        data.damage = 1;
        hero.AddShield(shieldAmount);
        var effect = SimpleObjectPool.Instance.GetObjectFromPool(this.effect, hero.transform.position);
        effect.transform.SetParent(hero.transform);

        var card = GetComponent<Card>();
        CardManager.Instance.MoveCardsAfterUse(card);
    }
}
