namespace PrototypePackages.MathematicsUtils.Noise
{
	public interface ICoherentNoise<TInput,TOutput>
	{
		void Sample(in TInput input, out TOutput output);
	}
}