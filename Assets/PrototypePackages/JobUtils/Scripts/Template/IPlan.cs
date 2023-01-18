using Unity.Jobs;
namespace PrototypePackages.JobUtils.Template
{
	public interface IPlan
	{
		public static JobHandle Plan<T>(T job, JobHandle deps = default)
			where T : struct, IPlan, IJob
		{
			return job.Schedule(deps);
		}
	}
}