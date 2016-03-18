--
-- Score shadow class.
--
-- @filename  LgScoreShadow.lua
-- @copyright Copyright (c) 2015 Yaukey/yaukeywang/WangYaoqi (yaukeywang@gmail.com) all rights reserved.
-- @license   The MIT License (MIT)
-- @author    Yaukey
-- @date      2015-09-01
--

local DLog = YwDebug.Log
local DLogWarn = YwDebug.LogWarning
local DLogError = YwDebug.LogError

-- Register new class LgScoreShadow.
local strClassName = "LgScoreShadow"
local LgScoreShadow = YwDeclare(strClassName, YwClass(strClassName, YwMonoBehaviour))

-- Member variables.

-- The gui copy object.
LgScoreShadow.m_cGuiCopy = false

-- The score text component.
LgScoreShadow.m_cScoreTxt = false

-- The copy score text component.
LgScoreShadow.m_cCopyScoreText = false

-- Awake method.
function LgScoreShadow:Awake()
    --print("LgScoreShadow:Awake")

    -- Check variable.
    if (not self.this) or (not self.transform) or (not self.gameObject) then
        DLogError("Init error in LgScoreShadow!")
        return
    end

    -- Get gui copy.
    self.m_cGuiCopy = self.m_aParameters[1]

    -- Setting up the reference.
    self.m_cScoreTxt = self.gameObject:GetComponent(GUIText)
    self.m_cCopyScoreText = self.m_cGuiCopy:GetComponent(GUIText)
end

-- Update.
function LgScoreShadow:Update()
    --print("LgScoreShadow:Update")
    self.m_cScoreTxt.text = self.m_cCopyScoreText.text
end

-- Return this class.
return LgScoreShadow
