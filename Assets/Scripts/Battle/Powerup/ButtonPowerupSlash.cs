public class ButtonPowerupSlash : ButtonPowerup
{
    public PowerupSlash powerupPrefab;

    public override void OnClick()
    {
        var hero = CardManager.Instance.hero;
        var unlockLevel = DataManager.Instance.dicPowerUp[id].unlockLevel;
        if (hero.heroData.level < unlockLevel) return;

        var neightbours = CardManager.Instance.heroNeighbours;
        for (int i = 0; i < neightbours.Count; i++)
        {
            GridPos grid = GridManager.Instance.dicGrids[neightbours[i]];
            var slash = Instantiate(powerupPrefab, grid.card.transform);
            slash.transform.position = grid.card.transform.position;
            slash.pos = grid.pos;
            slash.card = grid.card;
            CardManager.Instance.UpdateHeroNeighbours();
        }
    }
}
