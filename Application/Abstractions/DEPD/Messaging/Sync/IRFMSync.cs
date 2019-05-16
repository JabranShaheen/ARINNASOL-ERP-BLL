using System.Threading.Tasks;

namespace Abstractions.Dependencies.Messaging.Sync
{
    public interface IRFMSync
    {

        void UpSyncEntity(string EntityType = "", string EntityID = "", string TechnicianID = "", bool IsFullSync = false);

    }
}
