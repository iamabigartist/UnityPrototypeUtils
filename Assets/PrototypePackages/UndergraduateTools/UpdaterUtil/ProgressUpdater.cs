using Unity.Mathematics;
namespace PrototypePackages.UndergraduateTools.UpdaterUtil
{
public struct ProgressUpdater
{
	public long total_points;
	public int update_interval;
	public int progress;
	public ProgressUpdater(long total_points, int update_interval = 10)
	{
		this.total_points = total_points;
		this.update_interval = update_interval;
		progress = 0;
	}
	public bool UpdateProgress(long cur_points)
	{
		int cur_progress = (int)(100 * cur_points / total_points);
		if (math.abs(cur_progress - progress) >= update_interval)
		{
			progress = cur_progress;
			return true;
		}
		return false;
	}
}
}