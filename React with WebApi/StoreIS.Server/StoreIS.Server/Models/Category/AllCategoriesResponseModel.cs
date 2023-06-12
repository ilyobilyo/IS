namespace StoreIS.Server.Models.Category
{
    public class AllCategoriesResponseModel : ResponseModel
    {
        public IEnumerable<CategoryModel> Categories { get; set; }
    }
}
