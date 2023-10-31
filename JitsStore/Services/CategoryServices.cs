namespace JitsStore.Services
{
    public class CategoryServices
    {
        private readonly JITS_STORE _context;

        public CategoryServices(JITS_STORE context)
        {
            _context = context;
        }
        public async Task Add(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
        }

        public IEnumerable<Category> GetAll()
        {
            var result = _context.Categories.ToList();

            return result;
        }

        public Category GetbyId(Guid id)
        {
            var result = _context.Categories.FirstOrDefault(x => x.CategoryId == id);

            return result ?? new Category();
        }
    }
}
