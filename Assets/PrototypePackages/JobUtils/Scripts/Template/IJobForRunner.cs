namespace PrototypePackages.JobUtils.Template
{
	public interface IJobForRunner
	{
		(int ExecuteLen, int InnerLoopBatchCount) ScheduleParam { get; }
		void Execute(int i);
	}
}