using System;
using System.Collections.Generic;
using System.Text;
using PatcherLib.Utilities;
using PatcherLib.Datatypes;
using PatcherLib;

namespace FFTPatcher.Datatypes
{
    public class AllPropositions : PatchableFile, IXmlDigest, IGenerateCodes
    {
        public const int NumPropositions = 96;
        public const int propLength = 23;

        public AllPropositions( IList<byte> bytes )
            : this( bytes, null as AllPropositions )
        {
        }

        public AllPropositions( IList<byte> bytes, IList<byte> defaultBytes )
            : this( bytes, new AllPropositions( defaultBytes ) )
        {
        }

        public IList<UInt16> Prices { get; private set; }
        public IList<UInt16> Bonuses { get; private set; }
        public AllPropositions Default { get; private set; }

        public AllPropositions( IList<byte> bytes, AllPropositions defaults )
        {
            Default = defaults;
            var names = FFTPatch.Context == Context.US_PSP ? PSPResources.Lists.Propositions : PSXResources.Lists.Propositions;

            List<Proposition> props = new List<Proposition>();
            if (defaults != null)
            {
                for (int i = 0; i < NumPropositions; i++)
                {
                    props.Add( new Proposition( names[i], bytes.Sub( i * propLength, (i + 1) * propLength - 1 ), defaults.Propositions[i] ) );
                }
            }
            else
            {
                for (int i = 0; i < NumPropositions; i++)
                {
                    props.Add( new Proposition( names[i], bytes.Sub( i * propLength, (i + 1) * propLength - 1 ) ) );
                }
            }

            Prices = new UInt16[9];
            for (int i = 0; i < 9; i++)
            {
                var b = bytes.Sub( propLength * NumPropositions + i * 2, propLength * NumPropositions + i * 2 + 1 );
                Prices[i] = Utilities.BytesToUShort( b[0], b[1] );
            }

            unknownBytes = bytes.Sub( propLength * NumPropositions + 18, 0xA33 ).ToArray();

            Bonuses = new UInt16[16];
            for (int i = 0; i < 16; i++)
            {
                var b = bytes.Sub( 0xA34 + i * 2, 0xA34 + i * 2 + 1 );
                Bonuses[i] = Utilities.BytesToUShort( b[0], b[1] );
            }

            Propositions = props.AsReadOnly();
        }

        private IList<byte> unknownBytes;

        public IList<Proposition> Propositions
        {
            get;
            private set;
        }

        public string GetCodeHeader( PatcherLib.Datatypes.Context context )
        {
            return context == Context.US_PSP ? "_C0 Propositions" : "\"Propositions";

        }

        public IList<string> GenerateCodes( PatcherLib.Datatypes.Context context )
        {
            if (context == Context.US_PSP)
            {
                return Codes.GenerateCodes( Context.US_PSP, PSPResources.Binaries.Propositions, this.ToByteArray(), 0x2E9634 );
            }
            else
            {
                return Codes.GenerateCodes( Context.US_PSX, PSXResources.Binaries.Propositions, this.ToByteArray(), 0x9D380, Codes.CodeEnabledOnlyWhen.World );
            }
        }

        public void WriteXmlDigest( System.Xml.XmlWriter writer )
        {
            throw new NotImplementedException();
        }

        public byte[] ToByteArray()
        {
            List<byte> result = new List<byte>( 23 * 96 );
            Propositions.ForEach( p => result.AddRange( p.ToByteArray() ) );
            Prices.ForEach( p => result.AddRange( p.ToBytes() ) );
            result.AddRange( unknownBytes );
            Bonuses.ForEach( b => result.AddRange( b.ToBytes() ) );
            return result.ToArray();
        }

        public override IList<PatchedByteArray> GetPatches( PatcherLib.Datatypes.Context context )
        {
            var result = new List<PatchedByteArray>( 2 );
            var bytes = ToByteArray();
            if (context == Context.US_PSX)
            {
                result.Add( PatcherLib.Iso.PsxIso.Propositions.GetPatchedByteArray( bytes ) );
            }
            else if (context == Context.US_PSP)
            {
                PatcherLib.Iso.PspIso.Propositions.ForEach( kp => result.Add( kp.GetPatchedByteArray( bytes ) ) );
            }

            return result;
        }

        public override bool HasChanged
        {
            get
            {
                return
                    Default != null &&
                    (!Utilities.CompareArrays( Prices, Default.Prices ) ||
                    !Utilities.CompareArrays( Bonuses, Default.Bonuses ) ||
                    !Propositions.TrueForAll( p => !p.HasChanged ));
            }
        }
    }
}
