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

namespace Zeiss.FirstImportFormat;

public class SimpleTxtImportParser : IImportParser
{
    public async Task<ImportData> ParseAsync(IImportGroup importGroup, IParseContext context,
        CancellationToken cancellationToken = new CancellationToken())
    {
        // Check file extension.
        if (!importGroup.PrimaryFile.HasExtension(".txt"))
            throw new ImportDataException(
                $"Expected Simple Txt file to import, but received '{importGroup.PrimaryFile.Name}'");

        await using var stream = importGroup.PrimaryFile.GetDataStream();
        using var reader = new StreamReader(stream);

        // Check file content match with SimpleTxt format.
        var firstLine = reader.ReadLine();
        if (firstLine == null || !firstLine.StartsWith("#Header"))
            throw new ImportDataException("Expected Simple Txt file to start contain '#Header'");

        var root = new InspectionPlanPart(importGroup.PrimaryFile.BaseName);
        var measurement = root.AddMeasurement();

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

            switch (attribute)
            {
                case "Date":
                    measurement.SetAttribute(4, value);
                    break;
                case "Text":
                    measurement.SetAttribute(9, value);
                    break;
            }

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
            measuredValue.SetAttribute(1, double.Parse(value));
        }

        return new ImportData(root);
    }
}
