using System;
namespace PrototypePackages.MainThreadExecutor.Scripts
{
public class UpdateTask
{
	Func<bool> OnCheck;
	Action OnDone;
	public bool Check()
	{
		if (OnCheck())
		{
			OnDone();
			return true;
		}
		return false;
	}
	public UpdateTask(Func<bool> OnCheck, Action OnDone)
	{
		this.OnCheck = OnCheck;
		this.OnDone = OnDone;
	}
}
}