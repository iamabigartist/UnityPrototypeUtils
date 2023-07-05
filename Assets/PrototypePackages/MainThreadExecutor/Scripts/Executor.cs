using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
namespace PrototypePackages.MainThreadExecutor.Scripts
{
public abstract class Executor
{
	ConcurrentQueue<Action> request_queue = new();
	List<UpdateTask> task_list = new();
	int max_request_per_frame = 20;
	TimeSpan task_check_interval = TimeSpan.FromSeconds(0.1);
	DateTime last_task_check = DateTime.Now;

	public Executor Config(int max_request_per_frame, TimeSpan task_check_interval)
	{
		this.max_request_per_frame = max_request_per_frame;
		this.task_check_interval = task_check_interval;
		return this;
	}

	public void AddRequest(Action action) => request_queue.Enqueue(action);
	public void AddTask(UpdateTask task) => request_queue.Enqueue(() => task_list.Add(task));
	void ExecuteRequests()
	{
		for (int i = 0; i < max_request_per_frame; i++)
		{
			if (request_queue.TryDequeue(out var action)) { action(); }
			else { break; }
		}
	}
	void CheckTasks()
	{
		if (DateTime.Now - last_task_check < task_check_interval) { return; }
		foreach (var task in task_list.Where(task => task.Check()).ToArray())
		{
			task_list.Remove(task);
		}
	}
	protected void UpdaterUpdate()
	{
		ExecuteRequests();
		CheckTasks();
	}
}
}