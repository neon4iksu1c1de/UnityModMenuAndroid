using UnityEngine;

public class SimpleFlatModMenu : MonoBehaviour
{
    /// fields
    public static GUIStyle BgStyle, OnStyle, OffStyle, LabelStyle, BtnStyle;

    public static GUIStyle BtnStyle1, BtnStyle2, BtnStyle3;
    public static bool toggle1, toggle2, toggle3;

    // Size of image/logo (x, y, width, height)
    public static Rect imageRect = new Rect(10, 10, 70, 70);
    public static bool buttonPressed = false;
    public static Texture2D Image = null;
    public static bool ShowHide = false;
    public static bool ifDragged = false;

    public static Texture2D ontexture, onpresstexture, offtexture, offpresstexture, backtexture, btntexture, btnpresstexture;
    public static Texture2D NewTexture2D { get { return new Texture2D(1, 1); } }

    // Damage multiplier
    public static int dmgMulti = 1;
    // Remember Y position
    public static int btnY;

    // Must be static and have other name than OnGUI if you create this as new class.
    // Find active classes like UIRoot, UIdrawcall, Soundmanager or something similar
    // and add:
    // public void OnGUI()
    // {
    //   	NewBehaviourScript.MyGUI();
    // }
	// Unity editor: public void OnGUI()
    public static void MyGUI()
    {
        // This is the bytes of .png image.
        // There must be a if-statement to load image once to avoid memory leaking
        if (Image == null)
        {
            imageHex();
            // No need Start(); if you are testing it on Unity Editor
            Start();
        }

        // Draw image
        GUI.DrawTexture(imageRect, Image);
        
        // Had to use "else if" statement to avoid problems
        if (imageRect.Contains(Event.current.mousePosition) && Event.current.type == EventType.MouseDrag)
        {
            buttonPressed = true;
            ifDragged = true;
        }
        else if (buttonPressed && Event.current.type == EventType.MouseDrag)
        {
            imageRect.x += Event.current.delta.x;
            // Invert y fix for Android: -= Event.current.delta.y;
            // Normal: += Event.current.delta.y;
            imageRect.y -= Event.current.delta.y;
            ifDragged = true;
        }
        else if (imageRect.Contains(Event.current.mousePosition) && Event.current.type == EventType.MouseUp)
        {
            buttonPressed = false;
            
            if (!ifDragged)
                ShowHide = !ShowHide;
            ifDragged = false;
        }

        // I created additional class (MyMenu) of GUI stuff to avoid high CPU usage
        // and lagging on low-end devices
        // Never make your whole GUI codes in OnGUI()
        if (ShowHide)
        {
            // if ShowHide true, call MyMenu();
            MyMenu();
        }
    }

	// Unity editor: public void MyMenu()
    public static void MyMenu()
    {
        /// Credit
        // (x, y, width, height)
        GUI.Box(new Rect(imageRect.x + 35f, imageRect.y + 65f, 260f, 320f), "", BgStyle);
        GUI.Label(new Rect(imageRect.x + 40f, imageRect.y + 70f, 260f, 280f), "Your name\nYour website", LabelStyle);

        // Off/On Buttons
        // Multiplier buttons. How to hack in game function:
        // public int getDamage
        // {
        //       if (MyClassNameOfModMenu.toggle1)
        //       {
        //              return 999999;
        //       }
        //       return this.get_dmg;
        // }
        if (toggle1)
        {
            if (GUI.Button(BtnRect(1, false), "God mode: ON", OnStyle))
            {
                toggle1 = false;
            }
        }
        else if (GUI.Button(BtnRect(1, false), "God mode: OFF", OffStyle))
        {
            toggle1 = true;
        }

        if (toggle2)
        {
            if (GUI.Button(BtnRect(2, false), "Infinite MP: ON", OnStyle))
            {
                toggle2 = false;
            }
        }
        else if (GUI.Button(BtnRect(2, false), "Infinite MP: OFF", OffStyle))
        {
            toggle2 = true;
        }

        if (toggle3)
        {
            if (GUI.Button(BtnRect(3, false), "Very high attack: ON", OnStyle))
            {
                toggle3 = false;
            }
        }
        else if (GUI.Button(BtnRect(3, false), "Very high attack: OFF", OffStyle))
        {
            toggle3 = true;
        }

        /// Clicker Button
        // You can add own code or re-use code to do something fun like
        // Instant win: PluginMM.scenario.DebugWinForAll();
        if (GUI.Button(BtnRect(4, false), "Instant win", BtnStyle))
        {

        }

        /// Multiplier buttons. How to hack in game function:
        // public int getDamage
        // {
        //     return this.get_dmg * MyClassNameOfModMenu.dmgMulti;
        // }
        if (GUI.Button(BtnRect(5, true), "DMG multiplier: " + dmgMulti.ToString(), BtnStyle))
        {

        }
        if (GUI.Button(new Rect(imageRect.x + 205, imageRect.y + btnY, 40, 40), "-", OffStyle))
        {
            if (dmgMulti > 1 && dmgMulti <= 10)
                dmgMulti--;
        }
        if (GUI.Button(new Rect(imageRect.x + 250, imageRect.y + btnY, 40, 40), "+", OffStyle))
        {
            if (dmgMulti >= 1 && dmgMulti < 10)
                dmgMulti++;
        }

        /// Clicker Button
        // Open URL
        if (GUI.Button(BtnRect(6, false), "Visit (your website)", BtnStyle))
        {
            Application.OpenURL("https://google.com/");
        }
    }

    /// Rect for buttons
    public static Rect BtnRect(int y, bool multiplyBtn)
    {
        if (multiplyBtn)
        {
            btnY = 70 + 45 * y;
            return new Rect(imageRect.x + 40, imageRect.y + 70 + 45 * y, 160, 40);
        }
        return new Rect(imageRect.x + 40, imageRect.y + 70 + 45 * y, 250, 40);
    }

    /// Load GUIStyle
	// Unity editor: public static void Start()
    public static void Start()
    {
        if (BgStyle == null)
        {
            BgStyle = new GUIStyle();
            BgStyle.normal.background = BackTexture;
            BgStyle.onNormal.background = BackTexture;
            BgStyle.active.background = BackTexture;
            BgStyle.onActive.background = BackTexture;
            BgStyle.normal.textColor = Color.white;
            BgStyle.onNormal.textColor = Color.white;
            BgStyle.active.textColor = Color.white;
            BgStyle.onActive.textColor = Color.white;
            BgStyle.fontSize = 18;
            BgStyle.fontStyle = FontStyle.Normal;
            BgStyle.alignment = TextAnchor.UpperCenter;
        }

        if (LabelStyle == null)
        {
            LabelStyle = new GUIStyle();
            LabelStyle.normal.textColor = Color.white;
            LabelStyle.onNormal.textColor = Color.white;
            LabelStyle.active.textColor = Color.white;
            LabelStyle.onActive.textColor = Color.white;
            LabelStyle.fontSize = 18;
            LabelStyle.fontStyle = FontStyle.Normal;
            LabelStyle.alignment = TextAnchor.UpperCenter;
        }

        if (OffStyle == null)
        {
            OffStyle = new GUIStyle();
            OffStyle.normal.background = OffTexture;
            OffStyle.onNormal.background = OffTexture;
            OffStyle.active.background = OffPressTexture;
            OffStyle.onActive.background = OffPressTexture;
            OffStyle.normal.textColor = Color.white;
            OffStyle.onNormal.textColor = Color.white;
            OffStyle.active.textColor = Color.white;
            OffStyle.onActive.textColor = Color.white;
            OffStyle.fontSize = 18;
            OffStyle.fontStyle = FontStyle.Normal;
            OffStyle.alignment = TextAnchor.MiddleCenter;
        }

        if (OnStyle == null)
        {
            OnStyle = new GUIStyle();
            OnStyle.normal.background = OnTexture;
            OnStyle.onNormal.background = OnTexture;
            OnStyle.active.background = OnPressTexture;
            OnStyle.onActive.background = OnPressTexture;
            OnStyle.normal.textColor = Color.white;
            OnStyle.onNormal.textColor = Color.white;
            OnStyle.active.textColor = Color.white;
            OnStyle.onActive.textColor = Color.white;
            OnStyle.fontSize = 18;
            OnStyle.fontStyle = FontStyle.Normal;
            OnStyle.alignment = TextAnchor.MiddleCenter;
        }

        if (BtnStyle == null)
        {
            BtnStyle = new GUIStyle();
            BtnStyle.normal.background = BtnTexture;
            BtnStyle.onNormal.background = BtnTexture;
            BtnStyle.active.background = BtnPressTexture;
            BtnStyle.onActive.background = BtnPressTexture;
            BtnStyle.normal.textColor = Color.white;
            BtnStyle.onNormal.textColor = Color.white;
            BtnStyle.active.textColor = Color.white;
            BtnStyle.onActive.textColor = Color.white;
            BtnStyle.fontSize = 18;
            BtnStyle.fontStyle = FontStyle.Normal;
            BtnStyle.alignment = TextAnchor.MiddleCenter;
        }
    }

    /// Textures
    // Use material colors and convert hex code to rbg https://www.materialpalette.com/colors
    public static Texture2D BtnTexture
    {
        get
        {
            if (btntexture == null)
            {
                btntexture = NewTexture2D;
                btntexture.SetPixel(0, 0, new Color32(3, 155, 229, 255));
                btntexture.Apply();
            }
            return btntexture;
        }
    }

    public static Texture2D BtnPressTexture
    {
        get
        {
            if (btnpresstexture == null)
            {
                btnpresstexture = NewTexture2D;
                btnpresstexture.SetPixel(0, 0, new Color32(2, 119, 189, 255));
                btnpresstexture.Apply();
            }
            return btnpresstexture;
        }
    }

    public static Texture2D OnPressTexture
    {
        get
        {
            if (onpresstexture == null)
            {
                onpresstexture = NewTexture2D;
                onpresstexture.SetPixel(0, 0, new Color32(56, 142, 60, 255));
                onpresstexture.Apply();
            }
            return onpresstexture;
        }
    }

    public static Texture2D OnTexture
    {
        get
        {
            if (ontexture == null)
            {
                ontexture = NewTexture2D;
                ontexture.SetPixel(0, 0, new Color32(76, 175, 80, 255));
                ontexture.Apply();
            }
            return ontexture;
        }
    }

    public static Texture2D OffPressTexture
    {
        get
        {
            if (offpresstexture == null)
            {
                offpresstexture = NewTexture2D;
                offpresstexture.SetPixel(0, 0, new Color32(211, 47, 47, 255));
                offpresstexture.Apply();
            }
            return offpresstexture;
        }
    }

    public static Texture2D OffTexture
    {
        get
        {
            if (offtexture == null)
            {
                offtexture = NewTexture2D;
                offtexture.SetPixel(0, 0, new Color32(244, 67, 54, 255));
                offtexture.Apply();
            }
            return offtexture;
        }
    }

    public static Texture2D BackTexture
    {
        get
        {
            if (backtexture == null)
            {
                backtexture = NewTexture2D;
                //ToHtmlStringRGBA  new Color(33, 150, 243, 1)
                backtexture.SetPixel(0, 0, new Color32(42, 42, 42, 200));
                backtexture.Apply();
            }
            return backtexture;
        }
    }

    ///Load Image/logo
    public static void imageHex()
    {
        //To add your own image, open image file in HxD, select all, Edit -> Copy as -> C#
        //and paste it here
        byte[] rawData = {
    0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A, 0x00, 0x00, 0x00, 0x0D,
    0x49, 0x48, 0x44, 0x52, 0x00, 0x00, 0x00, 0x46, 0x00, 0x00, 0x00, 0x46,
    0x08, 0x06, 0x00, 0x00, 0x00, 0x71, 0x2E, 0xE2, 0x84, 0x00, 0x00, 0x01,
    0x28, 0x69, 0x43, 0x43, 0x50, 0x41, 0x64, 0x6F, 0x62, 0x65, 0x20, 0x52,
    0x47, 0x42, 0x20, 0x28, 0x31, 0x39, 0x39, 0x38, 0x29, 0x00, 0x00, 0x28,
    0xCF, 0x63, 0x60, 0x60, 0x32, 0x70, 0x74, 0x71, 0x72, 0x65, 0x12, 0x60,
    0x60, 0xC8, 0xCD, 0x2B, 0x29, 0x0A, 0x72, 0x77, 0x52, 0x88, 0x88, 0x8C,
    0x52, 0x60, 0x3F, 0xCF, 0xC0, 0xC6, 0xC0, 0xCC, 0x00, 0x06, 0x89, 0xC9,
    0xC5, 0x05, 0x8E, 0x01, 0x01, 0x3E, 0x20, 0x76, 0x5E, 0x7E, 0x5E, 0x2A,
    0x03, 0x2A, 0x60, 0x64, 0x60, 0xF8, 0x76, 0x0D, 0x44, 0x32, 0x30, 0x5C,
    0xD6, 0x05, 0x99, 0xC5, 0x40, 0x1A, 0xE0, 0x4A, 0x2E, 0x28, 0x2A, 0x01,
    0xD2, 0x7F, 0x80, 0xD8, 0x28, 0x25, 0xB5, 0x38, 0x19, 0x68, 0xA4, 0x01,
    0x90, 0x9D, 0x5D, 0x5E, 0x52, 0x00, 0x14, 0x67, 0x9C, 0x03, 0x64, 0x8B,
    0x24, 0x65, 0x83, 0xD9, 0x1B, 0x40, 0xEC, 0xA2, 0x90, 0x20, 0x67, 0x20,
    0xFB, 0x08, 0x90, 0xCD, 0x97, 0x0E, 0x61, 0x5F, 0x01, 0xB1, 0x93, 0x20,
    0xEC, 0x27, 0x20, 0x76, 0x11, 0xD0, 0x13, 0x40, 0xF6, 0x17, 0x90, 0xFA,
    0x74, 0x30, 0x9B, 0x89, 0x03, 0x6C, 0x0E, 0x84, 0x2D, 0x03, 0x62, 0x97,
    0xA4, 0x56, 0x80, 0xEC, 0x65, 0x70, 0xCE, 0x2F, 0xA8, 0x2C, 0xCA, 0x4C,
    0xCF, 0x28, 0x51, 0x30, 0xB4, 0xB4, 0xB4, 0x54, 0x70, 0x4C, 0xC9, 0x4F,
    0x4A, 0x55, 0x08, 0xAE, 0x2C, 0x2E, 0x49, 0xCD, 0x2D, 0x56, 0xF0, 0xCC,
    0x4B, 0xCE, 0x2F, 0x2A, 0xC8, 0x2F, 0x4A, 0x2C, 0x49, 0x4D, 0x01, 0xAA,
    0x85, 0xB8, 0x0F, 0x0C, 0x04, 0x21, 0x0A, 0x41, 0x21, 0xA6, 0x01, 0xD4,
    0x68, 0xA1, 0xC9, 0x40, 0x65, 0x00, 0x8A, 0x07, 0x08, 0xEB, 0x73, 0x20,
    0x38, 0x7C, 0x19, 0xC5, 0xCE, 0x20, 0xC4, 0x10, 0x20, 0xB9, 0xB4, 0xA8,
    0x0C, 0x16, 0x17, 0x4C, 0xC6, 0x84, 0xF9, 0x08, 0x33, 0xE6, 0x48, 0x30,
    0x30, 0xF8, 0x2F, 0x65, 0x60, 0x60, 0xF9, 0x83, 0x10, 0x33, 0xE9, 0x65,
    0x60, 0x58, 0xA0, 0xC3, 0xC0, 0xC0, 0x3F, 0x15, 0x21, 0xA6, 0x66, 0xC8,
    0xC0, 0x20, 0xA0, 0xCF, 0xC0, 0xB0, 0x6F, 0x0E, 0x00, 0xC2, 0xB3, 0x4F,
    0xFE, 0xC6, 0x46, 0xEC, 0xE5, 0x00, 0x00, 0x00, 0x09, 0x70, 0x48, 0x59,
    0x73, 0x00, 0x00, 0x2E, 0x23, 0x00, 0x00, 0x2E, 0x23, 0x01, 0x78, 0xA5,
    0x3F, 0x76, 0x00, 0x00, 0x05, 0xF9, 0x69, 0x54, 0x58, 0x74, 0x58, 0x4D,
    0x4C, 0x3A, 0x63, 0x6F, 0x6D, 0x2E, 0x61, 0x64, 0x6F, 0x62, 0x65, 0x2E,
    0x78, 0x6D, 0x70, 0x00, 0x00, 0x00, 0x00, 0x00, 0x3C, 0x3F, 0x78, 0x70,
    0x61, 0x63, 0x6B, 0x65, 0x74, 0x20, 0x62, 0x65, 0x67, 0x69, 0x6E, 0x3D,
    0x22, 0xEF, 0xBB, 0xBF, 0x22, 0x20, 0x69, 0x64, 0x3D, 0x22, 0x57, 0x35,
    0x4D, 0x30, 0x4D, 0x70, 0x43, 0x65, 0x68, 0x69, 0x48, 0x7A, 0x72, 0x65,
    0x53, 0x7A, 0x4E, 0x54, 0x63, 0x7A, 0x6B, 0x63, 0x39, 0x64, 0x22, 0x3F,
    0x3E, 0x20, 0x3C, 0x78, 0x3A, 0x78, 0x6D, 0x70, 0x6D, 0x65, 0x74, 0x61,
    0x20, 0x78, 0x6D, 0x6C, 0x6E, 0x73, 0x3A, 0x78, 0x3D, 0x22, 0x61, 0x64,
    0x6F, 0x62, 0x65, 0x3A, 0x6E, 0x73, 0x3A, 0x6D, 0x65, 0x74, 0x61, 0x2F,
    0x22, 0x20, 0x78, 0x3A, 0x78, 0x6D, 0x70, 0x74, 0x6B, 0x3D, 0x22, 0x41,
    0x64, 0x6F, 0x62, 0x65, 0x20, 0x58, 0x4D, 0x50, 0x20, 0x43, 0x6F, 0x72,
    0x65, 0x20, 0x35, 0x2E, 0x36, 0x2D, 0x63, 0x31, 0x34, 0x32, 0x20, 0x37,
    0x39, 0x2E, 0x31, 0x36, 0x30, 0x39, 0x32, 0x34, 0x2C, 0x20, 0x32, 0x30,
    0x31, 0x37, 0x2F, 0x30, 0x37, 0x2F, 0x31, 0x33, 0x2D, 0x30, 0x31, 0x3A,
    0x30, 0x36, 0x3A, 0x33, 0x39, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20,
    0x20, 0x22, 0x3E, 0x20, 0x3C, 0x72, 0x64, 0x66, 0x3A, 0x52, 0x44, 0x46,
    0x20, 0x78, 0x6D, 0x6C, 0x6E, 0x73, 0x3A, 0x72, 0x64, 0x66, 0x3D, 0x22,
    0x68, 0x74, 0x74, 0x70, 0x3A, 0x2F, 0x2F, 0x77, 0x77, 0x77, 0x2E, 0x77,
    0x33, 0x2E, 0x6F, 0x72, 0x67, 0x2F, 0x31, 0x39, 0x39, 0x39, 0x2F, 0x30,
    0x32, 0x2F, 0x32, 0x32, 0x2D, 0x72, 0x64, 0x66, 0x2D, 0x73, 0x79, 0x6E,
    0x74, 0x61, 0x78, 0x2D, 0x6E, 0x73, 0x23, 0x22, 0x3E, 0x20, 0x3C, 0x72,
    0x64, 0x66, 0x3A, 0x44, 0x65, 0x73, 0x63, 0x72, 0x69, 0x70, 0x74, 0x69,
    0x6F, 0x6E, 0x20, 0x72, 0x64, 0x66, 0x3A, 0x61, 0x62, 0x6F, 0x75, 0x74,
    0x3D, 0x22, 0x22, 0x20, 0x78, 0x6D, 0x6C, 0x6E, 0x73, 0x3A, 0x78, 0x6D,
    0x70, 0x3D, 0x22, 0x68, 0x74, 0x74, 0x70, 0x3A, 0x2F, 0x2F, 0x6E, 0x73,
    0x2E, 0x61, 0x64, 0x6F, 0x62, 0x65, 0x2E, 0x63, 0x6F, 0x6D, 0x2F, 0x78,
    0x61, 0x70, 0x2F, 0x31, 0x2E, 0x30, 0x2F, 0x22, 0x20, 0x78, 0x6D, 0x6C,
    0x6E, 0x73, 0x3A, 0x78, 0x6D, 0x70, 0x4D, 0x4D, 0x3D, 0x22, 0x68, 0x74,
    0x74, 0x70, 0x3A, 0x2F, 0x2F, 0x6E, 0x73, 0x2E, 0x61, 0x64, 0x6F, 0x62,
    0x65, 0x2E, 0x63, 0x6F, 0x6D, 0x2F, 0x78, 0x61, 0x70, 0x2F, 0x31, 0x2E,
    0x30, 0x2F, 0x6D, 0x6D, 0x2F, 0x22, 0x20, 0x78, 0x6D, 0x6C, 0x6E, 0x73,
    0x3A, 0x73, 0x74, 0x45, 0x76, 0x74, 0x3D, 0x22, 0x68, 0x74, 0x74, 0x70,
    0x3A, 0x2F, 0x2F, 0x6E, 0x73, 0x2E, 0x61, 0x64, 0x6F, 0x62, 0x65, 0x2E,
    0x63, 0x6F, 0x6D, 0x2F, 0x78, 0x61, 0x70, 0x2F, 0x31, 0x2E, 0x30, 0x2F,
    0x73, 0x54, 0x79, 0x70, 0x65, 0x2F, 0x52, 0x65, 0x73, 0x6F, 0x75, 0x72,
    0x63, 0x65, 0x45, 0x76, 0x65, 0x6E, 0x74, 0x23, 0x22, 0x20, 0x78, 0x6D,
    0x6C, 0x6E, 0x73, 0x3A, 0x64, 0x63, 0x3D, 0x22, 0x68, 0x74, 0x74, 0x70,
    0x3A, 0x2F, 0x2F, 0x70, 0x75, 0x72, 0x6C, 0x2E, 0x6F, 0x72, 0x67, 0x2F,
    0x64, 0x63, 0x2F, 0x65, 0x6C, 0x65, 0x6D, 0x65, 0x6E, 0x74, 0x73, 0x2F,
    0x31, 0x2E, 0x31, 0x2F, 0x22, 0x20, 0x78, 0x6D, 0x6C, 0x6E, 0x73, 0x3A,
    0x70, 0x68, 0x6F, 0x74, 0x6F, 0x73, 0x68, 0x6F, 0x70, 0x3D, 0x22, 0x68,
    0x74, 0x74, 0x70, 0x3A, 0x2F, 0x2F, 0x6E, 0x73, 0x2E, 0x61, 0x64, 0x6F,
    0x62, 0x65, 0x2E, 0x63, 0x6F, 0x6D, 0x2F, 0x70, 0x68, 0x6F, 0x74, 0x6F,
    0x73, 0x68, 0x6F, 0x70, 0x2F, 0x31, 0x2E, 0x30, 0x2F, 0x22, 0x20, 0x78,
    0x6D, 0x70, 0x3A, 0x43, 0x72, 0x65, 0x61, 0x74, 0x6F, 0x72, 0x54, 0x6F,
    0x6F, 0x6C, 0x3D, 0x22, 0x41, 0x64, 0x6F, 0x62, 0x65, 0x20, 0x50, 0x68,
    0x6F, 0x74, 0x6F, 0x73, 0x68, 0x6F, 0x70, 0x20, 0x43, 0x43, 0x20, 0x32,
    0x30, 0x31, 0x38, 0x20, 0x28, 0x57, 0x69, 0x6E, 0x64, 0x6F, 0x77, 0x73,
    0x29, 0x22, 0x20, 0x78, 0x6D, 0x70, 0x3A, 0x43, 0x72, 0x65, 0x61, 0x74,
    0x65, 0x44, 0x61, 0x74, 0x65, 0x3D, 0x22, 0x32, 0x30, 0x31, 0x38, 0x2D,
    0x30, 0x37, 0x2D, 0x31, 0x39, 0x54, 0x31, 0x31, 0x3A, 0x33, 0x33, 0x3A,
    0x34, 0x37, 0x2B, 0x30, 0x32, 0x3A, 0x30, 0x30, 0x22, 0x20, 0x78, 0x6D,
    0x70, 0x3A, 0x4D, 0x65, 0x74, 0x61, 0x64, 0x61, 0x74, 0x61, 0x44, 0x61,
    0x74, 0x65, 0x3D, 0x22, 0x32, 0x30, 0x31, 0x38, 0x2D, 0x30, 0x37, 0x2D,
    0x31, 0x39, 0x54, 0x31, 0x31, 0x3A, 0x33, 0x33, 0x3A, 0x34, 0x37, 0x2B,
    0x30, 0x32, 0x3A, 0x30, 0x30, 0x22, 0x20, 0x78, 0x6D, 0x70, 0x3A, 0x4D,
    0x6F, 0x64, 0x69, 0x66, 0x79, 0x44, 0x61, 0x74, 0x65, 0x3D, 0x22, 0x32,
    0x30, 0x31, 0x38, 0x2D, 0x30, 0x37, 0x2D, 0x31, 0x39, 0x54, 0x31, 0x31,
    0x3A, 0x33, 0x33, 0x3A, 0x34, 0x37, 0x2B, 0x30, 0x32, 0x3A, 0x30, 0x30,
    0x22, 0x20, 0x78, 0x6D, 0x70, 0x4D, 0x4D, 0x3A, 0x49, 0x6E, 0x73, 0x74,
    0x61, 0x6E, 0x63, 0x65, 0x49, 0x44, 0x3D, 0x22, 0x78, 0x6D, 0x70, 0x2E,
    0x69, 0x69, 0x64, 0x3A, 0x38, 0x36, 0x31, 0x66, 0x32, 0x32, 0x38, 0x65,
    0x2D, 0x65, 0x34, 0x33, 0x61, 0x2D, 0x62, 0x61, 0x34, 0x35, 0x2D, 0x61,
    0x65, 0x31, 0x39, 0x2D, 0x39, 0x30, 0x38, 0x61, 0x33, 0x34, 0x34, 0x31,
    0x62, 0x64, 0x62, 0x66, 0x22, 0x20, 0x78, 0x6D, 0x70, 0x4D, 0x4D, 0x3A,
    0x44, 0x6F, 0x63, 0x75, 0x6D, 0x65, 0x6E, 0x74, 0x49, 0x44, 0x3D, 0x22,
    0x61, 0x64, 0x6F, 0x62, 0x65, 0x3A, 0x64, 0x6F, 0x63, 0x69, 0x64, 0x3A,
    0x70, 0x68, 0x6F, 0x74, 0x6F, 0x73, 0x68, 0x6F, 0x70, 0x3A, 0x61, 0x61,
    0x64, 0x35, 0x38, 0x37, 0x64, 0x38, 0x2D, 0x66, 0x65, 0x37, 0x38, 0x2D,
    0x32, 0x39, 0x34, 0x34, 0x2D, 0x39, 0x36, 0x36, 0x30, 0x2D, 0x37, 0x64,
    0x32, 0x31, 0x39, 0x64, 0x61, 0x36, 0x64, 0x35, 0x34, 0x66, 0x22, 0x20,
    0x78, 0x6D, 0x70, 0x4D, 0x4D, 0x3A, 0x4F, 0x72, 0x69, 0x67, 0x69, 0x6E,
    0x61, 0x6C, 0x44, 0x6F, 0x63, 0x75, 0x6D, 0x65, 0x6E, 0x74, 0x49, 0x44,
    0x3D, 0x22, 0x78, 0x6D, 0x70, 0x2E, 0x64, 0x69, 0x64, 0x3A, 0x37, 0x37,
    0x32, 0x61, 0x36, 0x39, 0x32, 0x36, 0x2D, 0x65, 0x38, 0x61, 0x62, 0x2D,
    0x31, 0x39, 0x34, 0x30, 0x2D, 0x62, 0x35, 0x61, 0x31, 0x2D, 0x35, 0x35,
    0x37, 0x61, 0x37, 0x36, 0x31, 0x62, 0x33, 0x32, 0x36, 0x30, 0x22, 0x20,
    0x64, 0x63, 0x3A, 0x66, 0x6F, 0x72, 0x6D, 0x61, 0x74, 0x3D, 0x22, 0x69,
    0x6D, 0x61, 0x67, 0x65, 0x2F, 0x70, 0x6E, 0x67, 0x22, 0x20, 0x70, 0x68,
    0x6F, 0x74, 0x6F, 0x73, 0x68, 0x6F, 0x70, 0x3A, 0x43, 0x6F, 0x6C, 0x6F,
    0x72, 0x4D, 0x6F, 0x64, 0x65, 0x3D, 0x22, 0x33, 0x22, 0x20, 0x70, 0x68,
    0x6F, 0x74, 0x6F, 0x73, 0x68, 0x6F, 0x70, 0x3A, 0x49, 0x43, 0x43, 0x50,
    0x72, 0x6F, 0x66, 0x69, 0x6C, 0x65, 0x3D, 0x22, 0x41, 0x64, 0x6F, 0x62,
    0x65, 0x20, 0x52, 0x47, 0x42, 0x20, 0x28, 0x31, 0x39, 0x39, 0x38, 0x29,
    0x22, 0x3E, 0x20, 0x3C, 0x78, 0x6D, 0x70, 0x4D, 0x4D, 0x3A, 0x48, 0x69,
    0x73, 0x74, 0x6F, 0x72, 0x79, 0x3E, 0x20, 0x3C, 0x72, 0x64, 0x66, 0x3A,
    0x53, 0x65, 0x71, 0x3E, 0x20, 0x3C, 0x72, 0x64, 0x66, 0x3A, 0x6C, 0x69,
    0x20, 0x73, 0x74, 0x45, 0x76, 0x74, 0x3A, 0x61, 0x63, 0x74, 0x69, 0x6F,
    0x6E, 0x3D, 0x22, 0x63, 0x72, 0x65, 0x61, 0x74, 0x65, 0x64, 0x22, 0x20,
    0x73, 0x74, 0x45, 0x76, 0x74, 0x3A, 0x69, 0x6E, 0x73, 0x74, 0x61, 0x6E,
    0x63, 0x65, 0x49, 0x44, 0x3D, 0x22, 0x78, 0x6D, 0x70, 0x2E, 0x69, 0x69,
    0x64, 0x3A, 0x37, 0x37, 0x32, 0x61, 0x36, 0x39, 0x32, 0x36, 0x2D, 0x65,
    0x38, 0x61, 0x62, 0x2D, 0x31, 0x39, 0x34, 0x30, 0x2D, 0x62, 0x35, 0x61,
    0x31, 0x2D, 0x35, 0x35, 0x37, 0x61, 0x37, 0x36, 0x31, 0x62, 0x33, 0x32,
    0x36, 0x30, 0x22, 0x20, 0x73, 0x74, 0x45, 0x76, 0x74, 0x3A, 0x77, 0x68,
    0x65, 0x6E, 0x3D, 0x22, 0x32, 0x30, 0x31, 0x38, 0x2D, 0x30, 0x37, 0x2D,
    0x31, 0x39, 0x54, 0x31, 0x31, 0x3A, 0x33, 0x33, 0x3A, 0x34, 0x37, 0x2B,
    0x30, 0x32, 0x3A, 0x30, 0x30, 0x22, 0x20, 0x73, 0x74, 0x45, 0x76, 0x74,
    0x3A, 0x73, 0x6F, 0x66, 0x74, 0x77, 0x61, 0x72, 0x65, 0x41, 0x67, 0x65,
    0x6E, 0x74, 0x3D, 0x22, 0x41, 0x64, 0x6F, 0x62, 0x65, 0x20, 0x50, 0x68,
    0x6F, 0x74, 0x6F, 0x73, 0x68, 0x6F, 0x70, 0x20, 0x43, 0x43, 0x20, 0x32,
    0x30, 0x31, 0x38, 0x20, 0x28, 0x57, 0x69, 0x6E, 0x64, 0x6F, 0x77, 0x73,
    0x29, 0x22, 0x2F, 0x3E, 0x20, 0x3C, 0x72, 0x64, 0x66, 0x3A, 0x6C, 0x69,
    0x20, 0x73, 0x74, 0x45, 0x76, 0x74, 0x3A, 0x61, 0x63, 0x74, 0x69, 0x6F,
    0x6E, 0x3D, 0x22, 0x73, 0x61, 0x76, 0x65, 0x64, 0x22, 0x20, 0x73, 0x74,
    0x45, 0x76, 0x74, 0x3A, 0x69, 0x6E, 0x73, 0x74, 0x61, 0x6E, 0x63, 0x65,
    0x49, 0x44, 0x3D, 0x22, 0x78, 0x6D, 0x70, 0x2E, 0x69, 0x69, 0x64, 0x3A,
    0x38, 0x36, 0x31, 0x66, 0x32, 0x32, 0x38, 0x65, 0x2D, 0x65, 0x34, 0x33,
    0x61, 0x2D, 0x62, 0x61, 0x34, 0x35, 0x2D, 0x61, 0x65, 0x31, 0x39, 0x2D,
    0x39, 0x30, 0x38, 0x61, 0x33, 0x34, 0x34, 0x31, 0x62, 0x64, 0x62, 0x66,
    0x22, 0x20, 0x73, 0x74, 0x45, 0x76, 0x74, 0x3A, 0x77, 0x68, 0x65, 0x6E,
    0x3D, 0x22, 0x32, 0x30, 0x31, 0x38, 0x2D, 0x30, 0x37, 0x2D, 0x31, 0x39,
    0x54, 0x31, 0x31, 0x3A, 0x33, 0x33, 0x3A, 0x34, 0x37, 0x2B, 0x30, 0x32,
    0x3A, 0x30, 0x30, 0x22, 0x20, 0x73, 0x74, 0x45, 0x76, 0x74, 0x3A, 0x73,
    0x6F, 0x66, 0x74, 0x77, 0x61, 0x72, 0x65, 0x41, 0x67, 0x65, 0x6E, 0x74,
    0x3D, 0x22, 0x41, 0x64, 0x6F, 0x62, 0x65, 0x20, 0x50, 0x68, 0x6F, 0x74,
    0x6F, 0x73, 0x68, 0x6F, 0x70, 0x20, 0x43, 0x43, 0x20, 0x32, 0x30, 0x31,
    0x38, 0x20, 0x28, 0x57, 0x69, 0x6E, 0x64, 0x6F, 0x77, 0x73, 0x29, 0x22,
    0x20, 0x73, 0x74, 0x45, 0x76, 0x74, 0x3A, 0x63, 0x68, 0x61, 0x6E, 0x67,
    0x65, 0x64, 0x3D, 0x22, 0x2F, 0x22, 0x2F, 0x3E, 0x20, 0x3C, 0x2F, 0x72,
    0x64, 0x66, 0x3A, 0x53, 0x65, 0x71, 0x3E, 0x20, 0x3C, 0x2F, 0x78, 0x6D,
    0x70, 0x4D, 0x4D, 0x3A, 0x48, 0x69, 0x73, 0x74, 0x6F, 0x72, 0x79, 0x3E,
    0x20, 0x3C, 0x2F, 0x72, 0x64, 0x66, 0x3A, 0x44, 0x65, 0x73, 0x63, 0x72,
    0x69, 0x70, 0x74, 0x69, 0x6F, 0x6E, 0x3E, 0x20, 0x3C, 0x2F, 0x72, 0x64,
    0x66, 0x3A, 0x52, 0x44, 0x46, 0x3E, 0x20, 0x3C, 0x2F, 0x78, 0x3A, 0x78,
    0x6D, 0x70, 0x6D, 0x65, 0x74, 0x61, 0x3E, 0x20, 0x3C, 0x3F, 0x78, 0x70,
    0x61, 0x63, 0x6B, 0x65, 0x74, 0x20, 0x65, 0x6E, 0x64, 0x3D, 0x22, 0x72,
    0x22, 0x3F, 0x3E, 0xB3, 0xEE, 0x8B, 0x2D, 0x00, 0x00, 0x07, 0x97, 0x49,
    0x44, 0x41, 0x54, 0x78, 0xDA, 0xD5, 0x9C, 0x5B, 0x68, 0x15, 0x47, 0x18,
    0xC7, 0x63, 0x14, 0x35, 0xDE, 0x22, 0xAD, 0x26, 0xE6, 0xE6, 0x95, 0xC4,
    0x0B, 0x42, 0x04, 0x1F, 0x34, 0x2F, 0xFA, 0xA0, 0xB4, 0xA9, 0x14, 0x2B,
    0xE4, 0x42, 0x63, 0x6C, 0x6E, 0xA7, 0x2A, 0x16, 0x0C, 0xD2, 0xA7, 0xDA,
    0x17, 0x11, 0x45, 0xC1, 0x5B, 0x8A, 0xE9, 0x8B, 0x50, 0x44, 0xBC, 0x10,
    0x4D, 0xAC, 0x29, 0xC5, 0xA2, 0xA2, 0x0D, 0x94, 0x1A, 0x10, 0x5F, 0x24,
    0x42, 0x8B, 0xFA, 0x60, 0x5B, 0xB0, 0x29, 0xBE, 0xB4, 0xF6, 0x68, 0x13,
    0x8C, 0x85, 0xED, 0xF7, 0x5F, 0x66, 0x0E, 0x9B, 0xCD, 0xCE, 0xEE, 0xCC,
    0xEE, 0x37, 0x27, 0xE9, 0xC0, 0x9F, 0xC3, 0xD9, 0xB3, 0x3B, 0x33, 0xFB,
    0x3B, 0x73, 0xF9, 0xBE, 0x6F, 0x67, 0x36, 0x67, 0xF1, 0xE2, 0xC5, 0x39,
    0x59, 0xD2, 0x24, 0xD2, 0x6A, 0x52, 0x0B, 0xE9, 0x08, 0xE9, 0x6B, 0xD2,
    0x43, 0xD2, 0xEF, 0xA4, 0x34, 0xC9, 0x11, 0x4A, 0x8B, 0x63, 0x0F, 0xC5,
    0x39, 0x47, 0xC4, 0x35, 0xAB, 0x45, 0x1E, 0x59, 0xA9, 0xAF, 0xED, 0x02,
    0xDE, 0x22, 0xB5, 0x91, 0xBA, 0x48, 0xCF, 0x3D, 0x37, 0x1F, 0x57, 0xC8,
    0xE3, 0xB2, 0xC8, 0xF3, 0xED, 0xFF, 0x1B, 0x98, 0x5C, 0xD2, 0x16, 0xD2,
    0x37, 0xA4, 0x11, 0x06, 0x18, 0x2A, 0x8D, 0x88, 0x32, 0xB6, 0x88, 0x32,
    0x27, 0x2C, 0x98, 0x29, 0xA4, 0x66, 0xD2, 0x23, 0x8B, 0x30, 0x54, 0x7A,
    0x24, 0xBA, 0xDB, 0x94, 0x89, 0x06, 0xE6, 0x1D, 0xD2, 0xE3, 0x71, 0x00,
    0xE2, 0xD7, 0x63, 0x51, 0x97, 0x71, 0x07, 0x53, 0x4A, 0xEA, 0x9E, 0x00,
    0x40, 0xFC, 0xEA, 0x11, 0x75, 0x1B, 0x17, 0x30, 0x35, 0xA4, 0xBF, 0x26,
    0x20, 0x14, 0x29, 0xD4, 0xAD, 0x36, 0x9B, 0x60, 0xA6, 0x91, 0x3A, 0x27,
    0x30, 0x10, 0xBF, 0x3A, 0x45, 0x9D, 0xAD, 0x82, 0x99, 0x47, 0xBA, 0x6B,
    0xE3, 0x06, 0x0E, 0x1E, 0x3C, 0xE8, 0x9C, 0x3F, 0x7F, 0xDE, 0x16, 0x9C,
    0x7E, 0x51, 0x77, 0x3E, 0x30, 0x32, 0x89, 0x3E, 0x6B, 0x65, 0xC6, 0xD9,
    0xBD, 0x7B, 0xB7, 0x23, 0xD3, 0xC9, 0x93, 0x27, 0x6D, 0xCE, 0x5C, 0xA5,
    0xAC, 0x60, 0x44, 0x86, 0x4F, 0x6D, 0x43, 0xC9, 0x02, 0x9C, 0xA7, 0xBA,
    0x70, 0x74, 0xE8, 0xCD, 0xB7, 0x35, 0x15, 0xB7, 0xB5, 0xB5, 0x39, 0xAA,
    0x74, 0xEA, 0xD4, 0x29, 0x9B, 0x53, 0xFA, 0xFC, 0xA4, 0x60, 0xA6, 0x92,
    0xEE, 0xD9, 0xA8, 0xE0, 0xCE, 0x9D, 0x3B, 0x9D, 0xA8, 0x64, 0x11, 0xCE,
    0x3D, 0x71, 0x6F, 0xB1, 0xC1, 0x74, 0x66, 0xAB, 0xFB, 0x8C, 0x03, 0x9C,
    0x2F, 0xE3, 0x82, 0xA9, 0xB1, 0x51, 0xA1, 0x7D, 0xFB, 0xF6, 0x39, 0xA6,
    0xC9, 0x22, 0x9C, 0x1A, 0x53, 0x30, 0x25, 0xA4, 0x17, 0xDC, 0x15, 0x49,
    0xA5, 0x52, 0x4E, 0xDC, 0x64, 0x09, 0x0E, 0xEE, 0xB1, 0xCC, 0x04, 0x4C,
    0x0F, 0x77, 0x25, 0xF6, 0xEC, 0xD9, 0xE3, 0x24, 0x4D, 0xA7, 0x4F, 0x9F,
    0xB6, 0x01, 0xE7, 0x3B, 0x5D, 0x30, 0xEF, 0x72, 0x17, 0x7E, 0xE0, 0xC0,
    0x01, 0x87, 0x2B, 0x75, 0x75, 0x75, 0x39, 0xE5, 0xE5, 0xE5, 0xDC, 0x70,
    0x3E, 0x88, 0x02, 0x93, 0x6B, 0xC3, 0x88, 0x43, 0x17, 0x6A, 0x6E, 0x6E,
    0x76, 0x6A, 0x6A, 0x6A, 0x46, 0x09, 0xD6, 0xAE, 0x2A, 0x75, 0x77, 0x77,
    0x3B, 0xB5, 0xB5, 0xB5, 0x19, 0xD5, 0xD5, 0xD5, 0x39, 0x0D, 0x0D, 0x0D,
    0x4E, 0x7B, 0x7B, 0xBB, 0xB3, 0x6C, 0xD9, 0x32, 0x1B, 0xC6, 0xDF, 0xE4,
    0x30, 0x30, 0x1F, 0x72, 0x43, 0x59, 0xB4, 0x68, 0x91, 0xFB, 0x59, 0x54,
    0x54, 0xE4, 0x14, 0x14, 0x14, 0x8C, 0x52, 0x55, 0x55, 0x95, 0x12, 0xCC,
    0xFE, 0xFD, 0xFB, 0x9D, 0xC2, 0xC2, 0x42, 0x57, 0x0B, 0x16, 0x2C, 0x18,
    0xA3, 0xD2, 0xD2, 0x52, 0x6E, 0x38, 0x0D, 0x2A, 0x30, 0x93, 0x44, 0x9C,
    0x95, 0xAD, 0xB0, 0x85, 0x0B, 0x17, 0x8E, 0xFA, 0xEE, 0x87, 0x83, 0x56,
    0xA3, 0x4A, 0xC7, 0x8F, 0x1F, 0x57, 0x42, 0xB1, 0x04, 0xE7, 0xA1, 0x37,
    0xA6, 0xEC, 0x05, 0xF3, 0x9E, 0x4D, 0x28, 0x41, 0x70, 0x92, 0x82, 0x81,
    0xCA, 0xCA, 0xCA, 0x38, 0xE1, 0x6C, 0x09, 0x02, 0x73, 0x99, 0xAB, 0x00,
    0x54, 0x56, 0x76, 0xA1, 0x20, 0x15, 0x17, 0x17, 0x6B, 0x81, 0x89, 0x82,
    0x02, 0x01, 0x34, 0x23, 0x9C, 0xCB, 0x7E, 0x30, 0xF9, 0xA4, 0x61, 0x2E,
    0x28, 0xAA, 0xD6, 0xE2, 0x87, 0x13, 0x06, 0xE6, 0xC4, 0x89, 0x13, 0x5A,
    0x50, 0xA4, 0x98, 0xE0, 0x0C, 0x0B, 0x16, 0x19, 0x30, 0x6D, 0x5C, 0x50,
    0x4C, 0x2A, 0x58, 0x5F, 0x5F, 0x1F, 0x1B, 0x8C, 0x17, 0x0A, 0x33, 0x9C,
    0x36, 0x2F, 0x98, 0xDE, 0xA4, 0x76, 0x0A, 0x9C, 0x42, 0xB4, 0x02, 0x93,
    0xEB, 0x1A, 0x1B, 0x1B, 0x59, 0xC1, 0x6C, 0xDC, 0xB8, 0xD1, 0x39, 0x74,
    0xE8, 0x50, 0x52, 0x30, 0xBD, 0x12, 0xCC, 0xE4, 0x24, 0xB1, 0x5B, 0x69,
    0xD1, 0xEE, 0xDD, 0xBB, 0xD7, 0x1D, 0x37, 0x4C, 0xFE, 0xB5, 0x30, 0x30,
    0x70, 0x01, 0x4C, 0xA0, 0x40, 0x95, 0x95, 0x95, 0xEE, 0xB5, 0xE7, 0xCE,
    0x9D, 0x4B, 0xEA, 0x26, 0x4C, 0x06, 0x98, 0x4A, 0x0E, 0xDF, 0x67, 0xEB,
    0xD6, 0xAD, 0x99, 0xD9, 0x46, 0x17, 0x4E, 0x4B, 0x4B, 0x8B, 0x12, 0xCC,
    0x99, 0x33, 0x67, 0x8C, 0xA0, 0x40, 0x4B, 0x96, 0x2C, 0x71, 0xDE, 0xBC,
    0x79, 0xC3, 0xE1, 0x5B, 0x55, 0x02, 0x4C, 0x8A, 0xC3, 0x4B, 0xC6, 0x40,
    0xEA, 0xB5, 0x51, 0x74, 0xE0, 0xAC, 0x59, 0xB3, 0xC6, 0xB5, 0x68, 0xA5,
    0x65, 0xEB, 0xD5, 0xBA, 0x75, 0xEB, 0x8C, 0xA0, 0x40, 0x70, 0x15, 0xD2,
    0xE9, 0x34, 0x87, 0xE3, 0x99, 0x02, 0x98, 0x2F, 0x38, 0xE2, 0x29, 0x7E,
    0x30, 0x26, 0x2D, 0x07, 0x63, 0x93, 0xC9, 0x0C, 0xA4, 0x52, 0x45, 0x45,
    0x85, 0xF3, 0xF2, 0xE5, 0x4B, 0x0E, 0xAF, 0xBC, 0x03, 0x60, 0xAE, 0x9B,
    0x5C, 0xB4, 0x6B, 0xD7, 0xAE, 0xC0, 0xA6, 0x0F, 0xF3, 0xDE, 0x0F, 0x06,
    0x06, 0x1A, 0x17, 0x1C, 0x1D, 0x30, 0xC8, 0xC3, 0xDB, 0x62, 0x12, 0xC0,
    0xB9, 0x0E, 0x30, 0x03, 0xBA, 0x17, 0xB4, 0xB6, 0xB6, 0x2A, 0xC7, 0x04,
    0x38, 0x84, 0x7E, 0x27, 0x11, 0x42, 0x37, 0xC1, 0xB4, 0x2C, 0xB5, 0x6A,
    0xD5, 0x2A, 0xA5, 0x29, 0xAF, 0x82, 0xB3, 0x72, 0xE5, 0xCA, 0x31, 0x5D,
    0xCD, 0x2F, 0xE4, 0x0D, 0x47, 0x75, 0x68, 0x68, 0x88, 0x23, 0x9E, 0x33,
    0x00, 0x30, 0x7F, 0x70, 0x87, 0x23, 0xC3, 0xD2, 0xF6, 0xED, 0xDB, 0xDD,
    0x96, 0xA4, 0x0B, 0x07, 0x2D, 0x61, 0xC7, 0x8E, 0x1D, 0x2C, 0x65, 0x77,
    0x74, 0x74, 0xE8, 0x82, 0x19, 0xCC, 0xF1, 0x2D, 0xDA, 0xB1, 0x0A, 0x05,
    0x09, 0xFF, 0xAE, 0xF4, 0x81, 0xA2, 0xE0, 0xC8, 0x2E, 0x12, 0x66, 0x08,
    0x5A, 0x8A, 0x04, 0xA6, 0x73, 0xA2, 0x4E, 0x82, 0xC1, 0xC4, 0x99, 0xD0,
    0xB5, 0x74, 0x3C, 0x64, 0xC0, 0x91, 0x60, 0x00, 0x93, 0x33, 0x5D, 0xBC,
    0x78, 0x31, 0x12, 0x4E, 0x24, 0x18, 0x64, 0x62, 0x13, 0x8C, 0x0E, 0x1C,
    0x6E, 0x30, 0x0F, 0x1E, 0x3C, 0x88, 0xF4, 0xE7, 0x22, 0xBB, 0x12, 0x32,
    0x38, 0x76, 0xEC, 0x18, 0x6B, 0x57, 0x32, 0x89, 0xAD, 0x94, 0x94, 0x94,
    0xB0, 0x82, 0xB9, 0x71, 0xE3, 0x86, 0x9B, 0x27, 0xCA, 0x0B, 0x81, 0x93,
    0xD6, 0x1A, 0x7C, 0x91, 0x09, 0xC2, 0x00, 0x1C, 0x09, 0xD6, 0xAE, 0x69,
    0xE0, 0x29, 0xC9, 0xD3, 0x05, 0x15, 0x14, 0x29, 0x05, 0x9C, 0x41, 0xED,
    0xE9, 0x3A, 0x0A, 0xCE, 0x95, 0x2B, 0x57, 0xDC, 0xDF, 0x55, 0x82, 0x53,
    0x08, 0x05, 0x59, 0xB4, 0xDE, 0x19, 0x28, 0x08, 0xCE, 0x86, 0x0D, 0x1B,
    0xDC, 0xE7, 0xD9, 0xB8, 0x1E, 0x9F, 0x2A, 0xE1, 0x29, 0xC2, 0xC8, 0xC8,
    0x48, 0x60, 0xFD, 0xEE, 0xDC, 0xB9, 0x33, 0x06, 0x4A, 0x08, 0x9C, 0x01,
    0x23, 0x03, 0x2F, 0x0C, 0xCE, 0xDA, 0xB5, 0x6B, 0x33, 0x31, 0x5A, 0xBF,
    0x74, 0x02, 0x4E, 0xDE, 0x59, 0x28, 0xAC, 0x5B, 0x61, 0xDC, 0x09, 0x53,
    0x90, 0x81, 0x77, 0xEB, 0xD6, 0x2D, 0x25, 0x14, 0x05, 0x9C, 0xEB, 0xC6,
    0x2E, 0x81, 0x0A, 0x0E, 0x06, 0xD5, 0x24, 0x60, 0x74, 0x63, 0x2B, 0x61,
    0x70, 0x82, 0x5C, 0x82, 0xA0, 0xEE, 0xA3, 0x01, 0xA7, 0x23, 0x96, 0x13,
    0x19, 0x04, 0x47, 0x05, 0x26, 0x0C, 0xC6, 0x8A, 0x15, 0x2B, 0x94, 0x96,
    0xEC, 0xFA, 0xF5, 0xEB, 0x95, 0x70, 0x50, 0x7E, 0x10, 0x98, 0xE5, 0xCB,
    0x97, 0x8F, 0x6A, 0x31, 0xD7, 0xAE, 0x5D, 0x73, 0xA1, 0xE8, 0x82, 0x81,
    0x44, 0x48, 0x36, 0x15, 0x3B, 0xEC, 0xE0, 0x87, 0xB3, 0x6D, 0xDB, 0x36,
    0xE3, 0x96, 0x12, 0x16, 0x8F, 0x41, 0xD8, 0x21, 0xAC, 0xE5, 0xF8, 0xE1,
    0xE0, 0xE6, 0xE1, 0x5D, 0x4B, 0x97, 0xE0, 0xF6, 0xED, 0xDB, 0xC6, 0x50,
    0x20, 0x51, 0x5E, 0xA5, 0x0C, 0x54, 0xBD, 0x88, 0x0B, 0x07, 0x66, 0x36,
    0x12, 0xFC, 0x28, 0xD3, 0xEE, 0x13, 0x66, 0xD1, 0xC2, 0x42, 0x95, 0x8E,
    0xA1, 0x0E, 0x1C, 0x00, 0x40, 0x0B, 0x44, 0xBA, 0x7F, 0xFF, 0x7E, 0x6C,
    0x28, 0xA4, 0x21, 0x19, 0xA8, 0x4A, 0x14, 0xDA, 0x44, 0x86, 0x97, 0x2E,
    0x5D, 0x72, 0xE3, 0x33, 0xA6, 0x60, 0xC2, 0xEC, 0x13, 0xCC, 0x40, 0x12,
    0x4C, 0x14, 0x1C, 0x09, 0x61, 0xF3, 0xE6, 0xCD, 0xCE, 0xD5, 0xAB, 0x57,
    0x33, 0xC7, 0x62, 0x40, 0x81, 0x7E, 0x64, 0x0B, 0x86, 0xCB, 0xBE, 0x69,
    0x3A, 0xD8, 0x46, 0x81, 0xF1, 0x8F, 0x21, 0x51, 0x70, 0x30, 0xC6, 0x24,
    0x68, 0x29, 0x52, 0x9F, 0xB1, 0x3E, 0x3E, 0x41, 0x01, 0xA6, 0x33, 0x50,
    0x18, 0x18, 0xD8, 0x25, 0x41, 0x03, 0xAC, 0x4E, 0xCB, 0x31, 0x01, 0xE3,
    0x83, 0x32, 0x42, 0x9A, 0xC7, 0xFE, 0xC0, 0x0D, 0x99, 0x9B, 0x4C, 0xCB,
    0x71, 0xC0, 0xE8, 0xC0, 0x49, 0xD0, 0x5A, 0xBE, 0xB7, 0xF6, 0x88, 0x36,
    0x0A, 0x0E, 0x07, 0x98, 0x30, 0x38, 0x38, 0x1E, 0x13, 0x0A, 0xD4, 0x64,
    0xF5, 0xA1, 0xBE, 0x0A, 0x8E, 0xDF, 0x88, 0x8B, 0x0B, 0x46, 0x76, 0x97,
    0x24, 0x70, 0x02, 0xA0, 0xFC, 0x4A, 0x06, 0x5E, 0xAE, 0x6A, 0x19, 0x48,
    0x03, 0xE7, 0xF3, 0xEB, 0xA8, 0x98, 0x6D, 0x75, 0x75, 0xB5, 0x12, 0xCC,
    0xE1, 0xC3, 0x87, 0x43, 0xA1, 0x48, 0xA9, 0x3C, 0xE4, 0x30, 0x38, 0x01,
    0x50, 0x90, 0x4F, 0x7B, 0xD6, 0x16, 0x0E, 0x49, 0x38, 0x70, 0x02, 0x61,
    0x63, 0xC0, 0x00, 0xF3, 0xAA, 0xA9, 0xA9, 0x49, 0x09, 0xA6, 0xB3, 0xB3,
    0xD3, 0x35, 0xF1, 0xBD, 0x42, 0x1E, 0x9B, 0x36, 0x6D, 0xCA, 0xB8, 0x06,
    0x71, 0xE0, 0x04, 0x41, 0x21, 0x3D, 0xA3, 0x3C, 0xA6, 0x44, 0x2D, 0x35,
    0xAB, 0xE6, 0x5C, 0x0E, 0x82, 0x82, 0xE5, 0xCA, 0xA9, 0xE1, 0xE1, 0x61,
    0xD7, 0x97, 0x91, 0x52, 0x05, 0xAE, 0x91, 0x5E, 0xBF, 0x7E, 0x3D, 0xEA,
    0xDC, 0x57, 0xAF, 0x5E, 0xB9, 0xC7, 0x7B, 0x7A, 0x7A, 0x9C, 0xA5, 0x4B,
    0x97, 0x8E, 0x99, 0x81, 0x74, 0xE1, 0x28, 0x5A, 0x4B, 0x6A, 0x5C, 0x16,
    0x27, 0xA2, 0xF0, 0xB3, 0x67, 0xCF, 0x26, 0x8E, 0xA7, 0xC0, 0xA2, 0xF5,
    0xB7, 0x16, 0x8D, 0xD8, 0x4A, 0x06, 0x8E, 0xA2, 0xB5, 0xDC, 0x33, 0x59,
    0xB5, 0x89, 0xF5, 0xF6, 0x7F, 0x73, 0xC3, 0x81, 0xD1, 0x16, 0x37, 0x79,
    0x7D, 0x9F, 0x30, 0x5B, 0x25, 0x0C, 0x4E, 0x80, 0x86, 0xE8, 0xFC, 0x0A,
    0xD3, 0x05, 0xD0, 0x1F, 0x71, 0xAF, 0xC7, 0x8B, 0x0B, 0xA7, 0xB7, 0xB7,
    0x77, 0x0C, 0x94, 0x30, 0x7B, 0x45, 0x17, 0x0E, 0x9D, 0xF7, 0x69, 0xDC,
    0x25, 0xF3, 0x5F, 0x8D, 0x37, 0x1C, 0x19, 0x4F, 0xD1, 0x85, 0x62, 0x00,
    0xE7, 0xDB, 0x24, 0x7B, 0x09, 0xA6, 0xD9, 0xD8, 0x64, 0x81, 0x8A, 0xC1,
    0x4E, 0x89, 0x4A, 0x37, 0x6F, 0xDE, 0x8C, 0x05, 0x25, 0x0A, 0x0E, 0x1D,
    0xFF, 0x89, 0x94, 0xC7, 0xB1, 0x2D, 0xE7, 0x49, 0xB6, 0xE1, 0xF4, 0xF5,
    0xF5, 0x25, 0x82, 0x22, 0x07, 0x5B, 0xFF, 0x5A, 0x40, 0xFA, 0xFE, 0x94,
    0xA0, 0x44, 0xEE, 0x59, 0x8A, 0x04, 0x33, 0x7D, 0xFA, 0x74, 0x6B, 0x1B,
    0xB9, 0xF0, 0x8F, 0x06, 0xC1, 0x89, 0xDB, 0x7D, 0x54, 0xD3, 0xB2, 0x84,
    0x43, 0x9F, 0xBF, 0x51, 0x99, 0xE5, 0x2C, 0x1B, 0xB9, 0x00, 0x26, 0x2F,
    0x2F, 0x2F, 0x47, 0x6C, 0x46, 0x78, 0x64, 0x1B, 0x8E, 0x0A, 0x4A, 0x02,
    0x8F, 0x59, 0x0E, 0xB4, 0xBF, 0xA8, 0x66, 0xA0, 0x58, 0x60, 0x00, 0x45,
    0x4A, 0x6C, 0xB8, 0xEC, 0xB7, 0x01, 0xE7, 0xC2, 0x85, 0x0B, 0x4E, 0x7F,
    0x7F, 0x7F, 0x62, 0x28, 0x0A, 0x30, 0x3F, 0x53, 0x19, 0x25, 0xAC, 0x9B,
    0x45, 0xBD, 0x60, 0x66, 0xCC, 0x98, 0x61, 0x6D, 0x7B, 0x31, 0xE0, 0x58,
    0x82, 0xD2, 0x1D, 0x35, 0xD0, 0x26, 0x02, 0x03, 0x28, 0x52, 0x33, 0x67,
    0xCE, 0xC4, 0x6F, 0x75, 0xDC, 0x1B, 0xD2, 0x83, 0xE0, 0x24, 0x80, 0x02,
    0xE3, 0xED, 0x13, 0x6B, 0x1B, 0xD2, 0x83, 0xA0, 0x48, 0x89, 0x71, 0xA7,
    0xC7, 0x26, 0x9C, 0x98, 0x50, 0x7E, 0xC0, 0x78, 0x42, 0x0E, 0xAC, 0xBD,
    0x57, 0x18, 0xA8, 0xA0, 0xCC, 0x9A, 0x35, 0x2B, 0x23, 0xB1, 0xC7, 0xE9,
    0x09, 0x37, 0x9C, 0x18, 0xAD, 0x65, 0x90, 0xD4, 0x58, 0x58, 0x58, 0x98,
    0x03, 0x28, 0x59, 0x01, 0xA3, 0x82, 0x32, 0x7B, 0xF6, 0x6C, 0x57, 0xE2,
    0x15, 0x25, 0xAD, 0x5C, 0x33, 0x17, 0xE0, 0x18, 0x40, 0x79, 0x86, 0x20,
    0x36, 0x69, 0xAA, 0x17, 0xCA, 0x84, 0x00, 0x03, 0xCD, 0x99, 0x33, 0x47,
    0xC6, 0x74, 0xDE, 0xE7, 0x78, 0xB1, 0x4E, 0x04, 0x9C, 0x7F, 0x09, 0xC4,
    0x5D, 0x52, 0x0B, 0x29, 0x17, 0x40, 0xFC, 0x50, 0xAC, 0x83, 0xD1, 0x85,
    0x22, 0x95, 0x9F, 0x9F, 0xEF, 0x4A, 0xBC, 0x2E, 0xE9, 0x63, 0x11, 0x68,
    0x7F, 0x9E, 0x14, 0x0E, 0x01, 0x78, 0x41, 0xEA, 0x23, 0x7D, 0x4E, 0x2A,
    0x96, 0x30, 0x54, 0x50, 0xAC, 0x82, 0x89, 0x0B, 0x05, 0x9A, 0x3B, 0x77,
    0x6E, 0x46, 0xBE, 0x97, 0x77, 0x1D, 0xF5, 0xBC, 0xBC, 0x6B, 0x90, 0xF4,
    0x8F, 0xE7, 0xF5, 0x4A, 0x7F, 0x0A, 0x2B, 0x7B, 0x40, 0x9C, 0x73, 0x14,
    0xB3, 0x0B, 0x81, 0xA8, 0x42, 0xCB, 0x28, 0x28, 0x28, 0xC8, 0x81, 0xFC,
    0x50, 0x82, 0xC0, 0x14, 0x15, 0x15, 0xC5, 0x06, 0xF3, 0x1F, 0xEE, 0x1C,
    0xC7, 0x8E, 0x6E, 0xF5, 0x5A, 0x30, 0x00, 0x00, 0x00, 0x00, 0x49, 0x45,
    0x4E, 0x44, 0xAE, 0x42, 0x60, 0x82

        };
        Image = new Texture2D(1, 1);
        Image.LoadImage(rawData);
    }
}
