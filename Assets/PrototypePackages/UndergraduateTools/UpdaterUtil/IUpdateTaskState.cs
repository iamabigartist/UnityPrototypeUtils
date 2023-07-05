namespace PrototypePackages.UndergraduateTools.UpdaterUtil
{

public interface IUpdateTaskState
{
	IUpdateTaskState Config(long total_points, int update_interval = 1);
	void Process(string process);
	void Progress(int cur_points);
	void Done();
}
}