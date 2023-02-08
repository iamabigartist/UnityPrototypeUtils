namespace PrototypePackages.Backpack.Model
{
	/// <remarks>
	///     <para>The state and type of a item are different.</para>
	///     <para>Type usually won't change during a item's lifetime.</para>
	///     <para>State changes frequently, like durability or an item's own pack. </para>
	/// </remarks>
	public abstract class BaseItem
	{
		public abstract BaseItemType BaseType { get; }
		public abstract BaseItemState BaseState { get; }
		public BaseItemLocation BaseLocation;
	}

	public class Item<TItemType, TItemState> : BaseItem
		where TItemType : BaseItemType
		where TItemState : BaseItemState
	{
		public readonly TItemType Type;
		public TItemState State;
		public override BaseItemType BaseType => Type;
		public override BaseItemState BaseState => State;
		public Item(TItemType Type, TItemState State)
		{
			this.Type = Type;
			this.State = State;
		}
	}


}