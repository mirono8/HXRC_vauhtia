using Fusion.Sockets;
using Fusion.XR.Host.Rig;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Fusion.XR.Host {
    /**
     * 
     * Handles:
     * - connexion launch
     * - user representation spawn on connection (on the host)
     * - user despawn by the host on associated player disconnection
     * 
     **/

    public class ConnectionManager : MonoBehaviour, INetworkRunnerCallbacks {
        [Header("Room configuration")]
        public GameMode mode = GameMode.AutoHostOrClient;
        public string roomName;
        public GameObject roomNameSaver;
        public bool connectOnStart = false;
        public GameObject speechly;  // sit ku host on tullu servulle voi laittaa speechlyn p��lle (?)
        public DoomsdayClock timer; //start timer after voice recognition is on

        [Header("Fusion settings")]
        [Tooltip("Fusion runner. Automatically created if not set")]
        public NetworkRunner runner;
        public INetworkSceneManager sceneManager;

        [Header("Local user spawner")]
        public NetworkObject hostPrefab;
        public NetworkObject clientPrefab;

        [Header("Event")]
        public UnityEvent onWillConnect = new UnityEvent();

        // Dictionary of spawned user prefabs, to destroy them on disconnection
        public Dictionary<PlayerRef, NetworkObject> _spawnedUsers = new Dictionary<PlayerRef, NetworkObject>();

        private void Awake() {
            roomNameSaver = GameObject.Find("RoomNameSaver");
            if (roomNameSaver != null) {
                roomName = roomNameSaver.GetComponent<RoomNameDDOL>().customRoomName;
            }
            else if (roomName == "Lobby") {
                roomName = "Lobby";
            }
            else roomName ??= "DefaultRoom"; //if roomName is null, set it to "DefaultRoom"

            // Check if a runner exist on the same game object
            if (runner == null) runner = GetComponent<NetworkRunner>();

            // Create the Fusion runner and let it know that we will be providing user input
            if (runner == null) runner = gameObject.AddComponent<NetworkRunner>();
            runner.ProvideInput = true;
        }

        private async void Start() {
            // Launch the connection at start
            if (connectOnStart) await Connect();
        }

        public async Task Connect()
        {
            // Create the scene manager if it does not exist
            if (sceneManager == null) sceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>();

            if (onWillConnect != null) onWillConnect.Invoke();
            // Start or join (depends on gamemode) a session with a specific name
            var args = new StartGameArgs()
            {
                GameMode = mode,
                SessionName = roomName,
                Scene = SceneManager.GetActiveScene().buildIndex,
                SceneManager = sceneManager
            };
            await runner.StartGame(args);
            if (!speechly.activeInHierarchy)
            {
                speechly.SetActive(true);
                Debug.Log("Restarting Speechly, " + TaskList._taskListInstance.taskList[0]);
                timer.StartCountdown();
            }
            else
            {
                SpeechlyRestarter._restarterInstance.RestartSpeechly();
                timer.StartCountdown();
            }
        }


        #region INetworkRunnerCallbacks
        public void OnPlayerJoined(NetworkRunner runner, PlayerRef player) {
            // The user's prefab has to be spawned by the host
            if (runner.IsServer) {
                NetworkObject userPrefab = hostPrefab;

                if (_spawnedUsers.Count > 0) {
                    userPrefab = clientPrefab;
                }
                Debug.Log($"OnPlayerJoined {player.PlayerId}/Local id: ({runner.LocalPlayer.PlayerId})");
                // We make sure to give the input authority to the connecting player for their user's object
                if (_spawnedUsers.Count == 0) // sallii vaan yhen pelaajan atm koska paska fusion
                {
                    NetworkObject networkPlayerObject = runner.Spawn(userPrefab, position: transform.position, rotation: transform.rotation, inputAuthority: player, (runner, obj) =>
                    {
                    });

                    _spawnedUsers.Add(player, networkPlayerObject); //alunperin t�n if-lauseen ulkopuolella  vvvv ja toi

                    if (userPrefab == hostPrefab)
                    {
                        Camera hostCam = networkPlayerObject.gameObject.GetComponent<NetworkRig>().hardwareRig.GetComponentInChildren<Camera>();
                        hostCam.cullingMask &= ~(1 << LayerMask.NameToLayer("Host Occlusion Mask"));

                    }
                }
                // Keep track of the player avatars so we can remove it when they disconnect
                

                // Stop host's camera from rendering 'Host Occlusion Mask' layer (aka client players)
                
            }
        }

        // Despawn the user object upon disconnection
        public void OnPlayerLeft(NetworkRunner runner, PlayerRef player) {
            // Find and remove the players avatar (only the host would have stored the spawned game object)
            if (_spawnedUsers.TryGetValue(player, out NetworkObject networkObject)) {
                runner.Despawn(networkObject);
                _spawnedUsers.Remove(player);
            }
        }
        #endregion


        #region Unused INetworkRunnerCallbacks 
        public void OnConnectedToServer(NetworkRunner runner) { }
        public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason) { }
        public void OnDisconnectedFromServer(NetworkRunner runner) { }
        public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason) { }
        public void OnInput(NetworkRunner runner, NetworkInput input) { }
        public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) { }
        public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token) { }
        public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message) { }
        public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList) { }
        public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data) { }
        public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken) { }
        public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data) { }
        public void OnSceneLoadDone(NetworkRunner runner) { }
        public void OnSceneLoadStart(NetworkRunner runner) { }
        #endregion
    }

}
