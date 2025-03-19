using Modules.UIModule.Runtime.Views;

namespace Modules.UIModule.Runtime.Controllers
{
    public interface IItemPanelController
    {
        BuildableItemType SelectedItemType { get; }
    }
}