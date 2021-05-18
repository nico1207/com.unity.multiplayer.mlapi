using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Multiplayer.NetStats.Data;
using Unity.Multiplayer.NetStats.Dispatch;
using Unity.Multiplayer.NetStats.Metrics;

namespace MLAPI.Metrics
{
    public class NetworkMetrics
    {
#if true
        private IMetricDispatcher m_Dispatcher;

        private Counter m_NbConnections = new Counter("Connections") { ShouldResetOnDispatch = false };
        private Counter m_BytesReceived = new Counter("Bytes Received");
        private Counter m_BytesSent = new Counter("Bytes Sent");

        private EventMetric<RpcEvent> m_RpcEvent = new EventMetric<RpcEvent>("RPC Event Sent");

        private Dictionary<ulong, ConnectionInfo> m_ConnectionInfos = new Dictionary<ulong, ConnectionInfo>();
        private Dictionary<ulong, NetworkObjectIdentifier> m_GameObjectIdentifiers = new Dictionary<ulong, NetworkObjectIdentifier>();

        private NetworkObjectIdentifier m_DummyNOI = new NetworkObjectIdentifier { Name = "Dummy NOI", NetworkId = 42 };
#endif

        public NetworkMetrics()
        {
            m_Dispatcher = new MetricDispatcherBuilder()
                .WithCounters(m_NbConnections, m_BytesReceived, m_BytesSent)
                .WithGauges()
                .WithMetricEvents()
                .Build();
        }

        public void TrackRpcSent()
        {
                m_RpcEvent.Mark(new RpcEvent());
        }

        public void TrackConnectionCount(long amount, ulong clientId)
        {
#if true
            m_NbConnections.Increment(amount);
            if (m_ConnectionInfos.ContainsKey(clientId) && amount < 0)
            {
                m_ConnectionInfos.Remove(clientId);
            }
            else
            {
                m_ConnectionInfos[clientId] = new ConnectionInfo { Id = clientId };
            }
#endif
        }

        public void TrackNetworkGameObject(ulong networkId, string name)
        {
            if (!m_GameObjectIdentifiers.ContainsKey(networkId))
            {
                m_GameObjectIdentifiers[networkId] = new NetworkObjectIdentifier { Name = name, NetworkId = networkId };
            }
        }

        public void RegisterRpcSent(ulong clientId, /*ulong gameObjectId,*/ string name, ulong bytes)
        {
            m_RpcEvent.Mark(new RpcEvent
            {
                Connection = m_ConnectionInfos[clientId],
                NetworkId = m_DummyNOI,
                Name = name,
                BytesCount = bytes
            });
        }

        public void TrackBytesReceived(long amount)
        {
#if true
            m_BytesReceived.Increment(amount);
#endif
        }

        public void TrackBytesSent(long amount)
        {
#if true
            m_BytesSent.Increment(amount);
#endif
        }
    }
}
