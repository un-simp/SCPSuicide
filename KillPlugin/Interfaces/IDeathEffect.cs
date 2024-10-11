
using Exiled.API.Features;

namespace KillPlugin.Interfaces
{
    public interface IDeathEffect
    {
        string Name { get;  }
        string Description { get; }
        bool Run(Player player);

    }
}