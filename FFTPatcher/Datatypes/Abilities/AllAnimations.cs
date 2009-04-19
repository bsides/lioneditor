using System;
using System.Collections.Generic;
using System.Text;

using PatcherLib.Datatypes;
using PatcherLib.Utilities;

namespace FFTPatcher.Datatypes
{
    public class AllAnimations : PatchableFile, IXmlDigest, ISupportDefault<AllAnimations>
    {
        private Animation[] animations;
        private IList<Animation> readOnlyAnimations;

        public Animation this[int i]
        {
            get { return animations[i]; }
        }

        public IList<Animation> Animations { get { return readOnlyAnimations; } }

        private AllAnimations(IList<byte> bytes)
        {
            animations = new Animation[512];
            for (int i = 0; i < 512; i++)
            {
                animations[i] = new Animation(bytes.Sub(i * 3, i * 3 + 3 - 1));
            }
            readOnlyAnimations = animations.AsReadOnly();
        }

        public AllAnimations(Context context, IList<byte> bytes, IList<byte> defaultBytes)
        {
            IList<string> names = context == Context.US_PSP ? AllAbilities.PSPNames : AllAbilities.PSXNames;

            animations = new Animation[512];
            for (int i = 0; i < 512; i++)
            {
                animations[i] = new Animation(
                    (ushort)i,
                    names[i],
                    bytes.Sub(i * 3, i * 3 + 3 - 1),
                    defaultBytes.Sub(i * 3, i * 3 + 3 - 1));
            }
            Default = new AllAnimations(defaultBytes);
            readOnlyAnimations = animations.AsReadOnly();
        }

        public byte[] ToByteArray()
        {
            byte[] result = new byte[512 * 3];
            for (int i = 0; i < 512; i++)
            {
                this[i].ToByteArray().CopyTo(result, i * 3);
            }
            return result;
        }

        public AllAnimations Default
        {
            get; private set;
        }

        public override IList<PatcherLib.Datatypes.PatchedByteArray> GetPatches(PatcherLib.Datatypes.Context context)
        {
            throw new NotImplementedException();
        }

        public override bool HasChanged
        {
            get { return animations.Exists(a => a.HasChanged); }
        }

        public void WriteXml(System.Xml.XmlWriter writer)
        {
            throw new NotImplementedException();
        }

    }
}
