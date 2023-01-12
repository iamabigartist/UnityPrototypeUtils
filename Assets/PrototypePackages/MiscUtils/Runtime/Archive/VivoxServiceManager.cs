// using System.Collections.Generic;
// using System.Threading.Tasks;
// using com.unity.MiGuGame;
// using Unity.Services.Vivox;
// using UnityEngine;
// using VivoxPlayground.MyVivoxFramework.Token;
// using VivoxPlayground.MyVivoxFramework.Util;
// using VivoxUnity;
// using static VivoxPlayground.MyVivoxFramework.Util.AsyncUtil;
// using static VivoxPlayground.MyVivoxFramework.Util.MiGuHybridUtil;
// using static VivoxPlayground.MyVivoxFramework.Util.VivoxUtil;
// namespace VivoxPlayground.MyVivoxFramework.Behaviour
// {
//     /// <summary>
//     ///     <para>1. Vivox <see cref="Client" /> Init, Terminate</para>
//     ///     <para>2. <see cref="Account" /> and <see cref="ILoginSession" /> manage</para>
//     ///     <para>3. AudioDevice <seealso cref="IAudioDevice" /> manage.</para>
//     /// </summary>
//     public class VivoxServiceManager : SingletonMonoBehaviour<VivoxServiceManager>
//     {
//         public enum ClientState
//         {
//             BeforeReady,
//             Ready,
//             Terminated
//         }
//
//     #region ManagerState
//
//         public ClientState state { get; private set; }
//         public async Task WaitReady()
//         {
//             await WaitUntil( () => state == ClientState.Ready );
//         }
//
//     #endregion
//
//     #region Data
//
//         public Client m_client;
//         public Account m_account;
//         public ILoginSession m_loginSession;
//         public string account_uri => m_account.ToString();
//         /// <summary>
//         ///     <param name="key">the channel name</param>
//         /// </summary>
//         public Dictionary<string, (ChannelId id, IChannelSession session, IParticipant me)> client_known_channels;
//
//         /// <summary>
//         ///     <param name="key">the channel name</param>
//         /// </summary>
//         public Dictionary<string, ChannelId> server_managed_channels;
//
//     #endregion
//
//     #region Behaviour
//
//     #region Client
//
//         public async void InitClient()
//         {
//             state = ClientState.BeforeReady;
//             await InitVivoxService();
//             m_client = RestartGetVivoxClient();
//             Client.tokenGen = new LocalVxTokenGen();
//             //BUG //
//             await GameWorldCreated();
//             var account_name = Game.IsServerInstance ? "Server" : "" + Game.PlayerNickname.Data.Value;
//             Debug.Log( $"account name: {account_name}" );
//             m_account = new(account_name);
//             var (success, loginSession) = await
//                 LoginVivox( m_client, m_account, (SubscriptionMode.Accept, null, null, null), TransmissionMode.All );
//
//             if (success)
//             {
//                 m_loginSession = loginSession;
//             }
//             else
//             {
//                 throw new("Can not login");
//             }
//
//             client_known_channels = new();
//             server_managed_channels = new();
//             state = ClientState.Ready;
//         }
//
//         public void TerminateClient()
//         {
//             state = ClientState.Terminated;
//             m_loginSession.Logout();
//             TerminateVivoxClient( m_client );
//         }
//
//     #endregion
//
//     #region Channel
//
//         public async Task JoinChannel(
//             string channel_name,
//             string channel_uri,
//             ( bool connectAudio, bool connectText, bool switchTransmission) join_channel_params)
//         {
//             VivoxLog( "Start join channel" );
//             var channel_id = new ChannelId( channel_uri );
//             var (success, channel_session, me) = await EstablishChannelConnection(
//                 m_loginSession, channel_id, join_channel_params );
//
//             if (success)
//             {
//                 client_known_channels[channel_name] = (channel_id, channel_session, me);
//             }
//             else
//             {
//                 VivoxLogError( $"Cannot join channel {channel_name}" );
//             }
//         }
//
//     #endregion
//
//     #region Device
//
//         public void ChangeInputDeviceVolume(int value) { m_client.AudioInputDevices.VolumeAdjustment = value; }
//         public void ChangeOutputDeviceVolume(int value) { m_client.AudioOutputDevices.VolumeAdjustment = value; }
//         public void SwitchMicrophone()
//         {
//             m_client.AudioInputDevices.Muted = !m_client.AudioInputDevices.Muted;
//         }
//         public void SetMuteMicrophone(bool muted)
//         {
//             m_client.AudioInputDevices.Muted = muted;
//         }
//
//     #endregion
//
//     #region Message
//
//     #endregion
//
//     #endregion
//
//     #region UnityCallback
//
//         protected override void Awake()
//         {
//             base.Awake();
//             InitClient();
//         }
//
//         void OnApplicationQuit()
//         {
//             //Bug 这个地方调用Terminate会导致Editor在停止play的时候崩溃
//             // TerminateClient();
//         }
//
//     #endregion
//     }
//
// }
