using System;
using System.Collections.Concurrent;
namespace PrototypePackages.MiscUtils
{
public static class FuncCache<T>
{
	static ConcurrentDictionary<Delegate, object> NamedFuncDict = new();
	static TRes GetOrExecute<TArgs, TRes>(Func<TRes> genRes, Delegate func, TArgs args)
	{
		var argsResDict = (ConcurrentDictionary<TArgs, TRes>)NamedFuncDict.GetOrAdd(func, _ => new ConcurrentDictionary<TArgs, TRes>());
		var res = argsResDict.GetOrAdd(args, _ => genRes());
		return res;
	}
	public static T0 Cached<T0>(Func<T0> func) =>
		GetOrExecute(() => func(), func, 0);
	public static T0 Cached<T0, T1>(Func<T1, T0> func, T1 arg1) =>
		GetOrExecute(() => func(arg1), func, arg1);
	public static T0 Cached<T0, T1, T2>(Func<T1, T2, T0> func, T1 arg1, T2 arg2) =>
		GetOrExecute(() => func(arg1, arg2), func, (arg1, arg2));
	public static T0 Cached<T0, T1, T2, T3>(Func<T1, T2, T3, T0> func, T1 arg1, T2 arg2, T3 arg3) =>
		GetOrExecute(() => func(arg1, arg2, arg3), func, (arg1, arg2, arg3));
	public static T0 Cached<T0, T1, T2, T3, T4>(Func<T1, T2, T3, T4, T0> func, T1 arg1, T2 arg2, T3 arg3, T4 arg4) =>
		GetOrExecute(() => func(arg1, arg2, arg3, arg4), func, (arg1, arg2, arg3, arg4));
	public static T0 Cached<T0, T1, T2, T3, T4, T5>(Func<T1, T2, T3, T4, T5, T0> func, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5) =>
		GetOrExecute(() => func(arg1, arg2, arg3, arg4, arg5), func, (arg1, arg2, arg3, arg4, arg5));
	public static T0 Cached<T0, T1, T2, T3, T4, T5, T6>(Func<T1, T2, T3, T4, T5, T6, T0> func, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6) =>
		GetOrExecute(() => func(arg1, arg2, arg3, arg4, arg5, arg6), func, (arg1, arg2, arg3, arg4, arg5, arg6));
	public static T0 Cached<T0, T1, T2, T3, T4, T5, T6, T7>(Func<T1, T2, T3, T4, T5, T6, T7, T0> func, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7) =>
		GetOrExecute(() => func(arg1, arg2, arg3, arg4, arg5, arg6, arg7), func, (arg1, arg2, arg3, arg4, arg5, arg6, arg7));
}
}