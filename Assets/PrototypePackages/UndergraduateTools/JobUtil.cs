using System.Threading.Tasks;
using Unity.Collections;
using Unity.Jobs;
using Unity.Jobs.LowLevel.Unsafe;
using static Unity.Mathematics.math;
namespace PrototypePackages.UndergraduateTools
{
public static class JobUtil
{
	public static async Task WaitJob(JobHandle jh)
	{
		while (!jh.IsCompleted) { await Task.Yield(); }
		jh.Complete();
	}
	
	public static JobHandle CombineDependencies(params JobHandle[] jhs)
	{
		var jhs_array = new NativeArray<JobHandle>(jhs, Allocator.TempJob);
		var jh = JobHandle.CombineDependencies(jhs_array);
		jhs_array.Dispose();
		return jh;
	}

	public static int Batch_WorkerThreadPow(int length, float y)
	{
		return (int)(length / pow(JobsUtility.JobWorkerCount, y));
	}
}
}