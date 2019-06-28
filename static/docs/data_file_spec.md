# Data file format spec
Every data file (ie for TreasureClasses or MagicPrefixes) must follow a set of requirements so that code can be reliably reused for serializing and deserializing the data.

Requirements:
* Each data file must have a header row
* Each header row label must be in all lowercase, no spaces
* Each column must be tab delimited
* Strings intended to be parsed and displayed in-game must have underscores denoting spaces
* All other strings must have no spaces
* All strings outside the header rows must be PascalCase
* All numbers must be whole integers

An easier way to edit these files would be to use Excel's Import and Export functions. This way, data is more intuitively represented on-screen while you edit, and you don't have to do any tab-counting or ridiculous stuff like that.