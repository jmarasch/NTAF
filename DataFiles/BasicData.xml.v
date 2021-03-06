﻿<NTDataFileExt>
  <InternalFile DataFileName="BasicData" IDPreFix="BSDS" FilePassword="">
    <DateCreated>0001-01-01T00:00:00</DateCreated>
    <DateLastEdited>0001-01-01T00:00:00</DateLastEdited>
  </InternalFile>
  <Collectors d2p1:type="ArchetypeCollector" xmlns:d2p1="http://www.w3.org/2001/XMLSchema-instance">
    <Objects>
      <anyType d2p1:type="Archetype" ID="BSDS175F654B" Name="Green" BaseType="Green" />
      <anyType d2p1:type="Archetype" ID="BSDS372AACE7" Name="Heavy" BaseType="Heavy" />
      <anyType d2p1:type="Archetype" ID="BSDS2CDC0E34" Name="Leader" BaseType="Leader" />
      <anyType d2p1:type="Archetype" ID="BSDS159919F9" Name="Member" BaseType="Member" />
      <anyType d2p1:type="Archetype" ID="BSDS2549C0A4" Name="Psychic" BaseType="Psychic" />
      <anyType d2p1:type="Archetype" ID="BSDS6EBD1680" Name="Special" BaseType="Special" />
      <anyType d2p1:type="Archetype" ID="BSDS8FDAC4BB" Name="Vet" BaseType="Vet" />
    </Objects>
  </Collectors>
  <Collectors d2p1:type="BaseUnits" xmlns:d2p1="http://www.w3.org/2001/XMLSchema-instance">
    <Objects>
      <anyType d2p1:type="BaseUnit" ID="BSDS6C46BF71" Name="Leader" Movement="6" HitPoints="15" HandToHand="8" ActionPoints="4" PsyPoints="0" RangedWeapons="8" Might="8" Toughness="8" Agility="8" Willpower="8" HorrorFactor="1" CostMod="0">
        <archetype ID="BSDS2CDC0E34" Name="Leader" BaseType="Leader" />
        <BaseRace ID="BSDS9A264EA9" Name="Human" species="Human" />
        <Description>Human Leader Unit</Description>
        <Special />
        <WPermissions />
        <SPermissions />
        <PPermissions />
        <Weapons />
        <SkillsAndAbilities />
        <Psys />
        <Items />
      </anyType>
      <anyType d2p1:type="BaseUnit" ID="BSDSDC936F78" Name="Green" Movement="3" HitPoints="5" HandToHand="4" ActionPoints="1" PsyPoints="0" RangedWeapons="4" Might="4" Toughness="4" Agility="4" Willpower="4" HorrorFactor="0" CostMod="0">
        <archetype ID="BSDS175F654B" Name="Green" BaseType="Green" />
        <BaseRace ID="BSDS9A264EA9" Name="Human" species="Human" />
        <Description />
        <Special />
        <WPermissions />
        <SPermissions />
        <PPermissions />
        <Weapons />
        <SkillsAndAbilities />
        <Psys />
        <Items />
      </anyType>
      <anyType d2p1:type="BaseUnit" ID="BSDSFB323414" Name="Heavy" Movement="4" HitPoints="10" HandToHand="3" ActionPoints="2" PsyPoints="0" RangedWeapons="6" Might="6" Toughness="6" Agility="4" Willpower="5" HorrorFactor="1" CostMod="0">
        <archetype ID="BSDS372AACE7" Name="Heavy" BaseType="Heavy" />
        <BaseRace ID="BSDS9A264EA9" Name="Human" species="Human" />
        <Description />
        <Special />
        <WPermissions />
        <SPermissions />
        <PPermissions />
        <Weapons />
        <SkillsAndAbilities />
        <Psys />
        <Items />
      </anyType>
      <anyType d2p1:type="BaseUnit" ID="BSDS26A810BE" Name="Psychic" Movement="4" HitPoints="4" HandToHand="2" ActionPoints="2" PsyPoints="6" RangedWeapons="2" Might="2" Toughness="4" Agility="4" Willpower="8" HorrorFactor="1" CostMod="0">
        <archetype ID="BSDS2549C0A4" Name="Psychic" BaseType="Psychic" />
        <BaseRace ID="BSDS9A264EA9" Name="Human" species="Human" />
        <Description />
        <Special />
        <WPermissions />
        <SPermissions />
        <PPermissions />
        <Weapons />
        <SkillsAndAbilities />
        <Psys />
        <Items />
      </anyType>
      <anyType d2p1:type="BaseUnit" ID="BSDS46DAD7C1" Name="Member" Movement="4" HitPoints="8" HandToHand="5" ActionPoints="2" PsyPoints="0" RangedWeapons="5" Might="5" Toughness="5" Agility="5" Willpower="5" HorrorFactor="1" CostMod="0">
        <archetype ID="BSDS159919F9" Name="Member" BaseType="Member" />
        <BaseRace ID="BSDS9A264EA9" Name="Human" species="Human" />
        <Description />
        <Special />
        <WPermissions />
        <SPermissions />
        <PPermissions />
        <Weapons />
        <SkillsAndAbilities />
        <Psys />
        <Items />
      </anyType>
      <anyType d2p1:type="BaseUnit" ID="BSDS2578607F" Name="Vet" Movement="5" HitPoints="10" HandToHand="7" ActionPoints="3" PsyPoints="0" RangedWeapons="7" Might="7" Toughness="7" Agility="7" Willpower="7" HorrorFactor="1" CostMod="0">
        <archetype ID="BSDS8FDAC4BB" Name="Vet" BaseType="Vet" />
        <BaseRace ID="BSDS9A264EA9" Name="Human" species="Human" />
        <Description />
        <Special />
        <WPermissions />
        <SPermissions />
        <PPermissions />
        <Weapons />
        <SkillsAndAbilities />
        <Psys />
        <Items />
      </anyType>
    </Objects>
  </Collectors>
  <Collectors d2p1:type="ItemCollector" xmlns:d2p1="http://www.w3.org/2001/XMLSchema-instance">
    <Objects>
      <anyType d2p1:type="Item" ID="BSDS737C5AAC" Name="Bionic Leg" Cost="500" TypesCanUse="All" SpeciesCanEquip="All">
        <ModifiesStats>
          <StatsMod StatToMod="Agility" Modifier="1" />
        </ModifiesStats>
        <Description />
      </anyType>
      <anyType d2p1:type="Item" ID="BSDSAA2B231C" Name="Cybernetic Leg" Cost="500" TypesCanUse="All" SpeciesCanEquip="All">
        <ModifiesStats>
          <StatsMod StatToMod="Agility" Modifier="-1" />
        </ModifiesStats>
        <Description />
      </anyType>
      <anyType d2p1:type="Item" ID="BSDS677EB7E5" Name="Wooden Leg" Cost="5" TypesCanUse="All" SpeciesCanEquip="All">
        <ModifiesStats>
          <StatsMod StatToMod="Movement" Modifier="-2" />
        </ModifiesStats>
        <Description>Gives the model the ability to walk again</Description>
      </anyType>
      <anyType d2p1:type="Item" ID="BSDS9A9F72D0" Name="Bionic Arm" Cost="550" TypesCanUse="All" SpeciesCanEquip="All">
        <ModifiesStats>
          <StatsMod StatToMod="HandToHand" Modifier="1" />
        </ModifiesStats>
        <Description />
      </anyType>
      <anyType d2p1:type="Item" ID="BSDS1A871879" Name="Cybernetic Arm" Cost="250" TypesCanUse="All" SpeciesCanEquip="All">
        <ModifiesStats>
          <StatsMod StatToMod="Might" Modifier="1" />
        </ModifiesStats>
        <Description>Requires a cybernetic hand</Description>
      </anyType>
      <anyType d2p1:type="Item" ID="BSDS89BB4EC5" Name="Cybernetic Hand" Cost="450" TypesCanUse="All" SpeciesCanEquip="All">
        <ModifiesStats />
        <Description />
      </anyType>
      <anyType d2p1:type="Item" ID="BSDSE467093E" Name="Wooden Hand" Cost="5" TypesCanUse="All" SpeciesCanEquip="All">
        <ModifiesStats />
        <Description>Counts as a dub in hand to hand</Description>
      </anyType>
      <anyType d2p1:type="Item" ID="BSDSD52C0F22" Name="Bionic Eye" Cost="1000" TypesCanUse="All" SpeciesCanEquip="All">
        <ModifiesStats>
          <StatsMod StatToMod="RangedWeapons" Modifier="1" />
        </ModifiesStats>
        <Description />
      </anyType>
      <anyType d2p1:type="Item" ID="BSDSCB8A1489" Name="Cybernetic Eye" Cost="1000" TypesCanUse="All" SpeciesCanEquip="All">
        <ModifiesStats>
          <StatsMod StatToMod="RangedWeapons" Modifier="1" />
        </ModifiesStats>
        <Description>+5" to any ranged weapon ranges if a cybernetic hand has been attached</Description>
      </anyType>
      <anyType d2p1:type="Item" ID="BSDSB106D655" Name="Glass Eye" Cost="100" TypesCanUse="All" SpeciesCanEquip="All">
        <ModifiesStats>
          <StatsMod StatToMod="HorrorFactor" Modifier="4" />
        </ModifiesStats>
        <Description>You can give someone the evil eye</Description>
      </anyType>
      <anyType d2p1:type="Item" ID="BSDSD2BF9FF8" Name="Bionic Ear" Cost="250" TypesCanUse="All" SpeciesCanEquip="All">
        <ModifiesStats />
        <Description>Cannot be snuck up upon</Description>
      </anyType>
      <anyType d2p1:type="Item" ID="BSDSF36ED935" Name="Scope" Cost="150" TypesCanUse="All" SpeciesCanEquip="All">
        <ModifiesStats />
        <Description>For evey 150 pq spent the weapon gets +1 to hit, cannot be attached to a pistol</Description>
      </anyType>
      <anyType d2p1:type="Item" ID="BSDS989D6CB1" Name="Hellfire Trigger" Cost="500" TypesCanUse="All" SpeciesCanEquip="All">
        <ModifiesStats />
        <Description>Give a semi automatic weapon +2 shots</Description>
      </anyType>
      <anyType d2p1:type="Item" ID="BSDSDE51AA53" Name="Double Trigger" Cost="150" TypesCanUse="All" SpeciesCanEquip="All">
        <ModifiesStats />
        <Description>Give +1 shot to a shot gun</Description>
      </anyType>
      <anyType d2p1:type="Item" ID="BSDSEE734280" Name="Auto Responce Trigger" Cost="255" TypesCanUse="All" SpeciesCanEquip="All">
        <ModifiesStats />
        <Description>x2 shots per trigger pull, only for semi automatic weapons</Description>
      </anyType>
      <anyType d2p1:type="Item" ID="BSDSC4D90305" Name="Extended Amo Clip" Cost="500" TypesCanUse="All" SpeciesCanEquip="All">
        <ModifiesStats />
        <Description>+2 to weapons SIOR roll</Description>
      </anyType>
      <anyType d2p1:type="Item" ID="BSDS83997E85" Name="Bayonet" Cost="25" TypesCanUse="All" SpeciesCanEquip="All">
        <ModifiesStats />
        <Description>Allows rifel to double as pole arm type weapon</Description>
      </anyType>
      <anyType d2p1:type="Item" ID="BSDSC9715096" Name="Laser Sight" Cost="100" TypesCanUse="All" SpeciesCanEquip="All">
        <ModifiesStats />
        <Description>+1 to hit for pistol and rifles</Description>
      </anyType>
      <anyType d2p1:type="Item" ID="BSDS8CE06E28" Name="Sniper Barrel" Cost="220" TypesCanUse="All" SpeciesCanEquip="All">
        <ModifiesStats />
        <Description>+1 to RW, +2 to RW with a scope</Description>
      </anyType>
      <anyType d2p1:type="Item" ID="BSDS1C28FCD0" Name="Comfort Grip" Cost="20" TypesCanUse="All" SpeciesCanEquip="All">
        <ModifiesStats />
        <Description>it just needs to feel right</Description>
      </anyType>
      <anyType d2p1:type="Item" ID="BSDS2CB6F4F9" Name="Beer" Cost="10" TypesCanUse="All" SpeciesCanEquip="All">
        <ModifiesStats />
        <Description>Heals 5 HP</Description>
      </anyType>
      <anyType d2p1:type="Item" ID="BSDSBC76B3B0" Name="Booze" Cost="40" TypesCanUse="All" SpeciesCanEquip="All">
        <ModifiesStats />
        <Description>Heals 20 HP</Description>
      </anyType>
      <anyType d2p1:type="Item" ID="BSDS69D6D6DA" Name="Morphine" Cost="45" TypesCanUse="All" SpeciesCanEquip="All">
        <ModifiesStats />
        <Description>+D4 Toughness for D6 rounds</Description>
      </anyType>
      <anyType d2p1:type="Item" ID="BSDSB20F5638" Name="Ephedrine" Cost="60" TypesCanUse="All" SpeciesCanEquip="All">
        <ModifiesStats />
        <Description>+2 movement, +1 attack for d6 rounds</Description>
      </anyType>
      <anyType d2p1:type="Item" ID="BSDS9BD816F5" Name="Ammo Reload" Cost="25" TypesCanUse="All" SpeciesCanEquip="All">
        <ModifiesStats />
        <Description>If you fail an SIOR you may reload the weapon next turn and continue as though the roll never happened, one use item, when its used its gone till you buy more</Description>
      </anyType>
      <anyType d2p1:type="Item" ID="BSDS9A070C57" Name="Flashlight" Cost="10" TypesCanUse="All" SpeciesCanEquip="All">
        <ModifiesStats />
        <Description>Makes negatives to shooting in the dark null</Description>
      </anyType>
      <anyType d2p1:type="Item" ID="BSDSB0D7499C" Name="Smelling Salt" Cost="15" TypesCanUse="All" SpeciesCanEquip="All">
        <ModifiesStats />
        <Description>A pungent salt that instantly wakes up an unconscious model</Description>
      </anyType>
      <anyType d2p1:type="Item" ID="BSDSA4FF7337" Name="Duct Tape" Cost="200" TypesCanUse="All" SpeciesCanEquip="All">
        <ModifiesStats />
        <Description>Allows you to combine any two weapons in to one weapon, one use item, when its used its gone till you buy more</Description>
      </anyType>
      <anyType d2p1:type="Item" ID="BSDS4D7834C3" Name="Gask Mask" Cost="25" TypesCanUse="All" SpeciesCanEquip="All">
        <ModifiesStats />
        <Description>Nullifies poison gas effects</Description>
      </anyType>
      <anyType d2p1:type="Item" ID="BSDS49A12ABA" Name="Grenade Casing Standard" Cost="5" TypesCanUse="All" SpeciesCanEquip="All">
        <ModifiesStats />
        <Description>The shell of grenade, Holds 3 units</Description>
      </anyType>
      <anyType d2p1:type="Item" ID="BSDS3061A4F9" Name="Grenade Casing Sticky" Cost="8" TypesCanUse="All" SpeciesCanEquip="All">
        <ModifiesStats />
        <Description>The out side has a sticky shell that when thrown can stick to a target, it sticks on a roll of a hit, 3 units</Description>
      </anyType>
      <anyType d2p1:type="Item" ID="BSDS92D6A215" Name="Landmine Casing Standard" Cost="15" TypesCanUse="All" SpeciesCanEquip="All">
        <ModifiesStats />
        <Description>Shell of a land mine, 6 units</Description>
      </anyType>
      <anyType d2p1:type="Item" ID="BSDS922FF04B" Name="Large Bomb Casing" Cost="25" TypesCanUse="All" SpeciesCanEquip="All">
        <ModifiesStats />
        <Description>The Shell of a large bomb, 9 Units</Description>
      </anyType>
      <anyType d2p1:type="Item" ID="BSDS89E88214" Name="Timer" Cost="5" TypesCanUse="All" SpeciesCanEquip="All">
        <ModifiesStats />
        <Description>Goes on any casing, makes it go off after d4 turns, costs 1 unit</Description>
      </anyType>
      <anyType d2p1:type="Item" ID="BSDS8D37EF69" Name="Smoke Filling" Cost="10" TypesCanUse="All" SpeciesCanEquip="All">
        <ModifiesStats />
        <Description>Adds smoke to a grenade, cost 1 unit</Description>
      </anyType>
      <anyType d2p1:type="Item" ID="BSDS2DFC8CF7" Name="Explosive Filling" Cost="5" TypesCanUse="All" SpeciesCanEquip="All">
        <ModifiesStats />
        <Description>MvsP = 2, MvsA = 1.5, 1” diameter, costs 1 unit</Description>
      </anyType>
      <anyType d2p1:type="Item" ID="BSDS7BBDC24E" Name="C4 Filling" Cost="35" TypesCanUse="All" SpeciesCanEquip="All">
        <ModifiesStats />
        <Description>MvsP = 6, MvsA = 3, 2” Diameter, costs 1 unit</Description>
      </anyType>
      <anyType d2p1:type="Item" ID="BSDS9D7356A7" Name="Black Powder Filling" Cost="1" TypesCanUse="All" SpeciesCanEquip="All">
        <ModifiesStats />
        <Description>MvsP = 1, .5” diameter, costs 1 unit</Description>
      </anyType>
      <anyType d2p1:type="Item" ID="BSDS43FE89F1" Name="Flash Filling" Cost="6" TypesCanUse="All" SpeciesCanEquip="All">
        <ModifiesStats />
        <Description>Blinds all units in a 12” diameter for d3 turns, costs 1 unit</Description>
      </anyType>
      <anyType d2p1:type="Item" ID="BSDSFCFB0A6C" Name="Nitroglycerin Filling" Cost="40" TypesCanUse="All" SpeciesCanEquip="All">
        <ModifiesStats />
        <Description>MvsP = 8, MvsA = 5, 3” Diameter, costs 1 unit</Description>
      </anyType>
      <anyType d2p1:type="Item" ID="BSDS33DB939B" Name="Metal Bits" Cost="2" TypesCanUse="All" SpeciesCanEquip="All">
        <ModifiesStats />
        <Description>MvsP = 3, small shrapnel costs 1 unit requires 1 unit of explosives</Description>
      </anyType>
      <anyType d2p1:type="Item" ID="BSDS7047CF96" Name="Ball Bearings" Cost="50" TypesCanUse="All" SpeciesCanEquip="All">
        <ModifiesStats />
        <Description>MvsP = 10, MvsA = 8, +3” in explosion costs 2 units, requires 1 unit of explosives for every units of bearings </Description>
      </anyType>
      <anyType d2p1:type="Item" ID="BSDS5ECC18F0" Name="Uranium" Cost="10000" TypesCanUse="All" SpeciesCanEquip="All">
        <ModifiesStats />
        <Description>Costs 1 units, requires 8 unit of explosives uses rules for mini nukes</Description>
      </anyType>
      <anyType d2p1:type="Item" ID="BSDSABD84296" Name="Electro Magnetic" Cost="20" TypesCanUse="All" SpeciesCanEquip="All">
        <ModifiesStats />
        <Description>Goes out 3” for every unit used, costs 1 unit</Description>
      </anyType>
      <anyType d2p1:type="Item" ID="BSDS6343BD8A" Name="Thermo Reduction" Cost="20" TypesCanUse="All" SpeciesCanEquip="All">
        <ModifiesStats />
        <Description>Reduces vehicle armor 2 points per unit, costs 1 unit</Description>
      </anyType>
    </Objects>
  </Collectors>
  <Collectors d2p1:type="PsyCollector" xmlns:d2p1="http://www.w3.org/2001/XMLSchema-instance">
    <Objects>
      <anyType d2p1:type="Psy" ID="BSDS6A3F28B8" Name="Acid Rain" ppCost="6" Range="0" Template="Round" TemplateSize="4D6&quot;" PsyTypes="Cloud">
        <Description>The caster decides where the storm starts up to 48" from caster and then the storm deviates d3" in a random direction from that point</Description>
        <Effect>Causes metallic armor to rust. The area affected by the acid rain is 4d6" diameter for each model caught in the storm Armor values drop to half of what they are normally.</Effect>
        <RequiresPermission d2p1:type="PsyPermission" ID="BSDS5ED11D2D" Name="Earth" SpeciesCanEquip="All" />
      </anyType>
      <anyType d2p1:type="Psy" ID="BSDS99E4109E" Name="Cats Grace" ppCost="4" Range="0" Template="None" TemplateSize="" PsyTypes="Agility">
        <Description />
        <Effect>Automatically allows a model to pass all agility checks for 1 round of combat i.e. jumping, falling, tumbling ect</Effect>
        <RequiresPermission d2p1:type="PsyPermission" ID="BSDS3BB830F7" Name="Medical" SpeciesCanEquip="All" />
      </anyType>
      <anyType d2p1:type="Psy" ID="BSDS5CCD092E" Name="Chain Lighting" ppCost="5" Range="0" Template="None" TemplateSize="" PsyTypes="Lighting">
        <Description />
        <Effect>When chain lightning hits it does a M7 d12 hit and will continue on randomly up to 3 more models doing a M3 d6 hit. This will only jump back and fourth between models that are within 2” of each other. Follow element rules.</Effect>
        <RequiresPermission d2p1:type="PsyPermission" ID="BSDSE4BC23B8" Name="Lighting" SpeciesCanEquip="All" />
      </anyType>
      <anyType d2p1:type="Psy" ID="BSDS4898A4C6" Name="Clairvoyance" ppCost="2" Range="0" Template="None" TemplateSize="" PsyTypes="Mental">
        <Description />
        <Effect>+3 agility, for d6 round effects are not cumulative</Effect>
        <RequiresPermission d2p1:type="PsyPermission" ID="BSDS599F7359" Name="Generic" SpeciesCanEquip="All" />
      </anyType>
      <anyType d2p1:type="Psy" ID="BSDSCF299AFC" Name="Double Dammage" ppCost="5" Range="0" Template="None" TemplateSize="" PsyTypes="Generic">
        <Description />
        <Effect>When a model hits and does damage the damage is multiplied by 2</Effect>
        <RequiresPermission d2p1:type="PsyPermission" ID="BSDS599F7359" Name="Generic" SpeciesCanEquip="All" />
      </anyType>
      <anyType d2p1:type="Psy" ID="BSDS2DA1BD57" Name="Fear" ppCost="3" Range="0" Template="None" TemplateSize="" PsyTypes="Mental">
        <Description />
        <Effect>Fear creates an illusion that a model is scarier than it really is. The spell lasts d4 rounds and adds d12 HF (horror) to a model within 24" of the caster.</Effect>
        <RequiresPermission d2p1:type="PsyPermission" ID="BSDS599F7359" Name="Generic" SpeciesCanEquip="All" />
      </anyType>
      <anyType d2p1:type="Psy" ID="BSDS63E38596" Name="Fearless" ppCost="2" Range="0" Template="None" TemplateSize="" PsyTypes="Medical">
        <Description />
        <Effect>This model doesn't need to make a WP (willpower) save when confronted by a model with a higher HF (horror factor) than them. Lasts d3 rounds</Effect>
        <RequiresPermission d2p1:type="PsyPermission" ID="BSDS599F7359" Name="Generic" SpeciesCanEquip="All" />
      </anyType>
      <anyType d2p1:type="Psy" ID="BSDSFAB771B0" Name="Fireball" ppCost="1" Range="0" Template="None" TemplateSize="" PsyTypes="Fire">
        <Description />
        <Effect>MvsP = 2, MvsA = 1, per point spent. follow element rules</Effect>
        <RequiresPermission d2p1:type="PsyPermission" ID="BSDS7133A8B3" Name="Fire" SpeciesCanEquip="All" />
      </anyType>
      <anyType d2p1:type="Psy" ID="BSDSE3CEC441" Name="Fog" ppCost="3" Range="0" Template="Round" TemplateSize="36&quot;d" PsyTypes="Cloud">
        <Description />
        <Effect>A rolling cloud of fog 36” in diameter comes on the board, and lasts d12 turns, follow rules for smoke clouds</Effect>
        <RequiresPermission d2p1:type="PsyPermission" ID="BSDS599F7359" Name="Generic" SpeciesCanEquip="All" />
      </anyType>
      <anyType d2p1:type="Psy" ID="BSDSB448C4E6" Name="Freezing Touch" ppCost="4" Range="0" Template="None" TemplateSize="" PsyTypes="Cold">
        <Description />
        <Effect>Works like liquid nitrogen… a spell caster may cast this on them self or a model within 12". Played as normal mele weapon doing MvsP=6 MvsA=3 if a model is wearing armor and is touched if another model can get a successful hit on the model the armor has a chance to shatter and be destroyed, the chance is equal to the weapons M (might) roll d12 under the stat.</Effect>
        <RequiresPermission d2p1:type="PsyPermission" ID="BSDSC077C357" Name="Touch" SpeciesCanEquip="All" />
      </anyType>
      <anyType d2p1:type="Psy" ID="BSDSB08076AF" Name="Give Me the Strength" ppCost="5" Range="0" Template="None" TemplateSize="" PsyTypes="Medical">
        <Description />
        <Effect>Can be cast on a model within 12" to give the model a +2 to their MT (might)</Effect>
        <RequiresPermission d2p1:type="PsyPermission" ID="BSDS3BB830F7" Name="Medical" SpeciesCanEquip="All" />
      </anyType>
      <anyType d2p1:type="Psy" ID="BSDS64475A9E" Name="Growth" ppCost="5" Range="0" Template="None" TemplateSize="" PsyTypes="Generic">
        <Description />
        <Effect>Can be cast on a model within 12", this spell will double the size of the model. The model doubles M, MT, T, and gains a HF of 12. The model then incurs a +4 penalty to be hit, can't use vehicles or guns. The spell lasts d3 rounds.</Effect>
        <RequiresPermission d2p1:type="PsyPermission" ID="BSDS599F7359" Name="Generic" SpeciesCanEquip="All" />
      </anyType>
      <anyType d2p1:type="Psy" ID="BSDSFFD568A4" Name="Hands of Flame" ppCost="4" Range="0" Template="None" TemplateSize="" PsyTypes="Fire">
        <Description />
        <Effect>give the model the ability to set things on fire. On a successful H+H attack follow the burning rules. The model takes a MvsP of 5</Effect>
        <RequiresPermission d2p1:type="PsyPermission" ID="BSDSC077C357" Name="Touch" SpeciesCanEquip="All" />
      </anyType>
      <anyType d2p1:type="Psy" ID="BSDS1842C76A" Name="Heavy Healing" ppCost="4" Range="0" Template="None" TemplateSize="" PsyTypes="Medical">
        <Description />
        <Effect>D12 hp regained, model must be with in 12”</Effect>
        <RequiresPermission d2p1:type="PsyPermission" ID="BSDS3BB830F7" Name="Medical" SpeciesCanEquip="All" />
      </anyType>
      <anyType d2p1:type="Psy" ID="BSDSB52DB41F" Name="Hidden" ppCost="1" Range="0" Template="None" TemplateSize="" PsyTypes="Generic">
        <Description />
        <Effect>Model gains instant heavy cover for the rest of the turn, may only be cast once per-turn, may be combined with other cover bonuses, if the model moves or shoots the hidden is no longer in effect</Effect>
        <RequiresPermission d2p1:type="PsyPermission" ID="BSDS599F7359" Name="Generic" SpeciesCanEquip="All" />
      </anyType>
      <anyType d2p1:type="Psy" ID="BSDS968072A5" Name="Ice Blast" ppCost="3" Range="0" Template="None" TemplateSize="" PsyTypes="Water">
        <Description />
        <Effect>A blast of icy wind hits a target model within 24" of the caster and does MvsP=5, MvsA=5 (only for models with armor), MvsA=3</Effect>
        <RequiresPermission d2p1:type="PsyPermission" ID="BSDS63F937B3" Name="Ice" SpeciesCanEquip="All" />
      </anyType>
      <anyType d2p1:type="Psy" ID="BSDS6BB5B1A6" Name="Ice Rain" ppCost="6" Range="0" Template="Round" TemplateSize="dD6&quot;d" PsyTypes="Water">
        <Description />
        <Effect>Large Chuncks of ice rocks fall from the sky and strike in a 4d6" diameter each model cought in the storm sustains d4 hits at MvsP=4, MvsA=3, The caster decides where the storm starts up to 48" from caster and then the storm deviates d3" in a random direction from that point</Effect>
        <RequiresPermission d2p1:type="PsyPermission" ID="BSDS63F937B3" Name="Ice" SpeciesCanEquip="All" />
      </anyType>
      <anyType d2p1:type="Psy" ID="BSDSF954A7B1" Name="Levitation" ppCost="2" Range="0" Template="None" TemplateSize="" PsyTypes="Generic">
        <Description />
        <Effect>A model may float for d3 turns may be cast on any model, if the model has no momentum it cannot move, a model that has momentum moves in a straight line equal to the number of inches it last moved.</Effect>
        <RequiresPermission d2p1:type="PsyPermission" ID="BSDS599F7359" Name="Generic" SpeciesCanEquip="All" />
      </anyType>
      <anyType d2p1:type="Psy" ID="BSDS7D990CD9" Name="Life Reaping" ppCost="2" Range="0" Template="None" TemplateSize="" PsyTypes="Medical">
        <Description />
        <Effect>caster is allowed to steal up to d4 life from another model up to 12" away. The model getting drained may make a WP save (d12 under WP) to stop the spell</Effect>
        <RequiresPermission d2p1:type="PsyPermission" ID="BSDS3BB830F7" Name="Medical" SpeciesCanEquip="All" />
      </anyType>
      <anyType d2p1:type="Psy" ID="BSDSD70C7107" Name="Light Healing" ppCost="2" Range="0" Template="None" TemplateSize="" PsyTypes="Medical">
        <Description />
        <Effect>D6 hp regained, model must be within 15”</Effect>
        <RequiresPermission d2p1:type="PsyPermission" ID="BSDS3BB830F7" Name="Medical" SpeciesCanEquip="All" />
      </anyType>
      <anyType d2p1:type="Psy" ID="BSDS2EDAA2F2" Name="Lighting Ball" ppCost="1" Range="0" Template="None" TemplateSize="" PsyTypes="Lighting">
        <Description />
        <Effect>MvsP = 2, MvsA = 1, per point spent. follow element rules</Effect>
        <RequiresPermission d2p1:type="PsyPermission" ID="BSDSE4BC23B8" Name="Lighting" SpeciesCanEquip="All" />
      </anyType>
      <anyType d2p1:type="Psy" ID="BSDS3F5AD9B7" Name="Flash Fire" ppCost="5" Range="0" Template="Round" TemplateSize="D12&quot;" PsyTypes="Fire">
        <Description />
        <Effect>Flash Fire pin points a model within 24" of the caster. The spell then bursts outward d12 inches in diameter the model and everything within that area catches fire (trees, long grass, bushes, paper ect) under brush burns for d4 turns and any model that touches the under brush may catch fire. the fire then spreads to other flammable objects at a rate of d4" per turn unless their is nothing in the area to catch fire. Models in the blast area take a MvsP=7 MvsA=2</Effect>
        <RequiresPermission d2p1:type="PsyPermission" ID="BSDS7133A8B3" Name="Fire" SpeciesCanEquip="All" />
      </anyType>
      <anyType d2p1:type="Psy" ID="BSDS8FB858B8" Name="Lighting Flash" ppCost="3" Range="0" Template="None" TemplateSize="" PsyTypes="Lighting">
        <Description />
        <Effect>A bright light flash that blinds all models with in a d4" diameter. Models may meke a AG save to negate the effects (D12 under AG). Any model that fails gets their M,H+H, RW, AG cut in half for d3 turns, but don’t neet make a HF save when confronting a model with a high HF.</Effect>
        <RequiresPermission d2p1:type="PsyPermission" ID="BSDSE4BC23B8" Name="Lighting" SpeciesCanEquip="All" />
      </anyType>
      <anyType d2p1:type="Psy" ID="BSDS741B5A93" Name="Magic Bolt" ppCost="2" Range="0" Template="None" TemplateSize="" PsyTypes="Lighting">
        <Description />
        <Effect>A bolt of shimmering light hits a model with in 12" of the caster MvsP=5, MvsA=5. The model getting attacked may make a reflex save (D12 under AG)</Effect>
        <RequiresPermission d2p1:type="PsyPermission" ID="BSDSE4BC23B8" Name="Lighting" SpeciesCanEquip="All" />
      </anyType>
      <anyType d2p1:type="Psy" ID="BSDSA13ADEC8" Name="Noxious Gas Cloud" ppCost="1" Range="0" Template="Round" TemplateSize="varries" PsyTypes="Cloud">
        <Description />
        <Effect>A cloud of horrible gas comes rolling out 1” in diameter for every point spent to cast it. The cloud hangs around for d6 rounds and randomly moves any model caught in the gas cloud finds it hard to breath and takes a might 4 d12 hit. Follow rules for smoke clouds.</Effect>
        <RequiresPermission d2p1:type="PsyPermission" ID="BSDS599F7359" Name="Generic" SpeciesCanEquip="All" />
      </anyType>
      <anyType d2p1:type="Psy" ID="BSDS7456909C" Name="Penetrator" ppCost="5" Range="0" Template="None" TemplateSize="" PsyTypes="Generic">
        <Description />
        <Effect>When an model has this cast on them they substitute MvsA with MvsP on attack rolls. Lasts d3 turns</Effect>
        <RequiresPermission d2p1:type="PsyPermission" ID="BSDS599F7359" Name="Generic" SpeciesCanEquip="All" />
      </anyType>
      <anyType d2p1:type="Psy" ID="BSDSCF48574B" Name="Reflect" ppCost="3" Range="0" Template="None" TemplateSize="" PsyTypes="Generic">
        <Description />
        <Effect>A model that has the reflect spell cast on them will reflect a spell cast at them in a random direction half the distance between the spell caster and the targeted model i.e. a spell caster casts fireball and hits a model with reflect the fireball, they are 12" apart the fire ball then goes 6" in a random direction. if a model is hit by the spell that model then "takes it" as thoe it were cast on them. </Effect>
        <RequiresPermission d2p1:type="PsyPermission" ID="BSDS599F7359" Name="Generic" SpeciesCanEquip="All" />
      </anyType>
      <anyType d2p1:type="Psy" ID="BSDSE2B57676" Name="Shield" ppCost="5" Range="0" Template="None" TemplateSize="" PsyTypes="Generic">
        <Description />
        <Effect>Shield does not count as armor but will add d3 T (toughness) to a single model with in 12" of the spell caster for d3 rounds. This spell cannot be stacked and requires a 2 round wait period until it can be cast again.</Effect>
        <RequiresPermission d2p1:type="PsyPermission" ID="BSDS599F7359" Name="Generic" SpeciesCanEquip="All" />
      </anyType>
      <anyType d2p1:type="Psy" ID="BSDSCF7E3180" Name="Shocking Poke" ppCost="1" Range="0" Template="None" TemplateSize="" PsyTypes="Lighting">
        <Description />
        <Effect>A model with shocking poke may tuch another model with their finger to do a MvsP=4, and MvsA=3 attack. </Effect>
        <RequiresPermission d2p1:type="PsyPermission" ID="BSDSE4BC23B8" Name="Lighting" SpeciesCanEquip="All" />
      </anyType>
      <anyType d2p1:type="Psy" ID="BSDS807E01CE" Name="Shrapnel Storm" ppCost="2" Range="0" Template="Round" TemplateSize="1&quot;" PsyTypes="Generic">
        <Description />
        <Effect>MvsP = 6, MvsA = 4</Effect>
        <RequiresPermission d2p1:type="PsyPermission" ID="BSDS599F7359" Name="Generic" SpeciesCanEquip="All" />
      </anyType>
      <anyType d2p1:type="Psy" ID="BSDSD04E6A8D" Name="Spirit Blast" ppCost="2" Range="0" Template="None" TemplateSize="" PsyTypes="Generic">
        <Description />
        <Effect>A blast of energy hits a model for MvsP=3, MvsA=3. Causes a knock back hit for D4" the model is considered knocked over</Effect>
        <RequiresPermission d2p1:type="PsyPermission" ID="BSDS599F7359" Name="Generic" SpeciesCanEquip="All" />
      </anyType>
      <anyType d2p1:type="Psy" ID="BSDS505AA936" Name="Straight Shot" ppCost="1" Range="0" Template="None" TemplateSize="" PsyTypes="Generic">
        <Description />
        <Effect>+4 RW for 1 shot</Effect>
        <RequiresPermission d2p1:type="PsyPermission" ID="BSDS599F7359" Name="Generic" SpeciesCanEquip="All" />
      </anyType>
      <anyType d2p1:type="Psy" ID="BSDS244607E5" Name="Teleportation" ppCost="2" Range="0" Template="None" TemplateSize="" PsyTypes="Generic">
        <Description />
        <Effect>Model may teleport it self or other friendly models 10 inches</Effect>
        <RequiresPermission d2p1:type="PsyPermission" ID="BSDS599F7359" Name="Generic" SpeciesCanEquip="All" />
      </anyType>
    </Objects>
  </Collectors>
  <Collectors d2p1:type="PsyPermissionCollector" xmlns:d2p1="http://www.w3.org/2001/XMLSchema-instance">
    <Objects>
      <anyType d2p1:type="PsyPermission" ID="BSDS7133A8B3" Name="Fire" SpeciesCanEquip="All" />
      <anyType d2p1:type="PsyPermission" ID="BSDSE4BC23B8" Name="Lighting" SpeciesCanEquip="All" />
      <anyType d2p1:type="PsyPermission" ID="BSDS63F937B3" Name="Ice" SpeciesCanEquip="All" />
      <anyType d2p1:type="PsyPermission" ID="BSDSDA1AE67C" Name="Water" SpeciesCanEquip="All" />
      <anyType d2p1:type="PsyPermission" ID="BSDSD235C395" Name="Wind" SpeciesCanEquip="All" />
      <anyType d2p1:type="PsyPermission" ID="BSDS5ED11D2D" Name="Earth" SpeciesCanEquip="All" />
      <anyType d2p1:type="PsyPermission" ID="BSDSC077C357" Name="Touch" SpeciesCanEquip="All" />
      <anyType d2p1:type="PsyPermission" ID="BSDS3BB830F7" Name="Medical" SpeciesCanEquip="All" />
      <anyType d2p1:type="PsyPermission" ID="BSDS599F7359" Name="Generic" SpeciesCanEquip="All" />
    </Objects>
  </Collectors>
  <Collectors d2p1:type="RaceCollector" xmlns:d2p1="http://www.w3.org/2001/XMLSchema-instance">
    <Objects>
      <anyType d2p1:type="Race" ID="BSDS9A264EA9" Name="Human" species="Human" />
    </Objects>
  </Collectors>
  <Collectors d2p1:type="SkillCollector" xmlns:d2p1="http://www.w3.org/2001/XMLSchema-instance">
    <Objects>
      <anyType d2p1:type="Skill" ID="BSDS2B1136D5" Name="Bomb Making" Cost="100" SpeciesCanUseSkill="Human Mutant Undead Genics_B Aquatic Fun" Group="Arsonist">
        <Description>When making a bomb you first need to select a shell for this example I will use a standard casing this shell has 3 units, each item put into a shell will take up space this space is defined by units. I will then decide to fill the case with 3 “explosive fillings” to do this you need to roll 1d12 under the models average wp and ag ( (wp+ag)/2 ) if successful you’ll have just made a standard grenade and saved 5 tabs its not much but it is kind of like buy 4 get one free. The only time you roll is after your done putting the bomb together. If you roll a 12 the bomb explodes in the models face causing damage equal to what would happen on the battle field if its enough to kill it the model has died the model will only completely recover at the start of the next battle damage between fights is cumulative, if you roll under the models Wp &amp; Ag average you have successfully made a bomb, if you roll equal to the average or above the bomb is a dud and must be discarded losing everything you put in to it.</Description>
        <ModifiesStats />
        <RequiresPermission d2p1:type="SkillPermission" ID="BSDSF4D4D86A" Name="Arsonists" SpeciesCanEquip="Human Mutant Undead Genics_B Aquatic Fun" />
      </anyType>
      <anyType d2p1:type="Skill" ID="BSDS67113D2B" Name="Precise Throw" Cost="150" SpeciesCanUseSkill="Human Mutant Genics_B Aquatic Fun" Group="Arsonist">
        <Description>When throwing something in combat the model gets a +1 to RW.</Description>
        <ModifiesStats />
        <RequiresPermission d2p1:type="SkillPermission" ID="BSDSF4D4D86A" Name="Arsonists" SpeciesCanEquip="Human Mutant Undead Genics_B Aquatic Fun" />
      </anyType>
      <anyType d2p1:type="Skill" ID="BSDSD5245162" Name="Bomb Improvements" Cost="400" SpeciesCanUseSkill="Human Genics_B Fun" Group="Arsonist">
        <Description>The model can dismantle dud bombs for ½ the insides (rounded down) and the casing.</Description>
        <ModifiesStats />
        <RequiresPermission d2p1:type="SkillPermission" ID="BSDSF4D4D86A" Name="Arsonists" SpeciesCanEquip="Human Mutant Undead Genics_B Aquatic Fun" />
      </anyType>
      <anyType d2p1:type="Skill" ID="BSDS6B03AFE2" Name="Demolitions" Cost="0" SpeciesCanUseSkill="Human Mutant Genics_B Aquatic Fun" Group="Arsonist">
        <Description>Makes all homemade bombs blast diameters 1½x their original size.</Description>
        <ModifiesStats />
        <RequiresPermission d2p1:type="SkillPermission" ID="BSDSF4D4D86A" Name="Arsonists" SpeciesCanEquip="Human Mutant Undead Genics_B Aquatic Fun" />
      </anyType>
      <anyType d2p1:type="Skill" ID="BSDSE0A25020" Name="Disarm" Cost="0" SpeciesCanUseSkill="Human Mutant Genics_B Aquatic Fun" Group="Arsonist">
        <Description>You may re-roll failed bomb making rolls once for each bomb.</Description>
        <ModifiesStats />
        <RequiresPermission d2p1:type="SkillPermission" ID="BSDSF4D4D86A" Name="Arsonists" SpeciesCanEquip="Human Mutant Undead Genics_B Aquatic Fun" />
      </anyType>
      <anyType d2p1:type="Skill" ID="BSDSE0F353F0" Name="Vehicle Repairs" Cost="100" SpeciesCanUseSkill="All" Group="Mechanic">
        <Description>½ cost for repairing broken vehicles</Description>
        <ModifiesStats />
        <RequiresPermission d2p1:type="SkillPermission" ID="BSDS6389ED91" Name="Mechanical" SpeciesCanEquip="All" />
      </anyType>
      <anyType d2p1:type="Skill" ID="BSDS4DB31BAF" Name="Refueling" Cost="100" SpeciesCanUseSkill="All" Group="Mechanic">
        <Description>½ cost for refueling vehicles</Description>
        <ModifiesStats />
        <RequiresPermission d2p1:type="SkillPermission" ID="BSDS6389ED91" Name="Mechanical" SpeciesCanEquip="All" />
      </anyType>
      <anyType d2p1:type="Skill" ID="BSDS3C43F015" Name="Vehicle Engineer" Cost="300" SpeciesCanUseSkill="All" Group="Mechanic">
        <Description>Makes it possible to build vehicles with out a land that allows you to do so. If you have a land that does allow you to, it will only take ½ the time to build a vehicle.</Description>
        <ModifiesStats />
        <RequiresPermission d2p1:type="SkillPermission" ID="BSDS6389ED91" Name="Mechanical" SpeciesCanEquip="All" />
      </anyType>
      <anyType d2p1:type="Skill" ID="BSDS37B64FE8" Name="Gunsmith" Cost="0" SpeciesCanUseSkill="Human Mutant Genics_B Aquatic Angelic Demonic Fun" Group="Mechanic">
        <Description>Allows for weapon attachments to be added to weapons (only one guy in the crew is needed to have the skill for all weapon upgrades)</Description>
        <ModifiesStats />
        <RequiresPermission d2p1:type="SkillPermission" ID="BSDS6389ED91" Name="Mechanical" SpeciesCanEquip="All" />
      </anyType>
      <anyType d2p1:type="Skill" ID="BSDS33E7A127" Name="Trap Making" Cost="100" SpeciesCanUseSkill="Human Genics_A Genics_B Demonic Fun" Group="TrapSetter">
        <Description>When Playing with traps its important t know where your traps are and what they are. Poker chips are a good way to do this, on a separate sheet write down some numbers (one to however many Traps you can set) then write down what traps are what numbers. Take out some poker chips with corresponding numbers. when you play a scenario that allows traps everyone but the player setting traps leaves the room. The numbered tokens are placed face down on the game board. On the sheet of paper with the numbers write down what each trap is and what it does. Then that same person lays the treasure tokens down face down on the board. If multiple people can lay traps the number of total allowed traps are divided equally and treasure tokens to be place are layer down are equally divided, players take turns leavening the room to set up traps or treasure. Ex player one lays one trap, player two comes in and lays one treasure, player one comes back in and lays one treasure, player two, so on and so fourth till all traps and treasure are placed.</Description>
        <ModifiesStats />
        <RequiresPermission d2p1:type="SkillPermission" ID="BSDS9DA4CA8A" Name="Trap Setter" SpeciesCanEquip="Human Mutant Genics_A Genics_B Aquatic Angelic Demonic Fun" />
      </anyType>
      <anyType d2p1:type="Skill" ID="BSDS649716D8" Name="Trap Hiding" Cost="200" SpeciesCanUseSkill="Human Genics_A Demonic Fun" Group="TrapSetter">
        <Description>Allows the model to switch out a treasure for a trap during a game</Description>
        <ModifiesStats />
        <RequiresPermission d2p1:type="SkillPermission" ID="BSDS9DA4CA8A" Name="Trap Setter" SpeciesCanEquip="Human Mutant Genics_A Genics_B Aquatic Angelic Demonic Fun" />
      </anyType>
      <anyType d2p1:type="Skill" ID="BSDSF964EE2C" Name="Better Mousetrap" Cost="300" SpeciesCanUseSkill="Human Genics_A Genics_B Demonic Fun" Group="TrapSetter">
        <Description>Roll a 1d12 when a trap goes off if its equal to or lowered than the models Ag that set the trap the trap does x2 damage</Description>
        <ModifiesStats />
        <RequiresPermission d2p1:type="SkillPermission" ID="BSDS9DA4CA8A" Name="Trap Setter" SpeciesCanEquip="Human Mutant Genics_A Genics_B Aquatic Angelic Demonic Fun" />
      </anyType>
      <anyType d2p1:type="Skill" ID="BSDSE0A55230" Name="Dodge" Cost="0" SpeciesCanUseSkill="Human Mutant Genics_A Genics_B Aquatic Angelic Demonic Fun" Group="TrapSetter">
        <Description>Roll a d12 if its equal to or under the models Ag the model may dive out of the way of a trap in a random direction.</Description>
        <ModifiesStats />
        <RequiresPermission d2p1:type="SkillPermission" ID="BSDS9DA4CA8A" Name="Trap Setter" SpeciesCanEquip="Human Mutant Genics_A Genics_B Aquatic Angelic Demonic Fun" />
      </anyType>
      <anyType d2p1:type="Skill" ID="BSDS7DD7051E" Name="Quick Shot" Cost="150" SpeciesCanUseSkill="Human Mutant Genics_B Demonic Fun" Group="Gunman">
        <Description>A model may make 1 sustained fire roll (d12 under the last rolled number)</Description>
        <ModifiesStats />
        <RequiresPermission d2p1:type="SkillPermission" ID="BSDSCEB1C6F7" Name="Gunman" SpeciesCanEquip="All" />
      </anyType>
      <anyType d2p1:type="Skill" ID="BSDS753809E4" Name="Trigger Happy" Cost="300" SpeciesCanUseSkill="Human Mutant Undead Genics_B Demonic Fun" Group="Gunman">
        <Description>If the model may take as many shots as it has attack with pistols</Description>
        <ModifiesStats />
        <RequiresPermission d2p1:type="SkillPermission" ID="BSDSCEB1C6F7" Name="Gunman" SpeciesCanEquip="All" />
      </anyType>
      <anyType d2p1:type="Skill" ID="BSDS991D8593" Name="Sniper" Cost="250" SpeciesCanUseSkill="Human Undead Genics_B Angelic Fun" Group="Gunman">
        <Description>+1 RW when using a scope</Description>
        <ModifiesStats />
        <RequiresPermission d2p1:type="SkillPermission" ID="BSDSCEB1C6F7" Name="Gunman" SpeciesCanEquip="All" />
      </anyType>
      <anyType d2p1:type="Skill" ID="BSDS40F84D93" Name="Gun Slinger" Cost="0" SpeciesCanUseSkill="Human Mutant Undead Genics_B Aquatic Angelic Demonic Fun" Group="Gunman">
        <Description>If the model is carrying 2 pistols it may fire both of them once per turn</Description>
        <ModifiesStats />
        <RequiresPermission d2p1:type="SkillPermission" ID="BSDSCEB1C6F7" Name="Gunman" SpeciesCanEquip="All" />
      </anyType>
      <anyType d2p1:type="Skill" ID="BSDSE5CBE712" Name="Hip Shot" Cost="0" SpeciesCanUseSkill="All" Group="Gunman">
        <Description>The model suffers no penalty to shooting at a model coming from or into cover when on over watch</Description>
        <ModifiesStats />
        <RequiresPermission d2p1:type="SkillPermission" ID="BSDSCEB1C6F7" Name="Gunman" SpeciesCanEquip="All" />
      </anyType>
      <anyType d2p1:type="Skill" ID="BSDS38E30AF7" Name="Blade Sharpener" Cost="100" SpeciesCanUseSkill="All" Group="ArmsMaster">
        <Description>+2 MvsP, +1 MvsA to bladed weapons that the model carries</Description>
        <ModifiesStats />
        <RequiresPermission d2p1:type="SkillPermission" ID="BSDSF8F13F38" Name="Arms Master" SpeciesCanEquip="All" />
      </anyType>
      <anyType d2p1:type="Skill" ID="BSDS65092811" Name="Quick Blade" Cost="200" SpeciesCanUseSkill="All" Group="ArmsMaster">
        <Description>When charge upon in combat the model may make one attack before the charging model attacks.</Description>
        <ModifiesStats />
        <RequiresPermission d2p1:type="SkillPermission" ID="BSDSF8F13F38" Name="Arms Master" SpeciesCanEquip="All" />
      </anyType>
      <anyType d2p1:type="Skill" ID="BSDS589FC9E3" Name="Swordman" Cost="300" SpeciesCanUseSkill="Human Undead Genics_A Genics_B Angelic Demonic Fun" Group="ArmsMaster">
        <Description>If the model carries two swords it may make one extra attack in H+H combat, beyond the extra attack for 2 weapons.</Description>
        <ModifiesStats />
        <RequiresPermission d2p1:type="SkillPermission" ID="BSDSF8F13F38" Name="Arms Master" SpeciesCanEquip="All" />
      </anyType>
      <anyType d2p1:type="Skill" ID="BSDS57B31A02" Name="Block" Cost="0" SpeciesCanUseSkill="All" Group="ArmsMaster">
        <Description>The model may make one extra block per turn</Description>
        <ModifiesStats />
        <RequiresPermission d2p1:type="SkillPermission" ID="BSDSF8F13F38" Name="Arms Master" SpeciesCanEquip="All" />
      </anyType>
      <anyType d2p1:type="Skill" ID="BSDS60E85A1D" Name="Step Aside" Cost="0" SpeciesCanUseSkill="Human Mutant Genics_A Genics_B Aquatic Angelic Demonic" Group="ArmsMaster">
        <Description>When charged you may roll d12 under the models Ag if successful the model steps aside dodging the charge</Description>
        <ModifiesStats />
        <RequiresPermission d2p1:type="SkillPermission" ID="BSDSF8F13F38" Name="Arms Master" SpeciesCanEquip="All" />
      </anyType>
      <anyType d2p1:type="Skill" ID="BSDSD0E41475" Name="First Aid" Cost="100" SpeciesCanUseSkill="Human Mutant Aquatic Angelic Fun" Group="Medic">
        <Description>+5 hp when using healing items.</Description>
        <ModifiesStats />
        <RequiresPermission d2p1:type="SkillPermission" ID="BSDSAD0CFF38" Name="Medic" SpeciesCanEquip="Human Mutant Aquatic Angelic Demonic Fun" />
      </anyType>
      <anyType d2p1:type="Skill" ID="BSDS3C2F9905" Name="Patient Care" Cost="300" SpeciesCanUseSkill="Human Mutant Aquatic Fun" Group="Medic">
        <Description>+5 hp when using healing items (may be used with the +5 from first aid)</Description>
        <ModifiesStats />
        <RequiresPermission d2p1:type="SkillPermission" ID="BSDSAD0CFF38" Name="Medic" SpeciesCanEquip="Human Mutant Aquatic Angelic Demonic Fun" />
      </anyType>
      <anyType d2p1:type="Skill" ID="BSDS7C052715" Name="Billing Agent" Cost="500" SpeciesCanUseSkill="Human Demonic Fun" Group="Medic">
        <Description>½ cost to all medical supplies</Description>
        <ModifiesStats />
        <RequiresPermission d2p1:type="SkillPermission" ID="BSDSAD0CFF38" Name="Medic" SpeciesCanEquip="Human Mutant Aquatic Angelic Demonic Fun" />
      </anyType>
      <anyType d2p1:type="Skill" ID="BSDS23590D4C" Name="Centered Thought" Cost="200" SpeciesCanUseSkill="Human Undead Genics_A Aquatic Angelic Demonic Fun" Group="Psyonicist">
        <Description />
        <ModifiesStats>
          <StatsMod StatToMod="Willpower" Modifier="1" />
        </ModifiesStats>
        <RequiresPermission d2p1:type="SkillPermission" ID="BSDS8A0C8A25" Name="Psionicist" SpeciesCanEquip="Human Undead Genics_A Genics_B Aquatic Angelic Demonic Fun" />
      </anyType>
      <anyType d2p1:type="Skill" ID="BSDS2F8A0236" Name="Earthen Power" Cost="400" SpeciesCanUseSkill="Human Undead Genics_A Aquatic Angelic Demonic Fun" Group="Psyonicist">
        <Description>+2 WP When Casting</Description>
        <ModifiesStats />
        <RequiresPermission d2p1:type="SkillPermission" ID="BSDS8A0C8A25" Name="Psionicist" SpeciesCanEquip="Human Undead Genics_A Genics_B Aquatic Angelic Demonic Fun" />
      </anyType>
      <anyType d2p1:type="Skill" ID="BSDSB9C8C751" Name="Spell Master" Cost="500" SpeciesCanUseSkill="Human Undead Genics_A Aquatic Angelic Demonic Fun" Group="Psyonicist">
        <Description>Re-roll flopped spell only once per-turn</Description>
        <ModifiesStats />
        <RequiresPermission d2p1:type="SkillPermission" ID="BSDS8A0C8A25" Name="Psionicist" SpeciesCanEquip="Human Undead Genics_A Genics_B Aquatic Angelic Demonic Fun" />
      </anyType>
      <anyType d2p1:type="Skill" ID="BSDS530C7D5E" Name="Sprinter" Cost="500" SpeciesCanUseSkill="Human Undead Genics_A Genics_B Aquatic Angelic Demonic Fun" Group="Physical">
        <Description />
        <ModifiesStats>
          <StatsMod StatToMod="Movement" Modifier="2" />
        </ModifiesStats>
        <RequiresPermission d2p1:type="SkillPermission" ID="BSDS7130F063" Name="Physical" SpeciesCanEquip="All" />
      </anyType>
      <anyType d2p1:type="Skill" ID="BSDS5E2E0983" Name="Muscles" Cost="300" SpeciesCanUseSkill="Human Mutant Genics_A Genics_B Aquatic Angelic Demonic Fun" Group="Physical">
        <Description />
        <ModifiesStats>
          <StatsMod StatToMod="Might" Modifier="1" />
        </ModifiesStats>
        <RequiresPermission d2p1:type="SkillPermission" ID="BSDS7130F063" Name="Physical" SpeciesCanEquip="All" />
      </anyType>
      <anyType d2p1:type="Skill" ID="BSDSCDC1A761" Name="Bullrush" Cost="300" SpeciesCanUseSkill="All" Group="Physical">
        <Description>The model may make a bull rush attack, MvsP = Str, the model being bull rushed will fall straight back and slide an equal number of inches to the attacking models str. If the model goes off an edge use the falling rules next if it hits a wall the model stops their and takes a strength hit equal to the number of inches not traveled when sliding.</Description>
        <ModifiesStats />
        <RequiresPermission d2p1:type="SkillPermission" ID="BSDS7130F063" Name="Physical" SpeciesCanEquip="All" />
      </anyType>
      <anyType d2p1:type="Skill" ID="BSDS8C1668F2" Name="Throwback" Cost="300" SpeciesCanUseSkill="Human Genics_B Aquatic Angelic Demonic Fun" Group="Physical">
        <Description>When a grenade or bomb is thrown the model may roll d12 under their Ag, if successful they may immediately throw the grenade back</Description>
        <ModifiesStats />
        <RequiresPermission d2p1:type="SkillPermission" ID="BSDS7130F063" Name="Physical" SpeciesCanEquip="All" />
      </anyType>
      <anyType d2p1:type="Skill" ID="BSDS40960E07" Name="Infiltration" Cost="300" SpeciesCanUseSkill="All" Group="Stealth">
        <Description>Use rules for showing up</Description>
        <ModifiesStats />
        <RequiresPermission d2p1:type="SkillPermission" ID="BSDSB532A5C3" Name="Stealth" SpeciesCanEquip="All" />
      </anyType>
      <anyType d2p1:type="Skill" ID="BSDS416CF945" Name="Dive for cover" Cost="100" SpeciesCanUseSkill="Human Mutant Genics_A Genics_B Aquatic Angelic Demonic Fun" Group="Stealth">
        <Description>Roll d12 under Ag if successful the model may dive to the nearest cover not more than their basic movement away</Description>
        <ModifiesStats />
        <RequiresPermission d2p1:type="SkillPermission" ID="BSDSB532A5C3" Name="Stealth" SpeciesCanEquip="All" />
      </anyType>
      <anyType d2p1:type="Skill" ID="BSDSFB715A61" Name="Ambush" Cost="200" SpeciesCanUseSkill="Human Genics_A Genics_B Aquatic Angelic Demonic Fun" Group="Stealth">
        <Description>Uses rules for showing up but it does not occur until d4 turns in to the game</Description>
        <ModifiesStats />
        <RequiresPermission d2p1:type="SkillPermission" ID="BSDSB532A5C3" Name="Stealth" SpeciesCanEquip="All" />
      </anyType>
      <anyType d2p1:type="Skill" ID="BSDS407E0C17" Name="Duck and Weave" Cost="200" SpeciesCanUseSkill="Human Mutant Genics_A Genics_B Aquatic Angelic Demonic Fun" Group="Stealth">
        <Description>When running from fire the model shooting has a –2 to hit that model</Description>
        <ModifiesStats />
        <RequiresPermission d2p1:type="SkillPermission" ID="BSDSB532A5C3" Name="Stealth" SpeciesCanEquip="All" />
      </anyType>
      <anyType d2p1:type="Skill" ID="BSDS7DC4C145" Name="Escape Artist" Cost="500" SpeciesCanUseSkill="All" Group="Stealth">
        <Description>Model may not be captured at the end of a battle</Description>
        <ModifiesStats />
        <RequiresPermission d2p1:type="SkillPermission" ID="BSDSB532A5C3" Name="Stealth" SpeciesCanEquip="All" />
      </anyType>
      <anyType d2p1:type="Skill" ID="BSDS49781AD9" Name="Cat Fall" Cost="100" SpeciesCanUseSkill="Human Mutant Genics_A Genics_B Angelic Demonic Fun" Group="Agility">
        <Description>The model rolls once for falling every inch until a success is rolled. </Description>
        <ModifiesStats />
        <RequiresPermission d2p1:type="SkillPermission" ID="BSDSDEC7D165" Name="Agility" SpeciesCanEquip="Human Mutant Genics_A Genics_B Angelic Demonic Fun" />
      </anyType>
      <anyType d2p1:type="Skill" ID="BSDSBE7F579B" Name="Leap" Cost="200" SpeciesCanUseSkill="Human Mutant Genics_A Genics_B Angelic Demonic Fun" Group="Agility">
        <Description>The model has superior jumping capabilities and only fails a jump on a 12</Description>
        <ModifiesStats />
        <RequiresPermission d2p1:type="SkillPermission" ID="BSDSDEC7D165" Name="Agility" SpeciesCanEquip="Human Mutant Genics_A Genics_B Angelic Demonic Fun" />
      </anyType>
      <anyType d2p1:type="Skill" ID="BSDSD41FAD54" Name="Gymnastics" Cost="300" SpeciesCanUseSkill="Human Mutant Genics_A Genics_B Angelic Demonic Fun" Group="Agility">
        <Description />
        <ModifiesStats>
          <StatsMod StatToMod="Agility" Modifier="1" />
        </ModifiesStats>
        <RequiresPermission d2p1:type="SkillPermission" ID="BSDSDEC7D165" Name="Agility" SpeciesCanEquip="Human Mutant Genics_A Genics_B Angelic Demonic Fun" />
      </anyType>
    </Objects>
  </Collectors>
  <Collectors d2p1:type="SkillPermissionCollector" xmlns:d2p1="http://www.w3.org/2001/XMLSchema-instance">
    <Objects>
      <anyType d2p1:type="SkillPermission" ID="BSDSDEC7D165" Name="Agility" SpeciesCanEquip="Human Mutant Genics_A Genics_B Angelic Demonic Fun" />
      <anyType d2p1:type="SkillPermission" ID="BSDSF8F13F38" Name="Arms Master" SpeciesCanEquip="All" />
      <anyType d2p1:type="SkillPermission" ID="BSDSF4D4D86A" Name="Arsonists" SpeciesCanEquip="Human Mutant Undead Genics_B Aquatic Fun" />
      <anyType d2p1:type="SkillPermission" ID="BSDSCEB1C6F7" Name="Gunman" SpeciesCanEquip="All" />
      <anyType d2p1:type="SkillPermission" ID="BSDS6389ED91" Name="Mechanical" SpeciesCanEquip="All" />
      <anyType d2p1:type="SkillPermission" ID="BSDSAD0CFF38" Name="Medic" SpeciesCanEquip="Human Mutant Aquatic Angelic Demonic Fun" />
      <anyType d2p1:type="SkillPermission" ID="BSDS7130F063" Name="Physical" SpeciesCanEquip="All" />
      <anyType d2p1:type="SkillPermission" ID="BSDS8A0C8A25" Name="Psionicist" SpeciesCanEquip="Human Undead Genics_A Genics_B Aquatic Angelic Demonic Fun" />
      <anyType d2p1:type="SkillPermission" ID="BSDSB532A5C3" Name="Stealth" SpeciesCanEquip="All" />
      <anyType d2p1:type="SkillPermission" ID="BSDS9DA4CA8A" Name="Trap Setter" SpeciesCanEquip="Human Mutant Genics_A Genics_B Aquatic Angelic Demonic Fun" />
    </Objects>
  </Collectors>
  <Collectors d2p1:type="WeaponCollector" xmlns:d2p1="http://www.w3.org/2001/XMLSchema-instance">
    <Objects>
      <anyType d2p1:type="Weapon" ID="BSDS3F633058" Name="Heat Fist" Range="0" MvsP="2" MvsA="0" SvMod="0" SIOR="0" Shots="0" Cost="100" BaseType="HandToHand">
        <RequiresPermission d2p1:type="WeaponPermission" ID="BSDS879CA176" Name="Bludgen" SpeciesCanEquip="All" />
        <Description />
        <Special />
      </anyType>
      <anyType d2p1:type="Weapon" ID="BSDS4A3EC11C" Name="Flail" Range="0" MvsP="1" MvsA="0" SvMod="0" SIOR="0" Shots="0" Cost="85" BaseType="HandToHand">
        <RequiresPermission d2p1:type="WeaponPermission" ID="BSDS879CA176" Name="Bludgen" SpeciesCanEquip="All" />
        <Description />
        <Special>M+4 on a charge +1 to disarm</Special>
      </anyType>
      <anyType d2p1:type="Weapon" ID="BSDSF9357E17" Name="Knife" Range="0" MvsP="0" MvsA="0" SvMod="0" SIOR="0" Shots="0" Cost="5" BaseType="Blade">
        <RequiresPermission d2p1:type="WeaponPermission" ID="BSDSFAC0DB1D" Name="Blade" SpeciesCanEquip="All" />
        <Description>Its a knife!</Description>
        <Special>Parry</Special>
      </anyType>
      <anyType d2p1:type="Weapon" ID="BSDS88F6AE9F" Name="Laser Knife" Range="0" MvsP="1" MvsA="0" SvMod="0" SIOR="0" Shots="0" Cost="40" BaseType="Blade">
        <RequiresPermission d2p1:type="WeaponPermission" ID="BSDSFAC0DB1D" Name="Blade" SpeciesCanEquip="All" />
        <Description>Oooo a sparkly knife!</Description>
        <Special>Parry</Special>
      </anyType>
      <anyType d2p1:type="Weapon" ID="BSDS5659247E" Name="Laser Sword" Range="0" MvsP="3" MvsA="1" SvMod="0" SIOR="0" Shots="0" Cost="110" BaseType="Blade">
        <RequiresPermission d2p1:type="WeaponPermission" ID="BSDSFAC0DB1D" Name="Blade" SpeciesCanEquip="All" />
        <Description>Similar to the Laser Knife but longer!</Description>
        <Special>Parry</Special>
      </anyType>
      <anyType d2p1:type="Weapon" ID="BSDSB3D4472B" Name="Mace" Range="0" MvsP="1" MvsA="0" SvMod="0" SIOR="0" Shots="0" Cost="60" BaseType="Bludgen">
        <RequiresPermission d2p1:type="WeaponPermission" ID="BSDS879CA176" Name="Bludgen" SpeciesCanEquip="All" />
        <Description>Lets go CLUBBIN!!!</Description>
        <Special>+2 on a charge</Special>
      </anyType>
      <anyType d2p1:type="Weapon" ID="BSDS28017D58" Name="Shield" Range="0" MvsP="0" MvsA="0" SvMod="0" SIOR="0" Shots="0" Cost="20" BaseType="Bludgen">
        <RequiresPermission d2p1:type="WeaponPermission" ID="BSDS879CA176" Name="Bludgen" SpeciesCanEquip="All" />
        <Description>protect the important stuff</Description>
        <Special>Causes a concussion on a roll of a natural 1 when attacking. +1 defensive dice</Special>
      </anyType>
      <anyType d2p1:type="Weapon" ID="BSDS913C52D9" Name="Sword" Range="0" MvsP="1" MvsA="0" SvMod="0" SIOR="0" Shots="0" Cost="15" BaseType="Blade">
        <RequiresPermission d2p1:type="WeaponPermission" ID="BSDSFAC0DB1D" Name="Blade" SpeciesCanEquip="All" />
        <Description> </Description>
        <Special>Parry</Special>
      </anyType>
      <anyType d2p1:type="Weapon" ID="BSDS73B8C134" Name="Whip" Range="2" MvsP="0" MvsA="2" SvMod="0" SIOR="0" Shots="0" Cost="5" BaseType="HandToHand">
        <RequiresPermission d2p1:type="WeaponPermission" ID="BSDSFAC0DB1D" Name="Blade" SpeciesCanEquip="All" />
        <Description>If you get the right hat you could play Indiana Jones!</Description>
        <Special>+1 to disarm</Special>
      </anyType>
      <anyType d2p1:type="Weapon" ID="BSDS860D572E" Name="Shotgun Slug" Range="20" MvsP="5" MvsA="3" SvMod="0" SIOR="5" Shots="1" Cost="175" BaseType="Gun">
        <RequiresPermission d2p1:type="WeaponPermission" ID="BSDSC1C35482" Name="Rifle" SpeciesCanEquip="All" />
        <Description> </Description>
        <Special> </Special>
      </anyType>
      <anyType d2p1:type="Weapon" ID="BSDS5C9EB1D7" Name="Shotgun Scatter" Range="20" MvsP="0" MvsA="2" SvMod="0" SIOR="5" Shots="1" Cost="175" BaseType="Gun">
        <RequiresPermission d2p1:type="WeaponPermission" ID="BSDSC1C35482" Name="Rifle" SpeciesCanEquip="All" />
        <Description> </Description>
        <Special>MvsP is determined by the weapons range</Special>
      </anyType>
      <anyType d2p1:type="Weapon" ID="BSDSBDC39F88" Name="Chain Saw" Range="0" MvsP="7" MvsA="4" SvMod="0" SIOR="8" Shots="0" Cost="130" BaseType="HandToHand">
        <RequiresPermission d2p1:type="WeaponPermission" ID="BSDS5AD1942E" Name="Mechanical Hand to Hand" SpeciesCanEquip="All" />
        <Description>This is my chan saw and this is my BOOM STICK!!!</Description>
        <Special>Runs on gas so it can run out, Parry</Special>
      </anyType>
      <anyType d2p1:type="Weapon" ID="BSDS7A3A98EB" Name="Grenade Standarded" Range="(Mx2)+6" MvsP="6" MvsA="6" SvMod="0" SIOR="11" Shots="1" Cost="25" BaseType="Thrown">
        <RequiresPermission d2p1:type="WeaponPermission" ID="BSDSF95E3BB8" Name="Thrown Weapons" SpeciesCanEquip="All" />
        <Description> </Description>
        <Special> </Special>
      </anyType>
      <anyType d2p1:type="Weapon" ID="BSDS7CD6E296" Name="Grenade MMRVE" Range="(Mx2)+6" MvsP="2" MvsA="1" SvMod="0" SIOR="11" Shots="1" Cost="30" BaseType="Thrown">
        <RequiresPermission d2p1:type="WeaponPermission" ID="BSDSF95E3BB8" Name="Thrown Weapons" SpeciesCanEquip="All" />
        <Description> Multi Magnetic Resonance Variable Explosive</Description>
        <Special>Causes all electrical systems with in a 12" radius to shut down for 1d3 turns, and anyhting with in a 6" radius to shutdown for 1d3 extra turns.</Special>
      </anyType>
      <anyType d2p1:type="Weapon" ID="BSDS65EBB5DF" Name="Grenade TRE" Range="(Mx2)+6" MvsP="0" MvsA="0" SvMod="0" SIOR="11" Shots="1" Cost="50" BaseType="Thrown">
        <RequiresPermission d2p1:type="WeaponPermission" ID="BSDSF95E3BB8" Name="Thrown Weapons" SpeciesCanEquip="All" />
        <Description />
        <Special>Lowers armour value for vehicles by 6 for d3 rounds.</Special>
      </anyType>
      <anyType d2p1:type="Weapon" ID="BSDS4AF472CD" Name="Large Rock" Range="(Mx2)+6" MvsP="1" MvsA="0" SvMod="0" SIOR="3" Shots="1" Cost="1" BaseType="Thrown">
        <RequiresPermission d2p1:type="WeaponPermission" ID="BSDSF95E3BB8" Name="Thrown Weapons" SpeciesCanEquip="All" />
        <Description />
        <Special>models may pick up rocks of the ground and hurl them after making a “shit I’m out roll”.  Models starting with large rocks must roll after the first is thrown.</Special>
      </anyType>
      <anyType d2p1:type="Weapon" ID="BSDS1B9E4ABE" Name="Small Rock" Range="(Mx2)+6" MvsP="0" MvsA="0" SvMod="0" SIOR="2" Shots="1" Cost="1" BaseType="Thrown">
        <RequiresPermission d2p1:type="WeaponPermission" ID="BSDSF95E3BB8" Name="Thrown Weapons" SpeciesCanEquip="All" />
        <Description />
        <Special>models may pick up rocks of the ground and hurl them after making a “shit I’m out roll”.  Models starting with large rocks must roll after the first is thrown.</Special>
      </anyType>
      <anyType d2p1:type="Weapon" ID="BSDSA43A1499" Name="Boltaction/Semautomatic Rifle" Range="30" MvsP="6" MvsA="4" SvMod="-2" SIOR="5" Shots="1" Cost="150" BaseType="Gun">
        <RequiresPermission d2p1:type="WeaponPermission" ID="BSDSC1C35482" Name="Rifle" SpeciesCanEquip="All" />
        <Description />
        <Special />
      </anyType>
      <anyType d2p1:type="Weapon" ID="BSDS3757EE62" Name="Full Auto Rifle" Range="24" MvsP="6" MvsA="4" SvMod="0" SIOR="7" Shots="2" Cost="150" BaseType="Gun">
        <RequiresPermission d2p1:type="WeaponPermission" ID="BSDSC1C35482" Name="Rifle" SpeciesCanEquip="All" />
        <Description />
        <Special />
      </anyType>
      <anyType d2p1:type="Weapon" ID="BSDS51A5A7F7" Name="Crossbow" Range="20" MvsP="4" MvsA="1" SvMod="0" SIOR="3" Shots="1" Cost="80" BaseType="Projectile">
        <RequiresPermission d2p1:type="WeaponPermission" ID="BSDS682A0477" Name="Bows" SpeciesCanEquip="All" />
        <Description />
        <Special />
      </anyType>
      <anyType d2p1:type="Weapon" ID="BSDS89E17FFA" Name="Bow" Range="24" MvsP="3" MvsA="1" SvMod="0" SIOR="3" Shots="1" Cost="80" BaseType="Projectile">
        <RequiresPermission d2p1:type="WeaponPermission" ID="BSDSFAC0DB1D" Name="Blade" SpeciesCanEquip="All" />
        <Description />
        <Special />
      </anyType>
      <anyType d2p1:type="Weapon" ID="BSDS10AD98BB" Name="Heat Gun" Range="24" MvsP="6" MvsA="6" SvMod="0" SIOR="6" Shots="1" Cost="170" BaseType="Gun">
        <RequiresPermission d2p1:type="WeaponPermission" ID="BSDS78536172" Name="Energy" SpeciesCanEquip="All" />
        <Description> </Description>
        <Special>When shooting at a target at half range or less the Heat guns MvsP=7 and MvsA=8</Special>
      </anyType>
      <anyType d2p1:type="Weapon" ID="BSDSBDDD93F0" Name="Grappling Hook" Range="40" MvsP="6" MvsA="4" SvMod="0" SIOR="10" Shots="1" Cost="150" BaseType="Gun">
        <RequiresPermission d2p1:type="WeaponPermission" ID="BSDS19EC42F8" Name="Pistole" SpeciesCanEquip="All" />
        <Description />
        <Special>Shoots every other turn, if a hit is scored, the model may pull itself to a vehicle, up/to a building, or another model to it with a might check and –2 (models may only be pulled if wounded)</Special>
      </anyType>
      <anyType d2p1:type="Weapon" ID="BSDSEA640AA4" Name="Laser Rifle" Range="20" MvsP="4" MvsA="4" SvMod="0" SIOR="4" Shots="1" Cost="90" BaseType="Gun">
        <RequiresPermission d2p1:type="WeaponPermission" ID="BSDS78536172" Name="Energy" SpeciesCanEquip="All" />
        <Description />
        <Special />
      </anyType>
      <anyType d2p1:type="Weapon" ID="BSDSC68E7851" Name="Blunderbus" Range="15" MvsP="4" MvsA="1" SvMod="0" SIOR="8" Shots="1" Cost="25" BaseType="Gun">
        <RequiresPermission d2p1:type="WeaponPermission" ID="BSDSC1C35482" Name="Rifle" SpeciesCanEquip="All" />
        <Description />
        <Special />
      </anyType>
      <anyType d2p1:type="Weapon" ID="BSDSD664470D" Name="Sling" Range="10" MvsP="3" MvsA="1" SvMod="0" SIOR="2" Shots="1" Cost="15" BaseType="Projectile">
        <RequiresPermission d2p1:type="WeaponPermission" ID="BSDSF95E3BB8" Name="Thrown Weapons" SpeciesCanEquip="All" />
        <Description />
        <Special />
      </anyType>
      <anyType d2p1:type="Weapon" ID="BSDSA8A44C21" Name="Revolver/Semiautomatic Pistol" Range="12" MvsP="5" MvsA="3" SvMod="0" SIOR="5" Shots="1" Cost="85" BaseType="Gun">
        <RequiresPermission d2p1:type="WeaponPermission" ID="BSDS19EC42F8" Name="Pistole" SpeciesCanEquip="All" />
        <Description />
        <Special />
      </anyType>
      <anyType d2p1:type="Weapon" ID="BSDSAF89E948" Name="Laser Pistole" Range="12" MvsP="4" MvsA="4" SvMod="0" SIOR="4" Shots="1" Cost="75" BaseType="Gun">
        <RequiresPermission d2p1:type="WeaponPermission" ID="BSDS78536172" Name="Energy" SpeciesCanEquip="All" />
        <Description />
        <Special />
      </anyType>
      <anyType d2p1:type="Weapon" ID="BSDS4F54675D" Name="Flame Thrower" Range="6" MvsP="4" MvsA="2" SvMod="0" SIOR="10" Shots="1" Cost="50" BaseType="Gun">
        <RequiresPermission d2p1:type="WeaponPermission" ID="BSDS6B0C799E" Name="Heavy" SpeciesCanEquip="All" />
        <Description />
        <Special>Models hit and wounded by a flamethrower are on fire on a 5+ and fall under the “dude your shit is on fire” rules.</Special>
      </anyType>
      <anyType d2p1:type="Weapon" ID="BSDS66F3D592" Name="Black Powder Pistol" Range="12" MvsP="4" MvsA="2" SvMod="0" SIOR="7" Shots="1" Cost="60" BaseType="Gun">
        <RequiresPermission d2p1:type="WeaponPermission" ID="BSDS19EC42F8" Name="Pistole" SpeciesCanEquip="All" />
        <Description />
        <Special />
      </anyType>
      <anyType d2p1:type="Weapon" ID="BSDS84337655" Name="NetGun" Range="10" MvsP="0" MvsA="0" SvMod="0" SIOR="12" Shots="1" Cost="70" BaseType="Gun">
        <RequiresPermission d2p1:type="WeaponPermission" ID="BSDS19EC42F8" Name="Pistole" SpeciesCanEquip="All" />
        <Description />
        <Special>Template 8", All models under the template are pinned on a 4+</Special>
      </anyType>
      <anyType d2p1:type="Weapon" ID="BSDSD0AC9AD5" Name="Rail Gun" Range="80" MvsP="11" MvsA="9" SvMod="0" SIOR="6" Shots="1" Cost="365" BaseType="Gun">
        <RequiresPermission d2p1:type="WeaponPermission" ID="BSDS6B0C799E" Name="Heavy" SpeciesCanEquip="All" />
        <Description />
        <Special>Move or Fire</Special>
      </anyType>
      <anyType d2p1:type="Weapon" ID="BSDS9348B270" Name="Chain Gun" Range="50" MvsP="7" MvsA="6" SvMod="0" SIOR="6" Shots="3" Cost="320" BaseType="Gun">
        <RequiresPermission d2p1:type="WeaponPermission" ID="BSDS6B0C799E" Name="Heavy" SpeciesCanEquip="All" />
        <Description />
        <Special />
      </anyType>
      <anyType d2p1:type="Weapon" ID="BSDS297E8120" Name="Rocket Launcher" Range="80" MvsP="11" MvsA="10" SvMod="0" SIOR="8" Shots="1" Cost="600" BaseType="Gun">
        <RequiresPermission d2p1:type="WeaponPermission" ID="BSDS6B0C799E" Name="Heavy" SpeciesCanEquip="All" />
        <Description />
        <Special>on impact creates a 1" explosion template for 1/2 dammage</Special>
      </anyType>
      <anyType d2p1:type="Weapon" ID="BSDS59AF2719" Name="Missile Launcher" Range="50" MvsP="8" MvsA="7" SvMod="0" SIOR="8" Shots="3" Cost="270" BaseType="Gun">
        <RequiresPermission d2p1:type="WeaponPermission" ID="BSDS6B0C799E" Name="Heavy" SpeciesCanEquip="All" />
        <Description />
        <Special>move and fire if the models strenght is 8 or greater, -2 to hit if allowed to move and fire. On impace creates a blast template of 1" for 1/2 dammage.</Special>
      </anyType>
      <anyType d2p1:type="Weapon" ID="BSDSFAC1C5D1" Name="Mortar" Range="60" MvsP="6" MvsA="6" SvMod="0" SIOR="8" Shots="1" Cost="320" BaseType="Gun">
        <RequiresPermission d2p1:type="WeaponPermission" ID="BSDS6B0C799E" Name="Heavy" SpeciesCanEquip="All" />
        <Description />
        <Special>No line of sight is required, shots may be guess placed creates a 2" blast template for 1/2 dammage</Special>
      </anyType>
      <anyType d2p1:type="Weapon" ID="BSDS383651FC" Name="Grenade Launcher" Range="48" MvsP="6" MvsA="7" SvMod="0" SIOR="8" Shots="1" Cost="200" BaseType="Gun">
        <RequiresPermission d2p1:type="WeaponPermission" ID="BSDS6B0C799E" Name="Heavy" SpeciesCanEquip="All" />
        <Description />
        <Special>creates a 4" blast template for 1/2 dammage</Special>
      </anyType>
      <anyType d2p1:type="Weapon" ID="BSDSC4CA0480" Name="HIgh Calibre Machine Gun" Range="40" MvsP="6" MvsA="4" SvMod="0" SIOR="6" Shots="6" Cost="225" BaseType="Gun">
        <RequiresPermission d2p1:type="WeaponPermission" ID="BSDS6B0C799E" Name="Heavy" SpeciesCanEquip="All" />
        <Description />
        <Special>Move and Fire, -2 to shoot when moved</Special>
      </anyType>
      <anyType d2p1:type="Weapon" ID="BSDS5A9D3372" Name="Low Calibre Machine Gun" Range="50" MvsP="5" MvsA="3" SvMod="0" SIOR="6" Shots="8" Cost="240" BaseType="Gun">
        <RequiresPermission d2p1:type="WeaponPermission" ID="BSDS6B0C799E" Name="Heavy" SpeciesCanEquip="All" />
        <Description />
        <Special>Move and Fire, -1 when moved</Special>
      </anyType>
      <anyType d2p1:type="Weapon" ID="BSDS8C3AB539" Name="Heat Cannon" Range="10" MvsP="3" MvsA="1" SvMod="0" SIOR="2" Shots="1" Cost="280" BaseType="Gun">
        <RequiresPermission d2p1:type="WeaponPermission" ID="BSDS6B0C799E" Name="Heavy" SpeciesCanEquip="All" />
        <Description />
        <Special>Move and Fire</Special>
      </anyType>
    </Objects>
  </Collectors>
  <Collectors d2p1:type="WeaponPermissionCollector" xmlns:d2p1="http://www.w3.org/2001/XMLSchema-instance">
    <Objects>
      <anyType d2p1:type="WeaponPermission" ID="BSDSFAC0DB1D" Name="Blade" SpeciesCanEquip="All" />
      <anyType d2p1:type="WeaponPermission" ID="BSDS879CA176" Name="Bludgen" SpeciesCanEquip="All" />
      <anyType d2p1:type="WeaponPermission" ID="BSDS13576692" Name="Sword" SpeciesCanEquip="All" />
      <anyType d2p1:type="WeaponPermission" ID="BSDS6B0C799E" Name="Heavy" SpeciesCanEquip="All" />
      <anyType d2p1:type="WeaponPermission" ID="BSDSC1C35482" Name="Rifle" SpeciesCanEquip="All" />
      <anyType d2p1:type="WeaponPermission" ID="BSDS19EC42F8" Name="Pistole" SpeciesCanEquip="All" />
      <anyType d2p1:type="WeaponPermission" ID="BSDS78536172" Name="Energy" SpeciesCanEquip="All" />
      <anyType d2p1:type="WeaponPermission" ID="BSDS682A0477" Name="Bows" SpeciesCanEquip="All" />
      <anyType d2p1:type="WeaponPermission" ID="BSDSC4366773" Name="Sniper" SpeciesCanEquip="All" />
      <anyType d2p1:type="WeaponPermission" ID="BSDSF95E3BB8" Name="Thrown Weapons" SpeciesCanEquip="All" />
      <anyType d2p1:type="WeaponPermission" ID="BSDS5AD1942E" Name="Mechanical Hand to Hand" SpeciesCanEquip="All" />
    </Objects>
  </Collectors>
</NTDataFileExt>