using System;
namespace PrototypePackages.RuleMachine
{

	public abstract class Machine<TEntryParam>
		where TEntryParam : class
	{
		protected abstract Action[] CreateRules();

		protected Machine()
		{
			Rules = CreateRules();
		}

		protected TEntryParam CurEntryParam;
		Action[] Rules;
		public void Run(TEntryParam EntryParam)
		{
			CurEntryParam = EntryParam;
			foreach (var rule in Rules)
			{
				rule();
			}
		}
	}
}