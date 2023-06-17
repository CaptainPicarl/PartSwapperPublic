using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using System.Xml;
using System.Xml.Linq;
using System.Windows;
using System.IO;
using System.Text;
using Microsoft.VisualBasic.FileIO;

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

        public static bool Debug = false;

        // declarations for collections of ship parts and their category names, as well as modblocks
        public Dictionary<string, List<XElement>> ShipParts = new Dictionary<string, List<XElement>>();

        public Dictionary<string, HashSet<string>> BlockVariantsAvail;

        public PartSwapper(string filename)
        {
            ShipParts = GenerateShipPartsList(filename);
            BlockVariantsAvail = LoadBlockVariantsDict();
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

            return LocalDirectories;
        }

        public static void REPLProto1()
        {
            bool quitflag = false;
            string inputShipSBC;
            string inputPartSwapOut;
            string inputPartSwapIn;

            RenderTextIntro();
            // partSwapper represents the ship we want to work on.
            PartSwapper partSwapper;

            PartSwapper.RenderSlowColoredText("Enter the .sbc filename of the ship you would like to work on (Q to quit):\n", 5, ConsoleColor.Red);

            inputShipSBC = Console.ReadLine();

            if (inputShipSBC.Equals("Q"))
            {
                quitflag = true;
                RenderSlowColoredText("Quitting!", 20, ConsoleColor.Cyan);
                return;
            }


            while (!quitflag)
            {
                partSwapper = new PartSwapper(inputShipSBC);

                foreach (string part in partSwapper.ShipParts.Keys)
                {
                    Console.WriteLine(part);
                }

                PartSwapper.RenderSlowColoredText("Please write which part to swap:\nPart to swap out:", 10, ConsoleColor.Magenta);

                inputPartSwapOut = Console.ReadLine();

                if (inputPartSwapOut.Equals("Q"))
                {
                    quitflag = true;
                    RenderSlowColoredText("Quitting!", 20, ConsoleColor.Cyan);
                    return;
                }

                if (partSwapper.ShipParts.Keys.Contains(inputPartSwapOut))
                {
                    // do next partswap
                    PartSwapper.RenderSlowColoredText("Please pick a replacement part for the category you have chosen.\nPart options are (This will be long):\n", 10, ConsoleColor.Red);

                    foreach (string modBlockFile in partSwapper.BlockVariantsAvail.Keys)
                    {
                        foreach (string part in partSwapper.BlockVariantsAvail[modBlockFile])
                        {
                            Console.WriteLine();
                        }
                    }

                    PartSwapper.RenderSlowColoredText("Be advised: This tool currently does NO SANITY CHECKING!\nI will happily let you shoot yourself in the foot.\n", 10, ConsoleColor.Yellow);

                    inputPartSwapIn = Console.ReadLine();

                    if (inputPartSwapOut.Equals("Q"))
                    {
                        quitflag = true;
                        RenderSlowColoredText("Quitting!", 20, ConsoleColor.Cyan);
                        return;
                    }


                    PartSwapper.SwapPartsViaPartname(inputShipSBC, inputPartSwapOut, inputPartSwapIn, Debug);

                    Console.WriteLine("Swap another part? Y/N");

                    switch (Console.ReadLine())
                    {
                        case "Y":
                            continue;
                            break;
                        case "N":
                            RenderSlowColoredText("Not swapping another part!", 10, ConsoleColor.DarkCyan);
                            quitflag = true;
                            break;
                        default:
                            Console.WriteLine("Not sure what you said. Restarting.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Couldn't find that part!");
                }
            }
        }
        public static void REPL(string inputShipSBC)
        {
            bool quitflag = false;
            string userInput = "";
            int userInputInt = -1;
            string inputPartSwapOut;
            string inputPartSwapIn;

            List<string> UserPartCategoriesOpts = new List<string>();
            List<string> UserShipCurrCatParts = new List<string>();

            // partSwapper represents the ship we want to work on.
            PartSwapper partSwapper;

            while (!quitflag)
            {
                partSwapper = new PartSwapper(inputShipSBC);

                PartSwapper.RenderSlowColoredText("Ship loaded.\n", 10, ConsoleColor.Cyan);

                if (Debug)
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

                PartSwapper.RenderSlowColoredText($"1. Gyroscopes\n2. Ion Thrusters\n3. Hydrogen Thrusters\n4. Batteries\n5. Jump Drives\n6. Hydrogen Tanks\n7. Oxygen Tanks\n8. Cargo Containers\nQ to quit editing this file.\nSelection > ", 10, ConsoleColor.DarkMagenta);

                userInput = Console.ReadLine();

                if (IsUserQuitting(userInput.ToUpper()))
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

                if (IsUserQuitting(userInput.ToUpper()))
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

                if (IsUserQuitting(userInput.ToUpper()))
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

            PartSwapper.RenderSlowColoredText("",10,ConsoleColor.Cyan);
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

                if (IsUserQuitting(userInput.ToUpper()))
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

                if (IsUserQuitting(userInput.ToUpper())){
                    return;
                }

                if(userInput.ToUpper() == "C")
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

                if (IsUserQuitting(userInput.ToUpper()))
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

                if (IsUserQuitting(userInput.ToUpper()))
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

        public static void HydroTankSwapperTier(PartSwapper partSwapper, string inputShipSBC)
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

                if (IsUserQuitting(userInput.ToUpper()))
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

                if (IsUserQuitting(userInput.ToUpper()))
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

                if (IsUserQuitting(userInput.ToUpper()))
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

                    if (IsUserQuitting(UserInput.ToUpper()))
                    {
                        return;
                    }

                    UserSelection = int.Parse(UserInput);

                    list.RemoveAt(UserSelection);
                }
            }
        }

        public static bool IsUserQuitting(string userInp)
        {
            if (userInp.ToUpper() == "Q")
            {
                Console.WriteLine("Quitting!");
                return true;
            } else
            {
                return false;
            }
        }

        public static void Main()
        {
            DirectoryInfo[] Blueprints;
            FileInfo[] BlueprintFiles;
            string UserInput = "";
            Boolean QuitFlag = false;

            RenderTextIntro();

            // This is all just dev and testing stuff
            if (Debug)
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

                Blueprints = GetLocalDirectories();
                Console.WriteLine("PartSwapper found the following eligible blueprint folders:");

                if (Blueprints.Length == 0)
                {
                    Console.WriteLine("ERROR: Unable to find any blueprint directories!\nYour only option is to quit. Sorry.");
                }

                // iterates through all the blueprint directories we found and 
                for (int i = 0; i < Blueprints.Length; i++)
                {
                    BlueprintFiles = Blueprints[i].GetFiles("bp.sbc");

                    for (int j = 0; j < BlueprintFiles.Length; j++)
                    {
                        if (BlueprintFiles[j].Name == "bp.sbc")
                        {
                            Console.WriteLine($"{i} - {Blueprints[i].Name}");
                        }
                    }
                }

                PartSwapper.RenderSlowColoredText("Which blueprint you would like to swap parts out of?\nType 'Q' to quit. (case insensitive)\nSelection >", 5, ConsoleColor.Magenta);
                

                UserInput = Console.ReadLine();

                if (UserInput.ToUpper() == "Q")
                {
                    Console.WriteLine("Quitting!");
                    return;
                }


                try
                { 
                    if (Blueprints[int.Parse(UserInput)] == null)
                    {
                        Console.WriteLine("Invalid blueprint number!");
                    }
                    else
                    {
                        BlueprintFiles = Blueprints[int.Parse(UserInput)].GetFiles("bp.sbc");

                        foreach (FileInfo BlueprintFile in BlueprintFiles)
                        {
                            if (Debug)
                            {
                                Console.WriteLine($"Found blueprint file {BlueprintFile.Name} in subdirectory {BlueprintFile.DirectoryName}");
                            }

                            if (BlueprintFile.Name == "bp.sbc")
                            {
                                if (Debug)
                                {
                                    Console.WriteLine($"DEBUG: Found bp.sbc in {BlueprintFile.DirectoryName}! Opening the ship up!");
                                }

                                REPL(BlueprintFile.FullName);
                            }
                        }
                    }
                } catch (Exception e)
                {
                    Console.WriteLine("ERROR: Bad input or failure while partswapping!");
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

                PartSwapper.RenderSlowColoredText("Returning to ship select menu...\n",10,ConsoleColor.DarkCyan);
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

            // Iterate through each file...
            foreach (FileInfo File in BlockVariantFilesList)
            {
                XElement root = XElement.Load(File.ToString());

                XElement BlockVariantGroups = root.Element("BlockVariantGroups");

                if (Debug)
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

                    if (Debug)
                    {
                        Console.WriteLine($"DEBUG: Current blockVariantGroup is:\n{blockVariantGroup}\n");
                    }

                    // Skip this blockVariantGroup if it has no elements
                    if (!blockVariantGroup.HasElements)
                    {
                        if (Debug)
                        {
                            Console.WriteLine($"DEBUG: blockVariantGroup {blockVariantGroup.Name} has no elements!");
                        }
                        continue;
                    }

                    IEnumerable<XElement> currBlocks = blockVariantGroup.Elements();

                    if (Debug)
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

                        if (Debug)
                        {
                            Console.WriteLine("DEBUG: Found the following subtype...");
                            Console.WriteLine(subtype);
                        }

                        BlockVariantsSet.Add(subtype);
                    }

                    if (Debug)
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

            // todo: Find a way to add the grid name to the backup filename
            string GridName = "";
            string timestamp = DateTime.Now.ToString("yyyyMMdd");
            string BackupFilename = timestamp + "pps_bp_bak.sbc";

            using (XmlWriter xWrite = XmlWriter.Create(BackupFilename, xwrSettings))
            {
                xmlDoc.Save(xWrite);
            }
        }

        private static Dictionary<string, List<XElement>> GenerateShipPartsList(string filename)
        {
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
                if (Debug)
                {
                    Console.WriteLine($"DEBUG: generatePartsLists found block {block.ToString()}");
                }

                try
                {
                    if (Debug)
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

                        if (Debug)
                        {
                            Console.WriteLine("DEBUG: Caught null exception!");
                        }

                        // situation where the try block fails due to the subtypeName field being non-existent
                        // for the moment - we'll just...Uhh...continue.
                        continue;
                    }

                    if (kms.GetType() == typeof(KeyNotFoundException))
                    {
                        // situation where the try block fails due to key not found.
                        // this is ESSENTIAL here.

                        if (Debug)
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

            if (Debug)
            {
                Console.WriteLine($"DEBUG: result is... \n{ShipPartsList}");
            }

            foreach (string type in ShipPartsList.Keys)
            {
                if (Debug)
                {
                    Console.WriteLine($"DEBUG:\nkey: {type},\ncount: {ShipPartsList[type].Count()}.");
                    //Console.WriteLine($"key: {type}, count: {result[type].Count()}, value: {result[type][0]}");
                }
            }
            return ShipPartsList;
        }

        public static void SwapPartsViaPartname(string filename, string oldPart, string newPart, bool debug)
        {
            XmlWriterSettings xws = new XmlWriterSettings();

            var currentDirectory = Directory.GetCurrentDirectory();
            var shipFilepath = Path.Combine(currentDirectory, filename);

            XElement shipTree = XElement.Load(shipFilepath);
            XElement ShipBPs = shipTree.Element("ShipBlueprints");
            XElement ShipBP = ShipBPs.Element("ShipBlueprint");
            XElement CubeGrids = ShipBP.Element("CubeGrids");
            XElement CubeGrid = CubeGrids.Element("CubeGrid");
            XElement CubeBlocks = CubeGrid.Element("CubeBlocks");

            // declarations for iterating block
            XElement currPart;

            //backup the original grid
            BackupShipXMLTimestamp(filename);

            if (debug)
            {
                // debug, iterate all elements so you can see them
                Console.WriteLine("DEBUG: Iterating through entire document!\n");

                foreach (XElement shipElement in shipTree.Elements())
                {
                    Console.WriteLine(shipElement);
                }

                // debug, iterate all cubeblocks so you can see them
                Console.WriteLine($"DEBUG: Cubeblocks is:\n {CubeBlocks}\n\n");
            }

            // Iterate CubeBlocks
            foreach (XElement cubeBlock in CubeBlocks.Elements())
            {
                if (debug)
                {
                    Console.WriteLine($"DEBUG: Checking element:{cubeBlock}");
                }

                if (!cubeBlock.Element("SubtypeName").Value.ToString().Equals(oldPart))
                {
                    if (debug)
                    {
                        Console.WriteLine($"DEBUG: cubeBlock.Element(\"SubtypeName\") {cubeBlock.Element("SubtypeName").Value} does not equal {oldPart}");
                    }

                    // If the name doesn't match, continue...
                    continue;
                }
                else
                {
                    currPart = cubeBlock.Element("SubtypeName");

                    //debug output
                    if (debug)
                    {
                        // if it does match - swap the part
                        Console.WriteLine($"DEBUG: Found XML part: {currPart}\n Replacing {oldPart} with {newPart}");
                    }

                    currPart.SetValue(newPart);

                    //debug output
                    if (debug)
                    {
                        Console.WriteLine($"DEBUG: Subtypename post-swap:{currPart}");
                    }
                }
            }
            //shipTree.Save($"pps_{filename}");
            shipTree.Save(filename);
        }

    }

}
