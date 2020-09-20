using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    //Manager
    public static AnimationManager Animations;

    //Interactables
    public const string Chessboard_KeyReveal = "ChessboardKeyRevealAnim";

    public const string Chest_Open = "ChestOpenAnim";

    public const string SkullLockChest_Open = "SkullLockChestOpenAnim";

    public const string FlagPost_Raise = "FlagPostRaiseAnim";
    public const string FlagPost_Lower = "FlagPostLowerAnim";

    public const string Lift_Lower = "LiftAnimGS";

    public const string HutDoor_Open = "HutDoorOpenAnim";
    public const string HutDoor_Close = "HutDoorCloseAnim";

    public const string CombinationWheel = "ShipWheelAnim";
    public const string Dig_ShipWheel = "ShipWheelDigAnim";

    public const string CliffDoor_Open = "CliffDoorAnim";
    public const string WoodDoor_Open = "WoodDoorSlideAnim";

    //UI
    public const string Statue_DialogueBox_FadeIn = "DialogueBoxFadeInAnim";
    public const string Statue_DialogueBox_FadeOut = "DialogueBoxFadeOutAnim";

    public const string Statue_DialogueChoice_FadeIn = "DialogueChoiceFadeInAnim";
    public const string Statue_DialogueChoice_FadeOut = "DialogueChoiceFadeOutAnim";

    public const string Statue_Dialogue_FadeIn = "DialogueFadeInAnim";
    public const string Statue_Dialogue_FadeOut = "DialogueFadeOutAnim";

    public const string InteractPrompt_FadeIn = "InteractPromptFadeInAnim";
    public const string InteractPrompt_FadeOut = "InteractPromptFadeOutAnim";

    public const string Inventory_Open = "InventoryOpenAnim";
    public const string Inventory_Close = "InventoryCloseAnim";

    public const string Document_Open = "NoteOpenAnim";
    public const string Document_Close = "NoteCloseAnim";

    public const string Tooltip_FadeIn = "TooltipFadeInAnim";
    public const string Tooltip_FadeOut = "TooltipFadeOutAnim";

    public const string OptionsMenu_Open = "OptionsMenuOpenAnim";
    public const string OptionsMenu_Close = "OptionsMenuCloseAnim";

    public const string PauseMenu_Open = "PauseMenuOpenAnim";
    public const string PauseMenu_Close = "PauseMenuCloseAnim";

    public const string Background_FadeIn = "BackgroundFadeInAnim";
    public const string Background_FadeOut = "BackgroundFadeOutAnim";

    public const string Subtitle_FadeOut = "SubTextFadeOutAnim";

    public const string OpeningCS_Cam = "IntroCutsceneCamAnim";

    public const string RoleCredits = "RoleCreditsAnim";

    //Empty
    public const string Nothing = "Empty";

    private void Start()
    {
        Animations = this;
    }
}
