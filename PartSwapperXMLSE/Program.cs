using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using System.Xml;
using System.Xml.Linq;
using System.Windows;
using System.IO;
using System.Text;
using Microsoft.VisualBasic.FileIO;
using System.Numerics;
using System.Text.RegularExpressions;
using System;

namespace PartSwapper
{
    public class PartSwapper
    {
        static List<string> PartInclusions = new List<string>{
                "LargeAdvancedStator",
                "LargeHinge",
                "LargeHingeHead",
                "LargeLCDPanelWide",
                "LargeLCDPanel",
                "LargeLCDPanel3x3",
                "LargeLCDPanel5x3",
                "LargeLCDPanel5x5",
                "LargeProgrammableBlock",
                "LargeProgrammableBlockReskin",
                "BasicAssembler",
                "LargeAssembler",
                "LargeAssemblerIndustrial",
                "LargeBlockSlideDoor",
                "LargeBlockOffsetDoor",
                "SlidingHatchDoor",
                "SlidingHatchDoorHalf",
                "LargeBlockBatteryBlock",
                "LargeBlockBatteryBlockWarfare2",
                "LargeBlockBeacon",
                "SafeZoneBlock",
                "ButtonPanelLarge",
                "LargeSciFiButtonPanel",
                "LargeSciFiButtonTerminal",
                "VerticalButtonPanelLarge",
                "LargeBlockLargeContainer",
                "LargeBlockLargeIndustrialContainer",
                "LargeBlockSmallContainer",
                "CockpitOpen",
                "LargeBlockStandingCockpit",
                "LargeBlockCockpit",
                "LargeBlockCockpitSeat",
                "PassengerSeatLarge",
                "Collector",
                "LargeBlockConveyor",
                "LargeBlockConveyorSorter",
                "LargeBlockConveyorSorterIndustrial",
                "ConveyorTube",
                "ConveyorTubeDuct",
                "ConveyorTubeDuctCurved",
                "ConveyorTubeDuctT",
                "ConveyorTubeCurved",
                "ConveyorTubeT",
                "LargeBlockConveyorCap",
                "LargeBlockConveyorPipeJunction",
                "LargeBlockConveyorPipeIntersection",
                "LargeBlockConveyorPipeT",
                "LargeBlockConveyorPipeSeamless",
                "LargeBlockConveyorPipeCorner",
                "LargeBlockConveyorPipeFlange",
                "LargeBlockConveyorPipeEnd",
                "LargeBlockConveyorPipeCap",
                "ArmorAlpha",
                "ArmorCenter",
                "ArmorCorner",
                "ArmorInvCorner",
                "ArmorSide",
                "LargeBlockArmorBlock",
                "LargeHalfArmorBlock",
                "LargeHalfSlopeArmorBlock",
                "LargeBlockArmorCorner",
                "LargeBlockArmorCornerInv",
                "LargeBlockArmorSlope",
                "LargeBlockArmorRoundedSlope",
                "LargeBlockArmorRoundedCorner",
                "LargeBlockArmorAngledSlope",
                "LargeBlockArmorAngledCorner",
                "LargeHeavyBlockArmorRoundedSlope",
                "LargeHeavyBlockArmorRoundedCorner",
                "LargeHeavyBlockArmorAngledSlope",
                "LargeHeavyBlockArmorAngledCorner",
                "LargeBlockArmorRoundSlope",
                "LargeBlockArmorRoundCorner",
                "LargeBlockArmorRoundCornerInv",
                "LargeHeavyBlockArmorRoundSlope",
                "LargeHeavyBlockArmorRoundCorner",
                "LargeHeavyBlockArmorRoundCornerInv",
                "LargeBlockArmorSlope2BaseSmooth",
                "LargeHeavyHalfArmorBlock",
                "LargeHeavyHalfSlopeArmorBlock",
                "LargeHeavyBlockArmorRoundSlope",
                "LargeHeavyBlockArmorRoundCorner",
                "LargeHeavyBlockArmorRoundCornerInv",
                "LargeBlockArmorRoundSlope",
                "LargeBlockArmorSlope2BaseSmooth",
                "LargeBlockArmorSlope2TipSmooth",
                "LargeBlockArmorCorner2BaseSmooth",
                "LargeBlockArmorCorner2TipSmooth",
                "LargeBlockArmorInvCorner2BaseSmooth",
                "LargeBlockArmorInvCorner2TipSmooth",
                "LargeHeavyBlockArmorSlope2BaseSmooth",
                "LargeHeavyBlockArmorSlope2TipSmooth",
                "LargeHeavyBlockArmorCorner2BaseSmooth",
                "LargeHeavyBlockArmorCorner2TipSmooth",
                "LargeHeavyBlockArmorInvCorner2BaseSmooth",
                "LargeHeavyBlockArmorInvCorner2TipSmooth",
                "LargeBlockArmorSlope2Base",
                "LargeBlockArmorSlope2Tip",
                "LargeBlockArmorCorner2Base",
                "LargeBlockArmorCorner2Tip",
                "LargeBlockArmorInvCorner2Base",
                "LargeBlockArmorInvCorner2Tip",
                "LargeHeavyBlockArmorSlope2Base",
                "LargeHeavyBlockArmorSlope2Tip",
                "LargeHeavyBlockArmorCorner2Base",
                "LargeHeavyBlockArmorCorner2Tip",
                "LargeHeavyBlockArmorInvCorner2Base",
                "LargeHeavyBlockArmorInvCorner2Tip",

                "LargeBlockArmorHalfSlopeInverted",
                "LargeBlockHeavyArmorHalfSlopeInverted",
                "LargeBlockArmorHalfSlopeCorner",
                "LargeBlockHeavyArmorHalfSlopeCorner",
                "LargeBlockArmorHalfSlopeCornerInverted",
                "LargeBlockHeavyArmorHalfSlopeCornerInverted",
                "LargeBlockArmorSlopedCornerTip",
                "LargeBlockHeavyArmorSlopedCornerTip",
                "LargeBlockArmorSlopedCornerBase",
                "LargeBlockHeavyArmorSlopedCornerBase",
                "LargeBlockArmorSlopedCorner",
                "LargeBlockHeavyArmorSlopedCorner",
                "LargeBlockArmorHalfSlopedCornerBase",
                "LargeBlockHeavyArmorHalfSlopedCornerBase",
                "LargeBlockArmorHalfCorner",
                "LargeBlockHeavyArmorHalfCorner",
                "LargeBlockArmorCornerSquare",
                "LargeBlockHeavyArmorCornerSquare",
                "LargeBlockArmorCornerSquareInverted",
                "LargeBlockHeavyArmorCornerSquareInverted",
                "LargeBlockArmorHalfSlopedCorner",
                "LargeBlockHeavyArmorHalfSlopedCorner",

                "LargeBlockArmorRaisedSlopedCorner",
                "LargeBlockHeavyArmorRaisedSlopedCorner",
                "LargeBlockArmorSlopeTransition",
                "LargeBlockHeavyArmorSlopeTransition",
                "LargeBlockArmorSlopeTransitionBase",
                "LargeBlockHeavyArmorSlopeTransitionBase",
                "LargeBlockArmorSlopeTransitionBaseMirrored",
                "LargeBlockHeavyArmorSlopeTransitionBaseMirrored",
                "LargeBlockArmorSlopeTransitionMirrored",
                "LargeBlockHeavyArmorSlopeTransitionMirrored",
                "LargeBlockArmorSlopeTransitionTip",
                "LargeBlockHeavyArmorSlopeTransitionTip",
                "LargeBlockArmorSlopeTransitionTipMirrored",
                "LargeBlockHeavyArmorSlopeTransitionTipMirrored",
                "LargeBlockArmorSquareSlopedCornerBase",
                "LargeBlockHeavyArmorSquareSlopedCornerBase",
                "LargeBlockArmorSquareSlopedCornerTip",
                "LargeBlockHeavyArmorSquareSlopedCornerTip",
                "LargeBlockArmorSquareSlopedCornerTipInv",
                "LargeBlockHeavyArmorSquareSlopedCornerTipInv",

                "LargeBlockInteriorWall",
                "PipeWorkBlockA",
                "PipeWorkBlockB",
                "AngledInteriorWallA",
                "AngledInteriorWallB",
                "AirDuct1",
                "AirDuct2",
                "AirDuctLight",
                "AirDuctCorner",
                "AirDuctT",
                "AirDuctX",
                "AirDuctRamp",
                "AirDuctGrate",
                "LargeBlockCylindricalColumn",
                "LargeBlockSciFiWall",
                "LargeCoverWall",
                "LargeCoverWallHalf",
                "LargeCoverWallHalfMirrored",
                "LargeHeavyBlockArmorBlock",
                "LargeHeavyBlockArmorCorner",
                "LargeHeavyBlockArmorCornerInv",
                "LargeHeavyBlockArmorSlope",
                "LargeInteriorPillar",
                "LargeRailStraight",
                "LargeRamp",
                "LargeRoundArmor_Corner",
                "LargeRoundArmor_CornerInv",
                "LargeRoundArmor_Slope",
                "LargeStairs",
                "LargeSteelCatwalk",
                "LargeSteelCatwalk2Sides",
                "LargeSteelCatwalkCorner",
                "LargeSteelCatwalkPlate",
                "LargeWindowCen",
                "LargeWindowEdge",
                "LargeWindowSquare",
                "Window1x1Face",
                "Window1x1Flat",
                "Window1x1FlatInv",
                "Window1x1Inv",
                "Window1x1Side",
                "Window1x1Slope",
                "Window1x2Face",
                "Window1x2Flat",
                "Window1x2FlatInv",
                "Window1x2Inv",
                "Window1x2SideLeft",
                "Window1x2SideRight",
                "Window1x2Slope",
                "Window2x3Flat",
                "Window2x3FlatInv",
                "Window3x3Flat",
                "Window3x3FlatInv",
                "Window1x2SideLeftInv",
                "Window1x2SideRightInv",
                "Window1x1SideInv",
                "WindowRound",
                "WindowRoundInv",
                "WindowRoundCorner",
                "WindowRoundCornerInv",
                "WindowRoundFace",
                "WindowRoundFaceInv",
                "WindowRoundInwardsCorner",
                "WindowRoundInwardsCornerInv",
                "Viewport1",
                "Viewport2",
                "HalfWindow",
                "HalfWindowInv",
                "HalfWindowCorner",
                "HalfWindowCornerInv",
                "HalfWindowDiagonal",
                "HalfWindowRound",
                "BridgeWindow1x1Slope",
                "BridgeWindow1x1Face",
                "BridgeWindow1x1FaceInverted",

                "Embrasure",
                "DeadBody01",
                "DeadBody02",
                "DeadBody03",
                "DeadBody04",
                "DeadBody05",
                "DeadBody06",

                "LargeSymbolA",
                "LargeSymbolB",
                "LargeSymbolC",
                "LargeSymbolD",
                "LargeSymbolE",
                "LargeSymbolF",
                "LargeSymbolG",
                "LargeSymbolH",
                "LargeSymbolI",
                "LargeSymbolJ",
                "LargeSymbolK",
                "LargeSymbolL",
                "LargeSymbolM",
                "LargeSymbolN",
                "LargeSymbolO",
                "LargeSymbolP",
                "LargeSymbolQ",
                "LargeSymbolR",
                "LargeSymbolS",
                "LargeSymbolT",
                "LargeSymbolU",
                "LargeSymbolV",
                "LargeSymbolW",
                "LargeSymbolX",
                "LargeSymbolY",
                "LargeSymbolZ",
                "LargeSymbol0",
                "LargeSymbol1",
                "LargeSymbol2",
                "LargeSymbol3",
                "LargeSymbol4",
                "LargeSymbol5",
                "LargeSymbol6",
                "LargeSymbol7",
                "LargeSymbol8",
                "LargeSymbol9",
                "LargeSymbolHyphen",
                "LargeSymbolUnderscore",
                "LargeSymbolDot",
                "LargeSymbolApostrophe",
                "LargeSymbolAnd",
                "LargeSymbolColon",
                "LargeSymbolExclamationMark",
                "LargeSymbolQuestionMark",
                "LargeExhaustPipe",
                "LargeHeatVentBlock",
                "LargeDecoy",
                "(null)",
                "LargeBlockDrill",
                "LargeNeonTubesStraight1",
                "LargeNeonTubesStraight2",
                "LargeNeonTubesCorner",
                "LargeNeonTubesCorner90",
                "LargeNeonTubesBendUp",
                "LargeNeonTubesBendDown",
                "LargeNeonTubesStraightEnd1",
                "LargeNeonTubesStraightEnd2",
                "LargeNeonTubesU",
                "LargeNeonTubesT",
                "LargeNeonTubesCircle",
                "(null)",
                "(null)",
                "LargeBlockGyro",
                "SmallLight",
                "LargeLightPanel",
                "LargeBlockInsetLight",
                "LargeInteriorTurret",
                "LargeBlockLandingGear",
                "LargeBlockMagneticPlate",
                "LargeBlockSmallMagneticPlate",
                "(null)",
                "(null)",
                "LargeCalibreTurret",
                "LargeBlockMediumCalibreTurret",
                "LargeMedicalRoom",
                "LargeShipMergeBlock",
                "LargeRotor",
                "LargeStator",
                "Suspension1x1",
                "Suspension2x2",
                "Suspension3x3",
                "Suspension5x5",
                "Suspension1x1mirrored",
                "Suspension2x2Mirrored",
                "Suspension3x3mirrored",
                "Suspension5x5mirrored",
                "OffroadSuspension1x1",
                "OffroadSuspension2x2",
                "OffroadSuspension3x3",
                "OffroadSuspension5x5",
                "OffroadSuspension1x1mirrored",
                "OffroadSuspension2x2Mirrored",
                "OffroadSuspension3x3mirrored",
                "OffroadSuspension5x5mirrored",
                "LargeOreDetector",
                "(null)",
                "Passage2",
                "Passage2Wall",
                "PassageSciFi",
                "PassageSciFiLight",
                "PassageSciFiWall",
                "PassageSciFiIntersection",
                "PassageSciFiGate",
                "PassageScifiCorner",
                "PassageSciFiTjunction",
                "PassageSciFiWindow",
                "LargePistonBase",
                "LargePistonTop",
                "LargeProjector",
                "LargeBlockRadioAntenna",
                "LargeBlockRadioAntennaDish",
                "LargeBlockLargeGenerator",
                "LargeBlockSmallGenerator",
                "LargeBlockLargeGeneratorWarfare2",
                "LargeBlockSmallGeneratorWarfare2",
                "LargeRefinery",
                "LargeRefineryIndustrial",
                "Blast Furnace",
                "LargeBlockFrontLight",
                "LargeBlockRemoteControl",
                "LargeTurretControlBlock",
                "LargeBlockSensor",
                "LargeBlockSensorReskin",
                "Connector",
                "LargeShipGrinder",
                "LargeShipWelder",
                "LargeMissileLauncher",
                "LargeRailgun",
                "LargeBlockLargeCalibreGun",
                "SpaceBallLarge",
                "LargeBlockSolarPanel",
                "LargeBlockSoundBlock",
                "ControlPanel",
                "LargeBlockAccessPanel1",
                "LargeBlockAccessPanel2",
                "LargeBlockAccessPanel3",
                "LargeBlockAccessPanel4",
                "LargeBlockSciFiTerminal",
                "LargeTextPanel",
                "LargeCameraBlock",
                "LargeCameraTopMounted",
                "LargeBlockLargeThrust",
                "LargeBlockSmallThrust",
                "LargeBlockLargeThrustSciFi",
                "LargeBlockSmallThrustSciFi",
                "LargeBlockLargeAtmosphericThrust",
                "LargeBlockSmallAtmosphericThrust",
                "LargeBlockLargeAtmosphericThrustSciFi",
                "LargeBlockSmallAtmosphericThrustSciFi",
                "LargeBlockLargeHydrogenThrust",
                "LargeBlockSmallHydrogenThrust",
                "LargeBlockLargeHydrogenThrustIndustrial",
                "LargeBlockSmallHydrogenThrustIndustrial",
                "LargeBlockLargeModularThruster",
                "LargeBlockSmallModularThruster",
                "VirtualMassLarge",
                "LargeWarhead",
                "RealWheel",
                "RealWheel1x1",
                "RealWheel5x5",
                "Wheel1x1",
                "Wheel2x2",
                "Wheel3x3",
                "Wheel5x5",
                "OffroadRealWheel",
                "OffroadRealWheel1x1",
                "OffroadRealWheel5x5",
                "OffroadWheel1x1",
                "OffroadWheel2x2",
                "OffroadWheel3x3",
                "OffroadWheel5x5",
                "TimerBlockLarge",
                "TimerBlockReskinLarge",
                "EventControllerLarge",
                "LargeBlockLaserAntenna",
                "(null)",
                "(null)",
                "LargeHydrogenTank",
                "LargeHydrogenTankSmall",
                "LargeHydrogenTankIndustrial",
                "(null)",
                "AirVentFull",
                "AirVentFan",
                "AirVentFanFull",
                "(null)",
                "AirtightHangarDoorWarfare2A",
                "AirtightHangarDoorWarfare2B",
                "AirtightHangarDoorWarfare2C",
                "LargeBlockGate",
                "LargeBlockOxygenFarm",
                "LargeBlockCryoChamber",
                "LargeProductivityModule",
                "LargeEffectivenessModule",
                "LargeEnergyModule",
                "LargeJumpDrive",
                "LargeBlockWindTurbine",
                "LargeHydrogenEngine",
                "SurvivalKitLarge",
                "(null)",
                "LadderShaft",
                "LargeBlockBed",
                "LargeBlockDesk",
                "LargeBlockDeskCorner",
                "LargeBlockDeskCornerInv",
                "LargeBlockDeskChairless",
                "LargeBlockDeskChairlessCorner",
                "LargeBlockDeskChairlessCornerInv",
                "LargeBlockCouch",
                "LargeBlockCouchCorner",
                "LargeBlockKitchen",
                "LargeBlockPlanters",
                "LargeBlockBarCounter",
                "LargeBlockBarCounterCorner",
                "LargeBlockLockerRoom",
                "LargeBlockLockerRoomCorner",
                "LargeBlockLockers",
                "LargeBlockWeaponRack",
                "FireCover",
                "FireCoverCorner",
                "LargeBlockBathroomOpen",
                "LargeBlockBathroom",
                "LargeBlockToilet",
                "LargeBlockConsole",
                "LargeBlockCockpitIndustrial",
                "StoreBlock",
                "SafeZoneBlock",
                "ContractBlock",
                "VendingMachine",
                "AtmBlock",
                "FoodDispenser",
                "FoodDispenser",
                "Jukebox",
                "OpenCockpitLarge",
                "LabEquipment",
                "Shower",
                "WindowWall",
                "WindowWallLeft",
                "WindowWallRight",
                "MedicalStation",
                "TransparentLCDLarge",
                "TransparentLCDSmall",
                "Catwalk",
                "CatwalkCorner",
                "CatwalkStraight",
                "CatwalkWall",
                "CatwalkRailingEnd",
                "CatwalkRailingHalfRight",
                "CatwalkRailingHalfLeft",
                "CatwalkHalf",
                "CatwalkHalfRailing",
                "CatwalkHalfCenterRailing",
                "CatwalkHalfOuterRailing",
                "GratedStairs",
                "GratedHalfStairs",
                "GratedHalfStairsMirrored",
                "RailingStraight",
                "RailingDouble",
                "RailingCorner",
                "RailingDiagonal",
                "RailingHalfRight",
                "RailingHalfLeft",
                "RailingCenter",
                "Railing2x1Right",
                "Railing2x1Left",
                "RotatingLightLarge",
                "RotatingLightSmall",
                "Freight1",
                "Freight2",
                "Freight3",
                "StorageShelf1",
                "StorageShelf2",
                "StorageShelf3",
                "LargeSearchlight",
                "TargetDummy",

                "LargeGridBeamBlock",
                "LargeGridBeamBlockSlope",
                "LargeGridBeamBlockRound",
                "LargeGridBeamBlockSlope2x1Base",
                "LargeGridBeamBlockSlope2x1Tip",
                "LargeGridBeamBlockHalf",
                "LargeGridBeamBlockHalfSlope",
                "LargeGridBeamBlockEnd",
                "LargeGridBeamBlockJunction",
                "LargeGridBeamBlockTJunction",

                "LargeArmorPanelLight",
                "LargeArmorCenterPanelLight",
                "LargeArmorSlopedSidePanelLight",
                "LargeArmorSlopedPanelLight",
                "LargeArmorHalfPanelLight",
                "LargeArmorHalfCenterPanelLight",
                "LargeArmorQuarterPanelLight",
                "LargeArmor2x1SlopedPanelLight",
                "LargeArmor2x1SlopedPanelTipLight",
                "LargeArmor2x1SlopedSideBasePanelLight",
                "LargeArmor2x1SlopedSideTipPanelLight",
                "LargeArmor2x1SlopedSideBasePanelLightInv",
                "LargeArmor2x1SlopedSideTipPanelLightInv",
                "LargeArmorHalfSlopedPanelLight",
                "LargeArmor2x1HalfSlopedPanelLightRight",
                "LargeArmor2x1HalfSlopedTipPanelLightRight",
                "LargeArmor2x1HalfSlopedPanelLightLeft",
                "LargeArmor2x1HalfSlopedTipPanelLightLeft",

                "LargeArmorPanelHeavy",
                "LargeArmorCenterPanelHeavy",
                "LargeArmorSlopedSidePanelHeavy",
                "LargeArmorSlopedPanelHeavy",
                "LargeArmorHalfPanelHeavy",
                "LargeArmorHalfCenterPanelHeavy",
                "LargeArmorQuarterPanelHeavy",
                "LargeArmor2x1SlopedPanelHeavy",
                "LargeArmor2x1SlopedPanelTipHeavy",
                "LargeArmor2x1SlopedSideBasePanelHeavy",
                "LargeArmor2x1SlopedSideTipPanelHeavy",
                "LargeArmor2x1SlopedSideBasePanelHeavyInv",
                "LargeArmor2x1SlopedSideTipPanelHeavyInv",
                "LargeArmorHalfSlopedPanelHeavy",
                "LargeArmor2x1HalfSlopedPanelHeavyRight",
                "LargeArmor2x1HalfSlopedTipPanelHeavyRight",
                "LargeArmor2x1HalfSlopedPanelHeavyLeft",
                "LargeArmor2x1HalfSlopedTipPanelHeavyLeft",

                "LargeWarningSign1",
                "LargeWarningSign2",
                "LargeWarningSign3",
                "LargeWarningSign4",
                "LargeWarningSign5",
                "LargeWarningSign6",
                "LargeWarningSign7",
                "LargeWarningSign8",
                "LargeWarningSign9",
                "LargeWarningSign10",
                "LargeWarningSign11",
                "LargeWarningSign12",
                "LargeWarningSign13",

                "LargePathRecorderBlock",
                "LargeBasicMission",
                "LargeFlightMovement",
                "LargeDefensiveCombat",
                "LargeOffensiveCombat",
                "EmotionControllerLarge", };

        // declarations for collections of ship parts and their category names, as well as modblocks
        public Dictionary<string, List<XElement>> ShipParts = new Dictionary<string, List<XElement>>();

        public Dictionary<string, HashSet<string>> BlockVariantsAvail;

        public PartSwapper(string filename)
        {
            ShipParts = GenerateShipPartsList2(filename);
            BlockVariantsAvail = LoadBlockVariantsDict();
            BlockVariantsAvail["default"] = PartInclusions.ToHashSet();
        }

        public static void RenderTextIntro()
        {

            string welcome = "<--Picarl's PartSwapper-->\n";

            RenderSlowColoredText(welcome, 30, ConsoleColor.Black, ConsoleColor.Green);

        }

        public static DirectoryInfo[] GetLocalDirectories()
        {
            DirectoryInfo Directory = new DirectoryInfo(System.IO.Directory.GetCurrentDirectory());
            DirectoryInfo[] LocalDirectories = Directory.GetDirectories();
            bool debug = false;

            if (debug)
            {
                Console.WriteLine($"DEBUG: Partswapper is working out of the following directory:\n{Directory}");
            }

            return LocalDirectories;
        }

        public static void REPL(string inputShipSBC)
        {
            // flags
            bool quitflag = false;
            bool debug = false;

            // ints
            int userInputInt = -1;

            // strings
            string userInput = "";
            string inputPartSwapOut;
            string inputPartSwapIn;

            //...you get it by now, yes?
            List<string> UserPartCategoriesOpts = new List<string>();
            List<string> UserShipCurrCatParts = new List<string>();

            // partSwapper represents the ship we want to work on.
            PartSwapper partSwapper;

            while (!quitflag)
            {
                PruneCubeGridsFromShipBlueprint(inputShipSBC, debug);

                partSwapper = new PartSwapper(inputShipSBC);

                PartSwapper.RenderSlowColoredText("Ship loaded.\n", 10, ConsoleColor.Cyan);

                if (debug)
                {
                    foreach (string blocktype in partSwapper.BlockVariantsAvail.Keys)
                    {
                        Console.WriteLine("Found the following blockvariant key:");
                        Console.WriteLine(blocktype);
                        Console.WriteLine("Found the following blockvariant values:");
                        foreach (string value in partSwapper.BlockVariantsAvail[blocktype])
                        {
                            Console.WriteLine(value);
                        }

                    }

                }

                PartSwapper.RenderSlowColoredText("Found the following parts on your ship:\n", 10, ConsoleColor.Cyan);

                foreach (string category in partSwapper.ShipParts.Keys)
                {
                    //Console.WriteLine($"{category} = {partSwapper.parts[category].Count}");
                    PartSwapper.RenderSlowColoredText($"{category} = {partSwapper.ShipParts[category].Count}\n", 0, ConsoleColor.DarkYellow);
                }

                PartSwapper.RenderSlowColoredText($"\nPlease select which category of parts you would like to swap:\n", 10, ConsoleColor.Magenta);

                PartSwapper.RenderSlowColoredText($"1. Gyroscopes\n2. Ion Thrusters\n3. Hydrogen Thrusters\n4. Batteries\n" +
                    $"5. Jump Drives\n6. Hydrogen Tanks\n7. Oxygen Tanks\n" +
                    $"8. Cargo Containers\n9. Drills\n10. Conveyors\n11.ArmorSwapper\n12.RefinerySwapper\n13.AssemblerSwapper" +
                    $"\nQ to quit editing this file.\nSelection > ", 10, ConsoleColor.DarkMagenta);


                userInput = Console.ReadLine();

                if (IsUserQuitting(userInput.ToUpper(), debug))
                {
                    return;
                }

                switch ((int.Parse(userInput)))
                {
                    case 1:
                        GyroSwapperTier(partSwapper, inputShipSBC);
                        break;
                    case 2:
                        IonThrusterSwapperTier(partSwapper, inputShipSBC);
                        break;
                    case 3:
                        HydrogenThrusterSwapperTier(partSwapper, inputShipSBC);
                        break;
                    case 4:
                        BatterySwapperTier(partSwapper, inputShipSBC);
                        break;
                    case 5:
                        JumpDriveSwapperTier(partSwapper, inputShipSBC);
                        break;
                    case 6:
                        HydroTankSwapperTier(partSwapper, inputShipSBC);
                        break;
                    case 7:
                        OxygenTankSwapperTier(partSwapper, inputShipSBC);
                        break;
                    case 8:
                        CargoContainerSwapperTier(partSwapper, inputShipSBC);
                        break;
                    case 9:
                        DrillSwapperTier(partSwapper, inputShipSBC);
                        break;
                    case 10:
                        ConveyorSwapperTier(partSwapper, inputShipSBC);
                        break;
                    case 11:
                        ArmorSwapper(partSwapper, inputShipSBC);
                        break;
                    case 12:
                        RefinerySwapper(partSwapper, inputShipSBC);
                        break;
                    case 13:
                        AssemblerSwapper(partSwapper, inputShipSBC);
                        break;
                    default:
                        Console.WriteLine("You chose something absurd. I'm quitting. Bye!\n");
                        return;
                }

                Console.WriteLine("Swap another part in this ship? Y/N");
                userInput = Console.ReadLine();

                switch (userInput.ToUpper())
                {
                    case "Y":
                        PartSwapper.RenderSlowColoredText("Swapping another part!\n", 5, ConsoleColor.Red);
                        break;
                    case "N":
                        Console.WriteLine("Ending Program!");
                        quitflag = true;
                        break;
                }
            }
        }

        public static void RenderSlowConsoleText(string text, int delay)
        {
            foreach (char letter in text)
            {
                Thread.Sleep(delay);
                Console.Write(letter);
            }

            // might need to add a newline here, test behavior.
        }

        public static void RenderSlowColoredText(string text, int delay, ConsoleColor color)
        {
            ConsoleColor preserve = Console.ForegroundColor;

            Console.ForegroundColor = color;
            Console.BackgroundColor = ConsoleColor.Black;

            foreach (char letter in text)
            {
                Thread.Sleep(delay);
                Console.Write(letter);
            }
            Console.ForegroundColor = preserve;
        }

        public static void RenderSlowColoredText(string text, int delay, ConsoleColor foregroundColor, ConsoleColor backgroundColor)
        {
            ConsoleColor preserveFG = Console.ForegroundColor;
            ConsoleColor preserveBG = Console.BackgroundColor;

            Console.ForegroundColor = foregroundColor;
            Console.BackgroundColor = backgroundColor;

            foreach (char letter in text)
            {
                Thread.Sleep(delay);
                Console.Write(letter);
            }

            Console.ForegroundColor = preserveFG;
            Console.BackgroundColor = preserveBG;
        }

        public static void PartSwapTUI(PartSwapper partSwapper, string inputShipSBC, string partCategoryString)
        {
            bool debug = false;
            string userInput = "";
            int userInputInt = -1;
            string inputPartSwapOut;
            string inputPartSwapIn;

            List<string> UserPartCategoriesOpts = new List<string>();
            List<string> UserShipCurrCatParts = new List<string>();

            foreach (string blockvariant in partSwapper.BlockVariantsAvail.Keys)
            {
                foreach (string blocktype in partSwapper.BlockVariantsAvail[blockvariant])
                {

                    if (blocktype.ToUpper().Contains(partCategoryString))
                    {
                        UserPartCategoriesOpts.Add(blocktype);
                    }
                    else
                    {
                        continue;
                    }

                }
            }

            foreach (string blocktype in partSwapper.ShipParts.Keys)
            {
                if (blocktype.ToUpper().Contains(partCategoryString))
                {

                    PartSwapper.RenderSlowColoredText($"Found block type {blocktype} on your ship! Eligible for replacement!\n", 10, ConsoleColor.Green);
                    UserShipCurrCatParts.Add(blocktype);

                }
            }

            UserPruneList(UserShipCurrCatParts);

            // Iterates through the blockvariants that are on the ship already, and the relevant blockvariants we found from blockvariant files,
            // And then we offer the user all the options for swapping out parts.
            foreach (string partCategory in UserShipCurrCatParts)
            {
                string blockvarSubstring = partCategory.Remove(partCategory.Length - 3);

                // the declaration of relatedCategories pulls in categories from the entirety of categories of ship parts, sorting by those categories that contain our partCategory string, minus 2 chars.
                List<string> relatedCategories = UserPartCategoriesOpts.Where(item => item.Contains(partCategory.Substring(0, partCategory.Length - 2))).Distinct<string>().ToList();

                for (int i = 0; i < relatedCategories.Count<string>(); i++)
                {
                    Console.WriteLine($"{i} - {relatedCategories[i]}");
                }

                Console.WriteLine($"What should we replace {partCategory} with? C to continue.");

                userInput = Console.ReadLine();

                if (IsUserQuitting(userInput.ToUpper(), debug))
                {
                    return;
                }

                if (userInput.ToUpper() == "C")
                {
                    Console.WriteLine("Continuing.");
                    continue;
                }

                userInputInt = int.Parse(userInput);

                PartSwapper.SwapPartsViaPartname(inputShipSBC, partCategory, relatedCategories[userInputInt], false);

                Console.WriteLine($"{partCategory} has been replaced with {relatedCategories[userInputInt]}");
            }
        }

        // TODO: figure out if this generic swapper is doable and worth it.
        // The idea is that is uses a switch and some given 'input category string'
        public static void GenericSwapper(PartSwapper partSwapper, string inputShipSBC, string partCategory)
        {
            int userInputInt = -1;
            string userInput = "";
            string inputPartSwapOut;
            string inputPartSwapIn;



            List<string> UserPartCategoriesOpts = new List<string>();
            List<string> UserShipCurrCatParts = new List<string>();

            foreach (string blockvariant in partSwapper.BlockVariantsAvail.Keys)
            {
                foreach (string blocktype in partSwapper.BlockVariantsAvail[blockvariant])
                {

                    switch (partCategory.ToUpper())
                    {
                        case "IONTHRUST":
                            if (blocktype.ToUpper().Contains("THRUST"))
                            {
                                if (!blocktype.ToUpper().Contains("HYDRO") && !blocktype.ToUpper().Contains("ATMO"))
                                {
                                    UserPartCategoriesOpts.Add(blocktype);
                                }
                                else
                                {
                                    continue;
                                }
                            }
                            break;
                        default:
                            break;
                    }


                }
            }


            foreach (string blocktype in partSwapper.ShipParts.Keys)
            {
                if (blocktype.ToUpper().Contains("THRUST"))
                {
                    if (!blocktype.ToUpper().Contains("HYDRO") && !blocktype.ToUpper().Contains("ATMO"))
                    {
                        UserShipCurrCatParts.Add(blocktype);
                        Console.WriteLine($"Found blocktype {blocktype}, all instances will be replaced!");
                    }
                    else
                    {
                        continue;
                    }
                }
            }

            UserPruneList(UserShipCurrCatParts);

            Console.WriteLine("What should we replace the above parts with?\n Selection > ");

            // Next, Iterate through what we found, and offer the user options:
            for (int i = 0; i < UserPartCategoriesOpts.Count; i++)
            {
                Console.WriteLine($"{i} - {UserPartCategoriesOpts[i]}");
            }

            userInput = Console.ReadLine();
            userInputInt = int.Parse(userInput);

            Console.WriteLine($"Replacing all with {UserPartCategoriesOpts[userInputInt]}");

            // Finally: Replace.
            foreach (string option in UserShipCurrCatParts)
            {
                SwapPartsViaPartname(inputShipSBC, option, UserPartCategoriesOpts[userInputInt], false);
            }
        }

        public static void AssemblerSwapper(PartSwapper partSwapper, string inputShipSBC)
        {
            bool debug = false;

            string userInput = "";
            int userInputInt = -1;
            string inputPartSwapOut;
            string inputPartSwapIn;

            List<string> UserPartCategoriesOpts = new List<string>();
            List<string> UserShipCurrCatParts = new List<string>();

            foreach (string blockvariant in partSwapper.BlockVariantsAvail.Keys)
            {
                foreach (string blocktype in partSwapper.BlockVariantsAvail[blockvariant])
                {

                    if (blocktype.ToUpper().Contains("ASSEMBLER"))
                    {
                        UserPartCategoriesOpts.Add(blocktype);
                    }
                    else
                    {
                        continue;
                    }

                }
            }

            foreach (string blocktype in partSwapper.ShipParts.Keys)
            {
                if (blocktype.ToUpper().Contains("ASSEMBLER"))
                {

                    PartSwapper.RenderSlowColoredText($"Found block type {blocktype} on your ship! Eligible for replacement!\n", 10, ConsoleColor.Green);
                    UserShipCurrCatParts.Add(blocktype);

                }
            }

            UserPruneList(UserShipCurrCatParts);

            // Iterates through the blockvariants that are on the ship already, and the relevant blockvariants we found from blockvariant files,
            // And then we offer the user all the options for swapping out parts.
            foreach (string blockvariant in UserShipCurrCatParts)
            {
                string blockvarSubstring = blockvariant.Remove(blockvariant.Length - 3);
                List<string> tieredBlocks = UserPartCategoriesOpts.Where(item => item.Contains(blockvariant.Substring(0, blockvariant.Length - 2))).Distinct<string>().ToList();

                for (int i = 0; i < tieredBlocks.Count<string>(); i++)
                {
                    Console.WriteLine($"{i} - {tieredBlocks[i]}");
                }

                Console.WriteLine($"What should we replace {blockvariant} with? C to continue.");

                userInput = Console.ReadLine();

                if (IsUserQuitting(userInput.ToUpper(), debug))
                {
                    return;
                }

                if (userInput.ToUpper() == "C")
                {
                    Console.WriteLine("Continuing.");
                    continue;
                }

                userInputInt = int.Parse(userInput);

                PartSwapper.SwapPartsViaPartname(inputShipSBC, blockvariant, tieredBlocks[userInputInt], false);

                Console.WriteLine($"{blockvariant} has been replaced with {tieredBlocks[userInputInt]}");
            }
        }

        public static void RefinerySwapper(PartSwapper partSwapper, string inputShipSBC)
        {
            bool debug = false;

            string userInput = "";
            int userInputInt = -1;
            string inputPartSwapOut;
            string inputPartSwapIn;

            List<string> UserPartCategoriesOpts = new List<string>();
            List<string> UserShipCurrCatParts = new List<string>();

            foreach (string blockvariant in partSwapper.BlockVariantsAvail.Keys)
            {
                foreach (string blocktype in partSwapper.BlockVariantsAvail[blockvariant])
                {

                    if (blocktype.ToUpper().Contains("REFINERY"))
                    {
                        UserPartCategoriesOpts.Add(blocktype);
                    }
                    else
                    {
                        continue;
                    }

                }
            }

            foreach (string blocktype in partSwapper.ShipParts.Keys)
            {
                if (blocktype.ToUpper().Contains("REFINERY"))
                {

                    PartSwapper.RenderSlowColoredText($"Found block type {blocktype} on your ship! Eligible for replacement!\n", 10, ConsoleColor.Green);
                    UserShipCurrCatParts.Add(blocktype);

                }
            }

            UserPruneList(UserShipCurrCatParts);

            // Iterates through the blockvariants that are on the ship already, and the relevant blockvariants we found from blockvariant files,
            // And then we offer the user all the options for swapping out parts.
            foreach (string blockvariant in UserShipCurrCatParts)
            {
                string blockvarSubstring = blockvariant.Remove(blockvariant.Length - 3);
                List<string> tieredBlocks = UserPartCategoriesOpts.Where(item => item.Contains(blockvariant.Substring(0, blockvariant.Length - 2))).Distinct<string>().ToList();

                for (int i = 0; i < tieredBlocks.Count<string>(); i++)
                {
                    Console.WriteLine($"{i} - {tieredBlocks[i]}");
                }

                Console.WriteLine($"What should we replace {blockvariant} with? C to continue.");

                userInput = Console.ReadLine();

                if (IsUserQuitting(userInput.ToUpper(), debug))
                {
                    return;
                }

                if (userInput.ToUpper() == "C")
                {
                    Console.WriteLine("Continuing.");
                    continue;
                }

                userInputInt = int.Parse(userInput);

                PartSwapper.SwapPartsViaPartname(inputShipSBC, blockvariant, tieredBlocks[userInputInt], false);

                Console.WriteLine($"{blockvariant} has been replaced with {tieredBlocks[userInputInt]}");
            }
        }

        public static void IonThrusterSwapper(PartSwapper partSwapper, string inputShipSBC)
        {
            string userInput = "";
            int userInputInt = -1;
            string inputPartSwapOut;
            string inputPartSwapIn;

            List<string> UserPartCategoriesOpts = new List<string>();
            List<string> UserShipCurrCatParts = new List<string>();

            foreach (string blockvariant in partSwapper.BlockVariantsAvail.Keys)
            {
                foreach (string blocktype in partSwapper.BlockVariantsAvail[blockvariant])
                {
                    if (blocktype.ToUpper().Contains("REFINERY"))
                    {

                        UserPartCategoriesOpts.Add(blocktype);

                    }
                }
            }


            foreach (string blocktype in partSwapper.ShipParts.Keys)
            {
                if (blocktype.ToUpper().Contains("THRUST"))
                {
                    if (!blocktype.ToUpper().Contains("HYDRO") && !blocktype.ToUpper().Contains("ATMO"))
                    {
                        UserShipCurrCatParts.Add(blocktype);
                        Console.WriteLine($"Found blocktype {blocktype}, all instances will be replaced!");
                    }
                    else
                    {
                        continue;
                    }
                }
            }

            UserPruneList(UserShipCurrCatParts);

            Console.WriteLine("What should we replace the above parts with?\n Selection > ");

            // Next, Iterate through what we found, and offer the user options:
            for (int i = 0; i < UserPartCategoriesOpts.Count; i++)
            {
                Console.WriteLine($"{i} - {UserPartCategoriesOpts[i]}");
            }

            userInput = Console.ReadLine();
            userInputInt = int.Parse(userInput);

            Console.WriteLine($"Replacing all with {UserPartCategoriesOpts[userInputInt]}");

            // Finally: Replace.
            foreach (string option in UserShipCurrCatParts)
            {
                SwapPartsViaPartname(inputShipSBC, option, UserPartCategoriesOpts[userInputInt], false);
            }
        }
        public static void HydrogenThrusterSwapper(PartSwapper partSwapper, string inputShipSBC)
        {
            string userInput = "";
            int userInputInt = -1;
            string inputPartSwapOut;
            string inputPartSwapIn;

            List<string> UserPartCategoriesOpts = new List<string>();
            List<string> UserShipCurrCatParts = new List<string>();

            foreach (string blockvariant in partSwapper.BlockVariantsAvail.Keys)
            {
                foreach (string blocktype in partSwapper.BlockVariantsAvail[blockvariant])
                {
                    Console.WriteLine($"{blocktype}");

                    if (blocktype.ToUpper().Contains("HYDRO") && blocktype.ToUpper().Contains("THRUST"))
                    {
                        UserPartCategoriesOpts.Add(blocktype);
                    }
                    else
                    {
                        continue;
                    }
                }
            }


            foreach (string blocktype in partSwapper.ShipParts.Keys)
            {
                if (blocktype.ToUpper().Contains("HYDRO") && blocktype.ToUpper().Contains("THRUST"))
                {
                    UserShipCurrCatParts.Add(blocktype);
                    Console.WriteLine($"Found blocktype {blocktype}, all instances of this type will be replaced!");
                }
                else
                {
                    continue;
                }

            }

            UserPruneList(UserShipCurrCatParts);

            Console.WriteLine("What should we replace the above parts with?");

            // Next, Iterate through what we found, and offer the user options:
            for (int i = 0; i < UserPartCategoriesOpts.Count; i++)
            {
                Console.WriteLine($"{i} - {UserPartCategoriesOpts[i]}");
            }

            userInput = Console.ReadLine();
            userInputInt = int.Parse(userInput);

            Console.WriteLine($"Replacing all with {UserPartCategoriesOpts[userInputInt]}");

            // Finally: Replace.
            foreach (string option in UserShipCurrCatParts)
            {
                SwapPartsViaPartname(inputShipSBC, option, UserPartCategoriesOpts[userInputInt], false);
            }
        }
        public static void BatterySwapper(PartSwapper partSwapper, string inputShipSBC)
        {
            string userInput = "";
            int userInputInt = -1;
            string inputPartSwapOut;
            string inputPartSwapIn;

            List<string> UserPartCategoriesOpts = new List<string>();
            List<string> UserShipCurrCatParts = new List<string>();

            foreach (string blockvariant in partSwapper.BlockVariantsAvail.Keys)
            {
                foreach (string blocktype in partSwapper.BlockVariantsAvail[blockvariant])
                {
                    if (blocktype.ToUpper().Contains("BATTERY"))
                    {
                        UserPartCategoriesOpts.Add(blocktype);
                    }
                    else
                    {
                        continue;
                    }
                }
            }


            foreach (string blocktype in partSwapper.ShipParts.Keys)
            {
                if (blocktype.ToUpper().Contains("BATTERY"))
                {
                    UserShipCurrCatParts.Add(blocktype);
                    Console.WriteLine($"Found blocktype {blocktype}, all instances of this type will be replaced!");
                }
                else
                {
                    continue;
                }

            }

            UserPruneList(UserShipCurrCatParts);

            Console.WriteLine("What should we replace the above parts with?");

            // Next, Iterate through what we found, and offer the user options:
            for (int i = 0; i < UserPartCategoriesOpts.Count; i++)
            {
                Console.WriteLine($"{i} - {UserPartCategoriesOpts[i]}");
            }

            userInput = Console.ReadLine();
            userInputInt = int.Parse(userInput);

            Console.WriteLine($"Replacing all with {UserPartCategoriesOpts[userInputInt]}");

            // Finally: Replace.
            foreach (string option in UserShipCurrCatParts)
            {
                SwapPartsViaPartname(inputShipSBC, option, UserPartCategoriesOpts[userInputInt], false);
            }
        }
        public static void GyroSwapper(PartSwapper partSwapper, string inputShipSBC)
        {
            string userInput = "";
            int userInputInt = -1;
            string inputPartSwapOut;
            string inputPartSwapIn;

            List<string> UserPartCategoriesOpts = new List<string>();
            List<string> UserShipCurrCatParts = new List<string>();

            foreach (string blockvariant in partSwapper.BlockVariantsAvail.Keys)
            {
                foreach (string blocktype in partSwapper.BlockVariantsAvail[blockvariant])
                {
                    if (blocktype.ToUpper().Contains("GYRO"))
                    {
                        UserPartCategoriesOpts.Add(blocktype);
                    }
                    else
                    {
                        continue;
                    }
                }
            }

            foreach (string blocktype in partSwapper.ShipParts.Keys)
            {
                if (blocktype.ToUpper().Contains("GYRO"))
                {
                    UserShipCurrCatParts.Add(blocktype);
                    Console.WriteLine($"Found blocktype {blocktype}, all instances of this type will be replaced!");
                }
                else
                {
                    continue;
                }
            }

            UserPruneList(UserShipCurrCatParts);

            Console.WriteLine("What should we replace the above parts with?");

            // Next, Iterate through what we found, and offer the user options:
            for (int i = 0; i < UserPartCategoriesOpts.Count; i++)
            {
                Console.WriteLine($"{i} - {UserPartCategoriesOpts[i]}");
            }

            userInput = Console.ReadLine();
            userInputInt = int.Parse(userInput);

            Console.WriteLine($"Replacing all with {UserPartCategoriesOpts[userInputInt]}");

            // Finally: Replace.
            foreach (string option in UserShipCurrCatParts)
            {
                SwapPartsViaPartname(inputShipSBC, option, UserPartCategoriesOpts[userInputInt], false);
            }
        }

        public static void AtmoThrusterSwapperTier(PartSwapper partSwapper, string inputShipSBC)
        {
            bool debug = false;

            string userInput = "";
            int userInputInt = -1;
            string inputPartSwapOut;
            string inputPartSwapIn;

            List<string> UserPartCategoriesOpts = new List<string>();
            List<string> UserShipCurrCatParts = new List<string>();

            foreach (string blockvariant in partSwapper.BlockVariantsAvail.Keys)
            {
                foreach (string blocktype in partSwapper.BlockVariantsAvail[blockvariant])
                {

                    if (!blocktype.ToUpper().Contains("HYDRO") && blocktype.ToUpper().Contains("ATMO") && blocktype.ToUpper().Contains("THRUST"))
                    {
                        UserPartCategoriesOpts.Add(blocktype);
                    }
                    else
                    {
                        continue;
                    }

                }
            }

            foreach (string blocktype in partSwapper.ShipParts.Keys)
            {
                if (blocktype.ToUpper().Contains("THRUST"))
                {
                    if (!blocktype.ToUpper().Contains("HYDRO") && blocktype.ToUpper().Contains("ATMO") && blocktype.ToUpper().Contains("THRUST"))
                    {
                        PartSwapper.RenderSlowColoredText($"Found block type {blocktype} on your ship! Eligible for replacement!\n", 10, ConsoleColor.Green);
                        UserShipCurrCatParts.Add(blocktype);

                    }
                    else
                    {
                        continue;
                    }
                }
            }

            UserPruneList(UserShipCurrCatParts);

            // Iterates through the blockvariants that are on the ship already, and the relevant blockvariants we found from blockvariant files,
            // And then we offer the user all the options for swapping out parts.
            foreach (string blockvariant in UserShipCurrCatParts)
            {
                string blockvarSubstring = blockvariant.Remove(blockvariant.Length - 3);
                List<string> tieredBlocks = UserPartCategoriesOpts.Where(item => item.Contains(blockvariant.Substring(0, blockvariant.Length - 2))).Distinct<string>().ToList();

                for (int i = 0; i < tieredBlocks.Count<string>(); i++)
                {
                    Console.WriteLine($"{i} - {tieredBlocks[i]}");
                }

                Console.WriteLine($"What should we replace {blockvariant} with? C to continue.");

                userInput = Console.ReadLine();

                if (IsUserQuitting(userInput.ToUpper(), debug))
                {
                    return;
                }

                if (userInput.ToUpper() == "C")
                {
                    Console.WriteLine("Continuing.");
                    continue;
                }

                userInputInt = int.Parse(userInput);

                PartSwapper.SwapPartsViaPartname(inputShipSBC, blockvariant, tieredBlocks[userInputInt], false);

                Console.WriteLine($"{blockvariant} has been replaced with {tieredBlocks[userInputInt]}");
            }
        }


        public static void IonThrusterSwapperTier(PartSwapper partSwapper, string inputShipSBC)
        {
            bool debug = false;

            string userInput = "";
            int userInputInt = -1;
            string inputPartSwapOut;
            string inputPartSwapIn;

            List<string> UserPartCategoriesOpts = new List<string>();
            List<string> UserShipCurrCatParts = new List<string>();

            foreach (string blockvariant in partSwapper.BlockVariantsAvail.Keys)
            {
                foreach (string blocktype in partSwapper.BlockVariantsAvail[blockvariant])
                {

                    if (!blocktype.ToUpper().Contains("HYDRO") && !blocktype.ToUpper().Contains("ATMO") && blocktype.ToUpper().Contains("THRUST"))
                    {
                        UserPartCategoriesOpts.Add(blocktype);
                    }
                    else
                    {
                        continue;
                    }

                }
            }

            foreach (string blocktype in partSwapper.ShipParts.Keys)
            {
                if (blocktype.ToUpper().Contains("THRUST"))
                {
                    if (!blocktype.ToUpper().Contains("HYDRO") && !blocktype.ToUpper().Contains("ATMO"))
                    {
                        PartSwapper.RenderSlowColoredText($"Found block type {blocktype} on your ship! Eligible for replacement!\n", 10, ConsoleColor.Green);
                        UserShipCurrCatParts.Add(blocktype);

                    }
                    else
                    {
                        continue;
                    }
                }
            }

            UserPruneList(UserShipCurrCatParts);

            // Iterates through the blockvariants that are on the ship already, and the relevant blockvariants we found from blockvariant files,
            // And then we offer the user all the options for swapping out parts.
            foreach (string blockvariant in UserShipCurrCatParts)
            {
                string blockvarSubstring = blockvariant.Remove(blockvariant.Length - 3);
                List<string> tieredBlocks = UserPartCategoriesOpts.Where(item => item.Contains(blockvariant.Substring(0, blockvariant.Length - 2))).Distinct<string>().ToList();

                for (int i = 0; i < tieredBlocks.Count<string>(); i++)
                {
                    Console.WriteLine($"{i} - {tieredBlocks[i]}");
                }

                Console.WriteLine($"What should we replace {blockvariant} with? C to continue.");

                userInput = Console.ReadLine();

                if (IsUserQuitting(userInput.ToUpper(), debug))
                {
                    return;
                }

                if (userInput.ToUpper() == "C")
                {
                    Console.WriteLine("Continuing.");
                    continue;
                }

                userInputInt = int.Parse(userInput);

                PartSwapper.SwapPartsViaPartname(inputShipSBC, blockvariant, tieredBlocks[userInputInt], false);

                Console.WriteLine($"{blockvariant} has been replaced with {tieredBlocks[userInputInt]}");
            }
        }

        public static void HydrogenThrusterSwapperTier(PartSwapper partSwapper, string inputShipSBC)
        {
            bool debug = false;
            string userInput = "";
            int userInputInt = -1;
            string inputPartSwapOut;
            string inputPartSwapIn;

            List<string> UserPartCategoriesOpts = new List<string>();
            List<string> UserShipCurrCatParts = new List<string>();

            foreach (string blockvariant in partSwapper.BlockVariantsAvail.Keys)
            {
                foreach (string blocktype in partSwapper.BlockVariantsAvail[blockvariant])
                {

                    if (blocktype.ToUpper().Contains("HYDRO") && blocktype.ToUpper().Contains("THRUST"))
                    {
                        UserPartCategoriesOpts.Add(blocktype);
                    }
                    else
                    {
                        continue;
                    }
                }
            }


            foreach (string blocktype in partSwapper.ShipParts.Keys)
            {
                if (blocktype.ToUpper().Contains("HYDRO") && blocktype.ToUpper().Contains("THRUST"))
                {
                    PartSwapper.RenderSlowColoredText($"Found block type {blocktype} on your ship! Eligible for replacement!\n", 10, ConsoleColor.Green);
                    UserShipCurrCatParts.Add(blocktype);
                }
                else
                {
                    continue;
                }

            }

            UserPruneList(UserShipCurrCatParts);

            PartSwapper.RenderSlowColoredText("", 10, ConsoleColor.Cyan);
            // Iterates through the blockvariants that are on the ship already, and the relevant blockvariants we found from blockvariant files,
            // And then we offer the user all the options for swapping out parts.
            foreach (string blockvariant in UserShipCurrCatParts)
            {
                string blockvarSubstring = blockvariant.Remove(blockvariant.Length - 3);
                List<string> tieredBlocks = UserPartCategoriesOpts.Where(item => item.Contains(blockvariant.Substring(0, blockvariant.Length - 2))).Distinct<string>().ToList();

                for (int i = 0; i < tieredBlocks.Count<string>(); i++)
                {
                    Console.WriteLine($"{i} - {tieredBlocks[i]}");
                }

                Console.WriteLine($"What should we replace {blockvariant} with? C to continue.");

                userInput = Console.ReadLine();

                if (IsUserQuitting(userInput.ToUpper(), debug))
                {
                    return;
                }

                if (userInput.ToUpper() == "C")
                {
                    Console.WriteLine("Continuing.");
                    continue;
                }

                userInputInt = int.Parse(userInput);

                PartSwapper.SwapPartsViaPartname(inputShipSBC, blockvariant, tieredBlocks[userInputInt], false);

                Console.WriteLine($"{blockvariant} has been replaced with {tieredBlocks[userInputInt]}");
            }

        }
        public static void BatterySwapperTier(PartSwapper partSwapper, string inputShipSBC)
        {
            bool debug = false;
            string userInput = "";
            int userInputInt = -1;
            string inputPartSwapOut;
            string inputPartSwapIn;

            List<string> UserPartCategoriesOpts = new List<string>();
            List<string> UserShipCurrCatParts = new List<string>();

            foreach (string blockvariant in partSwapper.BlockVariantsAvail.Keys)
            {
                foreach (string blocktype in partSwapper.BlockVariantsAvail[blockvariant])
                {
                    if (blocktype.ToUpper().Contains("BATTERY"))
                    {

                        UserPartCategoriesOpts.Add(blocktype);
                    }
                    else
                    {
                        continue;
                    }
                }
            }

            foreach (string blocktype in partSwapper.ShipParts.Keys)
            {
                if (blocktype.ToUpper().Contains("BATTERY"))
                {
                    PartSwapper.RenderSlowColoredText($"Found block type {blocktype} on your ship! Eligible for replacement!\n", 10, ConsoleColor.Green);
                    UserShipCurrCatParts.Add(blocktype);
                }
                else
                {
                    continue;
                }
            }

            UserPruneList(UserShipCurrCatParts);

            // Iterates through the blockvariants that are on the ship already, and the relevant blockvariants we found from blockvariant files,
            // And then we offer the user all the options for swapping out parts.
            foreach (string blockvariant in UserShipCurrCatParts)
            {
                string blockvarSubstring = blockvariant.Remove(blockvariant.Length - 3);

                List<string> tieredBlocks = UserPartCategoriesOpts.Where(item => item.Contains(blockvariant.Substring(0, blockvariant.Length - 2))).Distinct<string>().ToList();

                for (int i = 0; i < tieredBlocks.Count<string>(); i++)
                {
                    Console.WriteLine($"{i} - {tieredBlocks[i]}");
                }

                Console.WriteLine($"What should we replace {blockvariant} with? C to continue.");

                userInput = Console.ReadLine();

                if (IsUserQuitting(userInput.ToUpper(), debug))
                {
                    return;
                }

                if (userInput.ToUpper() == "C")
                {
                    Console.WriteLine("Continuing.");
                    continue;
                }

                userInputInt = int.Parse(userInput);

                PartSwapper.SwapPartsViaPartname(inputShipSBC, blockvariant, tieredBlocks[userInputInt], false);

                Console.WriteLine($"{blockvariant} has been replaced with {tieredBlocks[userInputInt]}");
            }

        }
        public static void GyroSwapperTier(PartSwapper partSwapper, string inputShipSBC)
        {
            bool debug = false;
            string userInput = "";
            int userInputInt = -1;
            string inputPartSwapOut;
            string inputPartSwapIn;

            List<string> UserPartCategoriesOpts = new List<string>();
            List<string> UserShipCurrCatParts = new List<string>();

            foreach (string blockvariant in partSwapper.BlockVariantsAvail.Keys)
            {
                foreach (string blocktype in partSwapper.BlockVariantsAvail[blockvariant])
                {
                    if (blocktype.ToUpper().Contains("GYRO"))
                    {
                        UserPartCategoriesOpts.Add(blocktype);
                    }
                    else
                    {
                        continue;
                    }
                }
            }


            foreach (string blocktype in partSwapper.ShipParts.Keys)
            {
                if (blocktype.ToUpper().Contains("GYRO"))
                {
                    PartSwapper.RenderSlowColoredText($"Found block type {blocktype} on your ship! Eligible for replacement!\n", 10, ConsoleColor.Green);
                    UserShipCurrCatParts.Add(blocktype);
                }
                else
                {
                    continue;
                }

            }

            UserPruneList(UserShipCurrCatParts);

            // Iterates through the blockvariants that are on the ship already, and the relevant blockvariants we found from blockvariant files,
            // And then we offer the user all the options for swapping out parts.
            foreach (string blockvariant in UserShipCurrCatParts)
            {
                string blockvarSubstring = blockvariant.Remove(blockvariant.Length - 3);
                List<string> tieredBlocks = UserPartCategoriesOpts.Where(item => item.Contains(blockvariant.Substring(0, blockvariant.Length - 2))).Distinct<string>().ToList();

                for (int i = 0; i < tieredBlocks.Count<string>(); i++)
                {
                    Console.WriteLine($"{i} - {tieredBlocks[i]}");
                }

                Console.WriteLine($"What should we replace {blockvariant} with? C to continue.");

                userInput = Console.ReadLine();

                if (IsUserQuitting(userInput.ToUpper(), debug))
                {
                    return;
                }

                if (userInput.ToUpper() == "C")
                {
                    Console.WriteLine("Continuing.");
                    continue;
                }

                userInputInt = int.Parse(userInput);

                PartSwapper.SwapPartsViaPartname(inputShipSBC, blockvariant, tieredBlocks[userInputInt], false);

                Console.WriteLine($"{blockvariant} has been replaced with {tieredBlocks[userInputInt]}");
            }

        }

        public static void JumpDriveSwapperTier(PartSwapper partSwapper, string inputShipSBC)
        {
            bool debug = false;
            string userInput = "";
            int userInputInt = -1;
            string inputPartSwapOut;
            string inputPartSwapIn;

            List<string> UserPartCategoriesOpts = new List<string>();
            List<string> UserShipCurrCatParts = new List<string>();

            foreach (string blockvariant in partSwapper.BlockVariantsAvail.Keys)
            {
                foreach (string blocktype in partSwapper.BlockVariantsAvail[blockvariant])
                {
                    if (blocktype.ToUpper().Contains("JUMPDRIVE"))
                    {
                        UserPartCategoriesOpts.Add(blocktype);
                    }
                    else
                    {
                        continue;
                    }
                }
            }


            foreach (string blocktype in partSwapper.ShipParts.Keys)
            {
                if (blocktype.ToUpper().Contains("JUMPDRIVE"))
                {
                    PartSwapper.RenderSlowColoredText($"Found block type {blocktype} on your ship! Eligible for replacement!\n", 10, ConsoleColor.Green);
                    UserShipCurrCatParts.Add(blocktype);
                }
                else
                {
                    continue;
                }

            }

            UserPruneList(UserShipCurrCatParts);

            // Iterates through the blockvariants that are on the ship already, and the relevant blockvariants we found from blockvariant files,
            // And then we offer the user all the options for swapping out parts.
            foreach (string blockvariant in UserShipCurrCatParts)
            {
                string blockvarSubstring = blockvariant.Remove(blockvariant.Length - 3);
                List<string> tieredBlocks = UserPartCategoriesOpts.Where(item => item.Contains(blockvariant.Substring(0, blockvariant.Length - 2))).Distinct<string>().ToList();

                for (int i = 0; i < tieredBlocks.Count<string>(); i++)
                {
                    Console.WriteLine($"{i} - {tieredBlocks[i]}");
                }

                Console.WriteLine($"What should we replace {blockvariant} with? C to continue.");

                userInput = Console.ReadLine();

                if (IsUserQuitting(userInput.ToUpper(), debug))
                {
                    return;
                }

                if (userInput.ToUpper() == "C")
                {
                    Console.WriteLine("Continuing.");
                    continue;
                }

                userInputInt = int.Parse(userInput);

                PartSwapper.SwapPartsViaPartname(inputShipSBC, blockvariant, tieredBlocks[userInputInt], false);

                Console.WriteLine($"{blockvariant} has been replaced with {tieredBlocks[userInputInt]}");
            }

        }

        public static void ArmorSwapper(PartSwapper partSwapper, string inputShipSBC)
        {
            string userInput = "";
            int userInputInt = -1;
            string inputPartSwapOut;
            string inputPartSwapIn;

            List<string> UserPartCategoriesOpts = new List<string>();
            List<string> UserShipCurrCatParts = new List<string>();

            // Warn the user. This is "all" or "None"
            PartSwapper.RenderSlowColoredText("<--WARNING-->\nArmorSwapper currently replaces EVERY ARMOR BLOCK IT FINDS!\nCancel this operation if this is unacceptable to you!\n", 5, ConsoleColor.Red);

            // With armorswapper: the user only has two choices - HEAVY or LIGHT armor.
            UserPartCategoriesOpts.Add("HEAVY ARMOR");
            UserPartCategoriesOpts.Add("LIGHT ARMOR");
            UserPartCategoriesOpts.Add("STRIP ARMOR");

            // Make the user choose what armor they want
            PartSwapper.RenderSlowColoredText("Set all armor to:\n1.Heavy\n2.Light\n3.Strip Armor\n0 to Quit Gracefully\n", 5, ConsoleColor.Red);

            userInputInt = Convert.ToInt32(Console.ReadLine());

            // Credit to SEToolbox for providing logic hints for this switch case
            switch (userInputInt)
            {
                case 0:
                    return;
                case 1:
                    foreach (string armor in partSwapper.ShipParts.Keys)
                    {
                        // These checks ensure that we don't accidentally overwrite some blocks!
                        if (armor.Contains("Battery") || armor.Contains("Cockpit"))
                        {
                            continue;
                        }
                        else

                        if (armor.Contains("LargeBlockArmor"))
                        {
                            string HeavyToLightString = armor.Replace("LargeBlockArmor", "LargeHeavyBlockArmor");
                            Console.WriteLine($"Replacing {armor} with {HeavyToLightString} via PartSwapper!");
                            PartSwapper.SwapPartsViaPartname(inputShipSBC, armor, HeavyToLightString, false);
                        }
                        else if (armor.StartsWith("Large") && (armor.EndsWith("HalfArmorBlock") || armor.EndsWith("HalfSlopeArmorBlock")))
                        {
                            string HeavyToLightString = armor.Replace("LargeHalf", "LargeHeavyHalf");
                            Console.WriteLine($"Replacing {armor} with {HeavyToLightString} via PartSwapper!");
                            PartSwapper.SwapPartsViaPartname(inputShipSBC, armor, HeavyToLightString, false);
                        }
                        else if (armor.StartsWith("SmallBlockArmor"))
                        {
                            string HeavyToLightString = armor.Replace("SmallBlockArmor", "SmallHeavyBlockArmor");
                            Console.WriteLine($"Replacing {armor} with {HeavyToLightString} via PartSwapper!");
                            PartSwapper.SwapPartsViaPartname(inputShipSBC, armor, HeavyToLightString, false);
                        }
                        else if (!armor.StartsWith("Large") && (armor.EndsWith("HalfArmorBlock") || armor.EndsWith("HalfSlopeArmorBlock")))
                        {
                            string HeavyToLightString = Regex.Replace(armor, "^(Half)(.*)", "HeavyHalf$2", RegexOptions.IgnoreCase);
                            Console.WriteLine($"Replacing {armor} with {HeavyToLightString} via PartSwapper!");
                            PartSwapper.SwapPartsViaPartname(inputShipSBC, armor, HeavyToLightString, false);
                        }
                        else if (armor.Contains("PanelLight"))
                        {
                            string HeavyToLightString = armor.Replace("PanelLight", "PanelHeavy");
                            Console.WriteLine($"Replacing {armor} with {HeavyToLightString} via PartSwapper!");
                            PartSwapper.SwapPartsViaPartname(inputShipSBC, armor, HeavyToLightString, false);
                        }
                    }
                    break;
                case 2:
                    foreach (string armor in partSwapper.ShipParts.Keys)
                    {
                        if (armor.Contains("LargeHeavyBlockHeavyArmor"))
                        {
                            string LightToHeavyString = armor.Replace("LargeHeavyBlockHeavyArmor", "LargeBlockArmor");
                            Console.WriteLine($"Replacing {armor} with {LightToHeavyString} via PartSwapper!");
                            PartSwapper.SwapPartsViaPartname(inputShipSBC, armor, LightToHeavyString, false);
                        }
                        else if (armor.StartsWith("Large") && (armor.EndsWith("HalfArmorBlock") || armor.EndsWith("HalfSlopeArmorBlock")))
                        {
                            string LightToHeavyString = armor.Replace("LargeHeavyHalf", "LargeHalf");
                            Console.WriteLine($"Replacing {armor} with {LightToHeavyString} via PartSwapper!");
                            PartSwapper.SwapPartsViaPartname(inputShipSBC, armor, LightToHeavyString, false);

                        }
                        else if (armor.StartsWith("SmallHeavyBlockArmor"))
                        {
                            var LightToHeavyString = armor.Replace("SmallHeavyBlockArmor", "SmallBlockArmor");
                            Console.WriteLine($"Replacing {armor} with {LightToHeavyString} via PartSwapper!");
                            PartSwapper.SwapPartsViaPartname(inputShipSBC, armor, LightToHeavyString, false);
                        }
                        else if (!armor.StartsWith("Large") && (armor.EndsWith("HalfArmorBlock") || armor.EndsWith("HalfSlopeArmorBlock")))
                        {
                            var LightToHeavyString = Regex.Replace(armor, "^(HeavyHalf)(.*)", "Half$2", RegexOptions.IgnoreCase);
                            Console.WriteLine($"Replacing {armor} with {LightToHeavyString} via PartSwapper!");
                            PartSwapper.SwapPartsViaPartname(inputShipSBC, armor, LightToHeavyString, false);
                        }
                        else if (armor.StartsWith("LargeHeavyBlockArmor"))
                        {
                            var LightToHeavyString = armor.Replace("LargeHeavyBlockArmor", "LargeBlockArmor");
                            Console.WriteLine($"Replacing {armor} with {LightToHeavyString} via PartSwapper!");
                            PartSwapper.SwapPartsViaPartname(inputShipSBC, armor, LightToHeavyString, false);
                        }
                        else if (armor.StartsWith("LargeBlockHeavyArmorSlope"))
                        {
                            var LightToHeavyString = armor.Replace("LargeBlockHeavyArmorSlope", "LargeBlockArmorSlope");
                            Console.WriteLine($"Replacing {armor} with {LightToHeavyString} via PartSwapper!");
                            PartSwapper.SwapPartsViaPartname(inputShipSBC, armor, LightToHeavyString, false);
                        }
                        else if (armor.EndsWith("HeavyArmorHalfCorner"))
                        {
                            var LightToHeavyString = armor.Replace("HeavyArmorHalfCorner", "ArmorHalfCorner");
                            Console.WriteLine($"Replacing {armor} with {LightToHeavyString} via PartSwapper!");
                            PartSwapper.SwapPartsViaPartname(inputShipSBC, armor, LightToHeavyString, false);
                        }
                        else if (armor.Contains("PanelHeavy"))
                        {
                            string HeavyToLightString = armor.Replace("PanelHeavy", "PanelLight");
                            Console.WriteLine($"Replacing {armor} with {HeavyToLightString} via PartSwapper!");
                            PartSwapper.SwapPartsViaPartname(inputShipSBC, armor, HeavyToLightString, false);
                        };
                    }
                    break;
                case 3:

                    break;
            }
        }


        public static void HydroTankSwapperTier(PartSwapper partSwapper, string inputShipSBC)
        {
            bool debug = false;
            string userInput = "";
            int userInputInt = -1;
            string inputPartSwapOut;
            string inputPartSwapIn;

            List<string> UserPartCategoriesOpts = new List<string>();
            List<string> UserShipCurrCatParts = new List<string>();

            foreach (string blockvariant in partSwapper.BlockVariantsAvail.Keys)
            {
                foreach (string blocktype in partSwapper.BlockVariantsAvail[blockvariant])
                {
                    if (blocktype.ToUpper().Contains("HYDROGENTANK"))
                    {
                        UserPartCategoriesOpts.Add(blocktype);
                    }
                    else
                    {
                        continue;
                    }
                }
            }


            foreach (string blocktype in partSwapper.ShipParts.Keys)
            {
                if (blocktype.ToUpper().Contains("HYDROGENTANK"))
                {
                    PartSwapper.RenderSlowColoredText($"Found block type {blocktype} on your ship! Eligible for replacement!\n", 10, ConsoleColor.Green);
                    UserShipCurrCatParts.Add(blocktype);
                }
                else
                {
                    continue;
                }

            }

            UserPruneList(UserShipCurrCatParts);

            // Iterates through the blockvariants that are on the ship already, and the relevant blockvariants we found from blockvariant files,
            // And then we offer the user all the options for swapping out parts.
            foreach (string blockvariant in UserShipCurrCatParts)
            {
                string blockvarSubstring = blockvariant.Remove(blockvariant.Length - 3);
                List<string> tieredBlocks = UserPartCategoriesOpts.Where(item => item.Contains(blockvariant.Substring(0, blockvariant.Length - 2))).Distinct<string>().ToList();

                for (int i = 0; i < tieredBlocks.Count<string>(); i++)
                {
                    Console.WriteLine($"{i} - {tieredBlocks[i]}");
                }

                Console.WriteLine($"What should we replace {blockvariant} with? C to continue.");

                userInput = Console.ReadLine();

                if (IsUserQuitting(userInput.ToUpper(), debug))
                {
                    return;
                }

                if (userInput.ToUpper() == "C")
                {
                    Console.WriteLine("Continuing.");
                    continue;
                }

                userInputInt = int.Parse(userInput);

                PartSwapper.SwapPartsViaPartname(inputShipSBC, blockvariant, tieredBlocks[userInputInt], false);

                Console.WriteLine($"{blockvariant} has been replaced with {tieredBlocks[userInputInt]}");
            }

        }

        public static void OxygenTankSwapperTier(PartSwapper partSwapper, string inputShipSBC)
        {
            bool debug = false;
            string userInput = "";
            int userInputInt = -1;
            string inputPartSwapOut;
            string inputPartSwapIn;

            List<string> UserPartCategoriesOpts = new List<string>();
            List<string> UserShipCurrCatParts = new List<string>();

            foreach (string blockvariant in partSwapper.BlockVariantsAvail.Keys)
            {
                foreach (string blocktype in partSwapper.BlockVariantsAvail[blockvariant])
                {
                    if (blocktype.ToUpper().Contains("OXYGENTANK"))
                    {
                        UserPartCategoriesOpts.Add(blocktype);
                    }
                    else
                    {
                        continue;
                    }
                }
            }


            foreach (string blocktype in partSwapper.ShipParts.Keys)
            {
                if (blocktype.ToUpper().Contains("OXYGENTANK"))
                {
                    PartSwapper.RenderSlowColoredText($"Found block type {blocktype} on your ship! Eligible for replacement!\n", 10, ConsoleColor.Green);
                    UserShipCurrCatParts.Add(blocktype);
                }
                else
                {
                    continue;
                }

            }

            UserPruneList(UserShipCurrCatParts);

            // Iterates through the blockvariants that are on the ship already, and the relevant blockvariants we found from blockvariant files,
            // And then we offer the user all the options for swapping out parts.
            foreach (string blockvariant in UserShipCurrCatParts)
            {
                string blockvarSubstring = blockvariant.Remove(blockvariant.Length - 3);
                List<string> tieredBlocks = UserPartCategoriesOpts.Where(item => item.Contains(blockvariant.Substring(0, blockvariant.Length - 2))).Distinct<string>().ToList();

                for (int i = 0; i < tieredBlocks.Count<string>(); i++)
                {
                    Console.WriteLine($"{i} - {tieredBlocks[i]}");
                }

                Console.WriteLine($"What should we replace {blockvariant} with? C to continue.");

                userInput = Console.ReadLine();

                if (IsUserQuitting(userInput.ToUpper(), debug))
                {
                    return;
                }

                if (userInput.ToUpper() == "C")
                {
                    Console.WriteLine("Continuing.");
                    continue;
                }

                userInputInt = int.Parse(userInput);

                PartSwapper.SwapPartsViaPartname(inputShipSBC, blockvariant, tieredBlocks[userInputInt], false);

                Console.WriteLine($"{blockvariant} has been replaced with {tieredBlocks[userInputInt]}");
            }

        }

        public static void CargoContainerSwapperTier(PartSwapper partSwapper, string inputShipSBC)
        {
            bool debug = false;
            string userInput = "";
            int userInputInt = -1;
            string inputPartSwapOut;
            string inputPartSwapIn;

            List<string> UserPartCategoriesOpts = new List<string>();
            List<string> UserShipCurrCatParts = new List<string>();

            foreach (string blockvariant in partSwapper.BlockVariantsAvail.Keys)
            {
                foreach (string blocktype in partSwapper.BlockVariantsAvail[blockvariant])
                {
                    if (blocktype.ToUpper().Contains("CONTAINER"))
                    {
                        UserPartCategoriesOpts.Add(blocktype);
                    }
                    else
                    {
                        continue;
                    }
                }
            }


            foreach (string blocktype in partSwapper.ShipParts.Keys)
            {
                if (blocktype.ToUpper().Contains("CONTAINER"))
                {
                    PartSwapper.RenderSlowColoredText($"Found block type {blocktype} on your ship! Eligible for replacement!\n", 10, ConsoleColor.Green);
                    UserShipCurrCatParts.Add(blocktype);
                }
                else
                {
                    continue;
                }

            }

            UserPruneList(UserShipCurrCatParts);

            // Iterates through the blockvariants that are on the ship already, and the relevant blockvariants we found from blockvariant files,
            // And then we offer the user all the options for swapping out parts.
            foreach (string blockvariant in UserShipCurrCatParts)
            {
                string blockvarSubstring = blockvariant.Remove(blockvariant.Length - 3);
                List<string> tieredBlocks = UserPartCategoriesOpts.Where(item => item.Contains(blockvariant.Substring(0, blockvariant.Length - 2))).Distinct<string>().ToList();

                for (int i = 0; i < tieredBlocks.Count<string>(); i++)
                {
                    Console.WriteLine($"{i} - {tieredBlocks[i]}");
                }

                Console.WriteLine($"What should we replace {blockvariant} with? C to continue.");

                userInput = Console.ReadLine();

                if (IsUserQuitting(userInput.ToUpper(), debug))
                {
                    return;
                }

                if (userInput.ToUpper() == "C")
                {
                    Console.WriteLine("Continuing.");
                    continue;
                }

                userInputInt = int.Parse(userInput);

                PartSwapper.SwapPartsViaPartname(inputShipSBC, blockvariant, tieredBlocks[userInputInt], false);

                Console.WriteLine($"{blockvariant} has been replaced with {tieredBlocks[userInputInt]}");
            }

        }

        public static void DrillSwapperTier(PartSwapper partSwapper, string inputShipSBC)
        {
            bool debug = false;
            string userInput = "";
            int userInputInt = -1;
            string inputPartSwapOut;
            string inputPartSwapIn;

            List<string> UserPartCategoriesOpts = new List<string>();
            List<string> UserShipCurrCatParts = new List<string>();

            foreach (string blockvariant in partSwapper.BlockVariantsAvail.Keys)
            {
                foreach (string blocktype in partSwapper.BlockVariantsAvail[blockvariant])
                {
                    if (blocktype.ToUpper().Contains("DRILL"))
                    {
                        UserPartCategoriesOpts.Add(blocktype);
                    }
                    else
                    {
                        continue;
                    }
                }
            }


            foreach (string blocktype in partSwapper.ShipParts.Keys)
            {
                if (blocktype.ToUpper().Contains("DRILL"))
                {
                    PartSwapper.RenderSlowColoredText($"Found block type {blocktype} on your ship! Eligible for replacement!\n", 10, ConsoleColor.Green);
                    UserShipCurrCatParts.Add(blocktype);
                }
                else
                {
                    continue;
                }

            }

            UserPruneList(UserShipCurrCatParts);

            // Iterates through the blockvariants that are on the ship already, and the relevant blockvariants we found from blockvariant files,
            // And then we offer the user all the options for swapping out parts.
            foreach (string blockvariant in UserShipCurrCatParts)
            {
                string blockvarSubstring = blockvariant.Remove(blockvariant.Length - 3);
                List<string> tieredBlocks = UserPartCategoriesOpts.Where(item => item.Contains(blockvariant.Substring(0, blockvariant.Length - 2))).Distinct<string>().ToList();

                for (int i = 0; i < tieredBlocks.Count<string>(); i++)
                {
                    Console.WriteLine($"{i} - {tieredBlocks[i]}");
                }

                Console.WriteLine($"What should we replace {blockvariant} with? C to continue.");

                userInput = Console.ReadLine();

                if (IsUserQuitting(userInput.ToUpper(), debug))
                {
                    return;
                }

                if (userInput.ToUpper() == "C")
                {
                    Console.WriteLine("Continuing.");
                    continue;
                }

                userInputInt = int.Parse(userInput);

                PartSwapper.SwapPartsViaPartname(inputShipSBC, blockvariant, tieredBlocks[userInputInt], false);

                Console.WriteLine($"{blockvariant} has been replaced with {tieredBlocks[userInputInt]}");
            }

        }

        public static void ConveyorSwapperTier(PartSwapper partSwapper, string inputShipSBC)
        {
            bool debug = false;
            string userInput = "";
            int userInputInt = -1;
            string inputPartSwapOut;
            string inputPartSwapIn;

            List<string> UserPartCategoriesOpts = new List<string>();
            List<string> UserShipCurrCatParts = new List<string>();

            foreach (string blockvariant in partSwapper.BlockVariantsAvail.Keys)
            {
                foreach (string blocktype in partSwapper.BlockVariantsAvail[blockvariant])
                {
                    if (blocktype.ToUpper().Contains("CONVEYOR"))
                    {
                        UserPartCategoriesOpts.Add(blocktype);
                    }
                    else
                    {
                        continue;
                    }
                }
            }


            foreach (string blocktype in partSwapper.ShipParts.Keys)
            {
                if (blocktype.ToUpper().Contains("CONVEYOR"))
                {
                    PartSwapper.RenderSlowColoredText($"Found block type {blocktype} on your ship! Eligible for replacement!\n", 10, ConsoleColor.Green);
                    UserShipCurrCatParts.Add(blocktype);
                }
                else
                {
                    continue;
                }

            }

            UserPruneList(UserShipCurrCatParts);

            // Iterates through the blockvariants that are on the ship already, and the relevant blockvariants we found from blockvariant files,
            // And then we offer the user all the options for swapping out parts.
            foreach (string blockvariant in UserShipCurrCatParts)
            {
                string blockvarSubstring = "CONVEYOR";
                List<string> tieredBlocks = UserPartCategoriesOpts.Where(item => item.ToUpper().Contains(blockvarSubstring)).Distinct<string>().ToList();

                for (int i = 0; i < tieredBlocks.Count<string>(); i++)
                {
                    Console.WriteLine($"{i} - {tieredBlocks[i]}");
                }

                Console.WriteLine($"What should we replace {blockvariant} with? C to continue.");

                userInput = Console.ReadLine();

                if (IsUserQuitting(userInput.ToUpper(), debug))
                {
                    return;
                }

                if (userInput.ToUpper() == "C")
                {
                    Console.WriteLine("Continuing.");
                    continue;
                }

                userInputInt = int.Parse(userInput);

                PartSwapper.SwapPartsViaPartname(inputShipSBC, blockvariant, tieredBlocks[userInputInt], false);

                Console.WriteLine($"{blockvariant} has been replaced with {tieredBlocks[userInputInt]}");
            }

        }

        public static void UserPruneList(List<string> list)
        {
            bool debug = false;
            // this function gives the user an input to modify a list.
            int UserSelection = -1;
            string UserInput = "";
            Boolean QuitFlag = false;

            while (!QuitFlag)
            {
                Console.WriteLine("We will be swapping out the following parts. Please select the category of parts you DO NOT WANT TO SWAP OUT!\nWhen finished, type 'C' to continue and begin swapping parts. (case insensitive)");

                for (int i = 0; i < list.Count; i++)
                {
                    Console.WriteLine($"{i} - {list[i]}");
                }
                UserInput = Console.ReadLine();

                if (UserInput.ToUpper() == "C")
                {
                    return;
                }
                else
                {

                    if (IsUserQuitting(UserInput.ToUpper(), debug))
                    {
                        return;
                    }

                    UserSelection = int.Parse(UserInput);

                    list.RemoveAt(UserSelection);
                }
            }
        }

        public static bool IsUserQuitting(string userInp, bool debug)
        {
            if (userInp.ToUpper() == "Q")
            {
                if (debug)
                {
                    Console.WriteLine("DEBUG: IsUserQuitting: User IS Quitting!");
                }
                return true;
            }
            else
            {
                if (debug)
                {
                    Console.WriteLine("DEBUG: IsUserQuitting: User IS NOT Quitting!");
                }
                return false;
            }
        }

        public static void Main()
        {
            DirectoryInfo[] BlueprintsDirInfo;
            FileInfo[] BlueprintFiles;
            string UserInput = "";
            bool QuitFlag = false;
            bool debug = false;

            RenderTextIntro();

            // This is all just dev and testing stuff
            if (debug)
            {
                // Testing calls:
                //swapParts("testShip.sbc", "LargeBlockGyro8x", "LargeBlockGyro", debug);
                //generatePartsLists("testShip.sbc");

                // Dictionary<string,HashSet<string>> result = LoadModDefinitionsDict();

                // Console.WriteLine("---FINAL---");

                // Console.WriteLine("Printing result:\n");

                // foreach(HashSet<string> hashset in result.Values)
                //{
                //    Console.WriteLine($"{hashset}");
                //    foreach(string element in hashset) {
                //        Console.WriteLine(element);
                //    }
                //}
            }


            while (!QuitFlag)
            {

                BlueprintsDirInfo = GetLocalDirectories();

                Console.WriteLine("PartSwapper found the following eligible blueprint folders:");

                if (BlueprintsDirInfo.Length == 0)
                {
                    Console.WriteLine("ERROR: Unable to find any blueprint directories!\nYour only option is to quit. Sorry.");
                }

                // iterates through all the blueprint directories we found and 
                for (int i = 0; i < BlueprintsDirInfo.Length; i++)
                {
                    BlueprintFiles = BlueprintsDirInfo[i].GetFiles("bp.sbc");

                    for (int j = 0; j < BlueprintFiles.Length; j++)
                    {
                        if (BlueprintFiles[j].Name == "bp.sbc")
                        {
                            Console.WriteLine($"{i} - {BlueprintsDirInfo[i].Name}");
                        }
                    }
                }

                PartSwapper.RenderSlowColoredText("Which blueprint you would like to swap parts out of?\nType 'Q' to quit. (case insensitive)\nSelection >", 5, ConsoleColor.Magenta);

                UserInput = Console.ReadLine();

                if (UserInput.ToUpper() == "Q")
                {
                    Console.WriteLine("Quitting Partswapper!");
                    return;
                }

                try
                {
                    if (BlueprintsDirInfo[int.Parse(UserInput)] == null)
                    {
                        Console.WriteLine("Invalid blueprint number!");
                    }
                    else
                    {
                        BlueprintFiles = BlueprintsDirInfo[int.Parse(UserInput)].GetFiles("bp.sbc");

                        foreach (FileInfo BlueprintFile in BlueprintFiles)
                        {
                            if (debug)
                            {
                                Console.WriteLine($"Found blueprint file {BlueprintFile.Name} in subdirectory {BlueprintFile.DirectoryName}");
                            }

                            if (BlueprintFile.Name == "bp.sbc")
                            {
                                if (debug)
                                {
                                    Console.WriteLine($"DEBUG: Found bp.sbc in {BlueprintFile.DirectoryName}! Opening the ship up!");
                                }
                                REPL(BlueprintFile.FullName);
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("ERROR: Bad input or failure while partswapping! Printing Exception!");
                    Console.WriteLine(e.ToString());
                    continue;
                }


                /*
                               blueprintFiles = blueprints[i].GetFiles("bp.sbc");

                    foreach (FileInfo blueprintFile in blueprintFiles)
                    {
                        Console.WriteLine($"Found blueprint file {blueprintFile.Name}");
                        if (blueprintFile.Name == "bp.sbc")
                        {
                            REPL(blueprintFile.FullName);
                        }
                    }


                */

                PartSwapper.RenderSlowColoredText("Returning to ship selection menu...\n", 10, ConsoleColor.DarkCyan);
            }
        }

        // Idea behind loadDefinitions:
        // You put named variants of "BlockVariantGroups.sbc" into the program directory (Ex: Aryx_BlockVariantGroups.sbc,SWTTBlockVariantGroups.sbc)
        // and this method will return Dict<string,List<String>> such that the key is the full name of the file we are pulling
        // definitions from, and the associated set
        public static Dictionary<string, HashSet<string>> LoadBlockVariantsDict()
        {
            FileInfo[] BlockVariantFilesList;

            string CurrentWorkingDirStr = Directory.GetCurrentDirectory();

            DirectoryInfo CurrentWorkingDirInfo = new DirectoryInfo(CurrentWorkingDirStr);

            BlockVariantFilesList = CurrentWorkingDirInfo.GetFiles("*BlockVariantGroups.sbc");

            Dictionary<string, HashSet<string>> BlockVariantsDict = new Dictionary<string, HashSet<string>>();

            HashSet<string> BlockVariantsSet = new HashSet<string>();

            bool debug = false;

            // Iterate through each file...
            foreach (FileInfo File in BlockVariantFilesList)
            {
                XElement root = XElement.Load(File.ToString());

                XElement BlockVariantGroups = root.Element("BlockVariantGroups");

                if (debug)
                {
                    Console.WriteLine($"DEBUG: BlockVariantGroups looks like:\n {BlockVariantGroups}");
                    Console.WriteLine("DEBUG: Iterating through all BlockVariantGroups. \n");

                    foreach (XElement Category in BlockVariantGroups.Descendants())
                    {
                        Console.WriteLine(Category);
                    }
                }

                // Iterate through each BlockVariantGroup via root/BlockVariantGroups descendants
                foreach (XElement blockVariantGroup in BlockVariantGroups.Descendants())
                {

                    if (debug)
                    {
                        Console.WriteLine($"DEBUG: Current blockVariantGroup is:\n{blockVariantGroup}\n");
                    }

                    // Skip this blockVariantGroup if it has no elements
                    if (!blockVariantGroup.HasElements)
                    {
                        if (debug)
                        {
                            Console.WriteLine($"DEBUG: blockVariantGroup {blockVariantGroup.Name} has no elements!");
                        }
                        continue;
                    }

                    IEnumerable<XElement> currBlocks = blockVariantGroup.Elements();

                    if (debug)
                    {
                        Console.WriteLine($"DEBUG: Iterating through elements found in blockVariantGroup.Elements()\n");
                        foreach (XElement element in blockVariantGroup.Elements())
                        {
                            Console.WriteLine(element);
                        }

                        foreach (XAttribute attribute in blockVariantGroup.Attributes())
                        {
                            Console.WriteLine(attribute);
                        }

                        Console.WriteLine($"DEBUG: currBlocks is:\n {currBlocks.ToString()}");

                    }

                    foreach (XElement block in currBlocks.Descendants())
                    {
                        string subtype = block.Attribute("Subtype").Value;

                        if (debug)
                        {
                            Console.WriteLine("DEBUG: Found the following subtype...");
                            Console.WriteLine(subtype);
                        }

                        BlockVariantsSet.Add(subtype);
                    }

                    if (debug)
                    {
                        foreach (string key in BlockVariantsDict.Keys)
                        {
                            Console.WriteLine($"DEBUG: \n\nkey:\n{key},\n\nresult:");

                            foreach (string value in BlockVariantsDict[key])
                            {
                                Console.WriteLine(value);
                            }
                        }
                    }
                }

                BlockVariantsDict[File.Name.ToString()] = BlockVariantsSet;

            }
            return BlockVariantsDict;
        }

        // Creates a backup of the XML document
        public static void BackupShipXMLTimestamp(string filename)
        {
            bool debug = false;

            string currentDirectory = Directory.GetCurrentDirectory();
            string shipFilepath = Path.Combine(currentDirectory, filename);

            if (debug)
            {
                Console.WriteLine($"DEBUG: BackupShipXML filename input is: {filename}");
            }

            XmlDocument xmlDoc = new XmlDocument();

            using (XmlReader xRead = XmlReader.Create(filename))
            {
                xmlDoc.Load(xRead);
            }

            XmlWriterSettings xwrSettings = new XmlWriterSettings();
            xwrSettings.IndentChars = "\t";
            xwrSettings.NewLineHandling = NewLineHandling.Entitize;
            xwrSettings.Indent = true;
            xwrSettings.NewLineChars = "\n";

            string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmm");
            string BackupFilename = filename + timestamp + "pps_bp_bak.sbc";

            using (XmlWriter xWrite = XmlWriter.Create(BackupFilename, xwrSettings))
            {
                xmlDoc.Save(xWrite);
            }
        }

        private static Dictionary<string, List<XElement>> GenerateShipPartsList2(string filename)
        {
            Dictionary<string, List<XElement>> ShipPartsList = new Dictionary<string, List<XElement>>();

            XElement root = XElement.Load(filename);
            XElement CubeGrids = root.Element("ShipBlueprints").Element("ShipBlueprint").Element("CubeGrids");
            List<XElement> NewListTemp = new List<XElement>();
            string BlockNameTemp = "INIT";
            bool debug = false;

            foreach (XElement cubeGrid in CubeGrids.Elements())
            {
                Console.WriteLine($"Found ship:{cubeGrid.Element("DisplayName").Value}");

                XElement cubeblocks = cubeGrid.Element("CubeBlocks");

                foreach (XElement block in cubeblocks.Elements())
                {
                    // Get the SubTypeName of the block, aka: block type name, or whatever. 

                    BlockNameTemp = block.Element("SubtypeName").Value;

                    // Use the blockname to check if the key exists in the shippartslist already. If so: Update from a temp list we create.
                    if (ShipPartsList.ContainsKey(BlockNameTemp))
                    {
                        // create a new list, start fresh.
                        NewListTemp = ShipPartsList.GetValueOrDefault(BlockNameTemp);
                        // Not sure if this would be 'null' here or what the "Default" value of List<XElement> is...
                        // (I hope it's an empty list). We're gonna assume that for the moment.
                        // TODO: If this causes crashes/bad behavior - Make NewListTemp a new() list if we detect a null or something.

                        NewListTemp.Add(block);

                        // Add the part to the ShipPartsList via key: Part name, Value: XElement representing that part
                        ShipPartsList[BlockNameTemp] = NewListTemp;
                    }
                    else
                    {
                        // If the key does *not* exist (Part is not currently in ShipPartsList) - simply create the first/new list with the XElement already added, and add to the dict.
                        NewListTemp = new List<XElement>() { block };

                        ShipPartsList.Add(BlockNameTemp, NewListTemp);
                    }
                }

            }
            //Iterate through each block, creating the lists (if necessary) to populate a string:List<Xelement> dictionary that will comprise the catalogue of parts in a ship.
            if (debug)
            {
                Console.WriteLine($"DEBUG: ShipPartsList result is:\n{ShipPartsList}");
            }


            if (debug)
            {
                foreach (string type in ShipPartsList.Keys)
                {
                    Console.WriteLine($"DEBUG:\nkey: {type},\ncount: {ShipPartsList[type].Count()}.");
                    //Console.WriteLine($"key: {type}, count: {result[type].Count()}, value: {result[type][0]}");
                }
            }


            return ShipPartsList;
        }


        private static Dictionary<string, List<XElement>> GenerateShipPartsList(string filename)
        {
            bool debug = false;

            Dictionary<string, List<XElement>> ShipPartsList = new Dictionary<string, List<XElement>>();

            XElement root = XElement.Load(filename);

            XElement cubeblocks = root.Element("ShipBlueprints").Element("ShipBlueprint").Element("CubeGrids").Element("CubeGrid").Element("CubeBlocks");

            XAttribute shipname = root.Element("ShipBlueprints").Element("ShipBlueprint").Element("Id").Attribute("Subtype");

            List<XElement> xElements;

            Console.WriteLine($"Found ship:{shipname.Value}");

            IEnumerable<XElement> CubeBlocksChildren = cubeblocks.Descendants();

            //Iterate through each block, creating the lists (if necessary) to populate a string:List<Xelement> dictionary that will comprise the catalogue of parts in a ship.
            foreach (XElement block in CubeBlocksChildren)
            {
                if (debug)
                {
                    Console.WriteLine($"DEBUG: generatePartsLists found block {block.ToString()}");
                }

                try
                {
                    if (debug)
                    {
                        try
                        {
                            if (block.Element("SubtypeName") != null)
                            {
                                Console.WriteLine($"DEBUG: Found block {block}, with SubtypeName {block.Element("SubtypeName").Value}");
                            }

                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("DEBUG: CAUGHT ERROR! Printing Error below:");
                            Console.Write(e.ToString());
                        }
                    }

                    if (block.Element("SubtypeName") != null)
                    {
                        xElements = ShipPartsList[block.Element("SubtypeName").Value.ToString()];

                        xElements.Add(block);

                        ShipPartsList[block.Element("SubtypeName").Value.ToString()] = xElements;
                    }

                }
                catch (Exception kms)
                {
                    if (kms.GetType() == typeof(NullReferenceException))
                    {

                        if (debug)
                        {
                            Console.WriteLine("DEBUG: Caught null exception! Skipping!");
                        }

                        // situation where the try block fails due to the subtypeName field being non-existent
                        // for the moment - we'll just...Uhh...continue.
                        continue;
                    }

                    if (kms.GetType() == typeof(KeyNotFoundException))
                    {
                        // situation where the try block fails due to key not found.
                        // this is ESSENTIAL here.

                        if (debug)
                        {
                            Console.WriteLine("DEBUG: Key not found! Creating new List and adding key" + block.Name.ToString() + "to the new list! ");
                            Console.WriteLine("\n" + kms.ToString());
                        }

                        xElements = new List<XElement>();
                        xElements.Add(block);

                        ShipPartsList[block.Element("SubtypeName").Value.ToString()] = xElements;
                    }
                }
            }

            if (debug)
            {
                Console.WriteLine($"DEBUG: result is... \n{ShipPartsList}");
            }

            foreach (string type in ShipPartsList.Keys)
            {
                if (debug)
                {
                    Console.WriteLine($"DEBUG:\nkey: {type},\ncount: {ShipPartsList[type].Count()}.");
                    //Console.WriteLine($"key: {type}, count: {result[type].Count()}, value: {result[type][0]}");
                }
            }
            return ShipPartsList;
        }

        public static void PruneCubeGridsFromShipBlueprint(string filename, bool debug)
        {
            XmlWriterSettings XMLWriterSettings = new XmlWriterSettings();

            string currentDirectory = Directory.GetCurrentDirectory();
            string shipFilepath = Path.Combine(currentDirectory, filename);
            string UserInput = "";

            int UserInputInt = -1;

            XElement shipTree = XElement.Load(shipFilepath);
            XElement ShipBPs = shipTree.Element("ShipBlueprints");
            XElement ShipBP = ShipBPs.Element("ShipBlueprint");
            XElement CubeGrids = ShipBP.Element("CubeGrids");

            string GridName = ShipBP.Element("Id").Attribute("Subtype").Value;

            // iterating block
            XElement currPart;
            if (debug)
            {
                Console.WriteLine($"DEBUG: PruneCubeGridsFromShipBlueprint found grid with GridName \"{GridName}\"");
            }

            //backup the original grid
            BackupShipXMLTimestamp(filename);

            if (CubeGrids.Elements().Count() > 1)
            {
                Console.WriteLine("Partswapper detected multiple CubeGrid defintions!\nThis is typically a mistake - You blueprinted multiple grids when they were connected together!" +
                    "\n...Or you have rotors. In which case: You may safely quit.\n");
                LoopStart:
                Console.WriteLine("Please select which CubeGrid definition to remove from the Blueprint, if any.\nQ to Quit and continue the program.\n");

                // Get the CubeGrids again (in case we are in a loop and checking post-removal)
                CubeGrids = ShipBP.Element("CubeGrids");

                // Put the elements in an array. The references should still be edit-able...I hope.
                XElement[] CubeGridsArr = CubeGrids.Elements().ToArray();

                // Iterate Cubegrids to offer for deletion.
                for (int i = 0; i < CubeGridsArr.Length; i++)
                {
                    Console.WriteLine($"{i} - {CubeGridsArr[i].Element("DisplayName").Value}");
                }

                // 
                UserInput = Console.ReadLine();

                if (IsUserQuitting(UserInput,debug))
                {
                    Console.WriteLine("User declined to delete any cubeGrids definitions. Continuing!");
                    return;
                }

                UserInputInt = Int32.Parse(UserInput);

                Console.WriteLine($"Deleting {CubeGridsArr[UserInputInt].Element("DisplayName").Value} from ShipBluePrint!");

                CubeGridsArr[UserInputInt].Remove();

                if (debug)
                {
                    Console.WriteLine($"New ShipBluePrints looks like:\n{ShipBP.ToString()}");
                }

                Console.WriteLine("Remove another CubeGrid definition from the ShipBluePrint?\n[Y]es/[N]o");

                UserInput = Console.ReadLine();

                switch (UserInput.ToUpper())
                {
                    case "Y":
                        goto LoopStart;
                    case "N":
                        Console.WriteLine("User declined to delete more CubeGrid definitions! Saving changes to file and continuing!");
                        shipTree.Save(filename);
                        return;
                    default:
                        Console.WriteLine("ERROR: You entered nonsense. So I'm continuing. NO CHANGES HAVE BEEN MADE!");
                        return;
                }
            }
        }

        public static void SwapPartsViaPartname(string filename, string oldPart, string newPart, bool debug)
        {
            XmlWriterSettings XMLWriterSettings = new XmlWriterSettings();

            string currentDirectory = Directory.GetCurrentDirectory();
            string shipFilepath = Path.Combine(currentDirectory, filename);

            XElement shipTree = XElement.Load(shipFilepath);
            XElement ShipBPs = shipTree.Element("ShipBlueprints");
            XElement ShipBP = ShipBPs.Element("ShipBlueprint");
            XElement CubeGrids = ShipBP.Element("CubeGrids");

            string GridName = ShipBP.Element("Id").Attribute("Subtype").Value;

            // iterating block
            XElement currPart;
            if (debug)
            {
                Console.WriteLine($"DEBUG: SwapPartsViaPartname found GridName: {GridName}");
            }

            //backup the original grid
            BackupShipXMLTimestamp(filename);

            if (debug)
            {
                Console.WriteLine($"Printing shipTree Nodes and self:\n{shipTree.DescendantNodesAndSelf().ToString()}");
            }

            // Iterate CubeBlocks
            foreach (XElement cubeGrid in CubeGrids.Elements())
            {
                IEnumerable<XElement> CubeBlocks = cubeGrid.Element("CubeBlocks").Elements();

                foreach (XElement cubeBlock in CubeBlocks)
                {
                    if (debug)
                    {
                        Console.WriteLine($"DEBUG: Checking element:{cubeBlock}");
                    }

                    // I dont know why this is inverted, but that's how it happened.
                    // If the block does NOT match the old part - we skip it...
                    if (!cubeBlock.Element("SubtypeName").Value.ToString().Equals(oldPart))
                    {
                        if (debug)
                        {
                            Console.WriteLine($"DEBUG: cubeBlock.Element(\"SubtypeName\") {cubeBlock.Element("SubtypeName").Value} does not equal {oldPart}");
                        }

                        // skip
                        continue;
                    }
                    else
                    {
                        // If we have a match... Change the partname, batman.
                        currPart = cubeBlock.Element("SubtypeName");

                        //debug output
                        if (debug)
                        {
                            // if it does match - swap the part
                            Console.WriteLine($"DEBUG: Found XML part: {currPart}\n Replacing {oldPart} with {newPart}");
                        }

                        // This is where the magic happens.
                        currPart.SetValue(newPart);
                        // The magic has occurred. 

                        //debug output
                        if (debug)
                        {
                            Console.WriteLine($"DEBUG: Subtypename post-swap:{currPart}");
                        }
                    }
                }

            }

            // Save the file.
            shipTree.Save(filename);
        }

    }

}
