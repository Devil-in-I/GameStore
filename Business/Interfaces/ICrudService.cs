using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface ICrudService<TModel>
    {
        Task<IEnumerable<TModel>> GetAllAsync();

        Task<TModel> GetByIdAsync(int id);

        Task AddAsync(TModel model);

        Task<TModel> UpdateAsync(int id,TModel model);

        Task DeleteAsync(int modelId);
    }
}
