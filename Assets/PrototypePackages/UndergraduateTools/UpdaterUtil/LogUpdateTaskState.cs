using UnityEngine;
namespace PrototypePackages.UndergraduateTools.UpdaterUtil
{
public struct LogUpdateTaskState : IUpdateTaskState
{
	string task_name;
	string process;
	ProgressUpdater progress_updater;
	public LogUpdateTaskState(string task_name)
	{
		this.task_name = task_name;
		progress_updater = default;
		process = "Start";
	}
	public IUpdateTaskState Config(long total_points, int update_interval = 10)
	{
		progress_updater = new(total_points, update_interval);
		return this;
	}
	public void Process(string process)
	{
		Debug.Log($"{task_name}:\n\t{process}");
		this.process = process;
	}
	public void Progress(int cur_points)
	{
		if (progress_updater.UpdateProgress(cur_points))
		{
			Debug.Log($"{task_name}:\n\t{process}:\t{progress_updater.progress}%");
		}
	}
	public void Done()
	{
		Debug.Log($"{task_name} Done");
	}
}
}