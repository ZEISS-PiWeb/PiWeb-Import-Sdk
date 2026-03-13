#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2024                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Zeiss.PiWeb.Sdk.Import.ImportData;
using Zeiss.PiWeb.Sdk.Import.ImportFiles;
using Zeiss.PiWeb.Sdk.Import.Modules.ImportFormat;

namespace SimpleTxtPlugin;

public class ImportParser : IImportParser
{
    public async Task<ImportData> ParseAsync(
    IImportGroup importGroup,
    IParseContext context,
    CancellationToken cancellationToken)
    {
        // Create root part and measurement.
        var root = new InspectionPlanPart("Root");

        var circle = root.AddCharacteristic("Circle");
        circle.SetVariable("XPosition", 200);
        circle.SetVariable("YPosition", 15);
        circle.SetVariable("ZPosition", 0);

        var circleX = circle.AddCharacteristic("Circle.X");
        circleX.SetVariable("Nominal", 200);

        var circleY = circle.AddCharacteristic("Circle.Y");
        circleY.SetVariable("Nominal", 20);

        var circleZ = circle.AddCharacteristic("Circle.Z");
        circleZ.SetVariable("Nominal", 0);

        var measurement = root.AddMeasurement();
        measurement.AddMeasuredValue(circleX).SetVariable("Value", 205);
        measurement.AddMeasuredValue(circleY).SetVariable("Value", 14);
        measurement.AddMeasuredValue(circleZ).SetVariable("Value", 1);

        //var measurement = root.AddMeasurement();

        // Create reader for import file.
        await using var stream = importGroup.PrimaryFile.GetDataStream();
        using var reader = new StreamReader(stream, Encoding.UTF8);

        string? line;

        // Parse header attributes.
        while ((line = await reader.ReadLineAsync(cancellationToken)) != null)
        {
            if (string.IsNullOrEmpty(line))
                continue;

            if (line.StartsWith("#Characteristic"))
                break;

            var rowItems = line.Split(":", 2);
            if (rowItems.Length < 2)
                continue;

            var attribute = rowItems[0].Trim();
            var value = rowItems[1].Trim();

            switch (attribute)
            {
                case "Date":
                    var isValidDate = DateTime.TryParse(
                        value,
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.AssumeLocal,
                        out var dateTimeValue);

                    if (isValidDate)
                        measurement.SetAttribute(4, dateTimeValue);
                    break;

                case "Text":
                    measurement.SetAttribute(9, value);
                    break;
            }
        }

        // Parse measured value for each characteristic.
        while ((line = await reader.ReadLineAsync(cancellationToken)) != null)
        {
            var rowItems = line.Split(',', 2);

            if (rowItems.Length < 1)
                continue;
            var characteristicName = rowItems[0].Trim();
            if (string.IsNullOrWhiteSpace(characteristicName))
                continue;
            var characteristic = root.AddCharacteristic(characteristicName);

            if (rowItems.Length < 2)
                continue;
            var value = rowItems[1].Trim();
            if (!double.TryParse(value, CultureInfo.InvariantCulture, out var doubleValue))
                continue;

            var measuredValue = measurement.AddMeasuredValue(characteristic);
            measuredValue.SetAttribute(1, doubleValue);
            measuredValue.SetVariable("value", 24.3);
        }

        return new ImportData(root);
    }
}
