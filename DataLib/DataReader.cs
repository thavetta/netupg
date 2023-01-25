using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DataLib
{
    public class DataReader
    {
        public DataReader()
        {
            GetNetworkAdaptorName();
            Debug.WriteLine(m_szAdaptorName);
        }

        private const int MAX_ADAPTER_NAME_LENGTH = 256;
        [DllImport("iphlpapi.dll", SetLastError = true)]
        private static extern int GetAdaptersInfo(byte[] abyAdaptor, ref int nSize);

        // ...
        private static string m_szAdaptorName = "DM9CE1";

        // ...
        private void GetNetworkAdaptorName()
        {
            // The initial call is to determine the size of the memory required. This will fail
            // with the error code "111" which is defined by MSDN to be "ERROR_BUFFER_OVERFLOW".
            // The structure size should be 640 bytes per adaptor.
            int nSize = 0;
            int nReturn = GetAdaptersInfo(null, ref nSize);

            // Allocate memory and get data
            byte[] abyAdapatorInfo = new byte[nSize];
            nReturn = GetAdaptersInfo(abyAdapatorInfo, ref nSize);
            if (nReturn == 0)
            {
                // Find the start and end bytes of the name in the returned structure
                int nStartNamePos = 8;
                int nEndNamePos = 8;
                while ((abyAdapatorInfo[nEndNamePos] != 0) &&
                       ((nEndNamePos - nStartNamePos) < MAX_ADAPTER_NAME_LENGTH))
                {
                    // Another character in the name
                    nEndNamePos++;
                }

                // Convert the name from a byte array into a string
                m_szAdaptorName = Encoding.UTF8.GetString(
                    abyAdapatorInfo, nStartNamePos, (nEndNamePos - nStartNamePos));
            }
            else
            {
                // Failed? Use a hard-coded network adaptor name.
                m_szAdaptorName = "DM9CE1";
            }
        }

        public IEnumerable<DataPoint> GetData()
        {
            yield return new DataPoint(0, 0);
            yield return new DataPoint(1, 1);
            yield return new DataPoint(2, 4);
            yield return new DataPoint(3, 9);
            yield return new DataPoint(4, 16);
            yield return new DataPoint(5, 25);
            yield return new DataPoint(6, 36);
            yield return new DataPoint(7, 49);
            yield return new DataPoint(8, 64);
            yield return new DataPoint(9, 81);
            yield return new DataPoint(10, 100);
        }
    }
}
