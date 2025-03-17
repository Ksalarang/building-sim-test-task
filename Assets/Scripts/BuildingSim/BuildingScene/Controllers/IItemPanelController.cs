using BuildingSim.BuildingScene.Items;

namespace BuildingSim.BuildingScene.Controllers
{
    public interface IItemPanelController
    {
        BuildableItemType SelectedItemType { get; }
    }
}