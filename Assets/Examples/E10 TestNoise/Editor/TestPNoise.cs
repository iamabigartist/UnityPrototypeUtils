﻿using PrototypePackages.JobUtils.Template;
using PrototypePackages.MathematicsUtils.Index;
using PrototypePackages.MathematicsUtils.Noise;
using PrototypePackages.MathematicsUtils.Vector;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using static PrototypePackages.DisplayUtils.EasyDisplayTexture;
using static Unity.Mathematics.math;
using static VolumeMegaStructure.Util.JobSystem.ScheduleUtils;
namespace Examples.E10_TestNoise.Editor
{
	public struct TestPNoiseJob : IJobFor, IPlanFor
	{
		public int length => array.Length;
		public int batch => length / GetBatchSize_WorkerThreadCount(length, 1.5f);

		PlaneHeightNoise<PerlinNoise_f2> pnoise;
		Index2D c;
		[WriteOnly] NativeArray<float> array;
		public void Execute(int i)
		{
			c.To2D(i, out var x, out var y);
			pnoise.Sample(new(x, y), out var height);
			array[i] = height;
		}

		public TestPNoiseJob(PlaneHeightNoise<PerlinNoise_f2> pnoise, int2 size, NativeArray<float> array)
		{
			this.pnoise = pnoise;
			c = new(size);
			this.array = array;
		}
	}

	public class TestPNoise : EditorWindow
	{
		[MenuItem("Labs/Examples.E10_TestNoise.Editor/TestPNoise")]
		static void ShowWindow()
		{
			var window = GetWindow<TestPNoise>();
			window.titleContent = new("TestPNoise");
			window.Show();
		}

		Texture2D DisplayTexture;
		void Generate()
		{
			var array = new NativeArray<float>(DisplayTexture.Size().area(), Allocator.TempJob);
			IPlanFor.Plan(new TestPNoiseJob(new(0.01f, 0, 2, 1, new(2, 1, 100)), DisplayTexture.Size(), array)).Complete();
			DisplayTexture.SetTextureSlice(array, 0);
			DisplayTexture.SetTextureSlice(array, 1);
			DisplayTexture.SetTextureSlice(array, 2);
			DisplayTexture.SetAlpha(1.0f);
			DisplayTexture.Apply();
			array.Dispose();
		}

		void OnEnable()
		{
			Debug.Log("OnEnable");
			const int exp = 10;
			int len = (int)pow(2, exp);
			CreateDisplayTexture(new(len, len), out DisplayTexture);
			Generate();
		}

		void CreateGUI()
		{
			Debug.Log("CreateGUI");
			var scroll_view = new ScrollView(ScrollViewMode.VerticalAndHorizontal) {};
			var container = new VisualElement
			{
				style =
				{
					width = 512,
					height = 512
				}
			};
			container.Add(new Image { image = DisplayTexture });
			scroll_view.Add(container);
			rootVisualElement.Add(scroll_view);
		}


	}
}