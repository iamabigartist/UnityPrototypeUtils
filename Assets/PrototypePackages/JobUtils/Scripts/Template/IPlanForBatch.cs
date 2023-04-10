using Unity.Jobs;
namespace PrototypePackages.JobUtils.Template
{
public interface IPlanForBatch
{
	int length { get; }
	int batch { get; }

	public static JobHandle Plan<T>(T job_for, JobHandle deps = default)
		where T : struct, IPlanForBatch, IJobParallelForBatch
	{
		return job_for.ScheduleBatch(job_for.length, job_for.batch, deps);
	}
	public static JobHandle PlanByRef<T>(ref T job_for, JobHandle Deps)
		where T : struct, IPlanFor, IJobParallelForBatch
	{
		return job_for.ScheduleBatchByRef(job_for.length, job_for.batch, Deps);
	}
}
}