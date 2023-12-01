using System.Text;

namespace AdventOfCode.Core.Puzzles.Day16;

public class Decoder
{
    private static readonly Dictionary<char, string> HexCharacterToBinary = new Dictionary<char, string>
    {
        {'0', "0000"},
        {'1', "0001"},
        {'2', "0010"},
        {'3', "0011"},
        {'4', "0100"},
        {'5', "0101"},
        {'6', "0110"},
        {'7', "0111"},
        {'8', "1000"},
        {'9', "1001"},
        {'a', "1010"},
        {'b', "1011"},
        {'c', "1100"},
        {'d', "1101"},
        {'e', "1110"},
        {'f', "1111"}
    };

    private readonly char[] _transmission;

    public Decoder(string transmission) =>
        _transmission = transmission
            .ToCharArray()
            .Select(x => HexCharacterToBinary[char.ToLower(x)])
            .ToArray()
            .Aggregate((i, j) => i + j)
            .ToCharArray(); 
       

    public Package DecodeTransMission() =>
        DecodeTransmissionToPackage(_transmission, 0).Package;

    private (int Position, Package Package) DecodeTransmissionToPackage(char[] transmission, int position)
    {
        position = ReadNumberFromMessage(transmission, position, 3, out var version);
        position = ReadNumberFromMessage(transmission, position, 3, out var typeId);

        if ((OperationType)typeId == OperationType.Literal)
        {
            position = ParseLiteralPackage(transmission, position, out var literalValue);
            return (position, new LiteralValuePackage(version, OperationType.Literal, literalValue));
        }
        
        position = ReadNumberFromMessage(transmission, position, 1, out var lengthTypeId);
        var subPackages = new List<Package>();
        
        if (lengthTypeId == 0)
        {
            var handledResult = HandleLengthBasedPackage(transmission, position);
            position = handledResult.Position;
            subPackages.AddRange(handledResult.SubPackages);
        }
        else
        {
            var handledResult = HandleNumberBasedPackage(transmission, position);
            position = handledResult.Position;
            subPackages.AddRange(handledResult.SubPackages);
        }

        return (position, new OperatorPackage(version, (OperationType)typeId, subPackages));
    }

    private int ReadNumberFromMessage(char[] message, int position, int length, out int value)
    {
        value = Convert.ToInt32(new string(new ArraySegment<char>(message, position, length)), 2);
        position += length;
        return position;
    }
    
    private int ParseLiteralPackage(char[] data, int index, out long literalValue)
    {
        var sb = new StringBuilder();
        bool keepReading = true;
        while (keepReading)
        {
            keepReading = data[index] == '1';
            index++;

            sb.Append(new string(new ArraySegment<char>(data, index, 4)));
            index += 4;
        }

        literalValue = Convert.ToInt64(sb.ToString(), 2);
        return index;
    }
    
    private (int Position, List<Package> SubPackages) HandleLengthBasedPackage(char[] transmission, int position)
    {
        var subPackages = new List<Package>();
        position = ReadNumberFromMessage(transmission, position, 15, out var subPacketLength);
        int start = position;
        while (position - start < subPacketLength)
        {
            var subDecodingResult = DecodeTransmissionToPackage(transmission, position);
            position = subDecodingResult.Position;
            subPackages.Add(subDecodingResult.Package);
        }

        return (position, subPackages);
    }
    
    private (int Position, List<Package> SubPackages) HandleNumberBasedPackage(char[] transmission, int position)
    {
        var subPackages = new List<Package>();
        position = ReadNumberFromMessage(transmission, position, 11, out var totalSubPackets);
        for (int i = 0; i < totalSubPackets; i++)
        {
            var subDecodingResult = DecodeTransmissionToPackage(transmission, position);
            position = subDecodingResult.Position;
            subPackages.Add(subDecodingResult.Package);
        }

        return (position, subPackages);
    }
}