The text files in this Content folder are intended to be tab-delimited values
with column headers denoting what each column is meant to be. Each file holds
information on a different portion of game data - armor, weapons, monsters,
affixes, etc.

They should be read in at startup and accessed as needed using the associated
entity's Clone() method, which will return a new copy of the entity with all
of the pertinent randomized data re-randomized.

For easy file editing, it may be preferable to copy the contents of the files
into Excel (or something like it), edit there, then export to back to tab-
delimited file.