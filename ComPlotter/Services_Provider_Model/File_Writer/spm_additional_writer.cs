using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Threading;

namespace ComPlotter
{
    class AdditionalWriter: AbstractFileWriter
    {
        public AdditionalWriter()
        {
            m_receivedData = new List<int>();
            m_blockSize = 8;
            m_blockIndex = 0;
        }

        public override void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (m_fileStream != null)
                ProcessReceivedByte( (byte)e.NewItems[0] );
        }

        public override void SetReceivedBlockSize(int _blockSize)
        {
            m_receivedData.Clear();
            m_blockSize = _blockSize;
        }

        void ProcessReceivedByte( byte _received )
        {
            m_receivedData.Add(_received);

            if (m_receivedData.Count == m_blockSize)
            {
                string fileData = Properties.Resources.TitleForBlockStart;

                //Forming the title for block
                int index = fileData.IndexOf('%');
                fileData = fileData.Insert(index, m_blockIndex.ToString());
                fileData = fileData.Replace('%', ' ');
                fileData += '\n';

                //Forming body data
                fileData += Properties.Resources.TitleAverageBlockValue;
                fileData += m_receivedData.Average().ToString();
                fileData += '\n';

                fileData += Properties.Resources.TitleZeroBitsCount;
                int zeroBitsCount = 0;
                foreach (byte element in m_receivedData)
                {
                    zeroBitsCount += Convert.ToString(element, 2).ToCharArray().Count(c => c == '0');
                }

                fileData += zeroBitsCount.ToString();
                fileData += '\n';

                fileData += Properties.Resources.TitleOneBitsCount;

                int oneBitsCount = 0;
                foreach (byte element in m_receivedData)
                {
                    oneBitsCount += Convert.ToString(element, 2).ToCharArray().Count(c => c == '1');
                }

                fileData += oneBitsCount.ToString();
                fileData += '\n';

                fileData += Properties.Resources.TitleForBlockEnd;
                fileData += '\n';

                m_fileStream.WriteLine(fileData);
                m_blockIndex++;
                m_receivedData.Clear();
            }
        }

        List<int> m_receivedData;
        int m_blockIndex;
        int m_blockSize;
    }
}
