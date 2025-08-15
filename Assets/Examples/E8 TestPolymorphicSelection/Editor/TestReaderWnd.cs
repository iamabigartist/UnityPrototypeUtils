using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEditor;
using static System.Threading.Tasks.Task;
namespace Examples.E8_TestPolymorphicSelection.Editor
{
public class TestReaderWnd : EditorWindow
{
	[MenuItem("Examples/E8 TestPolymorphicSelection/TestReaderWnd")]
	static void Init()
	{
		var wnd = GetWindow<TestReaderWnd>();
		wnd.titleContent = new("Test Reader");
		wnd.Show();
		Test1();
	}

	static List<string> GetJsonCuts()
	{
		// 生成一个较为复杂的Json字符串
		var json = @"
{
  ""user"": {
    ""id"": 12345,
    ""name"": ""John Doe"",
    ""email"": ""johndoe@example.com"",
    ""profile"": {
      ""age"": 30,
      ""gender"": ""male"",
      ""location"": {
        ""city"": ""New York"",
        ""state"": ""NY"",
        ""coordinates"": {
          ""latitude"": 40.712776,
          ""longitude"": -74.005974
        }
      },
      ""preferences"": {
        ""language"": ""en"",
        ""notifications"": {
          ""email"": true,
          ""sms"": false,
          ""push"": true
        }
      }
    }
  },
  ""orderHistory"": [
    {
      ""orderId"": ""A123"",
      ""date"": ""2024-12-20T15:30:00Z"",
      ""items"": [
        {
          ""productId"": 1001,
          ""productName"": ""Wireless Mouse"",
          ""quantity"": 2,
          ""price"": 25.99
        },
        {
          ""productId"": 1002,
          ""productName"": ""Keyboard"",
          ""quantity"": 1,
          ""price"": 45.49
        }
      ],
      ""totalAmount"": 97.47,
      ""status"": ""Delivered""
    },
    {
      ""orderId"": ""B456"",
      ""date"": ""2024-11-15T10:45:00Z"",
      ""items"": [
        {
          ""productId"": 2001,
          ""productName"": ""Bluetooth Speaker"",
          ""quantity"": 1,
          ""price"": 59.99
        }
      ],
      ""totalAmount"": 59.99,
      ""status"": ""Pending""
    }
  ],
  ""subscription"": {
    ""type"": ""Premium"",
    ""startDate"": ""2024-01-01"",
    ""renewalDate"": ""2025-01-01"",
    ""status"": ""Active""
  }
}
";
		// 随机按顺序截取为不同长度片段列表
		var fragments = new List<string>();
		var random = new Random();
		var jsonLength = json.Length;
		var start = 0;
		while (start < jsonLength)
		{
			var end = start + random.Next(1, jsonLength - start);
			fragments.Add(json.Substring(start, end - start));
			start = end;
		}
		return fragments;
	}

	static async void ContinueEnqueue(QueueStringReader queue_reader, List<string> jsonCuts)
	{
		foreach (var cut in jsonCuts)
		{
			queue_reader.Enqueue(cut);
			await Delay(1000);
		}
	}

	static async void Test1()
	{
		var jsonCuts = GetJsonCuts();
		var queue_reader = new QueueStringReader();
		var json_reader = new JsonTextReader(queue_reader);
		ContinueEnqueue(queue_reader, jsonCuts);
		while (true)
		{
			json_reader.Read();
		}
	}
}
}