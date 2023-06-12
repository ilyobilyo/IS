using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StoreIS.Contracts;
using StoreIS.Data;
using StoreIS.Data.Models;
using StoreIS.Models.ShoppingList;

namespace StoreIS.Services
{
    public class ShoppingListService : IShoppingListService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IProductService productService;
        private readonly IMapper mapper;

        public ShoppingListService(ApplicationDbContext dbContext,
            IMapper mapper,
            IProductService productService)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.productService = productService;
        }

        public async Task<bool> AddProductToList(AddProductInputModel model)
        {
            var isListIdValid = Guid.TryParse(model.ShoppingListId, out var listId);
            var isProductIdValid = Guid.TryParse(model.ProductId, out var productId);

            if (isListIdValid && isProductIdValid && !await IsProductAlreadyInList(listId, productId))
            {
                var product = await productService.GetById(productId);
                var list = await GetById(listId);

                if (product != null && list != null)
                {
                    var productShoppingList = new ProductShoppingList
                    {
                        ProductId = productId,
                        ShoppingListId = listId,
                    };

                    await dbContext.AddAsync(productShoppingList);
                    await dbContext.SaveChangesAsync();

                    return true;
                }
            }

            return false;
        }

        public async Task<bool> BuyProduct(BuyProductInputModel model)
        {
            var isListIdValid = Guid.TryParse(model.ShoppingListId, out var listId);
            var isProductIdValid = Guid.TryParse(model.ProductId, out var productId);

            if (isListIdValid && isProductIdValid)
            {
                var listProduct = await dbContext
                    .ProductShoppingLists
                    .FirstOrDefaultAsync(x => x.ProductId == productId && x.ShoppingListId == listId);

                if (listProduct == null || listProduct.IsPurchased)
                {
                    return false;
                }

                listProduct.IsPurchased = true;
                await dbContext.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task CreateShoppingList(CreateShoppingListViewModel model)
        {
            var list = new ShoppingList
            {
                Name = model.Name,
                UserId = model.UserId,
                CreatedAt = DateTime.Now,
            };

            await dbContext.AddAsync(list);
            await dbContext.SaveChangesAsync();
        }

        public async Task<bool> DeleteList(Guid id)
        {
            var items = await dbContext
                .ProductShoppingLists
                .Where(x => x.ShoppingListId == id)
                .ToListAsync();

            if (items.Count > 0)
            {
                dbContext.ProductShoppingLists.RemoveRange(items);
            }

            var list = await GetById(id);

            if (list != null)
            {
                dbContext.ShoppingLists.Remove(list);
                await dbContext.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public Task<ShoppingList> GetById(Guid id)
        {
            return dbContext.ShoppingLists.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<ShopingListDetailsViewModel> GetListDetails(Guid id)
        {
            var list = await dbContext
                .ShoppingLists
                .Include(x => x.ProductsShoppingLists)
                .ThenInclude(x => x.Product)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            return mapper.Map<ShopingListDetailsViewModel>(list);
        }

        public async Task<IEnumerable<ShoppingListViewModel>> GetMyShoppingLists(string userId)
        {
            var lists = await dbContext
                .ShoppingLists
                .Where(x => x.UserId == userId)
                .ToListAsync();

            return mapper.Map<IEnumerable<ShoppingListViewModel>>(lists);
        }

        public async Task<bool> UpdateList(EditListViewModel model)
        {
            var listToUpdate = await GetById(model.Id);

            if (listToUpdate == null)
            {
                return false;
            }

            listToUpdate.Name = model.Name;

            await dbContext.SaveChangesAsync();

            return true;
        }

        private async Task<bool> IsProductAlreadyInList(Guid listid, Guid productId)
        {
            return await dbContext.ProductShoppingLists.AnyAsync(x => x.ProductId == productId && x.ShoppingListId == listid);
        }
    }
}
