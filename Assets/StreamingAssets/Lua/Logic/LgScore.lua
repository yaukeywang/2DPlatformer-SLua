--
-- Score class.
--
-- @filename  LgScore.lua
-- @copyright Copyright (c) 2015 Yaukey/yaukeywang/WangYaoqi (yaukeywang@gmail.com) all rights reserved.
-- @license   The MIT License (MIT)
-- @author    Yaukey
-- @date      2015-09-01
--

local YwDeclare = YwDeclare
local YwClass = YwClass

local DLog = YwDebug.Log
local DLogWarn = YwDebug.LogWarning
local DLogError = YwDebug.LogError

-- Register new class LgScore.
local strClassName = "LgScore"
local LgScore = YwDeclare(strClassName, YwClass(strClassName))

-- Member variables.

-- The c# class object.
LgScore.this = false

-- The transform.
LgScore.transform = false

-- The c# gameObject.
LgScore.gameObject = false

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
	self.m_cPlayerControl = GameObject.FindGameObjectWithTag("Player"):GetComponent(PlayerControl);
end

-- Update.
function LgScore:Update()
    --print("LgScore:Update")

    local this = self.this

    -- Check if player control is valid (Maybe player shoot enemies after dead).
    if Slua.IsNull(self.m_cPlayerControl) then
        return
    end

    -- Set the score text.
    self.m_cLgScoreTxt.text = "Score" .. tostring(this.m_score)
   
    -- If the score has changed...
    if self.m_nPreviousLgScore ~= this.m_score then
        self.m_cPlayerControl:Taunt()
    end

    -- Set the previous score to this frame's score.
    self.m_nPreviousLgScore = this.m_score
end

-- Return this class.
return LgScore
