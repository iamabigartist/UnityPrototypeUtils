using Unity.Burst;
using Unity.Jobs;
namespace PrototypePackages.JobUtils.Template
{
	[BurstCompile(
		DisableSafetyChecks = true, OptimizeFor = OptimizeFor.Performance,
		CompileSynchronously = true)]
	public struct JobFor_Optimized<TJobForRunner> : IJobFor
		where TJobForRunner : struct, IJobForRunner
	{
		public TJobForRunner runner;
		public void Execute(int i)
		{
			runner.Execute(i);
		}
		public static void Plan(TJobForRunner Runner, ref JobHandle Deps)
		{
			var job = new JobFor_Optimized<TJobForRunner>() { runner = Runner };
			var (execute_len, inner_loop_batch_count) = Runner.ScheduleParam;
			Deps = job.ScheduleParallel(execute_len, inner_loop_batch_count, Deps);
		}

		public static void PlanByRef(TJobForRunner Runner, ref JobHandle Deps, out JobFor_Optimized<TJobForRunner> job)
		{
			job = new() { runner = Runner };
			var (execute_len, inner_loop_batch_count) = Runner.ScheduleParam;
			Deps = job.ScheduleParallelByRef(execute_len, inner_loop_batch_count, Deps);
		}
	}
	
}