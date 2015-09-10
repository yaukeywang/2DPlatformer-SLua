--
-- SetParticleSortingLayer class.
--
-- @filename  LgSetParticleSortingLayer.lua
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

-- Register new class LgSetParticleSortingLayer.
local strClassName = "LgSetParticleSortingLayer"
local LgSetParticleSortingLayer = YwDeclare(strClassName, YwClass(strClassName))

-- Member variables.

-- The c# class object.
LgSetParticleSortingLayer.this = false

-- The transform.
LgSetParticleSortingLayer.transform = false

-- The c# gameObject.
LgSetParticleSortingLayer.gameObject = false

-- Awake method.
function LgSetParticleSortingLayer:Awake()
    --print("LgSetParticleSortingLayer:Awake")

    -- Check variable.
    if (not self.this) or (not self.transform) or (not self.gameObject) then
        DLogError("Init error in LgSetParticleSortingLayer!")
        return
    end
end

-- Start method.
function LgSetParticleSortingLayer:Start()
    --print("LgSetParticleSortingLayer:Start")

    -- Set the sorting layer of the particle system.
    self.gameObject:GetComponent(ParticleSystem):GetComponent(Renderer).sortingLayerName = self.this.m_sortingLayerName
end

-- Return this class.
return LgSetParticleSortingLayer
