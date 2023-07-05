namespace PrototypePackages.UndergraduateTools.UpdaterUtil
{
public struct NoUpdate : IUpdateTaskState
{
	public IUpdateTaskState Config(long total_points, int update_interval = 10) => this;
	public void Process(string process) { }
	public void Progress(int cur_points) { }
	public void Done() { }
}
}