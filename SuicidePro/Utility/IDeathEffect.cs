
using Exiled.API.Features;

namespace SuicidePro.Utility
{
    public interface IDeathEffect
    {
        string Name { get;  }
        string Description { get; }
        bool Run(Player player);

    }
}