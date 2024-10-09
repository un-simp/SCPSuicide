
using Exiled.API.Features;

namespace SuicidePro.util
{
    public interface IDeathEffect
    {
        string Name { get;  }
        string Description { get; }
        bool Run(Player player);

    }
}