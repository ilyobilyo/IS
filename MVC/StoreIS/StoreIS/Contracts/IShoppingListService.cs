using StoreIS.Data.Models;
using StoreIS.Models.ShoppingList;

namespace StoreIS.Contracts
{
    public interface IShoppingListService
    {
        Task<IEnumerable<ShoppingListViewModel>> GetMyShoppingLists(string userId);
        Task CreateShoppingList (CreateShoppingListViewModel model);

        Task<ShoppingList> GetById(Guid id);

        Task<ShopingListDetailsViewModel> GetListDetails(Guid id);

        Task<bool> AddProductToList(AddProductInputModel model);

        Task<bool> BuyProduct(BuyProductInputModel model);

        Task<bool> DeleteList(Guid id);

        Task<bool> UpdateList(EditListViewModel model);
    }
}
