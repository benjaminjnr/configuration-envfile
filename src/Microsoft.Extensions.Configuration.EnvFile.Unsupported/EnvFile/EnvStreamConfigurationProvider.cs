namespace Microsoft.Extensions.Configuration.EnvFile;

public class EnvStreamConfigurationProvider : StreamConfigurationProvider
{
	public EnvStreamConfigurationProvider(EnvStreamConfigurationSource source)
		: base(source)
	{
	}

	public static IDictionary<string, string> Read(Stream stream)
	{
		Dictionary<string, string> dictionary = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
		using StreamReader streamReader = new StreamReader(stream);

		//string text = string.Empty; //holder variable used by ini file reader

		while (streamReader.Peek() != -1)
		{
			var rawLine = streamReader.ReadLine();
			var trimmedLine = rawLine.Trim();

			if (string.IsNullOrWhiteSpace(trimmedLine) || trimmedLine[0] == ';' || trimmedLine[0] == '#' || trimmedLine[0] == '/')
			{
				continue;
			}

			////we are commenting out this block because env files will not have section headers like ini files
			//if (trimmedLine[0] == '[' && trimmedLine[trimmedLine.Length - 1] == ']')
			//{
			//	text = trimmedLine.Substring(1, trimmedLine.Length - 2) + ConfigurationPath.KeyDelimiter;
			//	continue;
			//}

			int num = trimmedLine.IndexOf('=');
			if (num < 0)
			{
				throw new FormatException($"UnrecognizedLineFormat {rawLine}");
			}

			var configKey = trimmedLine.Substring(0, num).Trim();
			//this is what is different than ini because of the keys; AddEnvironment and AddKeyPerFile have to use __ as delimeters and replace with ':' to be acceptable by the OS
			configKey = configKey.Replace("__", ":");

			if (dictionary.ContainsKey(configKey))
			{
				throw new FormatException($"Error_KeyIsDuplicated {configKey}");
			}

			var configValue = trimmedLine.Substring(num + 1).Trim();

			//this block strips the quotes from quoted values
			if (configValue.Length > 1 && configValue[0] == '"' && configValue[configValue.Length - 1] == '"')
			{
				configValue = configValue.Substring(1, configValue.Length - 2);
			}

			dictionary[configKey] = configValue;
		}
		return dictionary;
	}

	public override void Load(Stream stream)
	{
		base.Data = Read(stream);
	}
}
