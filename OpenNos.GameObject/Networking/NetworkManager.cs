﻿/*
 * This file is part of the OpenNos Emulator Project. See AUTHORS file for Copyright information
 *
 * This program is free software; you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation; either version 2 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 */
using OpenNos.Core.Communication.Scs.Communication.EndPoints.Tcp;
using OpenNos.Core.Communication.Scs.Communication.Messages;
using OpenNos.Core.Communication.Scs.Server;
using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Concurrent;
using OpenNos.Core;
using System.IO;
using OpenNos.Data;
using OpenNos.DAL;

namespace OpenNos.GameObject
{
    public class NetworkManager<EncryptorT>
        where EncryptorT : EncryptionBase
    {

        #region Members

        private Type _packetHandler;
        private EncryptorT _encryptor;
        private IDictionary<String, DateTime> _connectionLog;
        private ConcurrentDictionary<long, ClientSession> _sessions = new ConcurrentDictionary<long, ClientSession>();
        private ConcurrentDictionary<Guid, Map> _maps = new ConcurrentDictionary<Guid, Map>();

        #endregion

        #region Instantiation

        public NetworkManager(string ipAddress, int port, Type packetHandler)
        {
            _packetHandler = packetHandler;
            _encryptor = (EncryptorT)Activator.CreateInstance(typeof(EncryptorT));

            var server = ScsServerFactory.CreateServer(new ScsTcpEndPoint(ipAddress, port));
            //Register events of the server to be informed about clients
            server.ClientConnected += Server_ClientConnected;
            server.ClientDisconnected += Server_ClientDisconnected;
            server.WireProtocolFactory = new WireProtocolFactory<EncryptorT>();

            server.Start(); //Start the server

            Logger.Log.Info(Language.Instance.GetMessageFromKey("STARTED"));
        }

        #endregion

        #region Event Handlers

        void Server_ClientConnected(object sender, ServerClientEventArgs e)
        {
            Logger.Log.Info(Language.Instance.GetMessageFromKey("NEW_CONNECT") + e.Client.ClientId);
            NetworkClient customClient = e.Client as NetworkClient;

            if (!CheckConnectionLog(customClient))
            {
                Logger.Log.WarnFormat(Language.Instance.GetMessageFromKey("FORCED_DISCONNECT"), customClient.ClientId);
                customClient.Disconnect();
                return;
            }

            ClientSession session = new ClientSession(customClient);
            session.Initialize(_encryptor, _packetHandler);
            ClientLinkManager.Instance.sessions.Add(session);
            if (!_sessions.TryAdd(customClient.ClientId, session))
            {
                ClientLinkManager.Instance.sessions.Remove(session);
                Logger.Log.WarnFormat(Language.Instance.GetMessageFromKey("FORCED_DISCONNECT"), customClient.ClientId);
                customClient.Disconnect();
                _sessions.TryRemove(customClient.ClientId, out session);
                return;
            };
        }

        void Server_ClientDisconnected(object sender, ServerClientEventArgs e)
        {
            ClientSession session;
            _sessions.TryRemove(e.Client.ClientId, out session);
            ClientLinkManager.Instance.sessions.Remove(session);
            if (session.Character != null)
            {
                //only remove the character from map if the character has been set
                session.CurrentMap.BroadCast(session, session.Character.GenerateOut(), ReceiverType.AllExceptMe);
            }
            session.UnregisterForMapNotification();
            if (session.HealthThread != null && session.HealthThread.IsAlive)
                session.HealthThread.Abort();
            session.Destroy();
            e.Client.Disconnect();
            Logger.Log.Info(Language.Instance.GetMessageFromKey("DISCONNECT") + e.Client.ClientId);
            session = null;
        }

        #endregion

        #region Methods
 

        private bool CheckConnectionLog(NetworkClient client)
        {
            if (ConnectionLog.Any())
            {
                IEnumerable<KeyValuePair<string, DateTime>> logsToDelete = ConnectionLog
                    .Where(cl => cl.Key.Equals(client.RemoteEndPoint.ToString()) && (DateTime.Now - cl.Value).Seconds > 5);

                foreach (KeyValuePair<string, DateTime> connectionLogEntry in logsToDelete)
                {
                    ConnectionLog.Remove(connectionLogEntry);
                }
            }

            if (ConnectionLog.ContainsKey(client.RemoteEndPoint.ToString()))
            {
                return false;
            }
            else
            {
                ConnectionLog.Add(client.RemoteEndPoint.ToString(), DateTime.Now);
                return true;
            }
        }

        #endregion

        #region Properties

        public IDictionary<String, DateTime> ConnectionLog
        {
            get
            {
                if (_connectionLog == null)
                {
                    _connectionLog = new Dictionary<String, DateTime>();
                }

                return _connectionLog;
            }
            set
            {
                if (_connectionLog != value)
                {
                    _connectionLog = value;
                }
            }
        }

        #endregion
    }
}
