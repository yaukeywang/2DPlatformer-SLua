--
-- Pauser class.
--
-- @filename  LgPauser.lua
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

local Time = Time
local Input = Input
local KeyCode = KeyCode

-- Register new class LgPauser.
local strClassName = "LgPauser"
local LgPauser = YwDeclare(strClassName, YwClass(strClassName))

-- Member variables.

-- Pause or not.
LgPauser.m_bPaused = false

-- Awake method.
function LgPauser:Awake()
    --print("LgPauser:Awake")
end

-- Virtual.
-- Update.
function LgPauser:Update()
    --print("LgPauser:Update")

    if Input.GetKeyUp(KeyCode.P) then
    	self.m_bPaused = not self.m_bPaused
    end

    if self.m_bPaused then
    	Time.timeScale = 0
    else
    	Time.timeScale = 1
    end
end

-- Return this class.
return LgPauser
