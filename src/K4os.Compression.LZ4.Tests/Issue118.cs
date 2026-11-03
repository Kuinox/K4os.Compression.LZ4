using System;
using System.Linq;
using Xunit;

namespace K4os.Compression.LZ4.Tests
{
	// Enforce32 is process-wide, so do not run these cases alongside other tests.
	[CollectionDefinition("Issue118", DisableParallelization = true)]
	public class Issue118Collection { }

	[Collection("Issue118")]
	public class Issue118
	{
		[Theory]
		[InlineData(false, false)]
		[InlineData(false, true)]
		[InlineData(true, false)]
		[InlineData(true, true)]
		public void RejectsZeroOffset(bool enforce32, bool shortcut)
		{
			// On a 32-bit process the 64-bit decoder cannot be selected.
			if (!enforce32 && IntPtr.Size != 8) return;
			byte[] compressed = shortcut
				? new byte[] { 0x13, 1, 0, 0, 0xf0, 17 }
					.Concat(Enumerable.Range(0, 32).Select(i => (byte)i)).ToArray()
				: new byte[] { 0x13, 1, 0, 0, 0x50, 2, 3, 4, 5, 6 };
			var target = Enumerable.Repeat((byte)0xa5, shortcut ? 40 : 13).ToArray();
			LZ4Codec.Enforce32 = enforce32;
			try
			{
				Assert.Equal(-1, LZ4Codec.Decode(compressed, 0, compressed.Length, target, 0, target.Length));
			}
			finally
			{
				LZ4Codec.Enforce32 = false;
			}
		}
	}
}
