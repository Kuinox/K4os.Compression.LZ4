# Plank.K4os.LZ4

This package is based on K4os.Compression.LZ4 1.3.8, with the namespace renamed
to `Plank.K4os.LZ4` and a fix that rejects invalid zero match offsets during
block decoding ([K4os issue #118](https://github.com/MiloszKrajewski/K4os.Compression.LZ4/issues/118)).
It can be used alongside the original K4os.Compression.LZ4 package.

K4os.Compression.LZ4 is by Milosz Krajewski and is distributed under the MIT license.
