--
-- Score class.
--
-- @filename  LgScore.lua
-- @copyright Copyright (c) 2015 Yaukey/yaukeywang/WangYaoqi (yaukeywang@gmail.com) all rights reserved.
-- @license   The MIT License (MIT)
-- @author    Yaukey
-- @date      2015-09-01
--

local DLog = YwDebug.Log
local DLogWarn = YwDebug.LogWarning
local DLogError = YwDebug.LogError

-- Register new class LgScore.
local strClassName = "LgScore"
local LgScore = YwDeclare(strClassName, YwClass(strClassName, YwMonoBehaviour))

-- Member variables.

-- The score.
LgScore.m_nScore = 0

-- The score text component.
LgScore.m_cLgScoreTxt = false

-- Player control.
LgScore.m_cPlayerControl = false

-- Previous scroe.
LgScore.m_nPreviousLgScore = 0

-- Awake method.
function LgScore:Awake()
    --print("LgScore:Awake")

    -- Check variable.
    if (not self.this) or (not self.transform) or (not self.gameObject) then
        DLogError("Init error in LgScore!")
        return
    end

    -- Setting up the reference.
    self.m_cLgScoreTxt = self.gameObject:GetComponent(GUIText)
	self.m_cPlayerControl = GameObject.FindGameObjectWithTag("Player"):GetComponent(YwLuaMonoBehaviour):GetLuaTable().m_cPlayerCtrl

    -- Get data bridge.
    local cDataBridge = self.gameObject:GetComponent(YwLuaMonoDataBridge)
    self.m_nScore = cDataBridge.m_integers[1]
end

-- Update.
function LgScore:Update()
    --print("LgScore:Update")
    
    -- Check if player control is valid (Maybe player shoot enemies after dead).
    local cPlayerControl = self:GetPlayerControl()
    if (not cPlayerControl) or Slua.IsNull(self.m_cPlayerControl) then
        return
    end

    -- Set the score text.
    self.m_cLgScoreTxt.text = "Score" .. tostring(self.m_nScore)
   
    -- If the score has changed...
    if self.m_nPreviousLgScore ~= self.m_nScore then
        self.m_cPlayerControl:Taunt()
    end

    -- Set the previous score to this frame's score.
    self.m_nPreviousLgScore = self.m_nScore
end

-- Get player control.
function LgScore:GetPlayerControl()
    --print("LgScore:GetPlayerHealth")
    if (not self.m_cPlayerControl) or Slua.IsNull(self.m_cPlayerControl) then
        self.m_cPlayerControl = GameObject.FindGameObjectWithTag("Player"):GetComponent(YwLuaMonoBehaviour):GetLuaTable().m_cPlayerCtrl
    end

    return self.m_cPlayerControl
end

-- Return this class.
return LgScore
