// using System;
// using System.Collections.Generic;
// using System.Security.Cryptography;
// using System.Text;
// using System.Threading.Tasks;
// using Unity.Services.Authentication;
// using Unity.Services.Core;
// using Unity.Services.Vivox;
// using UnityEngine;
// using VivoxUnity;
// using static VivoxPlayground.MyVivoxFramework.Util.AsyncUtil;
// namespace VivoxPlayground.MyVivoxFramework.Util
// {
//     public static class VivoxUtil
//     {
//
//     #region Debug
//
//         public static void VivoxLog(string msg)
//         {
//             Debug.Log( "<color=green>VivoxVoice: </color>: " + msg );
//         }
//
//         public static void VivoxLogError(string msg)
//         {
//             Debug.LogError( "<color=red>VivoxVoice: </color>: " + msg );
//         }
//
//     #endregion
//
//     #region ServiceStateManagement
//
//         /// <summary>
//         ///     Manually and Independently Init the Unity Services for prototype use.
//         /// </summary>
//         public static async Task InitUnityService()
//         {
//             await UnityServices.InitializeAsync();
//             await AuthenticationService.Instance.SignInAnonymouslyAsync();
//         }
//
//         /// <summary>
//         ///     Init Vivox Services assuming that <see cref="AuthenticationService" /> is already been signed in to.
//         /// </summary>
//         public static async Task InitVivoxService()
//         {
//             VivoxLog( $"{nameof(InitVivoxService)} Start" );
//             var service = await WaitNull( () => VivoxService.Instance );
//             service.Initialize();
//             VivoxLog( $"{nameof(InitVivoxService)} Complete." );
//         }
//
//         /// <summary>
//         ///     Restart the client from service and Get it.
//         /// </summary>
//         public static Client RestartGetVivoxClient()
//         {
//             var client = VivoxService.Instance.Client;
//             client.Uninitialize();
//             client.Initialize();
//             VivoxLog( $"{nameof(RestartGetVivoxClient)} Complete." );
//
//             return client;
//         }
//
//         /// <summary>
//         ///     Clean and Uninit the client
//         /// </summary>
//         /// <param name="client"></param>
//         public static void TerminateVivoxClient(Client client)
//         {
//             Client.Cleanup();
//
//             if (client != null)
//             {
//                 client.Uninitialize();
//             }
//         }
//
//     #endregion
//
//     #region Connection
//
//         /// <summary>
//         ///     Login and set the transmission mode
//         /// </summary>
//         /// <param name="client"></param>
//         /// <param name="account"></param>
//         /// <param name="subscription_configs"></param>
//         /// <returns></returns>
//         public static async Task<(bool success, ILoginSession loginSession)> LoginVivox(
//             Client client,
//             Account account,
//             (
//                 SubscriptionMode subscription_mode,
//                 IReadOnlyHashSet<AccountId> presence_subscriptions,
//                 IReadOnlyHashSet<AccountId> blocked_presence_subscriptions,
//                 IReadOnlyHashSet<AccountId> allowed_presence_subscriptions
//                 )
//                 subscription_configs,
//             TransmissionMode transmission_mode)
//         {
//             var login_session = client.GetLoginSession( account );
//             login_session.BeginLogin( login_session.GetLoginToken(), SubscriptionMode.Accept, subscription_configs.presence_subscriptions, null, null,
//                 ar =>
//                 {
//                     try
//                     {
//                         login_session.EndLogin( ar );
//                     }
//                     catch (Exception e)
//                     {
//                         VivoxLogError( e.ToString() );
//                     }
//                 } );
//
//             var success = await WaitForLogin( login_session );
//
//             if (!success)
//             {
//                 VivoxLog( "This manager cannot login." );
//
//                 return (false, null);
//             }
//
//             login_session.SetTransmissionMode( transmission_mode );
//             VivoxLog( $"{nameof(LoginVivox)} Complete." );
//
//             return (true, login_session);
//         }
//
//         public static async
//             Task<(bool success, IChannelSession channelSession, IParticipant me)>
//             EstablishChannelConnection(
//                 ILoginSession loginSession,
//                 ChannelId channel,
//                 ( bool connectAudio, bool connectText, bool switchTransmission) join_channel_params)
//         {
//             (bool connect_audio, bool connect_text, bool switch_transmission) = join_channel_params;
//             var channel_session = loginSession.GetChannelSession( channel );
//             var ar = channel_session.BeginConnect( connect_audio, connect_text, switch_transmission, channel_session.GetConnectToken(),
//                 ar =>
//                 {
//                     try
//                     {
//                         channel_session.EndConnect( ar );
//                     }
//                     catch (Exception e)
//                     {
//                         VivoxLogError( $"Could not connect to voice channel: {e.Message}" );
//                     }
//                 } );
//
//             var success = await WaitForChannelJoin( channel_session );
//
//             if (!success)
//             {
//                 VivoxLogError( "Cannot join this channel." );
//
//                 return (false, channel_session, null);
//             }
//
//             VivoxLog( $"Establish Connection of channel {{{channel.Name}: {channel.Type}}} Complete." );
//
//             return (true, channel_session, channel_session.Participants[loginSession.LoginSessionId.ToString()]);
//         }
//
//     #endregion
//
//     #region ChannelManagement
//
//         public static void MuteParticipantForAll(IParticipant participant, bool setMuted)
//         {
//             participant.SetIsMuteForAll( setMuted,
//                 ar =>
//                 {
//                     if (ar.IsCompleted)
//                     {
//                         VivoxLog( $"Mute participant:{participant.Account.Name} in channel{participant.ParentChannelSession.Channel.Name} for all participants" );
//                     }
//                     else
//                     {
//                         VivoxLogError( $"Mute not complete!" );
//                     }
//                 } );
//         }
//
//     #endregion
//
//     #region Sync
//
//     #endregion
//
//     #region AsyncCheckWait
//
//         public static async Task<bool> WaitForLogin(ILoginSession loginSession, int check_interval = 1000)
//         {
//             while (loginSession.State != LoginState.LoggedIn)
//             {
//                 if (loginSession.State != LoginState.LoggingIn)
//                 {
//                     return false;
//                 }
//
//                 await Task.Delay( check_interval );
//             }
//
//             return true;
//         }
//
//         public static async Task<bool> WaitForChannelJoin(IChannelSession channelSession, int check_interval = 1000)
//         {
//             while (channelSession.ChannelState != ConnectionState.Connected)
//             {
//                 if (channelSession.ChannelState != ConnectionState.Connecting)
//                 {
//                     return false;
//                 }
//
//                 await Task.Delay( check_interval );
//             }
//
//             return true;
//         }
//
//     #endregion
//
//     #region Token
//
//         /// <summary>
//         ///     <para>Used to sign and gen token with key on the server.</para>
//         ///     <para>The code itself is publicly visible and the key is confidential stored on the server</para>
//         /// </summary>
//         public static class TokenGenerator
//         {
//             public static string vxGenerateToken(string key, string issuer, int exp, string vxa, int vxi, string f, string t)
//             {
//                 var claims = new Claims
//                 {
//                     iss = issuer,
//                     exp = exp,
//                     vxa = vxa,
//                     vxi = vxi,
//                     f = f,
//                     t = t
//                 };
//
//                 List<string> segments = new List<string>();
//                 // Header is static - base64url encoded {} - Can also be defined as a constant "e30"
//                 var header = Base64URLEncode( "{}" );
//                 segments.Add( header );
//
//                 // Encode payload
//                 var claimsString = JsonUtility.ToJson( claims );
//                 var encodedClaims = Base64URLEncode( claimsString );
//
//                 // Join segments to prepare for signing
//                 segments.Add( encodedClaims );
//                 string toSign = string.Join( ".", segments );
//
//                 // Sign token with key and SHA256
//                 string sig = SHA256Hash( key, toSign );
//                 segments.Add( sig );
//
//                 // Join all 3 parts of token with . and return
//                 string token = string.Join( ".", segments );
//
//                 return token;
//             }
//
//             static string Base64URLEncode(string plainText)
//             {
//                 var plainTextBytes = Encoding.UTF8.GetBytes( plainText );
//                 // Remove padding at the end
//                 var encodedString = Convert.ToBase64String( plainTextBytes ).TrimEnd( '=' );
//                 // Substitute URL-safe characters
//                 string urlEncoded = encodedString.Replace( "+", "-" ).Replace( "/", "_" );
//
//                 return urlEncoded;
//             }
//
//             static string SHA256Hash(string secret, string message)
//             {
//                 var encoding = new ASCIIEncoding();
//                 byte[] keyByte = encoding.GetBytes( secret );
//                 byte[] messageBytes = encoding.GetBytes( message );
//
//                 using (var hmacsha256 = new HMACSHA256( keyByte ))
//                 {
//                     byte[] hashmessage = hmacsha256.ComputeHash( messageBytes );
//                     var hashString = Convert.ToBase64String( hashmessage ).TrimEnd( '=' );
//                     string urlEncoded = hashString.Replace( "+", "-" ).Replace( "/", "_" );
//
//                     return urlEncoded;
//                 }
//             }
//
//             [Serializable]
//             public class Claims
//             {
//                 public string iss;
//                 public int exp { get; set; }
//                 public string vxa;
//                 public int vxi;
//                 public string f;
//                 public string t;
//                 public string sub;
//             }
//         }
//
//     #endregion
//
//     }
// }
