namespace PrototypePackages.MathematicsUtils.ValueTransformers
{
	public interface IValueTransformer<T>
	{
		void Transform(in T original_value, out T transformed_value);
	}
}