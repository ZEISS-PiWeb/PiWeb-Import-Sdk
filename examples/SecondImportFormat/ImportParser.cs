#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2024                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Zeiss.PiWeb.Sdk.Import.ImportData;
using Zeiss.PiWeb.Sdk.Import.ImportFiles;
using Zeiss.PiWeb.Sdk.Import.Modules.ImportFormat;

namespace Zeiss.SecondImportFormat;

public class ImportParser : IImportParser
{
    public async Task<ImportData> ParseAsync(IImportGroup importGroup, IParseContext context,
        CancellationToken cancellationToken = new CancellationToken())
    {
        // Create root part and measurement.
        var root = new InspectionPlanPart(importGroup.PrimaryFile.BaseName);
        var measurement = root.AddMeasurement();

        // Create reader for import file.
        await using var stream = importGroup.PrimaryFile.GetDataStream();
        using var reader = new StreamReader(stream);

        string? line;

        // Parse header attributes.
        while ((line = reader.ReadLine()) != null)
        {
            if (string.IsNullOrEmpty(line))
                continue;

            if (line.StartsWith("#Characteristic"))
                break;

            var rowItems = line.Split(": ");
            var attribute = rowItems[0].Trim();
            var value = rowItems[1].Trim();

            measurement.SetVariable(attribute,value);
        }

        // Parse measured value for each characteristic.
        while ((line = reader.ReadLine()) != null)
        {
            if (string.IsNullOrEmpty(line))
                continue;

            var rowItems = line.Split(',');
            var characteristicName = rowItems[0].Trim();
            var value = rowItems[1].Trim();

            var characteristic = root.AddCharacteristic(characteristicName);
            var measuredValue = measurement.AddMeasuredValue(characteristic);
            measuredValue.SetVariable("Value",double.Parse(value));
        }

        // Add additional data.
        foreach (var rawData in importGroup.AdditionalFiles)
            measurement.AddAdditionalData(rawData.Name, rawData.GetDataStream());

        return new ImportData(root);
    }
}
