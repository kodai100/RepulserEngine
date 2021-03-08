using System.Net;
using System.Threading.Tasks;
using ProjectBlue.RepulserEngine.DataStore;

namespace ProjectBlue.RepulserEngine.Repository
{

    public class ConnectionCheckRepository : IConnectionCheckRepository
    {
        private IConnectionCheckDataStore connectionCheckDataStore;

        public ConnectionCheckRepository(IConnectionCheckDataStore connectionCheckDataStore)
        {
            this.connectionCheckDataStore = connectionCheckDataStore;
        }
        
        public Task<bool> Check(IPAddress address)
        {
            return connectionCheckDataStore.Check(address);
        }
        
    }

}