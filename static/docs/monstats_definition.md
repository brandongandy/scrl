These are the columns in use by the MonStats.txt file, and the definition of the data that is meant to live inside each column.

## ID Settings

**nameid** - this column is the pointer by which other files and functions should refer to this entry in the file. Each nameid should be unique.

**baseid** - this column is the nameid of the "base" unit for this monster type (for example, there may by 5 different types of Orc creatures, but they are all descendants of "orc1" in the file). The baseid will be responsible for assigning behaviors to units and controlling how they react to other units nearby.

**nextinclass** - this is the nameid of the next unit in the chain. For example, if a shrine is accessed that upgrades all units in an area, then the nextinclass will be access to convert an orc1 into an orc2.

## Display Settings
**namestr** - a case-sensitive, user-friendly string used for display on-screen

## Metadata
**classification** - the group ID of the "supergroup" this monster belongs to -- for example, a Small Orc, Large Orc, and Orc Shaman are all in the group of "orc". This is used for special modifiers, such as extra damage to monster classes.

**aspect1** - the first aspect held by this monster. This alone does not affect monster behavior, but see the Religion page for more details. This is a foreign key to the Aspect.txt file, where aspect info is held.

**aspect2** - the second aspect held by this monster.

**aspect3** - the third aspect held by this monster.

**deity** - which Deity is worshiped by this monster. Foreign key to Deity.txt.

## Level Settings
**level** - used to determine the stats this monster has

## Group and Minion Settings
**minion1/minion2** - these contain pointers to the nameid of the minions that spawn around this monster when it spawns. If an Orc Chieftan has 5 Small Orcs as minions, then minion1 would be set to orc1, and minparty/maxparty would be 5.

**minparty/maxparty** - how many minions are spawned together with this unit.

**mingroup/maxgroup** - how many of this base unit will spawn together.

## Stat Settings
**maxhealth** - the maximum health this monster will spawn with

**maxenergy** - the maximum energy this monster will spawn with

**con/wis/dex** - the constitution, wisdom, and dexterity this monster has.

## Treasure Class Settings
**treasureclass1** - the treasureclass used by this unit as a Normal monster

**treasureclass2** - the treasureclass used by this unit as a Champion monster

**treasureclass3** - the treasureclass used by this unit as a unique or superunique monster

**treasureclass4** the treasureclass used by this unit as a Quest monster (like a boss)