--
-- LgPlayer class.
--
-- @filename  LgPlayer.lua
-- @copyright Copyright (c) 2015 Yaukey/yaukeywang/WangYaoqi (yaukeywang@gmail.com) all rights reserved.
-- @license   The MIT License (MIT)
-- @author    Yaukey yaukeywang@gmail.com
-- @date      2016-03-17
--

local DLog = YwDebug.Log
local DLogWarn = YwDebug.LogWarning
local DLogError = YwDebug.LogError

-- Register new class LgPlayer.
local strClassName = "LgPlayer"
local LgPlayer = YwDeclare(strClassName, YwClass(strClassName, YwMonoBehaviour))

-- Member variables.

-- The player control.
LgPlayer.m_cPlayerCtrl = nil

-- The player health.
LgPlayer.m_cPlayerHealth = nil

-- The player lay bombs.
LgPlayer.m_cLayBombs = nil

-- Constructor.
function LgPlayer:ctor()
    --print("LgPlayer:ctor")
end

-- Destructor.
function LgPlayer:dtor()
    --print("LgPlayer:dtor")
end

-- Awake method.
function LgPlayer:Awake()
    --print("LgPlayer:Awake")

    -- Check variable.
    if (not self.this) or (not self.transform) or (not self.gameObject) then
        DLogError("Init error in LgPlayer!")
        return
    end
end

-- Start method.
function LgPlayer:Start()
    --print("LgPlayer:Start")

    -- Get data bridge.
    local cDataBridge = self.gameObject:GetComponent(LgLuaMonoDataBridge)
    local aIntArray = cDataBridge.m_integers
    local aFloatArray = cDataBridge.m_floats
    local aAudioArray = cDataBridge.m_audioClips

    -- Init player ctrl.
    self.m_cPlayerCtrl = LgPlayerControl(self)
    local cPlayerCtrl = self.m_cPlayerCtrl
    cPlayerCtrl.m_fMoveForce = aFloatArray[1]
    cPlayerCtrl.m_fMaxSpeed = aFloatArray[2]
    cPlayerCtrl.m_fJumpForce = aFloatArray[3]
    cPlayerCtrl.m_fTauntProbability = aFloatArray[4]
    cPlayerCtrl.m_fTauntDelay = aFloatArray[5]

    local cJumpClips = cPlayerCtrl.m_aJumpClips
    cJumpClips[1] = aAudioArray[1]
    cJumpClips[2] = aAudioArray[2]
    cJumpClips[3] = aAudioArray[3]

    local aTauntClips = cPlayerCtrl.m_aTaunts
    aTauntClips[1] = aAudioArray[4]
    aTauntClips[2] = aAudioArray[5]
    aTauntClips[3] = aAudioArray[6]
    aTauntClips[4] = aAudioArray[7]
    aTauntClips[5] = aAudioArray[8]
    aTauntClips[6] = aAudioArray[9]
    aTauntClips[7] = aAudioArray[10]
    aTauntClips[8] = aAudioArray[11]

    -- Init player health.
    self.m_cPlayerHealth = LgPlayerHealth(self)
    local cPlayerHealth = self.m_cPlayerHealth
    cPlayerHealth.m_fHealth = aFloatArray[6]
    cPlayerHealth.m_fRepeatDamagePeriod = aFloatArray[7]
    cPlayerHealth.m_fHurtForce = aFloatArray[8]
    cPlayerHealth.m_fDamageAmount = aFloatArray[9]

    local aOuchClips = cPlayerHealth.m_aOuchClips
    aOuchClips[1] = aAudioArray[12]
    aOuchClips[2] = aAudioArray[13]
    aOuchClips[3] = aAudioArray[14]
    aOuchClips[4] = aAudioArray[15]

    cPlayerHealth.m_cPlayerControl = cPlayerCtrl
    cPlayerHealth.m_cGun = self.m_aParameters[1]:GetComponent(YwLuaMonoBehaviour):GetLuaTable()
    cPlayerHealth.m_cGun.m_cPlayerCtrl = cPlayerCtrl

    -- Init lay bombs.
    self.m_cLayBombs = LgLayBombs(self)
    local cLayBombs = self.m_cLayBombs
    cLayBombs.m_nBombCount = aIntArray[1]
    cLayBombs.m_cBombsAway = aAudioArray[16]
    cLayBombs.m_cBomb = self.m_aParameters[2]
end

-- Update method.
function LgPlayer:Update()
    --print("LgPlayer:Update")

    self.m_cPlayerCtrl:Update()
    self.m_cLayBombs:Update()
end

-- Fixed update method.
function LgPlayer:FixedUpdate()
    --print("LgPlayer:FixedUpdate")

    self.m_cPlayerCtrl:FixedUpdate()
end

-- On destroy method.
function LgPlayer:OnDestroy()
    --print("LgPlayer:OnDestroy")

    self.m_cPlayerCtrl:OnDestroy()
    self.m_cPlayerCtrl = nil

    self.m_cPlayerHealth:OnDestroy()
    self.m_cPlayerHealth = nil

    self.m_cLayBombs:OnDestroy()
    self.m_cLayBombs = nil
end

-- LgPlayer method.
function LgPlayer:OnCollisionEnter2D(cOtherCollider2D)
    --print("LgPlayer:OnCollisionEnter2D")

    self.m_cPlayerHealth:OnCollisionEnter2D(cOtherCollider2D)
end

-- Return this class.
return LgPlayer
