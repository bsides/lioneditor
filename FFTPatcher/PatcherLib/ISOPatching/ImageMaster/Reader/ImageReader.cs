using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace ImageMaster
{
    public abstract class ImageReader
    {
        public const int SECTOR_SIZE = 2048;
        public const int VIRTUAL_SECTOR_SIZE = 512;
        public const int PRIMARY_VOLUME_SECTOR = 16;
        public const int BOOTRECORD_VOLUME_SECTOR = 17;

        internal ImageRecord _rootDirectory;
        internal int CurrentBlockSize = SECTOR_SIZE;
        internal bool bootIsDefined;
        internal string _volumename;

        private string _filename;
        private IStream _stream;
        private long _archiveSize;
        private bool _cancel;

        public IStream BaseStream
        {
            get { return _stream; }
        }

        public ImageRecord RootDirectory
        {
            get { return _rootDirectory; }
        }

        public Collection<ImageRecord> Items
        {
            get { return _rootDirectory.SubItems; }
        }

        public ImageRecord GetItem( string name )
        {
            foreach ( ImageRecord record in _rootDirectory.SubItems )
            {
                if ( record.Name == name )
                    return record;
            }
            return null;
        }

        public ImageRecord GetItemPath( string relativePath )
        {
            string[] segments = relativePath.Split( new char[] { '/', '\\' }, 2, StringSplitOptions.RemoveEmptyEntries );
            ImageRecord child = GetItem( segments[0] );
            if ( segments.Length == 1 )
            {
                return child;
            }
            else
            {
                return child.GetItemPath( segments[1] );
            }
        }

        public ImageRecord GetItemRecursive( string name )
        {
            foreach ( ImageRecord item in _rootDirectory.SubItems )
            {
                if ( item.Name == name )
                {
                    return item;
                }
                else
                {
                    ImageRecord recurse = item.GetItemRecursive( name );
                    if ( recurse != null )
                    {
                        return recurse;
                    }
                }
            }

            return null;
        }

        public bool IsBootDisc
        {
            get { return bootIsDefined; }
        }

        public long Size
        {
            get { return _archiveSize; }
        }

        public bool CancelExtraction
        {
            get { return _cancel; }
            set { _cancel = value; }
        }

        public string FileName
        {
            get { return _filename; }
        }

        public string VolumeName
        {
            get { return _volumename; }
        }

        public void Initialize(string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException();
            if (!File.Exists(path))
                throw new FileNotFoundException();
            _filename = path;
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            Initialize(ImapiV2.Interop.AStream.ToIStream(fs));
        }

        public void Initialize(Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException();
            Initialize(ImapiV2.Interop.AStream.ToIStream(stream));
        }

        public void Initialize(IStream stream)
        {
            if (stream == null)
                throw new ArgumentNullException();
            _stream = stream;
            IntPtr size = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(long)));
            Marshal.WriteInt64(size, 0);
            _stream.Seek(0, (int)SeekOrigin.End, size);
            _archiveSize = Marshal.ReadInt64(size);
            Marshal.FreeHGlobal(size);
        }

        public virtual bool Open()
        {
            if (this.BaseStream == null)
                throw new InvalidOperationException();
            return true;
        }

        public virtual void Close()
        {
            bootIsDefined = false;
            _cancel = false;
            _archiveSize = 0;
            _filename = string.Empty;
            _volumename = string.Empty;

            if (_stream != null)
            {
                ((ImapiV2.Interop.AStream)_stream).Close();
            }
        }

        protected virtual int ReadStream(byte[] buffer, int size)
        {
            int processedSize = 0;
            while (size != 0)
            {
                int curSize = (size < CurrentBlockSize) ? size : CurrentBlockSize;
                int bytesRead = 0;
                IntPtr br = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(int)));
                Marshal.WriteInt32(br, 0);
                this.BaseStream.Read(buffer, curSize, br);
                bytesRead = Marshal.ReadInt32(br);
                Marshal.FreeHGlobal(br);
                processedSize += bytesRead;
                size -= bytesRead;
                if (bytesRead == 0)
                    return processedSize;
            }
            return processedSize;
        }
    }
}