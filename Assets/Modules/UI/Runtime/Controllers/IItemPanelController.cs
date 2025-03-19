using Modules.UI.Runtime.Views;

namespace Modules.UI.Runtime.Controllers
{
    public interface IItemPanelController
    {
        BuildableItemType SelectedItemType { get; }
    }
}