using Unity.Jobs;
namespace PrototypePackages.JobUtils.Template
{
	public interface IPlanFor
	{
		int length { get; }
		int batch { get; }

		public static JobHandle Plan<T>(T job_for, JobHandle deps = default)
			where T : struct, IPlanFor, IJobFor
		{
			return job_for.ScheduleParallel(job_for.length, job_for.batch, deps);
		}
		public static JobHandle PlanByRef<T>(ref T job_for, JobHandle Deps)
			where T : struct, IPlanFor, IJobFor
		{
			return job_for.ScheduleParallelByRef(job_for.length, job_for.batch, Deps);
		}
	}
}