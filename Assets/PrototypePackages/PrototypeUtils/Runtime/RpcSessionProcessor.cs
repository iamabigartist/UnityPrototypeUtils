#if NET_CODE
using System;
using System.Collections.Generic;
using Unity.Collections;
using Unity.NetCode;
using UnityEngine;
namespace VivoxPlayground.MyVivoxFramework.Util
{
    public interface IRpcSessionMessage
    {
        FixedString64Bytes session_id { get; }
    }
    public interface IRpcRequest : IRpcSessionMessage { }
    public interface IRpcResponse : IRpcSessionMessage { }

    /// <summary>
    ///     <para>The <see cref="IRpcCommand" /> itself can't handle the Http-Request-like conversation.</para>
    ///     <para>Therefore, this is the class which is used to imitate the pattern of HttpRequest.</para>
    ///     <para>
    ///         Without thread safety check, this processor store the <see cref="request_buffer" /> id and callback raised
    ///         by external systems; When external systems receive one or more responses, they should also send them to this
    ///         processor, it will try to correspond each response to one request in <see cref="request_buffer" />, execute
    ///         the callback and remove it.
    ///     </para>
    /// </summary>
    public class RpcSessionProcessor : SingletonMonoBehaviour<RpcSessionProcessor>
    {
        Dictionary<string, Action<IRpcResponse>> request_buffer;
        protected override void Awake()
        {
            base.Awake();
            request_buffer = new();
        }
        public void BufferRequest(IRpcRequest request, Action<IRpcResponse> receive_callback)
        {
            request_buffer[request.session_id.Value] = receive_callback;
        }
        public void EmitResponse(IRpcResponse response)
        {
            if (!request_buffer.ContainsKey( response.session_id.Value ))
            {
                Debug.Log(
                    "Response Request not found.\n" +
                    $"requests: {request_buffer.ToListString()}\n" +
                    $"cur_response_session_id: {response.session_id}" );

                return;
            }

            Debug.Log( "Response Request found" );

            request_buffer[response.session_id.Value]?.Invoke( response );
            request_buffer.Remove( response.session_id.Value );
        }
    }
#endif
