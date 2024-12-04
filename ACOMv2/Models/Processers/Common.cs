using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACOMv2.Models.Processers;

class Common
{
}
public class ByteSplitter
{
    public static List<byte[]> Split(List<byte> data, List<byte> delimiter)
    {
        List<byte[]> result = new List<byte[]>();
        int start = 0;
        int index;

        while ((index = IndexOf(data, delimiter, start)) != -1)
        {
            int length = index - start;
            byte[] chunk = data.GetRange(start, length).ToArray();
            result.Add(chunk);
            start = index + delimiter.Count;
        }

        // Add the last chunk
        if (start < data.Count)
        {
            byte[] chunk = data.GetRange(start, data.Count - start).ToArray();
            result.Add(chunk);
        }

        return result;
    }
    public static List<byte[]> Split(byte[] data, List<byte> delimiter)
    {
        List<byte[]> result = new List<byte[]>();
        int start = 0;
        int index;

        while ((index = IndexOf(data, delimiter, start)) != -1)
        {
            int length = index - start;
            byte[] chunk = new byte[length];
            Array.Copy(data, start, chunk, 0, length);
            result.Add(chunk);
            start = index + delimiter.Count;
        }

        // Add the last chunk
        if (start < data.Length)
        {
            byte[] chunk = new byte[data.Length - start];
            Array.Copy(data, start, chunk, 0, chunk.Length);
            result.Add(chunk);
        }

        return result;
    }

    private static int IndexOf(byte[] data, List<byte> delimiter, int start)
    {
        for (int i = start; i <= data.Length - delimiter.Count; i++)
        {
            bool match = true;
            for (int j = 0; j < delimiter.Count; j++)
            {
                if (data[i + j] != delimiter[j])
                {
                    match = false;
                    break;
                }
            }
            if (match)
            {
                return i;
            }
        }
        return -1;
    }
    private static int IndexOf(List<byte> data, List<byte> delimiter, int start)
    {
        for (int i = start; i <= data.Count - delimiter.Count; i++)
        {
            bool match = true;
            for (int j = 0; j < delimiter.Count; j++)
            {
                if (data[i + j] != delimiter[j])
                {
                    match = false;
                    break;
                }
            }
            if (match)
            {
                return i;
            }
        }
        return -1;
    }
}
