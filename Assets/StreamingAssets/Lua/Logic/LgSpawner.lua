--
-- Spawner class.
--
-- @filename  LgSpawner.lua
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

local math = math
local GameObject = GameObject

-- Register new class LgSpawner.
local strClassName = "LgSpawner"
local LgSpawner = YwDeclare(strClassName, YwClass(strClassName))

-- Member variables.

-- The c# class object.
LgSpawner.this = false

-- The transform.
LgSpawner.transform = false

-- The c# gameObject.
LgSpawner.gameObject = false

-- The spawner time.
LgSpawner.m_fSpawnTime = 5.0

-- The spawn delay time.
LgSpawner.m_fSpawnDelay = 3.0

-- The enemys.
LgSpawner.m_aEnemys = false

-- The timer id.
LgSpawner.m_nTimerID = -1

-- Local function.
-- The spawn method.
local function LgSpawner_Spawn(LgSpawner)
    local self = LgSpawner

    -- Instantiate a random enemy.
    local nEnemyIdx = math.random(1, #self.m_aEnemys)
    GameObject.Instantiate(self.m_aEnemys[nEnemyIdx], self.transform.position, self.transform.rotation)

    --Play the spawning effect from all of the particle systems.
    local aParticles = self.gameObject:GetComponentsInChildren(ParticleSystem)
    for i = 1, aParticles.Length do
        aParticles[i]:Play()
    end
end

-- Constructor.
function LgSpawner:ctor()
    --print("LgSpawner:ctor")
    self.m_aEnemys = {}
end

-- Destructor.
function LgSpawner:dtor()
    --print("LgSpawner:dtor")
    self.m_aEnemys = nil
    if -1 ~= self.m_nTimerID then
        LuaTimer.Delete(self.m_nTimerID)
        self.m_nTimerID = -1
    end
end

-- Awake method.
function LgSpawner:Awake()
    --print("LgSpawner:Awake")

    -- Check variable.
    if (not self.this) or (not self.transform) or (not self.gameObject) then
        DLogError("Init error in LgSpawner!")
        return
    end
end

-- Start method.
function LgSpawner:Start()
    --print("LgSpawner:Start")

    -- Start calling the Spawn function repeatedly after a delay.
    self.m_nTimerID = LuaTimer.Add(self.m_fSpawnDelay * 1000, self.m_fSpawnTime * 1000, function (nID)
        LgSpawner_Spawn(self)
    end)
end

-- On destroy method.
function LgSpawner:OnDestroy()
    --print("LgSpawner:OnDestroy")
    if -1 ~= self.m_nTimerID then
        LuaTimer.Delete(self.m_nTimerID)
        self.m_nTimerID = -1
    end
end

-- Return this class.
return LgSpawner
