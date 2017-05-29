using Model.Common.ViewModels;
using System.Threading.Tasks;

namespace Service.Common
{
    public interface IWarningService
    {
        Task<IWarningViewModel> CreateWarningViewModel();
    }
}
